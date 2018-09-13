using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    class Instrument : SongComponent
    {
        Staff[] mStaves;
        Clef[] mClefs;
        WaveForm[] mWaveForms;
        Grouping mGrouping;

        Staff mSelectedStaff;
        int mInstrumentNumber;

        public Instrument(List<Clef> clefs, List<WaveForm> waveforms, Grouping g, int instrumentNumber)
        {
            area = new RectangleF();
            mInstrumentNumber = instrumentNumber;
            if (clefs.Count > 4)
                mClefs = new Clef[4];
            else
                mClefs = new Clef[clefs.Count];
            for (int i = 0; i < mClefs.Length; i++)
                mClefs[i] = clefs[i];
            if (waveforms.Count > 4)
                mWaveForms = new WaveForm[4];
            else
                mWaveForms = new WaveForm[waveforms.Count];
            for (int i = 0; i < mWaveForms.Length; i++)
                mWaveForms[i] = waveforms[i];
            AddStaves(Math.Min(mClefs.Length, mWaveForms.Length));
            mGrouping = g;
            Song.SELECTABLES.Add(this);
        }

        public int GetStaffCount()
        {
            return mStaves.Length;
        }

        public Staff GetStaff(int i)
        {
            return mStaves[i];
        }

        public Staff GetSelection()
        {
            return mSelectedStaff;
        }

        public int GetInstrumentNumber()
        {
            return mInstrumentNumber;
        }

        public int GetClefCount()
        {
            return mClefs.Length;
        }

        public Clef GetClef(int i)
        {
            return mClefs[i];
        }

        public Grouping GetGrouping()
        {
            return mGrouping;
        }

        public int GetDuration()
        {
            int d = 0;
            foreach (Staff staff in mStaves)
                d = Math.Max(d, staff.GetDuration());
            return d;
        }

        public void SetSelection(Staff s)
        {
            mSelectedStaff = s;
        }

        public void SetSelection(int s)
        {
            mSelectedStaff = GetStaff(s);
        }

        public void AddStaves(int numberOfStaves)
        {
            mStaves = new Staff[numberOfStaves];
            for (int i = 0; i < mStaves.Length; i++)
            {
                mStaves[i] = new Staff(mClefs[i], mWaveForms[i], Song.TOTAL_INSTRUMENTS, Song.TOTAL_STAVES, i);
                Song.cursorY += Staff.HEIGHT + Song.STAFF_SPACING;
                if (i == mStaves.Length - 1)
                    Song.TOTAL_INSTRUMENTS++;
            }
            if (GetStaffCount() == numberOfStaves)
                mSelectedStaff = GetStaff(0);
        }

        public void Update()
        {
            foreach (Staff staff in mStaves)
                staff.Update();
            area.Location = mStaves[0].GetArea().Location;
            Staff last = mStaves[mStaves.Length - 1];
            area.Width = last.GetArea().Right - area.Left;
            area.Height = last.GetArea().Bottom - area.Top;

            area.Y -= Song.INSTRUMENT_SPACING;
            area.Height += Song.INSTRUMENT_SPACING * 2;
        }

        public void Draw(PaintEventArgs e)
        {
            if (isSelected)
                if (e.Graphics.IsVisible(area))
                    e.Graphics.FillRectangle(new SolidBrush(Color.Cyan), area);
            foreach (Staff staff in mStaves)
                staff.Draw(e);
        }

        public void Play()
        {
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);

            int msDuration = GetDuration() * 60;
            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            {
                // var encoding = new System.Text.UTF8Encoding();
                writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
                writer.Write(fileSize);
                writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
                writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
                writer.Write(formatChunkSize);
                writer.Write(formatType);
                writer.Write(tracks);
                writer.Write(samplesPerSecond);
                writer.Write(bytesPerSecond);
                writer.Write(frameSize);
                writer.Write(bitsPerSample);
                writer.Write(0x61746164); // = encoding.GetBytes("data")
                writer.Write(dataChunkSize);
            }

            short[] preSamples = new short[samples];

            foreach (Staff staff in mStaves)
            {
                int chordNumber = 0;
                for (int m = 0; m < staff.GetMeasureCount(); m++)
                {
                    Measure measure = staff.GetMeasure(m);
                    for (int c = 0; c < measure.GetChordCount(); c++)
                    {
                        Chord chord = measure.GetChord(c);
                        if (chord.GetNote(0).GetPitch() != Pitch.Rest)
                        {
                            double[] angles = new double[chord.GetNoteCount()];
                            ushort[] frequency = new ushort[chord.GetNoteCount()];
                            int[] samplesPerWavelength = new int[chord.GetNoteCount()];
                            short[] ampSteps = new short[chord.GetNoteCount()];
                            int chordSamples = (int)((decimal)samplesPerSecond * (int)chord.GetDuration() * 60 / 1000);

                            ushort volume = 16383;
                            if (chord.GetWaveForm() == WaveForm.Square)
                                volume /= 2;

                            const double TAU = 2 * Math.PI;
                            double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

                            double amp = volume / 2;

                            for (int i = 0; i < chord.GetNoteCount(); i++)
                            {
                                double step = (int)Pitch.A;
                                if (chord.GetNote(i).GetPitch() <= Pitch.F && chord.GetNote(i).GetPitch() >= Pitch.B)
                                    step += 0.5;
                                double exp = -2 * ((double)chord.GetNote(i).GetPitch() - step);

                                frequency[i] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                                samplesPerWavelength[i] = bytesPerSecond / frequency[i];
                                ampSteps[i] = (short)(amp * 2 / samplesPerWavelength[i]);
                            }

                            for (int i = 0; i < angles.Length; i++)
                                angles[i] = frequency[i] * TAU / samplesPerSecond;

                            short tempSample = (short)-amp;
                            for (int i = 0; i < chordSamples; i++)
                            {
                                short s = 0;

                                switch (chord.GetWaveForm())
                                {
                                    case WaveForm.Sine:
                                        foreach (double theta in angles)
                                            s += (short)(amp * Math.Sin(theta * i));
                                        break;
                                    case WaveForm.Square:
                                        foreach (double theta in angles)
                                            s += (short)(amp * Math.Sign(Math.Sin(theta * i)));
                                        break;
                                    case WaveForm.Sawtooth:
                                        foreach (short ampStep in ampSteps)
                                        {
                                            tempSample += ampStep;
                                            s += (short)(tempSample / ampSteps.Length);
                                        }
                                        break;
                                    case WaveForm.Triangle:
                                        for (int j = 0; j < ampSteps.Length; j++)
                                        {
                                            if (Math.Abs(tempSample) > amp)
                                                ampSteps[j] = (short)-ampSteps[j];

                                            tempSample += ampSteps[j];
                                            s += (short)(tempSample / ampSteps.Length);
                                        }
                                        break;
                                    case WaveForm.Noise:
                                        s += (short)((new Random().NextDouble() * 2 - 1) * amp);
                                        break;
                                }
                                preSamples[chordNumber] += s;
                                chordNumber++;
                            }
                        }
                    }
                }
            }

            foreach (short s in preSamples)
                writer.Write(s);

            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).Play();
            writer.Close();
            mStrm.Close();
        }
    }
}

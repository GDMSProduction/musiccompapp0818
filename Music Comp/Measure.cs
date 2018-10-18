using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    [Serializable]
    public class Measure : SongComponent
    {
        List<Chord> mChords;
        Chord mSelectedChord;
        int mMeasureNumber;

        float mLength;

        int mTotalDuration;

        Clef mClef;
        WaveForm mWaveForm;

        public Measure(Clef clef, WaveForm wave, float yPosition, int measureNumber)
        {
            mMeasureNumber = measureNumber;
            mClef = clef;
            mWaveForm = wave;
            mChords = new List<Chord>();
            Song.SELECTABLES.Add(this);
            if (Song.CHORD_POSITIONS.Count <= mMeasureNumber)
            {
                int last = Song.CHORD_POSITIONS.Count;
                switch (Song.TIME)
                {
                    case Time.NineEight:
                        Song.CHORD_POSITIONS.Add(new float[18]);
                        break;
                    case Time.SixEight:
                    case Time.ThreeFour:
                        Song.CHORD_POSITIONS.Add(new float[12]);
                        break;
                    case Time.ThreeEight:
                    case Time.FourFour:
                        Song.CHORD_POSITIONS.Add(new float[16]);
                        break;
                    case Time.TwoFour:
                        Song.CHORD_POSITIONS.Add(new float[8]);
                        break;
                }
                for (int i = 0; i < Song.CHORD_POSITIONS[last].Length; i++)
                    Song.CHORD_POSITIONS[last][i] = 0;
            }
            Song.TOTAL_MEASURES++;
        }

        public int GetChordCount()
        {
            return mChords.Count;
        }

        public float GetLength()
        {
            return mLength;
        }

        public Chord GetChord(int i)
        {
            return mChords[i];
        }

        public Chord GetSelection()
        {
            return mSelectedChord;
        }

        public bool IsFull()
        {
            return mTotalDuration >= (Song.TIME > 0 ? (int)Song.TIME : -(int)Song.TIME);
        }

        public bool IsEmpty()
        {
            return mChords.Count == 0 || mTotalDuration == 0;
        }

        public int GetDuration()
        {
            return mTotalDuration;
        }

        public void SetSelection(Chord c)
        {
            mSelectedChord.Deselect();
            mSelectedChord = c;
            mSelectedChord.Select();
        }

        public Chord Add(Chord chord)
        {
            if (chord == null)
                return null;
            if (mChords.Count == 0)
            {
                mChords.Add(chord);
                mTotalDuration += (int)chord.GetDuration();
                if (GetChordCount() == 1)
                    mSelectedChord = GetChord(0);
                return null;
            }
            else
            {
                mTotalDuration += (int)chord.GetDuration();
                int time = Math.Abs((int)Song.TIME);
                if (mTotalDuration > time)
                {
                    Chord split = new Chord(0);
                    Chord remainder = new Chord(0);

                    int remainderDuration = mTotalDuration - time;
                    int splitDuration = (int)chord.GetDuration() - remainderDuration;

                    split = chord.Clone();
                    remainder = chord.Clone();

                    split.SetDuration((Duration)splitDuration);
                    remainder.SetDuration((Duration)remainderDuration);

                    mChords.Add(split);
                    if (Song.TOTAL_CHORDS == 1)
                        mSelectedChord = GetChord(0);
                    return remainder;
                }
                else
                {
                    mChords.Add(chord);
                    if (Song.TOTAL_CHORDS == 1)
                        mSelectedChord = GetChord(0);
                    return null;
                }
            }
        }

        public void Remove(Chord chord)
        {
            mTotalDuration -= (int)mChords[mChords.Count - 1].GetDuration();
            mChords.Remove(chord);
        }

        public void Update(float barline, float yPosition)
        {
            int stnza = 0;
            for (int i = mMeasureNumber; i < Song.BARLINES.Count; i++)
                if (Song.BARLINES[i] > (Song.PAGE_WIDTH - Song.RIGHT_MARGIN) * stnza)
                    stnza++;
            float cursor = 0;
            int durationCursor = 0;
            foreach (Chord chord in mChords)
            {
                if (Song.CHORD_POSITIONS[mMeasureNumber][durationCursor] - stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN) < cursor)
                {
                    float diff = cursor - Song.CHORD_POSITIONS[mMeasureNumber][durationCursor] * Song._SCALE;
                    for (int i = durationCursor; i < Song.CHORD_POSITIONS[mMeasureNumber].Length; i++)
                        Song.CHORD_POSITIONS[mMeasureNumber][i] += diff;
                    if (Song.BARLINES.Count > mMeasureNumber + 1)
                        for (int i = mMeasureNumber + 1; i < Song.BARLINES.Count; i++)
                        {
                            if (Song.BARLINES[i] < (Song.PAGE_WIDTH - Song.RIGHT_MARGIN) * (Song.stanzas - 1))
                                Song.BARLINES[i] += diff;
                        }
                }
                chord.Update(barline + Song.CHORD_POSITIONS[mMeasureNumber][durationCursor] * Song._SCALE, yPosition, mClef);
                cursor += chord.GetWidth();
                if (chord == mChords[GetChordCount() - 1])
                    cursor = Song.CHORD_POSITIONS[mMeasureNumber][durationCursor] + chord.GetWidth();
                durationCursor += (int)chord.GetDuration() / 3;
            }
            mLength = cursor;

            area.X = barline;
            area.Y = Song.TOP_MARGIN + yPosition - Song.STAFF_SPACING;

            area.Width = mLength;
            if (Song.BARLINES.Count > mMeasureNumber + 1)
                area.Width = Song.BARLINES[mMeasureNumber + 1] - barline;
            if (mMeasureNumber < Song.BARLINES.Count - 1)
                if (Song.BARLINES[mMeasureNumber + 1] < barline + cursor)
                    Song.BARLINES[mMeasureNumber + 1] = barline + cursor;
            area.Height = Staff.HEIGHT + Song.STAFF_SPACING * 2;

            if (area.Right > (Song.PAGE_WIDTH - Song.RIGHT_MARGIN) * Song.stanzas)
            {
                float margin = Song.LEFT_MARGIN + Staff.LENGTH - barline;
                float extra = margin / (Song.BARLINES.Count * ((float)(Song.BARLINES.Count - 1) / 2));
                float scale = (Song.BARLINES[1] - Song.BARLINES[0] + extra) / (Song.BARLINES[1] - Song.BARLINES[0]);
                for (int i = 1; i < Song.BARLINES.Count; i++)
                    Song.BARLINES[i] += extra * i * Song._SCALE;
                for (int i = 0; i < Song.CHORD_POSITIONS.Count; i++)
                    for (int j = 0; j < Song.CHORD_POSITIONS[i].Length; j++)
                        Song.CHORD_POSITIONS[i][j] *= scale * Song._SCALE;
                Song.BARLINES[Song.BARLINES.Count - 1] = Song.BARLINES[0] + (Song.PAGE_WIDTH - Song.RIGHT_MARGIN) * Song.stanzas;
                Song.OVERFLOW = this;
            }
        }

        public void Draw(Graphics g)
        {
            if (isSelected)
                if (g.IsVisible(area))
                    g.FillRectangle(new SolidBrush(Color.Blue), area);
            foreach (Chord chord in mChords)
                chord.Draw(g);
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

            foreach (Chord chord in mChords)
            {
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
                        if (chord.GetNote(i).GetPitch() < Pitch.E)
                            step -= 0.5;
                        step += (chord.GetNote(i).Octave - 4) * 6;
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
                        writer.Write(s);
                    }
                }
            }

            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).Play();
            writer.Close();
            mStrm.Close();
        }
    }
}

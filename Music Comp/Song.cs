using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    class Song
    {
        RectangleF area;
        RectangleF groupingArea;

        System.Media.SoundPlayer soundPlayer;

        static Image bracketImage = Properties.Resources.Bracket;
        static Image braceImage = Properties.Resources.Brace;

        public static float SCREEN_WIDTH;
        public static float PAGE_WIDTH;
        public static float _SCALE;
        public static float TOP_MARGIN;
        public static float LEFT_MARGIN;
        public static float RIGHT_MARGIN;
        public static float STAFF_SPACING;
        public static float INSTRUMENT_SPACING;

        public static int TOTAL_INSTRUMENTS;
        public static int TOTAL_STAVES;
        public static int TOTAL_MEASURES;
        public static int TOTAL_CHORDS;
        public static int TOTAL_NOTES;

        public static float cursorY;
        public static float cursorX;

        public static Key KEY = Key.C;
        public static Time TIME = Time.Common;

        public static List<float> BARLINES;
        public static List<SongComponent> SELECTABLES;

        List<Instrument> mInstruments = new List<Instrument>();
        Instrument mSelectedInstrument;

        public Song(float panelWidth, Key k = Key.C, Time t = Time.Common)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            TOP_MARGIN = 300 * _SCALE;
            LEFT_MARGIN = 100 * _SCALE;
            RIGHT_MARGIN = 50 * _SCALE;
            STAFF_SPACING = 60 * _SCALE;
            INSTRUMENT_SPACING = 80 * _SCALE;

            TOTAL_INSTRUMENTS = 0;
            TOTAL_STAVES = 0;
            TOTAL_MEASURES = 0;
            TOTAL_CHORDS = 0;
            TOTAL_NOTES = 0;

            cursorY = TOP_MARGIN;
            cursorX = LEFT_MARGIN;

            KEY = k;
            TIME = t;

            area = new Rectangle(0, (int)TOP_MARGIN - 5, (int)PAGE_WIDTH, (int)cursorY);

            BARLINES = new List<float>();
            SELECTABLES = new List<SongComponent>();
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public int GetDuration()
        {
            int d = 0;
            foreach (Instrument instrument in mInstruments)
                d = Math.Max(d, instrument.GetDuration());
            return d;
        }

        public Instrument GetInstrument(int i)
        {
            return mInstruments[i];
        }

        public Instrument GetSelection()
        {
            return mSelectedInstrument;
        }

        public int GetInstrumentCount()
        {
            return mInstruments.Count;
        }

        public void SetKeySignature(Key k)
        {
            KEY = k;
        }

        public void SetTimeSignature(Time t)
        {
            TIME = t;
        }

        public void SetSelection(Instrument i)
        {
            mSelectedInstrument = i;
        }

        public void SetSelection(int i)
        {
            mSelectedInstrument = GetInstrument(i);
        }

        public void AddInstrument(List<Clef> clefs, List<WaveForm> waveforms, Grouping g)
        {
            mInstruments.Add(new Instrument(clefs, waveforms, g, mInstruments.Count));
            if (TOTAL_INSTRUMENTS == 1)
            {
                GetInstrument(0).GetStaff(0).Select();
                mSelectedInstrument = GetInstrument(0);
            }
        }

        public void RemoveInstrument(int i)
        {
            if (i < mInstruments.Count && i >= 0)
                mInstruments.Remove(mInstruments[i]);

            //   If the active staff was in the instrument

            /*\  if (mInstruments.Count > 0)
             *   {
             *       if (i != mInstruments.Count)
             *           mInstruments[i].GetStaff(0).Select();
             *       else
             *           mInstruments[i - 1].GetStaff(0).Select();
             *   }
            \*/
        }

        public void Update()
        {
            foreach (Instrument instrument in mInstruments)
                instrument.Update();
            area.X = 0;
            if (mInstruments.Count > 0)
            {
                area.Y = mInstruments[0].GetArea().Y;
                Instrument last = mInstruments[mInstruments.Count - 1];
                area.Width = SCREEN_WIDTH;
                area.Height = last.GetArea().Bottom - area.Top;
            }
        }

        public void UpdateGrouping(Grouping g, float instTop, float instBtm)
        {
            PointF location = new PointF();
            SizeF size = new SizeF();

            switch (g)
            {
                case Grouping.Bracket:
                    location.X = LEFT_MARGIN - 35 * _SCALE;
                    location.Y = instTop - 15 * _SCALE;
                    size.Width = 40 * _SCALE;
                    size.Height = instBtm - instTop + 30 * _SCALE;

                    groupingArea = new RectangleF(location, size);
                    break;
                case Grouping.Brace:
                    location.X = LEFT_MARGIN - 50 * _SCALE;
                    location.Y = instTop;
                    size.Width = 50 * _SCALE;
                    size.Height = instBtm - instTop;

                    groupingArea = new RectangleF(location, size);
                    break;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES - STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING;

            PointF start = new PointF(LEFT_MARGIN, TOP_MARGIN);
            PointF end = new PointF(LEFT_MARGIN, btm_song_line);

            if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                e.Graphics.DrawLine(new Pen(Color.Black, 3.4f * _SCALE), start, end);

            foreach (Instrument instrument in mInstruments)
                instrument.Draw(e);

            DrawBarLines(BARLINES, e);
        }

        public void DrawBarLines(List<float> barlines, PaintEventArgs e)
        {
            Pen barLinePen = new Pen(Color.Black, 3.5f * _SCALE);

            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES -
                STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING;

            float btm_inst_line;
            float top_inst_line = TOP_MARGIN;

            for (int j = 0; j < mInstruments.Count; j++)
            {
                PointF start;
                PointF end;

                btm_inst_line = top_inst_line + (Staff.HEIGHT + STAFF_SPACING) * mInstruments[j].GetStaffCount() - STAFF_SPACING;
                UpdateGrouping(mInstruments[j].GetGrouping(), top_inst_line, btm_inst_line);
                DrawGrouping(mInstruments[j].GetGrouping(), e);

                for (int i = 0; i < barlines.Count; i++)
                {
                    start = new PointF(barlines[i], top_inst_line);
                    end = new PointF(barlines[i], btm_inst_line);

                    if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                        e.Graphics.DrawLine(barLinePen, start, end);
                }

                start = new PointF(PAGE_WIDTH - RIGHT_MARGIN, top_inst_line);
                end = new PointF(PAGE_WIDTH - RIGHT_MARGIN, btm_inst_line);

                if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                    e.Graphics.DrawLine(barLinePen, start, end);

                top_inst_line = btm_inst_line + STAFF_SPACING + INSTRUMENT_SPACING;
            }

            barLinePen.Dispose();
        }

        public void DrawGrouping(Grouping g, PaintEventArgs e)
        {
            switch (g)
            {
                case Grouping.Bracket:
                    if (e.Graphics.IsVisible(groupingArea))
                        e.Graphics.DrawImage(bracketImage, groupingArea);
                    break;
                case Grouping.Brace:
                    if (e.Graphics.IsVisible(groupingArea))
                        e.Graphics.DrawImage(braceImage, groupingArea);
                    break;
            }
        }

        public int Play()
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

            foreach (Instrument instrument in mInstruments)
            {
                for (int st = 0; st < instrument.GetStaffCount(); st++)
                {
                    int chordNumber = 0;
                    Staff staff = instrument.GetStaff(st);
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
            }

            foreach (short s in preSamples)
                writer.Write(s);

            mStrm.Seek(0, SeekOrigin.Begin);
            soundPlayer = new System.Media.SoundPlayer(mStrm);
            soundPlayer.Play();
            soundPlayer.Dispose();
            writer.Close();
            mStrm.Close();
            return msDuration;
        }

        public void Stop()
        {
            soundPlayer.Stop();
        }
    }
}

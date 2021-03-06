﻿using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    [Serializable]
    class Song
    {
        string mFileName;
        string mTitle;
        string mComposer;

        Image image;

        public static int stanzas;

        RectangleF area;
        RectangleF groupingArea;

        System.Media.SoundPlayer soundPlayer;

        static Image bracketImage = Properties.Resources.Bracket;
        static Image braceImage = Properties.Resources.Brace;

        public static bool needsFullUpdate;

        public static ImageAttributes _REDSHIFT;

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
        public static int BPM;
        public static sbyte OCTAVE = 4;

        public static List<float> BARLINES;
        public static List<float[]> CHORD_POSITIONS;

        public static List<SongComponent> SELECTABLES;
        public static List<Note[]> LASTNOTES;

        public static Measure OVERFLOW;

        List<Instrument> mInstruments = new List<Instrument>();
        Instrument mSelectedInstrument;

        public Song(float panelWidth, Key k = Key.C, Time t = Time.Common)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 0, 0, 0, 1}
            });

            image = new Bitmap(2550, 3300);

            stanzas = 1;

            needsFullUpdate = true;

            _REDSHIFT = new ImageAttributes();
            _REDSHIFT.SetColorMatrix(colorMatrix);

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            TOP_MARGIN = 400 * _SCALE;
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
            BPM = 60;

            area = new Rectangle(0, (int)TOP_MARGIN - 5, (int)PAGE_WIDTH, (int)cursorY);

            BARLINES = new List<float>();
            CHORD_POSITIONS = new List<float[]>();

            SELECTABLES = new List<SongComponent>();
            LASTNOTES = new List<Note[]>();
        }

        public string Title
        {
            get { return mTitle; }
            set { mTitle = value; }
        }

        public string Composer
        {
            get { return mComposer; }
            set { mComposer = value; }
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public int GetDuration()
        {
            int t = 0;
            for (int sz = 0; sz < stanzas; sz++)
            {
                int d = 0;
                for (int inst = GetInstrumentCount() * sz / stanzas; inst < GetInstrumentCount() * (sz + 1) / stanzas; inst++)
                    d = Math.Max(d, GetInstrument(inst).GetDuration());
                t += d;
            }
            return t;
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

        public string GetFileName()
        {
            return mFileName;
        }

        public void SetKeySignature(Key k)
        {
            KEY = k;
            needsFullUpdate = true;
        }

        public void SetTimeSignature(Time t)
        {
            TIME = t;
            needsFullUpdate = true;
        }

        public void SetSelection(Instrument i)
        {
            mSelectedInstrument.Deselect();
            mSelectedInstrument = i;
            mSelectedInstrument.Select();
        }

        public void SetSelection(int i)
        {
            mSelectedInstrument.Deselect();
            mSelectedInstrument = GetInstrument(i);
            mSelectedInstrument.Select();
        }

        public void AddInstrument(List<Clef> clefs, List<WaveForm> waveforms, Grouping grouping)
        {
            mInstruments.Add(new Instrument(clefs, waveforms, grouping, mInstruments.Count));
            if (TOTAL_INSTRUMENTS == 1)
            {
                GetInstrument(0).GetStaff(0).Select();
                mSelectedInstrument = GetInstrument(0);
            }
            needsFullUpdate = true;
        }

        public void AddInstrument(Instrument instrument)
        {
            mInstruments.Add(instrument.CloneAttributes());
            if (TOTAL_INSTRUMENTS == 1)
            {
                GetInstrument(0).GetStaff(0).Select();
                mSelectedInstrument = GetInstrument(0);
            }
            needsFullUpdate = true;
        }

        public void RemoveInstrument(int i)
        {
            if (i < mInstruments.Count && i >= 0)
            {
                TOTAL_STAVES -= mInstruments[i].GetStaffCount();
                mInstruments.Remove(mInstruments[i]);
                TOTAL_INSTRUMENTS--;
                LASTNOTES.RemoveAt(i);
            }
            needsFullUpdate = true;
        }

        public void Update()
        {
            foreach (Instrument instrument in mInstruments)
                instrument.Update();
            if (OVERFLOW != null)
            {
                int inst = GetSelection().GetInstrumentNumber() + TOTAL_INSTRUMENTS;
                int st = GetSelection().GetSelection().GetStaffNumber();
                GetSelection().GetSelection().RemoveMeasure(OVERFLOW);
                int c = GetInstrumentCount();
                for (int i = 0; i < c; i++)
                    AddInstrument(GetInstrument(i));
                GetSelection().GetSelection().Deselect();
                for (int i = 0; i < OVERFLOW.GetChordCount(); i++)
                    GetInstrument(inst).GetStaff(st).GetNextMeasure().Add(OVERFLOW.GetChord(i));
                SetSelection(GetInstrument(inst));
                GetSelection().GetSelection().Select();
                OVERFLOW = null;
                stanzas++;
            }
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
            Graphics g = Graphics.FromImage(image);
            if (!needsFullUpdate && BARLINES.Count != 0)
            {
                Brush brush = new TextureBrush(image);
                g.Clear(Color.Transparent);
                g.FillRectangle(brush, 0, 0, BARLINES[0], 3300);
            }
            else
                g.Clear(Color.Transparent);
            Draw(g);

            needsFullUpdate = false;
        }

        private void UpdateGrouping(Grouping g, float instTop, float instBtm)
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

        private void Draw(Graphics g)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            Font font = new Font("Arial", 64 * _SCALE);
            RectangleF layoutRectangle = new RectangleF(0, 0, PAGE_WIDTH, TOP_MARGIN);
            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;
            g.DrawString(Title, font, new SolidBrush(Color.Black), layoutRectangle, stringFormat);

            font = new Font("Arial", 36 * _SCALE);
            layoutRectangle = new RectangleF(PAGE_WIDTH * 2 / 3, TOP_MARGIN * 3 / 4, PAGE_WIDTH / 3, TOP_MARGIN / 4);
            g.DrawString(Composer, font, new SolidBrush(Color.Black), layoutRectangle, stringFormat);

            if (TOTAL_INSTRUMENTS != 0)
            {
                float stanzaHeight = ((Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES - STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING) / stanzas
                    - (stanzas - 1) * 68 * _SCALE;
                PointF start = new PointF(LEFT_MARGIN, TOP_MARGIN);
                PointF end = new PointF(LEFT_MARGIN, TOP_MARGIN);
                for (int i = 0; i < stanzas; i++)
                {
                    end.Y = start.Y + stanzaHeight;

                    if (g.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                        g.DrawLine(new Pen(Color.Black, 3.4f * _SCALE), start, end);

                    start.Y = end.Y + 136 * _SCALE;
                }

                foreach (Instrument instrument in mInstruments)
                    instrument.Draw(g);

                DrawBarLines(BARLINES, g);
            }
        }

        public void Paint(Graphics g)
        {
            g.DrawImage(image, 0, 0);
        }

        private void DrawBarLines(List<float> barlines, Graphics g)
        {
            Pen barLinePen = new Pen(Color.Black, 3.5f * _SCALE);

            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES -
                STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING;

            float btm_inst_line;
            float top_inst_line = TOP_MARGIN;
            for (int s = 1; s < stanzas + 1; s++)
                for (int i = 0; i < mInstruments.Count / stanzas; i++)
                {
                    PointF start;
                    PointF end;

                    btm_inst_line = top_inst_line + (Staff.HEIGHT + STAFF_SPACING) * mInstruments[i * s].GetStaffCount() - STAFF_SPACING;
                    UpdateGrouping(mInstruments[i * s].GetGrouping(), top_inst_line, btm_inst_line);
                    DrawGrouping(mInstruments[i * s].GetGrouping(), g);

                    for (int b = 0; b < barlines.Count; b++)
                        if (barlines[b] >= (s - 1) * (PAGE_WIDTH - RIGHT_MARGIN) && barlines[b] <= s * (PAGE_WIDTH - RIGHT_MARGIN))
                        {
                            start = new PointF(barlines[b] - (s - 1) * (PAGE_WIDTH - RIGHT_MARGIN), top_inst_line);
                            end = new PointF(barlines[b] - (s - 1) * (PAGE_WIDTH - RIGHT_MARGIN), btm_inst_line);

                            if (g.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                                g.DrawLine(barLinePen, start, end);
                        }

                    start = new PointF(PAGE_WIDTH - RIGHT_MARGIN, top_inst_line);
                    end = new PointF(PAGE_WIDTH - RIGHT_MARGIN, btm_inst_line);

                    if (g.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                        g.DrawLine(barLinePen, start, end);

                    top_inst_line = btm_inst_line + STAFF_SPACING + INSTRUMENT_SPACING;
                }

            barLinePen.Dispose();
        }

        private void DrawGrouping(Grouping grouping, Graphics g)
        {
            switch (grouping)
            {
                case Grouping.Bracket:
                    if (g.IsVisible(groupingArea))
                        g.DrawImage(bracketImage, groupingArea);
                    break;
                case Grouping.Brace:
                    if (g.IsVisible(groupingArea))
                        g.DrawImage(braceImage, groupingArea);
                    break;
            }
        }

        public int Play()
        {
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);

            Random RG = new Random();

            int msDuration = GetDuration() * 3600 / BPM;
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

            for (int sz = 0; sz < stanzas; sz++)
            {
                short[] preSamples = new short[samples];
                for (int inst = GetInstrumentCount() * sz / stanzas; inst < GetInstrumentCount() * (sz + 1) / stanzas; inst++)
                {
                    Instrument instrument = GetInstrument(inst);
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

                                double[] angles = new double[chord.GetNoteCount()];
                                ushort[] frequency = new ushort[chord.GetNoteCount()];
                                int[] samplesPerWavelength = new int[chord.GetNoteCount()];
                                short[] ampSteps = new short[chord.GetNoteCount()];
                                int chordSamples = (int)((decimal)samplesPerSecond * (int)chord.GetDuration() * 3600 / BPM / 1000);

                                ushort volume = 16383;
                                if (chord.GetWaveForm() == WaveForm.Square)
                                    volume /= 2;

                                const double TAU = 2 * Math.PI;
                                double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

                                double amp = volume / 2;

                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                {
                                    double step = (int)Pitch.A;
                                    if (chord.GetNote(n).GetPitch() < Pitch.E)
                                        step -= 0.5;
                                    step += (chord.GetNote(n).Octave - 4) * 6;
                                    double exp = -2 * ((double)chord.GetNote(n).GetPitch() - step);

                                    frequency[n] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                                    samplesPerWavelength[n] = bytesPerSecond / frequency[n];
                                    ampSteps[n] = (short)(amp * 2 / samplesPerWavelength[n]);
                                }

                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                    angles[n] = frequency[n] * TAU / samplesPerSecond;

                                short tempSample = (short)-amp;
                                for (int s = 0; s < chordSamples; s++)
                                {
                                    short sample = 0;

                                    if (chord.GetNote(0).GetPitch() != Pitch.Rest)
                                        switch (chord.GetWaveForm())
                                        {
                                            case WaveForm.Sine:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(amp * Math.Sin(angles[n] * s));
                                                break;
                                            case WaveForm.Square:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(amp * Math.Sign(Math.Sin(angles[n] * s)));
                                                break;
                                            case WaveForm.Sawtooth:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(s % samplesPerWavelength[n] * ampSteps[n]);
                                                break;
                                            case WaveForm.Triangle:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                {
                                                    int sLoc = s % samplesPerWavelength[n];
                                                    if (sLoc < samplesPerWavelength[n] / 2)
                                                        if (sLoc < samplesPerWavelength[n] / 4)
                                                            sample += (short)(sLoc * ampSteps[n]);
                                                        else
                                                            sample += (short)((samplesPerWavelength[n] - sLoc) * ampSteps[n]);
                                                    else
                                                        if (sLoc < samplesPerWavelength[n] * 3 / 4)
                                                        sample += (short)(sLoc * ampSteps[n]);
                                                    else
                                                        sample += (short)((samplesPerWavelength[n] - sLoc) * ampSteps[n]);
                                                }
                                                break;
                                            case WaveForm.Noise:
                                                sample += (short)RG.Next((int)-amp, (int)amp);
                                                break;
                                        }

                                    preSamples[chordNumber] += sample;
                                    chordNumber++;
                                }
                            }
                        }
                    }
                }

            foreach (short s in preSamples)
                writer.Write(s);
            }


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

        public void ExportAudio(string filename)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create));

            Random RG = new Random();

            int msDuration = GetDuration() * 3600 / BPM;
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

            for (int sz = 0; sz < stanzas; sz++)
            {
                short[] preSamples = new short[samples];
                for (int inst = GetInstrumentCount() * sz / stanzas; inst < GetInstrumentCount() * (sz + 1) / stanzas; inst++)
                {
                    Instrument instrument = GetInstrument(inst);
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

                                double[] angles = new double[chord.GetNoteCount()];
                                ushort[] frequency = new ushort[chord.GetNoteCount()];
                                int[] samplesPerWavelength = new int[chord.GetNoteCount()];
                                short[] ampSteps = new short[chord.GetNoteCount()];
                                int chordSamples = (int)((decimal)samplesPerSecond * (int)chord.GetDuration() * 3600 / BPM / 1000);

                                ushort volume = 16383;
                                if (chord.GetWaveForm() == WaveForm.Square)
                                    volume /= 2;

                                const double TAU = 2 * Math.PI;
                                double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

                                double amp = volume / 2;

                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                {
                                    double step = (int)Pitch.A;
                                    if (chord.GetNote(n).GetPitch() < Pitch.E)
                                        step -= 0.5;
                                    step += (chord.GetNote(n).Octave - 4) * 6;
                                    double exp = -2 * ((double)chord.GetNote(n).GetPitch() - step);

                                    frequency[n] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                                    samplesPerWavelength[n] = bytesPerSecond / frequency[n];
                                    ampSteps[n] = (short)(amp * 2 / samplesPerWavelength[n]);
                                }

                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                    angles[n] = frequency[n] * TAU / samplesPerSecond;

                                short tempSample = (short)-amp;
                                for (int s = 0; s < chordSamples; s++)
                                {
                                    short sample = 0;

                                    if (chord.GetNote(0).GetPitch() != Pitch.Rest)
                                        switch (chord.GetWaveForm())
                                        {
                                            case WaveForm.Sine:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(amp * Math.Sin(angles[n] * s));
                                                break;
                                            case WaveForm.Square:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(amp * Math.Sign(Math.Sin(angles[n] * s)));
                                                break;
                                            case WaveForm.Sawtooth:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                    sample += (short)(s % samplesPerWavelength[n] * ampSteps[n]);
                                                break;
                                            case WaveForm.Triangle:
                                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                                {
                                                    int sLoc = s % samplesPerWavelength[n];
                                                    if (sLoc < samplesPerWavelength[n] / 2)
                                                        if (sLoc < samplesPerWavelength[n] / 4)
                                                            sample += (short)(sLoc * ampSteps[n]);
                                                        else
                                                            sample += (short)((samplesPerWavelength[n] - sLoc) * ampSteps[n]);
                                                    else
                                                        if (sLoc < samplesPerWavelength[n] * 3 / 4)
                                                        sample += (short)(sLoc * ampSteps[n]);
                                                    else
                                                        sample += (short)((samplesPerWavelength[n] - sLoc) * ampSteps[n]);
                                                }
                                                break;
                                            case WaveForm.Noise:
                                                sample += (short)RG.Next((int)-amp, (int)amp);
                                                break;
                                        }

                                    preSamples[chordNumber] += sample;
                                    chordNumber++;
                                }
                            }
                        }
                    }
                }

                foreach (short s in preSamples)
                    writer.Write(s);
            }

            writer.Close();
        }

        public void ExportImage(string filename, ImageFormat fileformat = null)
        {
            foreach (SongComponent component in SELECTABLES)
                component.Deselect();
            Bitmap bmp = new Bitmap((int)PAGE_WIDTH, (int)(PAGE_WIDTH / 8.5 * 11));
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Draw(g);
            if (fileformat == null)
                bmp.Save(filename, ImageFormat.Png);
            else
                bmp.Save(filename, fileformat);
        }

        public void Save(string filename = null)
        {
            if (filename != null)
                mFileName = filename;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(mFileName, FileMode.Append, FileAccess.Write);
            MemoryStream settingsStream = new MemoryStream();

            formatter.Serialize(stream, this);
            SongSettings settings = new SongSettings();

            long sSongLength = stream.Length;

            formatter.Serialize(settingsStream, settings);
            settingsStream.Position = 0;
            settingsStream.CopyTo(stream);

            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(sSongLength);

            settingsStream.Close();
            stream.Close();

            ExportAudio(filename.Substring(0, filename.Length - 3) + "wav");
            ExportImage(filename.Substring(0, filename.Length - 3) + "png");
        }
    }
}

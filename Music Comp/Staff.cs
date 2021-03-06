﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    [Serializable]
    public class Staff : SongComponent
    {
        RectangleF staffArea;
        RectangleF clefArea;
        RectangleF keyArea;
        RectangleF timeArea;
        RectangleF cursorArea;
        List<RectangleF> keySignature;

        static Image doubleflatImage = Properties.Resources.DoubleFlat;
        static Image flatImage = Properties.Resources.Flat;
        static Image naturalImage = Properties.Resources.Natural;
        static Image sharpImage = Properties.Resources.Sharp;
        static Image doublesharpImage = Properties.Resources.DoubleSharp;
        static Image trebleClefImage = Properties.Resources.TrebleClef;
        static Image cClefImage = Properties.Resources.C_Clef;
        static Image bassClefImage = Properties.Resources.BassClef;
        static Image fourFourImage = Properties.Resources.FourFour;
        static Image sixEightImage = Properties.Resources.SixEight;

        Color cursorColor = Properties.Settings.Default.CursorColor;

        public static float LINE_SPACING;
        public static float LENGTH;
        public static float HEIGHT;

        int mTotalInstrumentNumber;
        int mTotalStaffNumber;

        float mYPosition;
        float mCursorX;

        List<Measure> mMeasures;
        Measure mSelectedMeasure;
        int mStaffNumber;
        Clef mClef;
        WaveForm mWaveForm;

        public Staff(Clef c, WaveForm w, int inst, int totalStaffNumber, int staffNumber)
        {
            LINE_SPACING = 30 * Song._SCALE;
            LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            HEIGHT = 4 * LINE_SPACING;

            mTotalInstrumentNumber = inst;
            mTotalStaffNumber = totalStaffNumber;

            mYPosition = mTotalInstrumentNumber * Song.INSTRUMENT_SPACING + mTotalStaffNumber * (HEIGHT + Song.STAFF_SPACING);

            mClef = c;
            mWaveForm = w;
            mMeasures = new List<Measure>();
            AddMeasure();

            keySignature = new List<RectangleF>();

            area = new RectangleF();
            mStaffNumber = staffNumber;

            Song.TOTAL_STAVES++;
            Song.SELECTABLES.Add(this);
            switch (mClef)
            {
                case Clef.Treble:
                    Song.LASTNOTES[inst][staffNumber] = new Note(Pitch.G, Accidental.Natural, Duration.Quarter, 4);
                    break;
                case Clef.Alto:
                    Song.LASTNOTES[inst][staffNumber] = new Note(Pitch.G, Accidental.Natural, Duration.Quarter, 4);
                    break;
                case Clef.Bass:
                    Song.LASTNOTES[inst][staffNumber] = new Note(Pitch.D, Accidental.Natural, Duration.Quarter, 3);
                    break;
                case Clef.Tenor:
                    Song.LASTNOTES[inst][staffNumber] = new Note(Pitch.G, Accidental.Natural, Duration.Quarter, 4);
                    break;
            }
        }

        public int GetMeasureCount()
        {
            return mMeasures.Count;
        }

        public Measure GetMeasure(int i)
        {
            return mMeasures[i];
        }

        public Measure GetSelection()
        {
            return mSelectedMeasure;
        }

        public int GetStaffNumber()
        {
            return mStaffNumber;
        }

        public Measure GetCurrentMeasure()
        {
            return mMeasures[mMeasures.Count - 1];
        }

        public Measure GetNextMeasure()
        {
            if (mMeasures[mMeasures.Count - 1].IsFull())
                AddMeasure();
            return mMeasures[mMeasures.Count - 1];
        }

        public int GetDuration()
        {
            int d = 0;
            foreach (Measure measure in mMeasures)
                d += measure.GetDuration();
            return d;
        }

        public Clef GetClef()
        {
            return mClef;
        }

        public WaveForm GetWaveForm()
        {
            return mWaveForm;
        }

        public float GetYPosition()
        {
            return mYPosition;
        }

        public void SetSelection(Measure m)
        {
            mSelectedMeasure = m;
        }

        public void SetSelection(int m)
        {
            mSelectedMeasure.Deselect();
            mSelectedMeasure = GetMeasure(m);
            mSelectedMeasure.Select();
        }

        public void AddMeasure()
        {
            mMeasures.Add(new Measure(mClef, mWaveForm, mYPosition, mMeasures.Count));
            if (GetMeasureCount() == 1)
                mSelectedMeasure = GetMeasure(0);
        }

        public void RemoveMeasure(Measure m)
        {
            mMeasures.Remove(m);
        }

        public void AddAccidental(float x, float y, Accidental a)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    keySignature.Add(new RectangleF(x, y - 50 * Song._SCALE, 60 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Flat:
                    keySignature.Add(new RectangleF(x, y - 50 * Song._SCALE, 35 * Song._SCALE, 68 * Song._SCALE));
                    break;
                case Accidental.Natural:
                    keySignature.Add(new RectangleF(x, y - 38 * Song._SCALE, 20 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Sharp:
                    keySignature.Add(new RectangleF(x, y - 38 * Song._SCALE, 35 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.DoubleSharp:
                    keySignature.Add(new RectangleF(x, y - 20 * Song._SCALE, 35 * Song._SCALE, 35 * Song._SCALE));
                    break;
            }
        }

        public void DrawAccidental(RectangleF rect, Accidental a, Graphics g)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    if (g.IsVisible(rect))
                        g.DrawImage(doubleflatImage, rect);
                    break;
                case Accidental.Flat:
                    if (g.IsVisible(rect))
                        g.DrawImage(flatImage, rect);
                    break;
                case Accidental.Natural:
                    if (g.IsVisible(rect))
                        g.DrawImage(naturalImage, rect);
                    break;
                case Accidental.Sharp:
                    if (g.IsVisible(rect))
                        g.DrawImage(sharpImage, rect);
                    break;
                case Accidental.DoubleSharp:
                    if (g.IsVisible(rect))
                        g.DrawImage(doublesharpImage, rect);
                    break;
            }
        }

        ///
        /// Private helper methods
        ///

        private void UpdateCursor()
        {
            if (isSelected)
                cursorArea = new RectangleF(Song.BARLINES[0], Song.TOP_MARGIN + mYPosition,
                    Song.PAGE_WIDTH - Song.BARLINES[0] - Song.RIGHT_MARGIN, HEIGHT);
        }

        private void DrawCursor(Graphics g)
        {
            if (isSelected)
                if (g.IsVisible(cursorArea))
                    g.FillRectangle(new SolidBrush(cursorColor), cursorArea);
        }

        private void UpdateStaff()
        {
            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);
            staffArea = new RectangleF(location, size);
        }

        private void DrawStaff(Graphics g)
        {
            for (int i = 0; i < 5; i++)
            {
                PointF start = new PointF(staffArea.X, staffArea.Y + i * staffArea.Height / 4);
                PointF end = new PointF(staffArea.X + staffArea.Width, staffArea.Y + i * staffArea.Height / 4);

                if (g.IsVisible(new RectangleF(start.X, start.Y, end.X - start.X, 1)))
                    g.DrawLine(new Pen(Color.Black, (int)(4.0f * Song._SCALE)), start, end);
            }
        }

        private void UpdateClef()
        {
            PointF location = new PointF();
            SizeF size = new SizeF();

            switch (mClef)
            {
                case Clef.Treble:
                    location.X = mCursorX - 49 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 70 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.50f;
                    size.Height = HEIGHT * 2.28f;
                    clefArea = new RectangleF(location, size);
                    break;
                case Clef.Alto:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 0 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.80f;
                    size.Height = HEIGHT;
                    clefArea = new RectangleF(location, size);
                    break;
                case Clef.Bass:
                    location.X = mCursorX - 35 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 58 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.48f;
                    size.Height = HEIGHT * 1.82f;
                    clefArea = new RectangleF(location, size);
                    break;
                case Clef.Tenor:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 29 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.8f;
                    size.Height = HEIGHT;
                    clefArea = new RectangleF(location, size);
                    break;
            }

            mCursorX += 120 * Song._SCALE;
        }

        private void DrawClef(Graphics g)
        {
            switch (mClef)
            {
                case Clef.Treble:
                    if (g.IsVisible(clefArea))
                        g.DrawImage(trebleClefImage, clefArea);
                    break;
                case Clef.Alto:
                    if (g.IsVisible(clefArea))
                        g.DrawImage(cClefImage, clefArea);
                    break;
                case Clef.Bass:
                    if (g.IsVisible(clefArea))
                        g.DrawImage(bassClefImage, clefArea);
                    break;
                case Clef.Tenor:
                    if (g.IsVisible(clefArea))
                        g.DrawImage(cClefImage, clefArea);
                    break;
            }
        }

        private void UpdateKeySignature()
        {
            keySignature = new List<RectangleF>();
            PointF location = new PointF(mCursorX, mYPosition);
            SizeF size = new SizeF();

            if (Song.KEY < 0)                                   // Flats
                for (int i = 0, y = 7; i > (int)Song.KEY; i--)  // B E A D G C F
                {
                    AddAccidental(mCursorX, mYPosition + Song.TOP_MARGIN + (-43.5f + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Flat);
                    y += (i % 2 == 0 ? -3 : 4);
                    mCursorX += 30 * Song._SCALE;
                }
            else if (Song.KEY > 0)                                                         // Sharps
                for (int i = 0, y = mClef != Clef.Tenor ? 3 : 10; i < (int)Song.KEY; i++)  // F C G D A E B
                {
                    AddAccidental(mCursorX, mYPosition + Song.TOP_MARGIN + (-43.5f + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Sharp);
                    if (mClef == Clef.Tenor || i > 2)
                        y += i % 2 == 0 ? -4 : 3;
                    else
                        y += i % 2 == 0 ? 3 : -4;
                    mCursorX += 30 * Song._SCALE;
                }

            size.Width = mCursorX;
            size.Height = HEIGHT;

            keyArea = new RectangleF(location, size);

            mCursorX += 30 * Song._SCALE;
        }

        private void DrawKeySignature(Graphics g)
        {
            foreach (RectangleF rectangleF in keySignature)
                DrawAccidental(rectangleF, Song.KEY > 0 ? Accidental.Sharp : Accidental.Flat, g);
        }

        private void UpdateTimeSignature()
        {
            PointF location = new PointF();
            SizeF size = new SizeF();

            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    location.X = mCursorX;
                    location.Y = Song.TOP_MARGIN + mYPosition;
                    size.Width = HEIGHT * 0.5f;
                    size.Height = HEIGHT;
                    timeArea = new RectangleF(location, size);
                    break;
                case Time.ThreeEight:

                    break;
                case Time.TwoFour:

                    break;
                case Time.ThreeFour:

                    break;
                case Time.FourFour:
                    location.X = mCursorX;
                    location.Y = Song.TOP_MARGIN + mYPosition;
                    size.Width = HEIGHT * 0.5f;
                    size.Height = HEIGHT;
                    timeArea = new RectangleF(location, size);
                    break;
            }

            mCursorX += 90 * Song._SCALE;

            if (Song.BARLINES.Count != 0)
                if (Song.BARLINES[0] > mCursorX + 30 * Song._SCALE)
                {
                    float diff = Song.BARLINES[0] - mCursorX + 30 * Song._SCALE;
                    for (int i = 0; i < Song.BARLINES.Count; i++)
                        Song.BARLINES[i] -= diff;
                }
        }

        private void DrawTimeSignature(Graphics g)
        {
            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    if (g.IsVisible(timeArea))
                        g.DrawImage(sixEightImage, timeArea);
                    break;
                case Time.ThreeEight:

                    break;
                case Time.TwoFour:

                    break;
                case Time.ThreeFour:

                    break;
                case Time.FourFour:
                    if (g.IsVisible(timeArea))
                        g.DrawImage(fourFourImage, timeArea);
                    break;
            }
        }

        private void UpdateBarLines()
        {
            int stnza = 0;
            //if (mTotalInstrumentNumber / Song.TOTAL_INSTRUMENTS > Song.stanzas)
            //    stnza++;
            for (int i = 0; i < mMeasures.Count; i++)
            {
                mCursorX += 30 * Song._SCALE;

                if (i >= Song.BARLINES.Count && mMeasures[i].IsFull())
                {
                    Song.BARLINES.Add(mCursorX + stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN));
                    AddMeasure();
                }

                else if (i >= Song.BARLINES.Count)
                    Song.BARLINES.Add(mCursorX + stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN));

                else if (mCursorX > Song.BARLINES[i] - stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN))
                    Song.BARLINES[i] = mCursorX + stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN);

                else
                    mCursorX = Song.BARLINES[i] - stnza * (Song.PAGE_WIDTH - Song.RIGHT_MARGIN);

                mCursorX += mMeasures[i].GetLength();
            }
        }

        public void Update()
        {
            mCursorX = Song.LEFT_MARGIN;

            mYPosition = mTotalInstrumentNumber * Song.INSTRUMENT_SPACING + mTotalStaffNumber * (HEIGHT + Song.STAFF_SPACING);

            UpdateStaff();

            UpdateClef();

            UpdateKeySignature();

            UpdateTimeSignature();

            UpdateBarLines();

            UpdateCursor();

            for (int i = 0; i < mMeasures.Count; i++)
                mMeasures[i].Update(Song.BARLINES[i], mYPosition);

            area.X = Song.BARLINES[0];
            area.Y = staffArea.Top - Song.STAFF_SPACING;
            area.Width = staffArea.Right - area.X;
            area.Height = HEIGHT + Song.STAFF_SPACING * 2;
        }

        public void Draw(Graphics g)
        {
            DrawCursor(g);

            DrawStaff(g);

            if (Song.needsFullUpdate)
            {
                DrawClef(g);

                DrawKeySignature(g);

                DrawTimeSignature(g);
            }

            foreach (Measure measure in mMeasures)
                measure.Draw(g);
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

            foreach (Measure measure in mMeasures)
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

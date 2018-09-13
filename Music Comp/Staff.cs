using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
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

        bool isActive = false;

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

        public float GetYPosition()
        {
            return mYPosition;
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void SetActive(bool active)
        {
            isActive = active;
        }

        public void SetSelection(Measure m)
        {
            mSelectedMeasure = m;
        }

        public void SetSelection(int m)
        {
            mSelectedMeasure = GetMeasure(m);
        }

        public void AddMeasure()
        {
            mMeasures.Add(new Measure(mClef, mYPosition, mMeasures.Count));
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

        public void DrawAccidental(RectangleF rect, Accidental a, PaintEventArgs e)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(doubleflatImage, rect);
                    break;
                case Accidental.Flat:
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(flatImage, rect);
                    break;
                case Accidental.Natural:
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(naturalImage, rect);
                    break;
                case Accidental.Sharp:
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(sharpImage, rect);
                    break;
                case Accidental.DoubleSharp:
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(doublesharpImage, rect);
                    break;
            }
        }

        ///
        /// Private helper methods
        ///

        private void UpdateCursor()
        {
            if (isActive)
                cursorArea = new RectangleF(Song.BARLINES[0], Song.TOP_MARGIN + mYPosition,
                    Song.PAGE_WIDTH - Song.BARLINES[0] - Song.RIGHT_MARGIN, HEIGHT);
        }

        private void DrawCursor(PaintEventArgs e)
        {
            if (isActive)
                if (e.Graphics.IsVisible(cursorArea))
                    e.Graphics.FillRectangle(new SolidBrush(cursorColor), cursorArea);
        }

        private void UpdateStaff()
        {
            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);
            staffArea = new RectangleF(location, size);
        }

        private void DrawStaff(PaintEventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                PointF start = new PointF(staffArea.X, staffArea.Y + i * staffArea.Height / 4);
                PointF end = new PointF(staffArea.X + staffArea.Width, staffArea.Y + i * staffArea.Height / 4);

                if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, end.X - start.X, 1)))
                    e.Graphics.DrawLine(new Pen(Color.Black, (int)(4.0f * Song._SCALE)), start, end);
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

        private void DrawClef(PaintEventArgs e)
        {
            switch (mClef)
            {
                case Clef.Treble:
                    if (e.Graphics.IsVisible(clefArea))
                        e.Graphics.DrawImage(trebleClefImage, clefArea);
                    break;
                case Clef.Alto:
                    if (e.Graphics.IsVisible(clefArea))
                        e.Graphics.DrawImage(cClefImage, clefArea);
                    break;
                case Clef.Bass:
                    if (e.Graphics.IsVisible(clefArea))
                        e.Graphics.DrawImage(bassClefImage, clefArea);
                    break;
                case Clef.Tenor:
                    if (e.Graphics.IsVisible(clefArea))
                        e.Graphics.DrawImage(cClefImage, clefArea);
                    break;
            }
        }

        private void UpdateKeySignature()
        {
            PointF location = new PointF(mCursorX, mYPosition);
            SizeF size = new SizeF();

            if (Song.KEY < 0)                                   // Flats
                for (int i = 0, y = 7; i > (int)Song.KEY; i--)  // B E A D G C F
                {
                    AddAccidental(mCursorX, mYPosition + (260 + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Flat);
                    y += (i % 2 == 0 ? -3 : 4);
                    mCursorX += 30 * Song._SCALE;
                }
            else if (Song.KEY > 0)                                                         // Sharps
                for (int i = 0, y = mClef != Clef.Tenor ? 3 : 10; i < (int)Song.KEY; i++)  // F C G D A E B
                {
                    AddAccidental(mCursorX, mYPosition + (260 + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Sharp);
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

        private void DrawKeySignature(PaintEventArgs e)
        {
            foreach (RectangleF rectangleF in keySignature)
                DrawAccidental(rectangleF, Song.KEY > 0 ? Accidental.Sharp : Accidental.Flat, e);
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
        }

        private void DrawTimeSignature(PaintEventArgs e)
        {
            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    if (e.Graphics.IsVisible(timeArea))
                        e.Graphics.DrawImage(sixEightImage, timeArea);
                    break;
                case Time.ThreeEight:

                    break;
                case Time.TwoFour:

                    break;
                case Time.ThreeFour:

                    break;
                case Time.FourFour:
                    if (e.Graphics.IsVisible(timeArea))
                        e.Graphics.DrawImage(fourFourImage, timeArea);
                    break;
            }
        }

        private void UpdateBarLines()
        {
            for (int i = 0; i < mMeasures.Count; i++)
            {
                mCursorX += 30 * Song._SCALE;

                if (i >= Song.BARLINES.Count && mMeasures[i].IsFull())
                {
                    Song.BARLINES.Add(mCursorX);
                    AddMeasure();
                }

                else if (i >= Song.BARLINES.Count)
                    Song.BARLINES.Add(mCursorX);

                else if (mCursorX > Song.BARLINES[i])
                    Song.BARLINES[i] = mCursorX;

                else
                    mCursorX = Song.BARLINES[i];

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

        public void Draw(PaintEventArgs e)
        {
            if (isSelected)
                if (e.Graphics.IsVisible(area))
                    e.Graphics.FillRectangle(new SolidBrush(Color.Green), area);
            DrawCursor(e);

            DrawStaff(e);

            DrawClef(e);

            DrawKeySignature(e);

            DrawTimeSignature(e);

            foreach (Measure measure in mMeasures)
                measure.Draw(e);
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

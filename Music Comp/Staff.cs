using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Staff
    {
        RectangleF area;
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

        int instrumentNumber;
        int staffNumber;

        float mYPosition;
        float mCursorX;

        List<Measure> mMeasures;
        Clef mClef;

        bool isActive = false;

        public Staff(Clef c, int inst, int staff)
        {
            LINE_SPACING = 30 * Song._SCALE;
            LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            HEIGHT = 4 * LINE_SPACING;

            instrumentNumber = inst;
            staffNumber = staff;

            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            mClef = c;
            mMeasures = new List<Measure>();
            AddMeasure();

            keySignature = new List<RectangleF>();

            Song.TOTAL_STAVES++;
        }

        public Measure GetMeasure(int i)
        {
            return mMeasures[i];
        }

        public Measure GetNextMeasure()
        {
            if (mMeasures[mMeasures.Count - 1].GetFull())
                AddMeasure();
            return mMeasures[mMeasures.Count - 1];
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public void AddMeasure()
        {
            mMeasures.Add(new Measure(mClef, mYPosition));
        }

        public bool IsActive()
        {
            return isActive;
        }

        public void SetActive(bool setA)
        {
            isActive = setA;
        }

        public float GetYPosition()
        {
            return mYPosition;
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
        /// Private healper methods
        ///

        private void UpdateCursor()
        {
            if (isActive)
                cursorArea = new RectangleF(Song.LEFT_MARGIN + 2.0f, Song.TOP_MARGIN + mYPosition, LENGTH, HEIGHT);
        }

        private void DrawCursor(PaintEventArgs e)
        {
            if (isActive)
            {
                SolidBrush brush = new SolidBrush(cursorColor);
                if (e.Graphics.IsVisible(cursorArea))
                    e.Graphics.FillRectangle(brush, cursorArea);
                brush.Dispose();
            }
        }

        private void UpdateStaff()
        {
            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);
            area = new RectangleF(location, size);
        }

        private void DrawStaff(PaintEventArgs e)
        {
            Pen staffLinePen = new Pen(Color.Black, (int)(4.0f * Song._SCALE));
            for (int i = 0; i < 5; i++)
            {
                PointF start = new PointF(area.X, area.Y + i * area.Height / 4);
                PointF end = new PointF(area.X + area.Width, area.Y + i * area.Height / 4);

                if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, end.X - start.X, 1)))
                    e.Graphics.DrawLine(staffLinePen, start, end);
            }
            staffLinePen.Dispose();
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
                if (i >= Song.mBarlines.Count) Song.mBarlines.Add(mCursorX);
                else if (mCursorX > Song.mBarlines[i]) Song.mBarlines[i] = mCursorX;
                else mCursorX = Song.mBarlines[i];
                mMeasures[i].UpdateLength();
                mCursorX += mMeasures[i].GetLength();
            }
        }

        public void Update()
        {
            mCursorX = Song.LEFT_MARGIN;
            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            UpdateCursor();

            UpdateStaff();

            UpdateClef();

            UpdateKeySignature();

            UpdateTimeSignature();

            UpdateBarLines();

            for (int i = 0; i < mMeasures.Count; i++)
                mMeasures[i].Update(Song.mBarlines[i], mYPosition);
        }

        public void Draw(PaintEventArgs e)
        {
            DrawCursor(e);

            DrawStaff(e);

            DrawClef(e);

            DrawKeySignature(e);

            DrawTimeSignature(e);

            foreach (Measure measure in mMeasures)
                measure.Draw(e);
        }
    }
}

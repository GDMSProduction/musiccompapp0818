using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Staff
    {
        Rectangle area;

        static Image staffImage = Properties.Resources.Staff;
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

        public Rectangle GetArea()
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

        public void DrawAccidental(float x, float y, Accidental a, PaintEventArgs e)
        {
            RectangleF rect;
            switch (a)
            {
                case Accidental.DoubleFlat:
                    rect = new RectangleF(x, y - 50 * Song._SCALE, 60 * Song._SCALE, 70 * Song._SCALE);
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(doubleflatImage, rect);
                    break;
                case Accidental.Flat:
                    rect = new RectangleF(x, y - 50 * Song._SCALE, 35 * Song._SCALE, 68 * Song._SCALE);
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(flatImage, rect);
                    break;
                case Accidental.Natural:
                    rect = new RectangleF(x, y - 38 * Song._SCALE, 20 * Song._SCALE, 70 * Song._SCALE);
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(naturalImage, rect);
                    break;
                case Accidental.Sharp:
                    rect = new RectangleF(x, y - 38 * Song._SCALE, 35 * Song._SCALE, 70 * Song._SCALE);
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(sharpImage, rect);
                    break;
                case Accidental.DoubleSharp:
                    rect = new RectangleF(x, y - 20 * Song._SCALE, 35 * Song._SCALE, 35 * Song._SCALE);
                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(doublesharpImage, rect);
                    break;
            }
        }

        ///
        /// Private healper methods
        ///

        private void DrawStaff(PaintEventArgs e)
        {
            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);
            RectangleF rect = new RectangleF(location, size);

            if (e.Graphics.IsVisible(rect))
                e.Graphics.DrawImage(staffImage, new RectangleF(location, size));
        }

        private void DrawClef(PaintEventArgs e)
        {
            PointF location = new PointF();
            SizeF size = new SizeF();
            RectangleF rect;

            switch (mClef)
            {
                case Clef.Treble:
                    location.X = mCursorX - 49 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 70 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.50f;
                    size.Height = HEIGHT * 2.28f;
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(trebleClefImage, rect);
                    break;
                case Clef.Alto:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 0 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.80f;
                    size.Height = HEIGHT;
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(cClefImage, rect);
                    break;
                case Clef.Bass:
                    location.X = mCursorX - 35 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 58 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.48f;
                    size.Height = HEIGHT * 1.82f;
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(bassClefImage, rect);
                    break;
                case Clef.Tenor:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 29 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.8f;
                    size.Height = HEIGHT;
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(cClefImage, rect);
                    break;
            }

            mCursorX += 120 * Song._SCALE;
        }

        private void DrawKeySignature(PaintEventArgs e)
        {
            if (Song.KEY < 0)                                   // Flats
                for (int i = 0, y = 7; i > (int)Song.KEY; i--)  // B E A D G C F
                {
                    DrawAccidental(mCursorX, mYPosition + (260 + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Flat, e);
                    y += (i % 2 == 0 ? -3 : 4);
                    mCursorX += 30 * Song._SCALE;
                }
            else if (Song.KEY > 0)                                                         // Sharps
                for (int i = 0, y = mClef != Clef.Tenor ? 3 : 10; i < (int)Song.KEY; i++)  // F C G D A E B
                {
                    DrawAccidental(mCursorX, mYPosition + (260 + (y + (int)mClef) * 14.5f) * Song._SCALE, Accidental.Sharp, e);
                    if (mClef == Clef.Tenor || i > 2)
                        y += i % 2 == 0 ? -4 : 3;
                    else
                        y += i % 2 == 0 ? 3 : -4;
                    mCursorX += 30 * Song._SCALE;
                }

            mCursorX += 30 * Song._SCALE;
        }

        private void DrawTimeSignature(PaintEventArgs e)
        {
            PointF location = new PointF();
            SizeF size = new SizeF();
            RectangleF rect;

            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    location.X = mCursorX;
                    location.Y = Song.TOP_MARGIN + mYPosition;
                    size.Width = HEIGHT * 0.5f;
                    size.Height = HEIGHT;
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(sixEightImage, rect);
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
                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(fourFourImage, rect);
                    break;
            }

            mCursorX += 90 * Song._SCALE;
        }

        private void DrawCursor(PaintEventArgs e)
        {
            if (isActive)
            {
                SolidBrush brush = new SolidBrush(Color.LightSkyBlue);
                RectangleF rect = new RectangleF(Song.LEFT_MARGIN + 2.0f, Song.TOP_MARGIN + mYPosition, LENGTH, HEIGHT);
                if (e.Graphics.IsVisible(rect))
                    e.Graphics.FillRectangle(brush, rect);
                brush.Dispose();
            }
        }

        public void Draw(PaintEventArgs e)
        {
            mCursorX = Song.LEFT_MARGIN;
            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            DrawCursor(e);

            DrawStaff(e);

            DrawClef(e);

            DrawKeySignature(e);

            DrawTimeSignature(e);

            for (int i = 0; i < mMeasures.Count; i++)
            {
                mCursorX += 30 * Song._SCALE;
                if (i >= Song.mBarlines.Count)
                {
                    Song.mBarlines.Add(mCursorX);
                }
                else
                {
                    if (mCursorX > Song.mBarlines[i])
                    {
                        Song.mBarlines[i] = mCursorX;
                    }
                    else
                    {
                        mCursorX = Song.mBarlines[i];
                    }
                }

                mMeasures[i].UpdateLength();
                mCursorX += mMeasures[i].GetLength();
            }
            for (int i = 0; i < mMeasures.Count; i++)
            {
                mMeasures[i].Draw(Song.mBarlines[i], e);
            }
        }
    }
}

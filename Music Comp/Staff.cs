using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Staff
    {
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

            Song.TOTAL_STAVES++;
        }

        public Measure GetMeasure(int i)
        {
            return mMeasures[i];
        }

        public void AddMeasure()
        {
            mMeasures.Add(new Measure(mClef, mCursorX, mYPosition));
        }

        public void DrawAccidental(float x, float y, Accidental a, PaintEventArgs e)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    e.Graphics.DrawImage(doubleflatImage, new RectangleF(x, y - 50 * Song._SCALE, 60 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Flat:
                    e.Graphics.DrawImage(flatImage, new RectangleF(x, y - 50 * Song._SCALE, 35 * Song._SCALE, 68 * Song._SCALE));
                    break;
                case Accidental.Natural:
                    e.Graphics.DrawImage(naturalImage, new RectangleF(x, y - 38 * Song._SCALE, 20 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Sharp:
                    e.Graphics.DrawImage(sharpImage, new RectangleF(x, y - 38 * Song._SCALE, 35 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.DoubleSharp:
                    e.Graphics.DrawImage(doublesharpImage, new RectangleF(x, y - 20 * Song._SCALE, 35 * Song._SCALE, 35 * Song._SCALE));
                    break;
            }
        }
        public void Draw(PaintEventArgs e)
        {
            if (isActive)
            {
                SolidBrush brush = new SolidBrush(Color.LightSkyBlue);
                e.Graphics.FillRectangle(brush, Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition, LENGTH, HEIGHT);
                brush.Dispose();
            }

            mCursorX = Song.LEFT_MARGIN;
            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);

            e.Graphics.DrawImage(staffImage, new RectangleF(location, size));

            switch (mClef)
            {
                case Clef.Treble:
                    location.X = mCursorX - 49 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 70 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.50f;
                    size.Height = HEIGHT * 2.28f;

                    e.Graphics.DrawImage(trebleClefImage, new RectangleF(location, size));
                    break;
                case Clef.Alto:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 0 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.80f;
                    size.Height = HEIGHT;

                    e.Graphics.DrawImage(cClefImage, new RectangleF(location, size));
                    break;
                case Clef.Bass:
                    location.X = mCursorX - 35 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 58 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.48f;
                    size.Height = HEIGHT * 1.82f;

                    e.Graphics.DrawImage(bassClefImage, new RectangleF(location, size));
                    break;
                case Clef.Tenor:
                    location.X = mCursorX + 10 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 29 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 0.8f;
                    size.Height = HEIGHT;

                    e.Graphics.DrawImage(cClefImage, new RectangleF(location, size));
                    break;
            }

            mCursorX += 120 * Song._SCALE;

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

            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    location.X = mCursorX;
                    location.Y = Song.TOP_MARGIN + mYPosition;
                    size.Width = HEIGHT * 0.5f;
                    size.Height = HEIGHT;

                    e.Graphics.DrawImage(sixEightImage, new RectangleF(location, size));
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

                    e.Graphics.DrawImage(fourFourImage, new RectangleF(location, size));
                    break;
            }

            mCursorX += 90 * Song._SCALE;

            if (staffNumber == 0 && mMeasures.Count == 0)
                AddMeasure();
            if (staffNumber == 1 && mMeasures.Count == 0)
                AddMeasure();
            if (staffNumber == 2 && mMeasures.Count == 0)
                AddMeasure();
            if (staffNumber == 3 && mMeasures.Count == 0)
                AddMeasure();

            foreach (Measure measure in mMeasures)
                measure.Draw(e);
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
    }
}

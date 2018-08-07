using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Staff
    {
        List<Measure> measures;

        public static float LINE_SPACING;
        public static float LENGTH;
        public static float HEIGHT;

        int instrumentNumber;
        int staffNumber;

        float mYPosition;
        float mCursorX;

        Clef mClef;

        public Staff(Clef c, int inst, int staff)
        {
            LINE_SPACING = 30 * Song._SCALE;
            LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            HEIGHT = 4 * LINE_SPACING;

            instrumentNumber = inst;
            staffNumber = staff;

            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            mClef = c;

            Song.TOTAL_STAVES++;
        }
        public Measure GetMeasure(int i)
        {
            return measures[i];
        }
        public void AddMeasure()
        {
            measures.Add(new Measure(mClef, mCursorX));
        }
        public void DrawAccidental(float x, float y, Accidental a, PaintEventArgs e)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:
                    e.Graphics.DrawImage(Properties.Resources.DoubleFlat, new RectangleF(x, y - 50 * Song._SCALE, 60 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Flat:
                    e.Graphics.DrawImage(Properties.Resources.Flat, new RectangleF(x, y - 50 * Song._SCALE, 35 * Song._SCALE, 68 * Song._SCALE));
                    break;
                case Accidental.Natural:
                    e.Graphics.DrawImage(Properties.Resources.Natural, new RectangleF(x, y - 38 * Song._SCALE, 20 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.Sharp:
                    e.Graphics.DrawImage(Properties.Resources.Sharp, new RectangleF(x, y - 38 * Song._SCALE, 35 * Song._SCALE, 70 * Song._SCALE));
                    break;
                case Accidental.DoubleSharp:
                    e.Graphics.DrawImage(Properties.Resources.DoubleSharp, new RectangleF(x, y - 20 * Song._SCALE, 35 * Song._SCALE, 35 * Song._SCALE));
                    break;
            }
        }
        public void Draw(PaintEventArgs e)
        {
            mCursorX = Song.LEFT_MARGIN;
            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);

            e.Graphics.DrawImage(Properties.Resources.Staff, new RectangleF(location, size));

            switch (mClef)
            {
                case Clef.Treble:
                    location.X = mCursorX - 49 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 70 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.50f;
                    size.Height = HEIGHT * 2.28f;

                    e.Graphics.DrawImage(Properties.Resources.TrebleClef, new RectangleF(location, size));
                    break;
                case Clef.Alto:
                    break;
                case Clef.Bass:
                    location.X = mCursorX - 35 * Song._SCALE;
                    location.Y = Song.TOP_MARGIN - 58 * Song._SCALE + mYPosition;
                    size.Width = HEIGHT * 1.48f;
                    size.Height = HEIGHT * 1.82f;

                    e.Graphics.DrawImage(Properties.Resources.BassClef, new RectangleF(location, size));
                    break;
                case Clef.Tenor:
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

                    e.Graphics.DrawImage(Properties.Resources.SixEight, new RectangleF(location, size));
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

                    e.Graphics.DrawImage(Properties.Resources.FourFour, new RectangleF(location, size));
                    break;
            }

            mCursorX += 90 * Song._SCALE;
        }
    }
}

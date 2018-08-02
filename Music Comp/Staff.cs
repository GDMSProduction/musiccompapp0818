using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Staff
    {
        List<Measure> measures;

        public static readonly int LINE_SPACING = 30;
        public static readonly int LENGTH = 1920 - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
        public static readonly int HEIGHT = 4 * LINE_SPACING;

        public int mYPosition;
        public int mCursorX;

        Clef mClef;

        public Staff(Clef c, int ypos)
        {
            mClef = c;
            mYPosition = ypos;
            mCursorX = Song.LEFT_MARGIN;
        }
        public Measure GetMeasure(int i)
        {
            return measures[i];
        }
        public void AddMeasures()
        {

        }
        public void Draw(PaintEventArgs e)
        {
            PointF p = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN);
            SizeF s = new SizeF(LENGTH, HEIGHT);
            RectangleF imageRect = new RectangleF(p, s);
            e.Graphics.DrawImage(Properties.Resources.Staff, imageRect);
            switch (mClef)
            {
                case Clef.Treble:

                    break;
                case Clef.Alto:

                    break;
                case Clef.Bass:

                    break;
                case Clef.Tenor:

                    break;
            }
            switch (Song.KEY)
            {
                case Key.Cflat:

                    break;
                case Key.Gflat:

                    break;
                case Key.Dflat:

                    break;
                case Key.Aflat:

                    break;
                case Key.Eflat:

                    break;
                case Key.Bflat:

                    break;
                case Key.F:

                    break;
                case Key.C:

                    break;
                case Key.G:

                    break;
                case Key.D:

                    break;
                case Key.A:

                    break;
                case Key.E:

                    break;
                case Key.B:

                    break;
                case Key.Fsharp:

                    break;
                case Key.Csharp:

                    break;
            }
            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:

                    break;
                case Time.ThreeEight:

                    break;
                case Time.TwoFour:

                    break;
                case Time.ThreeFour:

                    break;
                case Time.FourFour:

                    break;
            }
        }
    }
}

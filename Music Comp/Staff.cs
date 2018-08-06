using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Staff
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
            LINE_SPACING = 30 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            HEIGHT = 4 * LINE_SPACING;

            instrumentNumber = inst;
            staffNumber = staff;

            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);
            mCursorX = Song.LEFT_MARGIN;

            mClef = c;

            Song.TOTAL_STAVES++;
        }
        public Measure GetMeasure(int i)
        {
            return measures[i];
        }
        public void AddMeasure()
        {
            measures.Add(new Measure(mCursorX));
        }
        public void DrawAccidental(float x, float y, Accidental a, PaintEventArgs e)
        {
            PointF location;
            SizeF size;

            switch (a)
            {
                case Accidental.DoubleFlat:
                    location = new PointF(x, y - 50 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    size = new SizeF(60 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 70 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    e.Graphics.DrawImage(Properties.Resources.DoubleFlat, new RectangleF(location, size));
                    break;
                case Accidental.Flat:
                    location = new PointF(x, y - 50 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    size = new SizeF(35 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 68 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    e.Graphics.DrawImage(Properties.Resources.Flat, new RectangleF(location, size));
                    break;
                case Accidental.Natural:
                    location = new PointF(x, y - 38 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    size = new SizeF(20 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 70 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    e.Graphics.DrawImage(Properties.Resources.Natural, new RectangleF(location, size));
                    break;
                case Accidental.Sharp:
                    location = new PointF(x, y - 38 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    size = new SizeF(35 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 70 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    e.Graphics.DrawImage(Properties.Resources.Sharp, new RectangleF(location, size));
                    break;
                case Accidental.DoubleSharp:
                    location = new PointF(x, y - 20 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    size = new SizeF(35 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 35 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH);
                    e.Graphics.DrawImage(Properties.Resources.DoubleSharp, new RectangleF(location, size));
                    break;
            }
        }
        public void Draw(PaintEventArgs e)
        {
            mYPosition = instrumentNumber * Song.INSTRUMENT_SPACING + staffNumber * (HEIGHT + Song.STAFF_SPACING);

            PointF location = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN + mYPosition);
            SizeF size = new SizeF(LENGTH, HEIGHT);

            e.Graphics.DrawImage(Properties.Resources.Staff, new RectangleF(location, size));

            switch (mClef)
            {
                case Clef.Treble:
                    location.X = Song.LEFT_MARGIN - 49 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
                    location.Y = Song.TOP_MARGIN - 70 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH + mYPosition;
                    size.Width = HEIGHT * 1.50f;
                    size.Height = HEIGHT * 2.28f;

                    e.Graphics.DrawImage(Properties.Resources.TrebleClef, new RectangleF(location, size));

                    DrawAccidental(400 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 420 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, Accidental.DoubleFlat, e);
                    DrawAccidental(500 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 420 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, Accidental.Flat, e);
                    DrawAccidental(600 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 420 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, Accidental.Natural, e);
                    DrawAccidental(700 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 420 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, Accidental.Sharp, e);
                    DrawAccidental(800 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, 420 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH, Accidental.DoubleSharp, e);

                    break;
                case Clef.Alto:

                    break;
                case Clef.Bass:
                    location.X = Song.LEFT_MARGIN - 35 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
                    location.Y = Song.TOP_MARGIN - 58 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH + mYPosition;
                    size.Width = HEIGHT * 1.48f;
                    size.Height = HEIGHT * 1.82f;

                    e.Graphics.DrawImage(Properties.Resources.BassClef, new RectangleF(location, size));
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

            // Draw Key Signature Algorythm
            {
                /*
                if (key < 0)
                {
                    drawAccidental(12, (staffNumber) * height * icount + height * inum + 7 + (int)clef, Accidental.Flat);                              //B
                    cursor += 2;
                    if (key < (Key)(-1))
                    {
                        drawAccidental(14, (staffNumber) * height * icount + height * inum + 4 + (int)clef, Accidental.Flat);                          //E
                        cursor += 2;
                        if (key < (Key)(-2))
                        {
                            drawAccidental(16, (staffNumber) * height * icount + height * inum + 8 + (int)clef, Accidental.Flat);                      //A
                            cursor += 2;
                            if (key < (Key)(-3))
                            {
                                drawAccidental(18, (staffNumber) * height * icount + height * inum + 5 + (int)clef, Accidental.Flat);                  //D
                                cursor += 2;
                                if (key < (Key)(-4))
                                {
                                    drawAccidental(20, (staffNumber) * height * icount + height * inum + 9 + (int)clef, Accidental.Flat);              //G
                                    cursor += 2;
                                    if (key < (Key)(-5))
                                    {
                                        drawAccidental(22, (staffNumber) * height * icount + height * inum + 6 + (int)clef, Accidental.Flat);          //C
                                        cursor += 2;
                                        if (key < (Key)(-6))
                                        {
                                            drawAccidental(24, (staffNumber) * height * icount + height * inum + 10 + (int)clef, Accidental.Flat);     //F
                                            cursor += 2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (key > 0)
                {
                    if (clef != Clef.Tenor) //Tenor Clef (Sharps)
                    {
                        drawAccidental(12, (staffNumber) * height * icount + height * inum + 3 + (int)clef, Accidental.Sharp);                             //F
                        cursor += 2;
                        if (key > (Key)1)
                        {
                            drawAccidental(14, (staffNumber) * height * icount + height * inum + 6 + (int)clef, Accidental.Sharp);                         //C
                            cursor += 2;
                            if (key > (Key)2)
                            {
                                drawAccidental(16, (staffNumber) * height * icount + height * inum + 2 + (int)clef, Accidental.Sharp);                     //G
                                cursor += 2;
                                if (key > (Key)3)
                                {
                                    drawAccidental(18, (staffNumber) * height * icount + height * inum + 5 + (int)clef, Accidental.Sharp);                 //D
                                    cursor += 2;
                                    if (key > (Key)4)
                                    {
                                        drawAccidental(20, (staffNumber) * height * icount + height * inum + 8 + (int)clef, Accidental.Sharp);             //A
                                        cursor += 2;
                                        if (key > (Key)5)
                                        {
                                            drawAccidental(22, (staffNumber) * height * icount + height * inum + 4 + (int)clef, Accidental.Sharp);         //E
                                            cursor += 2;
                                            if (key > (Key)6)
                                            {
                                                drawAccidental(24, (staffNumber) * height * icount + height * inum + 7 + (int)clef, Accidental.Sharp);     //B
                                                cursor += 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else //Not Tenor Clef (Sharps)
                    {
                        drawAccidental(24, (staffNumber) * height * icount + height * inum + 7 + (int)clef, Accidental.Sharp);                             //F
                        cursor += 2;
                        if (key > (Key)1)
                        {
                            drawAccidental(12, (staffNumber) * height * icount + height * inum + 10 + (int)clef, Accidental.Sharp);                        //A
                            cursor += 2;
                            if (key > (Key)2)
                            {
                                drawAccidental(14, (staffNumber) * height * icount + height * inum + 6 + (int)clef, Accidental.Sharp);                     //C
                                cursor += 2;
                                if (key > (Key)3)
                                {
                                    drawAccidental(16, (staffNumber) * height * icount + height * inum + 9 + (int)clef, Accidental.Sharp);                 //E
                                    cursor += 2;
                                    if (key > (Key)4)
                                    {
                                        drawAccidental(18, (staffNumber) * height * icount + height * inum + 5 + (int)clef, Accidental.Sharp);             //G
                                        cursor += 2;
                                        if (key > (Key)5)
                                        {
                                            drawAccidental(20, (staffNumber) * height * icount + height * inum + 8 + (int)clef, Accidental.Sharp);         //C
                                            cursor += 2;
                                            if (key > (Key)6)
                                            {
                                                drawAccidental(22, (staffNumber) * height * icount + height * inum + 4 + (int)clef, Accidental.Sharp);     //F
                                                cursor += 2;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }*/
            }

            switch (Song.TIME)
            {
                case Time.NineEight:

                    break;
                case Time.SixEight:
                    location.X = Song.LEFT_MARGIN + 120 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
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
                    location.X = Song.LEFT_MARGIN + 120 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
                    location.Y = Song.TOP_MARGIN + mYPosition;
                    size.Width = HEIGHT * 0.5f;
                    size.Height = HEIGHT;

                    e.Graphics.DrawImage(Properties.Resources.FourFour, new RectangleF(location, size));
                    break;
            }
        }
    }
}

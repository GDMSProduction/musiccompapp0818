using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Staff
    {
        List<Measure> measures;

        public static int LINE_SPACING;
        public static int LENGTH;
        public static int HEIGHT;

        public int mYPosition;
        public int mCursorX;

        Clef mClef;

        public Staff(Clef c, int ypos)
        {
            LINE_SPACING = 30 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            HEIGHT = 4 * LINE_SPACING * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            mClef = c;
            mYPosition = ypos;
            mCursorX = Song.LEFT_MARGIN;
        }
        public Measure GetMeasure(int i)
        {
            return measures[i];
        }
        public void AddMeasure()
        {
            measures.Add(new Measure(mCursorX));
        }
        public void Draw(PaintEventArgs e)
        {
            PointF p = new PointF(Song.LEFT_MARGIN, Song.TOP_MARGIN);
            SizeF s = new SizeF(LENGTH, HEIGHT);
            RectangleF imageRect = new RectangleF(p, s);

            e.Graphics.DrawImage(Properties.Resources.Staff, imageRect);

            p.X = Song.LEFT_MARGIN - 50;
            p.Y = Song.TOP_MARGIN - 50;
            s.Width = HEIGHT * 1.5f;
            s.Height = HEIGHT * 2;

            imageRect = new RectangleF(p, s);

            switch (mClef)
            {
                case Clef.Treble:
                    e.Graphics.DrawImage(Properties.Resources.TrebleClef, imageRect);
                    break;
                case Clef.Alto:

                    break;
                case Clef.Bass:
                    e.Graphics.DrawImage(Properties.Resources.BassClef, imageRect);
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
                            drawAccidental(12, (staffNumber) * height * icount + height * inum + 10 + (int)clef, Accidental.Sharp);                         //A
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
                                                drawAccidental(22, (staffNumber) * height * icount + height * inum + 4 + (int)clef, Accidental.Sharp);         //F
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

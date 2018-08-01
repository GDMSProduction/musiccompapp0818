using System;
using System.Collections.Generic;

namespace Music_Comp
{
    class Staff
    {
        List<Measure> measures = new List<Measure>();
        int staffNumber;
        int length = (Console.LargestWindowWidth - 3);
        int height = 15;
        int cursor = 18;
        Clef clef;
        Key key;
        Time time;

        public Staff(int sn, Clef c, Key k, Time t)
        {
            staffNumber = sn;
            clef = c;
            key = k;
            time = t;
        }
        public int getLength()
        {
            return length;
        }
        public int getHeight()
        {
            return height;
        }
        public int getStaffNumber()
        {
            return staffNumber;
        }
        public Clef getClef()
        {
            return clef;
        }
        public Key getKey()
        {
            return key;
        }
        public int getCursor()
        {
            return cursor;
        }
        public Measure getMeasure(int i)
        {
            return measures[i];
        }
        public void drawStaff(int inum, int icount)
        {

        }
        public void drawClef(int inum, int icount)
        {
            switch (clef)
            {
                case Clef.Treble:

                    break;
                case Clef.Alto:

                    break;
                case Clef.Bass:

                    break;
                case Clef.Tenor:

                    break;
                default:
                    break;
            }
        }
        public void drawKeySignature(int inum, int icount)
        {
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
        }
        public void drawTimeSignature(int x, int y)
        {
            switch (time)
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
                default:
                    break;
            }
        }
        public void drawAccidental(int x, int y, Accidental a)
        {
            switch (a)
            {
                case Accidental.DoubleFlat:

                    break;
                case Accidental.Flat:

                    break;
                case Accidental.Natural:

                    break;
                case Accidental.Sharp:

                    break;
                case Accidental.DoubleSharp:

                    break;
                default:
                    break;
            }
        }
    }
}

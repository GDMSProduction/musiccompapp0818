using System.Drawing;
using System.Windows.Forms;

namespace Music_Comp
{
    public class Note
    {
        RectangleF area;

        Pitch mPitch;
        Accidental mAccidental;
        Duration mDuration;
        sbyte mOctave = 4;
        Image image;

        float mWidth;

        public Note(Pitch p, Accidental a, Duration d, sbyte o)
        {
            mPitch = p;
            mAccidental = a;
            mDuration = d;
            mOctave = o;
        }

        public Pitch GetPitch()
        {
            return mPitch;
        }

        public void SetPitch(Pitch p)
        {
            mPitch = p;
        }

        public Accidental GetAccidental()
        {
            return mAccidental;
        }

        public void SetAccidental(Accidental a)
        {
            mAccidental = a;
        }

        public Duration GetDuration()
        {
            return mDuration;
        }

        public void SetDuration(Duration d)
        {
            mDuration = d;
        }

        public sbyte GetOctave()
        {
            return mOctave;
        }

        public void SetOctave(sbyte o)
        {
            mOctave = o;
        }

        public float GetWidth()
        {
            return mWidth;
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public void Update(float cursorX, float staffYPosition, Clef clef)
        {
            float x = cursorX;
            float y = staffYPosition;
            PointF location;
            SizeF size = new SizeF();


            if (mPitch == Pitch.Rest)
            {
                switch (mDuration)
                {
                    case Duration.Quarter:
                        image = Properties.Resources.QuarterRest;
                        size = new SizeF(35 * Song._SCALE, 90 * Song._SCALE);
                        y += 312 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        break;
                    case Duration.Half:
                        image = Properties.Resources.HalfRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y += 295 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        break;
                    case Duration.Whole:
                        image = Properties.Resources.WholeRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y += 295 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        break;
                    case Duration.Eighth:
                        image = Properties.Resources.EighthRest;
                        size = new SizeF(35 * Song._SCALE, 90 * Song._SCALE);
                        y += 312 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        break;
                    case Duration.Sixteenth:
                        image = Properties.Resources.SixteenthRest;
                        size = new SizeF(35 * Song._SCALE, 90 * Song._SCALE);
                        y += 312 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        break;
                }
            }
            else
            {
                y += (260 + ((int)mPitch + (int)clef + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;

                switch (mDuration)
                {
                    case Duration.Quarter:
                        image = Properties.Resources.Note;
                        size = new SizeF(90 * Song._SCALE, 135 * Song._SCALE);
                        y -= 50 * Song._SCALE;
                        break;
                    case Duration.Half:
                        image = Properties.Resources.HalfNote;
                        size = new SizeF(120 * Song._SCALE, 135 * Song._SCALE);
                        y -= 56.5f * Song._SCALE;
                        break;
                    case Duration.Whole:
                        image = Properties.Resources.WholeNote;
                        size = new SizeF(90 * Song._SCALE, 135 * Song._SCALE);
                        y -= 50 * Song._SCALE;
                        break;
                    case Duration.Eighth:
                        image = Properties.Resources.EighthNote;
                        size = new SizeF(90 * Song._SCALE, 135 * Song._SCALE);
                        y -= 50 * Song._SCALE;
                        break;
                    case Duration.Sixteenth:
                        image = Properties.Resources.SixteenthNote;
                        size = new SizeF(90 * Song._SCALE, 135 * Song._SCALE);
                        y -= 50 * Song._SCALE;
                        break;
                }
            }

            mWidth = size.Width;
            location = new PointF(x, y);
            area = new RectangleF(location, size);
        }

        public void Draw(PaintEventArgs e)
        {
            if (e.Graphics.IsVisible(area))
                e.Graphics.DrawImage(image, area);
        }
    }
}

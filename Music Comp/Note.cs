using System.Drawing;
using System.Windows.Forms;

namespace Music_Comp
{
    public class Note
    {
        Pitch mPitch;
        Accidental mAccidental;
        Duration mDuration;
        sbyte mOctave = 4;
        Image image;

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
        public void Draw(float cursorX, float staffYPosition, Clef c, PaintEventArgs e)
        {
            float x = cursorX;
            float y = staffYPosition;
            PointF location;
            SizeF size = new SizeF();

            y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;

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

            location = new PointF(x, y);

            e.Graphics.DrawImage(image, new RectangleF(location, size));
        }
    }
}

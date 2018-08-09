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

            switch (mPitch)
            {
                case Pitch.C:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.D:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.E:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.F:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.G:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.A:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.B:
                    y = staffYPosition + (260 + ((int)mPitch + (int)c + (mOctave - 4) * 8) * 14.5f) * Song._SCALE;
                    break;
                case Pitch.Rest:
                    break;
            }
            switch (mDuration)
            {
                case Duration.Quarter:
                    image = Properties.Resources.Note;
                    break;
                case Duration.Half:
                    image = Properties.Resources.HalfNote;
                    break;
                case Duration.Whole:
                    image = Properties.Resources.WholeNote;
                    break;
                case Duration.Eighth:
                    image = Properties.Resources.EighthNote;
                    break;
                case Duration.Sixteenth:
                    image = Properties.Resources.SixteenthNote;
                    break;
            }

            y -= 50 * Song._SCALE;

            PointF location = new PointF(x, y);
            SizeF size = new SizeF(60 * Song._SCALE, 70 * Song._SCALE);

            e.Graphics.DrawImage(image, new RectangleF(location, size));
        }
    }
}

using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Note
    {
        RectangleF noteArea;
        RectangleF area;
        RectangleF dotArea;
        Image image;

        Pitch mPitch;
        Accidental mAccidental;
        Duration mDuration;
        sbyte mOctave = 4;
        WaveForm mWaveForm = WaveForm.Sine;

        public Note(Pitch p, Accidental a, Duration d, sbyte o, Image i = null, RectangleF ar = new RectangleF())
        {
            mPitch = p;
            mAccidental = a;
            mDuration = d;
            mOctave = o;
            image = i;
            noteArea = ar;
        }

        public Note Clone()
        {
            return new Note(mPitch, mAccidental, mDuration, mOctave, image, noteArea);
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

        public void SetDuration(Duration w)
        {
            mDuration = w;
        }

        public WaveForm GetWaveForm()
        {
            return mWaveForm;
        }

        public void SetWaveForm(WaveForm w)
        {
            mWaveForm = w;
        }

        public sbyte GetOctave()
        {
            return mOctave;
        }

        public void SetOctave(sbyte o)
        {
            mOctave = o;
        }

        private bool IsDotted()
        {
            return (int)mDuration % 9 == 0;
        }

        public float GetWidth()
        {
            return area.Width;
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
                    case Duration.DottedQuarter:
                        image = Properties.Resources.QuarterRest;
                        size = new SizeF(35 * Song._SCALE, 90 * Song._SCALE);
                        y += 312 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 50 * Song._SCALE, y + size.Height - 40 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Half:
                    case Duration.DottedHalf:
                        image = Properties.Resources.HalfRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y += 296 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 90 * Song._SCALE, y + size.Height - 120 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Whole:
                        image = Properties.Resources.WholeRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y += 277 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        break;
                    case Duration.Eighth:
                    case Duration.DottedEighth:
                        image = Properties.Resources.EighthRest;
                        size = new SizeF(35 * Song._SCALE, 62 * Song._SCALE);
                        y += 333 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 45 * Song._SCALE, y + size.Height - 30 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Sixteenth:
                        image = Properties.Resources.SixteenthRest;
                        size = new SizeF(45 * Song._SCALE, 90 * Song._SCALE);
                        y += 333 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        break;
                }
            }
            else
            {
                y += (260 + ((int)mPitch + (int)clef + (mOctave - 4) * 8) * 14.7f) * Song._SCALE;

                switch (mDuration)
                {
                    case Duration.Quarter:
                    case Duration.DottedQuarter:
                        image = Properties.Resources.Note;
                        size = new SizeF(90 * Song._SCALE, 135 * Song._SCALE);
                        y -= 50 * Song._SCALE;
                        if ( IsDotted())
                            dotArea = new RectangleF(x + 80 * Song._SCALE, y + size.Height - 40 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Half:
                    case Duration.DottedHalf:
                        image = Properties.Resources.HalfNote;
                        size = new SizeF(120 * Song._SCALE, 135 * Song._SCALE);
                        y -= 56.5f * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 90 * Song._SCALE, y + size.Height - 35 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Whole:
                        image = Properties.Resources.WholeNote;
                        size = new SizeF(65 * Song._SCALE, 29 * Song._SCALE);
                        y += 42 * Song._SCALE;
                        x += 10 * Song._SCALE;
                        break;
                    case Duration.Eighth:
                    case Duration.DottedEighth:
                        image = Properties.Resources.EighthNote;
                        size = new SizeF(72 * Song._SCALE, 117.5f * Song._SCALE);
                        y -= 47 * Song._SCALE;
                        x += 10 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 60 * Song._SCALE, y + size.Height - 30 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Sixteenth:
                        image = Properties.Resources.SixteenthNote;
                        size = new SizeF(120 * Song._SCALE, 120 * Song._SCALE);
                        y -= 45 * Song._SCALE;
                        break;
                }
            }

            location = new PointF(x, y);
            noteArea = new RectangleF(location, size);
            if (IsDotted())
                area = new RectangleF(noteArea.X, noteArea.Y, dotArea.X - noteArea.X, dotArea.Y - noteArea.Y);
            else
                area = noteArea;
        }

        public void Draw(PaintEventArgs e)
        {
            if (e.Graphics.IsVisible(noteArea))
                e.Graphics.DrawImage(image, noteArea);
            if (IsDotted())
                e.Graphics.FillEllipse(new SolidBrush(Color.Black), dotArea);
        }

        public void Play()
        {

        }
    }
}


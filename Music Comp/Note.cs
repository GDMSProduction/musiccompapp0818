using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    public class Note : SongComponent
    {
        RectangleF noteArea;
        RectangleF dotArea;
        Image image;
        ImageAttributes attr;

        int mNoteNumber;

        Pitch mPitch;
        Accidental mAccidental;
        Duration mDuration;
        sbyte mOctave;
        WaveForm mWaveForm;

        public Note(Pitch p, Accidental a, Duration d, sbyte o = 4, WaveForm w = WaveForm.Sine, int noteNumber = 0, Image i = null, RectangleF ar = new RectangleF())
        {
            mNoteNumber = noteNumber;
            mPitch = p;
            mAccidental = a;
            mDuration = d;
            mOctave = o;
            mWaveForm = w;
            image = i;
            noteArea = ar;
            Song.SELECTABLES.Add(this);
            Song.TOTAL_NOTES++;

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 0, 0, 0, 1}
            });

            attr = new ImageAttributes();
            attr.SetColorMatrix(colorMatrix);
        }

        public Note Clone()
        {
            return new Note(mPitch, mAccidental, mDuration, mOctave, mWaveForm, 0, image, noteArea);
        }

        private PointF[] GetPoints(RectangleF rectangle)
        {
            return new PointF[3]
            {
                new PointF(rectangle.Left, rectangle.Top),
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Left, rectangle.Bottom)
            };
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

        public Duration Duration
        {
            get { return mDuration; }
            set { mDuration = value; }
        }

        public WaveForm Waveform
        {
            get { return mWaveForm; }
            set { mWaveForm = value; }
        }

        public sbyte Octave
        {
            get { return mOctave; }
            set { mOctave = value; }
        }

        private bool IsDotted()
        {
            return (int)mDuration % 9 == 0;
        }

        public float Width
        {
            get { return area.Width; }
        }

        public void Update(float cursorX, float staffYPosition, Clef clef)
        {
            float x = cursorX;
            float y = Song.TOP_MARGIN + staffYPosition;
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
                        y += 12 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 50 * Song._SCALE, y + size.Height - 40 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Half:
                    case Duration.DottedHalf:
                        image = Properties.Resources.HalfRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y -= 4 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 90 * Song._SCALE, y + size.Height - 120 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Whole:
                        image = Properties.Resources.WholeRest;
                        size = new SizeF(120 * Song._SCALE, 170 * Song._SCALE);
                        y -= 23 * Song._SCALE;
                        x -= 15 * Song._SCALE;
                        break;
                    case Duration.Eighth:
                    case Duration.DottedEighth:
                        image = Properties.Resources.EighthRest;
                        size = new SizeF(35 * Song._SCALE, 62 * Song._SCALE);
                        y += 33 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        if (IsDotted())
                            dotArea = new RectangleF(x + 45 * Song._SCALE, y + size.Height - 30 * Song._SCALE, 10, 10);
                        break;
                    case Duration.Sixteenth:
                        image = Properties.Resources.SixteenthRest;
                        size = new SizeF(45 * Song._SCALE, 90 * Song._SCALE);
                        y += 33 * Song._SCALE;
                        x += 33 * Song._SCALE;
                        break;
                }
            }
            else
            {
                y += (4.1f + ((int)mPitch + (int)clef - (mOctave - 4) * 8) * 14.7f) * Song._SCALE;

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
            area = noteArea;
            if (IsDotted())
                area.Width = dotArea.Right - noteArea.Left;
        }

        public void Draw(Graphics g)
        {

            if (g.IsVisible(noteArea))
                if (isSelected)
                {
                    g.DrawImage(image, GetPoints(noteArea), new RectangleF(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, attr);
                    if (IsDotted())
                        g.FillEllipse(new SolidBrush(Color.Red), dotArea);
                }
                else
                {
                    g.DrawImage(image, noteArea);
                    if (IsDotted())
                        g.FillEllipse(new SolidBrush(Color.Black), dotArea);
                }
        }

        public void Play()
        {
            if (mPitch != Pitch.Rest)
            {
                var mStrm = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(mStrm);

                int msDuration = (int)Duration * 60;
                int samplesPerSecond = 44100;
                short bitsPerSample = 16;
                short tracks = 1;
                short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
                int bytesPerSecond = samplesPerSecond * frameSize;
                int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
                {
                    int formatChunkSize = 16;
                    int headerSize = 8;
                    short formatType = 1;
                    int waveSize = 4;
                    int dataChunkSize = samples * frameSize;
                    int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
                    // var encoding = new System.Text.UTF8Encoding();
                    writer.Write(0x46464952); // = encoding.GetBytes("RIFF")
                    writer.Write(fileSize);
                    writer.Write(0x45564157); // = encoding.GetBytes("WAVE")
                    writer.Write(0x20746D66); // = encoding.GetBytes("fmt ")
                    writer.Write(formatChunkSize);
                    writer.Write(formatType);
                    writer.Write(tracks);
                    writer.Write(samplesPerSecond);
                    writer.Write(bytesPerSecond);
                    writer.Write(frameSize);
                    writer.Write(bitsPerSample);
                    writer.Write(0x61746164); // = encoding.GetBytes("data")
                    writer.Write(dataChunkSize);
                }

                double theta;
                ushort frequency;
                int samplesPerWavelength;
                short ampStep;

                ushort volume = 16383;
                if (this.Waveform == WaveForm.Square)
                    volume /= 2;

                const double TAU = 2 * Math.PI;
                double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

                double amp = volume / 2;

                double step = (int)Pitch.A;
                if (mPitch < Pitch.E)
                    step += 0.5;
                step += (mOctave - 4) * 6;
                double exp = -2 * ((double)mPitch - step);

                frequency = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                samplesPerWavelength = bytesPerSecond / frequency;
                ampStep = (short)(amp * 2 / samplesPerWavelength);

                theta = frequency * TAU / samplesPerSecond;

                short tempSample = (short)-amp;
                for (int i = 0; i < samples; i++)
                {
                    short s = 0;

                    switch (this.Waveform)
                    {
                        case WaveForm.Sine:
                                s += (short)(amp * Math.Sin(theta * i));
                            break;
                        case WaveForm.Square:
                                s += (short)(amp * Math.Sign(Math.Sin(theta * i)));
                            break;
                        case WaveForm.Sawtooth:
                                tempSample += ampStep;
                                s += (short)(tempSample / ampStep);
                            break;
                        case WaveForm.Triangle:
                                if (Math.Abs(tempSample) > amp)
                                    ampStep = (short)-ampStep;
                                tempSample += ampStep;
                                s += (short)(tempSample / ampStep);
                            break;
                        case WaveForm.Noise:
                            s += (short)((new Random().NextDouble() * 2 - 1) * amp);
                            break;
                    }
                    writer.Write(s);
                }
                mStrm.Seek(0, SeekOrigin.Begin);
                new System.Media.SoundPlayer(mStrm).Play();
                writer.Close();
                mStrm.Close();
            }
        }
    }
}


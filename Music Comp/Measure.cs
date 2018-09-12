using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    public class Measure
    {
        RectangleF area;

        List<Chord> mChords;

        float mLength;

        int mTotalDuration;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float yPosition)
        {
            mClef = clef;
            mChords = new List<Chord>();
        }

        public int GetChordCount()
        {
            return mChords.Count;
        }

        public float GetLength()
        {
            return mLength;
        }

        public Chord GetChord(int i)
        {
            return mChords[i];
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public bool IsFull()
        {
            if (mTotalDuration >= ((int)Song.TIME > 0 ? (int)Song.TIME : -(int)Song.TIME))
                return true;
            return false;
        }

        public int GetDuration()
        {
            return mTotalDuration;
        }

        public Chord AddChord(Chord chord)
        {
            if (chord == null)
                return null;
            if (mChords.Count == 0)
            {
                mChords.Add(chord);
                mTotalDuration += (int)chord.GetDuration();
                return null;
            }
            else
            {
                mTotalDuration += (int)chord.GetDuration();
                if (mTotalDuration > (int)Song.TIME)
                {
                    Chord split = new Chord();
                    Chord remainder = new Chord();

                    int remainderDuration = mTotalDuration - (int)Song.TIME;
                    int splitDuration = (int)chord.GetDuration() - remainderDuration;

                    split = chord.Clone();
                    remainder = chord.Clone();

                    split.SetDuration((Duration)splitDuration);
                    remainder.SetDuration((Duration)remainderDuration);

                    mChords.Add(split);
                    return remainder;
                }
                else if (mTotalDuration == (int)Song.TIME)
                {
                    mChords.Add(chord);
                    return null;
                }
                else
                {
                    mChords.Add(chord);
                    return null;
                }
            }
        }

        public void Update(float barline, float yPosition)
        {
            float cursor = 0;
            foreach (Chord chord in mChords)
            {
                chord.Update(barline + cursor, yPosition, mClef);
                cursor += chord.GetWidth();
            }
            mLength = cursor;

            area.X = barline;
            area.Y = yPosition + Song.STAFF_SPACING;
            area.Width = mLength;
            area.Height = Staff.HEIGHT + Song.STAFF_SPACING * 2;
        }
        public void Draw(PaintEventArgs e)
        {
            foreach (Chord chord in mChords)
                chord.Draw(e);
        }

        public void Play()
        {
            var mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);

            int msDuration = GetDuration() * 60;
            int formatChunkSize = 16;
            int headerSize = 8;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int samples = (int)((decimal)samplesPerSecond * msDuration / 1000);
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            {
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

            foreach (Chord chord in mChords)
            {
                if (chord.GetNote(0).GetPitch() != Pitch.Rest)
                {
                    double[] angles = new double[chord.GetNoteCount()];
                    ushort[] frequency = new ushort[chord.GetNoteCount()];
                    int[] samplesPerWavelength = new int[chord.GetNoteCount()];
                    short[] ampSteps = new short[chord.GetNoteCount()];
                    int chordSamples = (int)((decimal)samplesPerSecond * (int)chord.GetDuration() * 60 / 1000);

                    ushort volume = 16383;
                    if (chord.GetWaveForm() == WaveForm.Square)
                        volume /= 2;

                    const double TAU = 2 * Math.PI;
                    double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

                    double amp = volume / 2;

                    for (int i = 0; i < chord.GetNoteCount(); i++)
                    {
                        double step = (int)Pitch.A;
                        if (chord.GetNote(i).GetPitch() <= Pitch.F && chord.GetNote(i).GetPitch() >= Pitch.B)
                            step += 0.5;
                        double exp = -2 * ((double)chord.GetNote(i).GetPitch() - step);

                        frequency[i] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                        samplesPerWavelength[i] = bytesPerSecond / frequency[i];
                        ampSteps[i] = (short)(amp * 2 / samplesPerWavelength[i]);
                    }

                    for (int i = 0; i < angles.Length; i++)
                        angles[i] = frequency[i] * TAU / samplesPerSecond;

                    short tempSample = (short)-amp;
                    for (int i = 0; i < chordSamples; i++)
                    {
                        short s = 0;

                        switch (chord.GetWaveForm())
                        {
                            case WaveForm.Sine:
                                foreach (double theta in angles)
                                    s += (short)(amp * Math.Sin(theta * i));
                                break;
                            case WaveForm.Square:
                                foreach (double theta in angles)
                                    s += (short)(amp * Math.Sign(Math.Sin(theta * i)));
                                break;
                            case WaveForm.Sawtooth:
                                foreach (short ampStep in ampSteps)
                                {
                                    tempSample += ampStep;
                                    s += (short)(tempSample / ampSteps.Length);
                                }
                                break;
                            case WaveForm.Triangle:
                                for (int j = 0; j < ampSteps.Length; j++)
                                {
                                    if (Math.Abs(tempSample) > amp)
                                        ampSteps[j] = (short)-ampSteps[j];

                                    tempSample += ampSteps[j];
                                    s += (short)(tempSample / ampSteps.Length);
                                }
                                break;
                            case WaveForm.Noise:
                                s += (short)((new Random().NextDouble() * 2 - 1) * amp);
                                break;
                        }
                        writer.Write(s);
                    }
                }
            }

            mStrm.Seek(0, SeekOrigin.Begin);
            new System.Media.SoundPlayer(mStrm).Play();
            writer.Close();
            mStrm.Close();
        }

        public void Remove(Chord c)
        {
            mTotalDuration -= (int)mChords[mChords.Count - 1].GetDuration();
            mChords.Remove(c);
        }

        public bool IsEmpty()
        {
            if (mChords.Count == 0 || mTotalDuration == 0)
                return true;
            return false;
        }
    }
}

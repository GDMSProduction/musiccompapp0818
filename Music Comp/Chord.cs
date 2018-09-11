﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    public class Chord
    {
        RectangleF area;
        List<Note> mNotes;

        public Chord()
        {
            area = new RectangleF();
            mNotes = new List<Note>();
        }

        public Chord(List<Note> notes)
        {
            mNotes = notes;
        }

        public void Add(Note n)
        {
            mNotes.Add(n);
            RectangleF noteArea = n.GetArea();
            if (area == null)
                area = noteArea;
            else
            {
                area.X = Math.Min(noteArea.X, area.X);
                area.Y = Math.Min(noteArea.Y, area.Y);
                area.Width = Math.Max(noteArea.X + noteArea.Width, area.X + area.Width) - area.X;
                area.Height = Math.Max(noteArea.Y + noteArea.Height, area.Y + area.Height) - area.Y;
            }
        }

        public void Remove(Note n)
        {
            mNotes.Remove(n);
        }

        public Chord Clone()
        {
            List<Note> notes = new List<Note>();
            foreach (Note note in mNotes)
                notes.Add(note.Clone());
            return new Chord(notes);
        }

        public Note GetNote(int i)
        {
            return mNotes[i];
        }

        public int GetNoteCount()
        {
            return mNotes.Count;
        }

        public Duration GetDuration()
        {
            return mNotes[0].GetDuration();
        }

        public void SetDuration(Duration d)
        {
            foreach (Note note in mNotes)
                note.SetDuration(d);
        }

        public WaveForm GetWaveForm()
        {
            return mNotes[0].GetWaveForm();
        }

        public void SetWaveForm(WaveForm w)
        {
            foreach (Note note in mNotes)
                note.SetWaveForm(w);
        }

        public float GetWidth()
        {
            return mNotes[0].GetWidth();
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public void Update(float cursorX, float yPosition, Clef clef)
        {
            foreach (Note note in mNotes)
            {
                note.Update(cursorX, yPosition, clef);
                RectangleF noteArea = note.GetArea();
                area.X = Math.Min(noteArea.X, area.X);
                area.Y = Math.Min(noteArea.Y, area.Y);
                area.Width = Math.Max(noteArea.X + noteArea.Width, area.X + area.Width) - area.X;
                area.Height = Math.Max(noteArea.Y + noteArea.Height, area.Y + area.Height) - area.Y;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            foreach (Note note in mNotes)
                note.Draw(e);
        }

        public void Play()
        {
            if (mNotes[0].GetPitch() != Pitch.Rest)
            {
                double[] angles = new double[mNotes.Count];
                ushort[] frequency = new ushort[mNotes.Count];
                int[] samplesPerWavelength = new int[mNotes.Count];
                short[] ampSteps = new short[mNotes.Count];

                int msDuration = (int)GetDuration() * 60;
                ushort volume = 16383;
                if (GetWaveForm() == WaveForm.Square)
                    volume /= 2;

                var mStrm = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(mStrm);

                const double TAU = 2 * Math.PI;
                double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));

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

                double amp = volume / 2;

                for (int i = 0; i < mNotes.Count; i++)
                {
                    double step = (int)Pitch.A;
                    if (mNotes[i].GetPitch() <= Pitch.F && mNotes[i].GetPitch() >= Pitch.B)
                        step += 0.5;
                    double exp = -2 * ((double)mNotes[i].GetPitch() - step);

                    frequency[i] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                    samplesPerWavelength[i] = bytesPerSecond / frequency[i];
                    ampSteps[i] = (short)(amp * 2 / samplesPerWavelength[i]);
                }

                for (int i = 0; i < angles.Length; i++)
                    angles[i] = frequency[i] * TAU / samplesPerSecond;

                short tempSample = (short)-amp;
                for (int i = 0; i < samples; i++)
                {
                    short s = 0;

                    switch (GetWaveForm())
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

                mStrm.Seek(0, SeekOrigin.Begin);
                new System.Media.SoundPlayer(mStrm).Play();
                writer.Close();
                mStrm.Close();
            }
        }
    }
}

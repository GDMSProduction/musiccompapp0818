using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace Music_Comp
{
    public class Chord : SongComponent
    {
        List<Note> mNotes;
        Note mSelectedNote;
        WaveForm mWaveForm;
        int mChordNumber;

        public Chord(WaveForm wave = WaveForm.Sine, int chordNumber = 0, List<Note> notes = null)
        {
            mChordNumber = chordNumber;
            mWaveForm = wave;
            mNotes = notes == null ? new List<Note>() : notes;
            if (Song.SELECTABLES != null)
                Song.SELECTABLES.Add(this);
            Song.TOTAL_CHORDS++;
        }

        public Chord Clone()
        {
            List<Note> notes = new List<Note>();
            foreach (Note note in mNotes)
                notes.Add(note.Clone());
            return new Chord(mWaveForm, mChordNumber, notes);
        }

        public Note GetNote(int i)
        {
            return mNotes[i];
        }

        public Note GetSelection()
        {
            return mSelectedNote;
        }

        public int GetNoteCount()
        {
            return mNotes.Count;
        }

        public Duration GetDuration()
        {
            return mNotes[0].Duration;
        }

        public WaveForm GetWaveForm()
        {
            return mNotes[0].Waveform;
        }

        public float GetWidth()
        {
            return mNotes[0].Width;
        }

        public void Add(Note n)
        {
            mNotes.Add(n);
            if (GetNoteCount() == 1)
                mSelectedNote = GetNote(0);
        }

        public void Remove(Note n)
        {
            mNotes.Remove(n);
        }

        public void SetSelection(Note n)
        {
            mSelectedNote.Deselect();
            mSelectedNote = n;
            mSelectedNote.Select();
        }

        public void SetDuration(Duration d)
        {
            foreach (Note note in mNotes)
                note.Duration = d;
        }

        public void SetWaveForm(WaveForm w)
        {
            mWaveForm = w;
            foreach (Note note in mNotes)
                note.Waveform = w;
        }

        public void Update(float cursorX, float yPosition, Clef clef)
        {
            area = new RectangleF(float.MaxValue, float.MaxValue, 0, 0);
            float right = 0;
            float bottom = 0;
            foreach (Note note in mNotes)
            {
                note.Update(cursorX, yPosition, clef);
                RectangleF noteArea = note.GetArea();
                area.X = Math.Min(noteArea.X, area.X);
                area.Y = Math.Min(noteArea.Y, area.Y);
                right = Math.Max(noteArea.Right, right);
                bottom = Math.Max(noteArea.Bottom, bottom);
            }
            area.Width = right - area.Left;
            area.Height = bottom - area.Top;
        }

        public void Draw(PaintEventArgs e)
        {
            if (isSelected) ;
            foreach (Note note in mNotes)
                note.Draw(e);
        }

        public void Play()
        {
            MemoryStream mStrm = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(mStrm);

            int msDuration = (int)GetDuration() * Song.BPM;
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

            if (GetNote(0).GetPitch() != Pitch.Rest)
            {
                double[] angles = new double[GetNoteCount()];
                ushort[] frequency = new ushort[GetNoteCount()];
                int[] samplesPerWavelength = new int[GetNoteCount()];
                short[] ampSteps = new short[GetNoteCount()];

                ushort volume = 16383;
                if (GetWaveForm() == WaveForm.Square)
                    volume /= 2;

                const double TAU = 2 * Math.PI;
                double NOTE_CONSTANT = Math.Pow(2, (1.0 / 12.0));
                
                double amp = volume / 2;

                for (int i = 0; i < GetNoteCount(); i++)
                {
                    double step = (int)Pitch.A;
                    if (GetNote(i).GetPitch() < Pitch.E)
                        step -= 0.5;
                    step += (GetNote(i).Octave - 4) * 6;
                    double exp = -2 * ((double)GetNote(i).GetPitch() - step);

                    frequency[i] = (ushort)(440 * Math.Pow(NOTE_CONSTANT, exp));
                    samplesPerWavelength[i] = bytesPerSecond / frequency[i];
                    ampSteps[i] = (short)(amp * 2 / samplesPerWavelength[i]);
                }

                for (int i = 0; i < angles.Length; i++)
                    angles[i] = frequency[i] * TAU / samplesPerSecond;

                for (int s = 0; s < samples; s++)
                {
                    short sample = 0;

                    switch (GetWaveForm())
                    {
                        case WaveForm.Sine:
                            for (int n = 0; n < GetNoteCount(); n++)
                                sample += (short)(amp * Math.Sin(angles[n] * s));
                            break;
                        case WaveForm.Square:
                            for (int n = 0; n < GetNoteCount(); n++)
                                sample += (short)(amp * Math.Sign(Math.Sin(angles[n] * s)));
                            break;
                        case WaveForm.Sawtooth:
                            for (int n = 0; n < GetNoteCount(); n++)
                                sample += (short)((s % (samplesPerWavelength[n])) * ampSteps[n]);
                            break;
                        case WaveForm.Triangle:
                            for (int n = 0; n < GetNoteCount(); n++)
                                if (s < samplesPerWavelength[n] / 4 || s > samplesPerWavelength[n] * 3 / 4)
                                    sample += (short)(s % samplesPerWavelength[n] * ampSteps[n]);
                                else
                                    sample += (short)((samplesPerWavelength[n] - (s % samplesPerWavelength[n])) * ampSteps[n]);
                            break;
                        case WaveForm.Noise:
                            sample += (short)new Random().Next((int)-amp, (int)amp);
                            break;
                    }
                    writer.Write(sample);
                }

                mStrm.Seek(0, SeekOrigin.Begin);
                new System.Media.SoundPlayer(mStrm).Play();
                writer.Close();
                mStrm.Close();
            }
        }
    }
}

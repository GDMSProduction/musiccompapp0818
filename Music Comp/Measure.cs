using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Measure
    {
        Rectangle area;

        List<Note[]> mNotes;

        float mXPosition;
        float mYPosition;
        float mLength;

        bool isFull;
        int mTotalDuration;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float cursorX, float yPosition)
        {
            mClef = clef;
            mXPosition = cursorX;
            mYPosition = yPosition;
            isFull = false;
            mNotes = new List<Note[]>();
        }

        public int GetNoteCount()
        {
            return mNotes.Count;
        }

        public float GetLength()
        {
            return mLength;
        }

        public Note[] GetNotes(int i)
        {
            return mNotes[i];
        }

        public Rectangle GetArea()
        {
            return area;
        }

        public bool GetFull()
        {
            return isFull;
        }

        public Note[] AddNote(Note[] notes)
        {
            if (notes == null)
                return null;
            if (mNotes.Count == 0)
            {
                mNotes.Add(notes);
                mTotalDuration += (int)notes[0].GetDuration();
                return null;
            }
            else
            {
                mTotalDuration += (int)notes[0].GetDuration();
                if (mTotalDuration > (int)Song.TIME)
                {
                    Note[] split = new Note[notes.Length];
                    Note[] remainder = new Note[notes.Length];

                    int remainderDuration = mTotalDuration - (int)Song.TIME;
                    int splitDuration = (int)notes[0].GetDuration() - remainderDuration;

                    for (int i = 0; i < notes.Length; i++)
                    {
                        split[i] = notes[i];
                        split[i].SetDuration((Duration)splitDuration);

                        remainder[i] = notes[i];
                        remainder[i].SetDuration((Duration)remainderDuration);
                    }
                    mNotes.Add(split);
                    return remainder;
                }
                else
                {
                    mNotes.Add(notes);
                    return null;
                }
            }
        }

        public void Draw(float cursorX, PaintEventArgs e)
        {
            mXPosition = cursorX;
            float cursor = 0;
            foreach (Note[] notes in mNotes)
            {
                foreach (Note note in notes)
                    note.Draw(mXPosition + cursor, mYPosition, mClef, e);
                cursor += 60 * Song._SCALE;
                mLength += (60 + notes[0].GetWidth()) * Song._SCALE;
            }
        }
    }
}

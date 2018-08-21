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

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float cursorX, float yPosition)
        {
            mClef = clef;
            mXPosition = cursorX;
            mYPosition = yPosition;
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

        public void AddNote(Note[] n)
        {
            mNotes.Add(n);
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

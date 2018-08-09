using System.Collections.Generic;
using System.Windows.Forms;

namespace Music_Comp
{
    public class Measure
    {
        List<Note> mNotes;

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
            mNotes = new List<Note>();
        }
        public int GetNoteCount()
        {
            return mNotes.Count;
        }
        public float GetLength()
        {
            return mLength;
        }
        public Note GetNote(int i)
        {
            return mNotes[i];
        }
        public void AddNote(Note n)
        {
            mNotes.Add(n);
        }
        public void Draw(PaintEventArgs e)
        {
            float cursor = 0;
            foreach (Note note in mNotes)
            {
                note.Draw(mXPosition + cursor, mYPosition, mClef, e);
                cursor += 60 * Song._SCALE;
                mLength += (60 + note.GetWidth()) * Song._SCALE;
            }
        }
    }
}

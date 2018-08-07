using System.Collections.Generic;
using System.Windows.Forms;

namespace Music_Comp
{
    class Measure
    {
        List<Note> mNotes;

        float mXPosition;
        int mLength;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float cursorX)
        {
            mClef = clef;
            mXPosition = cursorX;
            mNotes = new List<Note>();
            // Add to cursor
        }
        public int GetNoteCount()
        {
            return mNotes.Count;
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
            foreach (Note note in mNotes)
                note.Draw(Song.cursorX, Song.cursorY, mClef, e);
        }
    }
}

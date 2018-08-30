using System.Collections.Generic;
using System.Windows.Forms;

namespace Music_Comp
{
    public class Chord
    {
        List<Note> mNotes;

        public Chord()
        {
            mNotes = new List<Note>();
        }

        public Chord(List<Note> notes)
        {
            mNotes = notes;
        }

        public void Add(Note n)
        {
            mNotes.Add(n);
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

        public void Update(float cursorX, float yPosition, Clef clef)
        {
            foreach (Note note in mNotes)
                note.Update(cursorX, yPosition, clef);
        }

        public void Draw(PaintEventArgs e)
        {
            foreach (Note note in mNotes)
                note.Draw(e);
        }
    }
}

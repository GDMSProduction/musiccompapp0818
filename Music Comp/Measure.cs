using System;
using System.Collections.Generic;

namespace Music_Comp
{
    class Measure
    {
        List<Note> mNotes;

        int mXPos;
        int mLength;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(int cursorX)
        {
            mXPos = cursorX;
            mNotes = new List<Note>();
            DrawBarLine();
            //add to cursor
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
        public void Draw()
        {

        }
        public void DrawBarLine()
        {

        }
        public void DrawNote(Note n)
        {
            if (n.GetPitch() == Pitch.Rest)
            {

            }
            else
            {
                switch (mClef)//move the cursorY to C4
                {
                    case Clef.Treble:
                        break;
                    case Clef.Alto:
                        break;
                    case Clef.Bass:
                        break;
                    case Clef.Tenor:
                        break;
                }
                switch (n.GetAccidental())
                {
                    case Accidental.DoubleFlat:
                        //draw the doubbleflat at the current cursor X,Y, and the note one width later
                        break;
                    case Accidental.Flat:
                        //draw the flat at the current cursor X,Y, and the note one width later
                        break;
                    case Accidental.Natural:
                        //draw the natural at the current cursor X,Y, and the note one width later
                        break;
                    case Accidental.Sharp:
                        //draw the sharp at the current cursor X,Y, and the note one width later
                        break;
                    case Accidental.DoubleSharp:
                        //draw the doubblesharp at the current cursor X,Y, and the note one width later
                        break;
                    default:
                        //draw the note at the current cursor X,Y
                        break;
                }
            }
        }
    }
}

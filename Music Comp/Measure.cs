using System;
using System.Collections.Generic;

namespace Music_Comp
{
    class Measure
    {
        List<Note> notes = new List<Note>();
        int length = (Console.LargestWindowWidth - 3);
        int height = 15;
        int staffNumber;
        int cursor;

        public Measure(int sn, int c)
        {
            staffNumber = sn;
            cursor = c;
            drawBarLine();
        }
        public Note getNote(int i)
        {
            return notes[i];
        }
        public void drawBarLine()
        {

        }
    }
}

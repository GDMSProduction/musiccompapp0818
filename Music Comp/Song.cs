using System.Collections.Generic;

namespace Music_Comp
{
    class Song
    {
        List<Instrument> mInstruments = new List<Instrument>();

        public static readonly int TOP_MARGIN = 300;
        public static readonly int LEFT_MARGIN = 50;
        public static readonly int RIGHT_MARGIN = 20;
        public static readonly int STAFF_SPACING = 60;
        public static int cursorY = TOP_MARGIN;
        public static int cursorX = LEFT_MARGIN;

        public static string TITLE = "";
        public static int TITLE_SIZE = 50;


        public static Key KEY = Key.C;
        public static Time TIME = Time.Common;

        public Song()
        {

        }
        public Song(string title)
        {
            TITLE = title;
        }
        public Song(string title, Key k, Time t)
        {
            TITLE = title;

            KEY = k;
            TIME = t;
        }
        public int GetInstrumentCount()
        {
            return mInstruments.Count;
        }
        public Instrument GetInstrument(int i)
        {
            return mInstruments[i];
        }
        public void EditTitle(string t)
        {
            TITLE = t;
        }
        public void Transpose(Key k)
        {
            KEY = k;
        }
        public void EditTimeSignature(Time t)
        {
            TIME = t;
        }
        public void AddInstrument(Clef clef)
        {
            mInstruments = new List<Instrument>();
            mInstruments.Add(new Instrument(clef));
        }
        public void AddInstrument(Clef clef1, Clef clef2)
        {
            mInstruments.Add(new Instrument(clef1, clef2));
        }
        public void AddInstrument(Clef clef1, Clef clef2, Clef clef3)
        {
            mInstruments = new List<Instrument>();
            mInstruments.Add(new Instrument(clef1, clef2, clef3));
        }
        public void AddInstrument(Clef clef1, Clef clef2, Clef clef3, Clef clef4)
        {
            mInstruments = new List<Instrument>();
            mInstruments.Add(new Instrument(clef1, clef2, clef3, clef4));
        }
        public void Draw()
        {

            foreach (Instrument instrument in mInstruments)
            {
                instrument.Draw();
            }

            int stanzas = mInstruments[0].GetNumberOfStaves() / mInstruments[0].GetNumberOfClefs();
            for (int i = 0; i < stanzas; i++)
            {

            }
        }
    }
}

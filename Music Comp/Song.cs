using System.Collections.Generic;
using System.Windows.Forms;

namespace Music_Comp
{
    class Song
    {
        List<Instrument> mInstruments = new List<Instrument>();

        public static int SCREEN_WIDTH;
        public static int PAGE_WIDTH;
        public static int TOP_MARGIN;
        public static int LEFT_MARGIN;
        public static int RIGHT_MARGIN;
        public static int STAFF_SPACING;
        public static int cursorY = TOP_MARGIN;
        public static int cursorX = LEFT_MARGIN;


        public static Key KEY = Key.C;
        public static Time TIME = Time.Common;

        public Song(int panelWidth)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            TOP_MARGIN = 300 * PAGE_WIDTH / SCREEN_WIDTH;
            LEFT_MARGIN = 100 * PAGE_WIDTH / SCREEN_WIDTH;
            RIGHT_MARGIN = 50 * PAGE_WIDTH / SCREEN_WIDTH;
            STAFF_SPACING = 60 * PAGE_WIDTH / SCREEN_WIDTH;
        }
        public Song(string title, Key k, Time t)
        {
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
            mInstruments.Add(new Instrument(clef));
        }
        public void AddInstrument(Clef clef1, Clef clef2)
        {
            mInstruments.Add(new Instrument(clef1, clef2));
        }
        public void AddInstrument(Clef clef1, Clef clef2, Clef clef3)
        {
            mInstruments.Add(new Instrument(clef1, clef2, clef3));
        }
        public void AddInstrument(Clef clef1, Clef clef2, Clef clef3, Clef clef4)
        {
            mInstruments.Add(new Instrument(clef1, clef2, clef3, clef4));
        }
        public void Draw(PaintEventArgs e)
        {
            foreach (Instrument instrument in mInstruments)
            {
                instrument.Draw(e);
            }
        }
    }
}

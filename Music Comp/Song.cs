using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Song
    {
        List<Instrument> mInstruments = new List<Instrument>();

        public static float SCREEN_WIDTH;
        public static float PAGE_WIDTH;
        public static float TOP_MARGIN;
        public static float LEFT_MARGIN;
        public static float RIGHT_MARGIN;
        public static float STAFF_SPACING;
        public static float INSTRUMENT_SPACING;
        public static int TOTAL_INSTRUMENTS;
        public static int TOTAL_STAVES;
        public static float cursorY = TOP_MARGIN;
        public static float cursorX = LEFT_MARGIN;


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
            INSTRUMENT_SPACING = 80 * PAGE_WIDTH / SCREEN_WIDTH;
            TOTAL_STAVES = 0;
        }
        public Song(int panelWidth, Key k, Time t)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            TOP_MARGIN = 300 * PAGE_WIDTH / SCREEN_WIDTH;
            LEFT_MARGIN = 100 * PAGE_WIDTH / SCREEN_WIDTH;
            RIGHT_MARGIN = 50 * PAGE_WIDTH / SCREEN_WIDTH;
            STAFF_SPACING = 60 * PAGE_WIDTH / SCREEN_WIDTH;
            INSTRUMENT_SPACING = 100 * PAGE_WIDTH / SCREEN_WIDTH;
            TOTAL_STAVES = 0;

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
            Pen barLinePen = new Pen(Color.Black, 3.0f);
            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES - STAFF_SPACING + (mInstruments.Count - 1) * INSTRUMENT_SPACING;
            float btm_inst_line;
            float drawing_right_margin;

            e.Graphics.DrawLine(barLinePen, new PointF(LEFT_MARGIN, TOP_MARGIN), new PointF(LEFT_MARGIN, btm_song_line));

            float top_inst_line = TOP_MARGIN;

            for (int i = 0; i < mInstruments.Count; i++)
            {
                mInstruments[i].Draw(e);

                btm_inst_line = top_inst_line + (Staff.HEIGHT + STAFF_SPACING) * mInstruments[i].GetNumberOfStaves() - STAFF_SPACING;
                drawing_right_margin = PAGE_WIDTH - RIGHT_MARGIN;

                e.Graphics.DrawLine(barLinePen, new PointF(drawing_right_margin, top_inst_line), new PointF(drawing_right_margin, btm_inst_line));

                top_inst_line = btm_inst_line + STAFF_SPACING + INSTRUMENT_SPACING;
            }

            barLinePen.Dispose();
        }
    }
}

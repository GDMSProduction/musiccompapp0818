using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Song
    {
        Rectangle area;

        static Image bracketImage = Properties.Resources.Bracket;
        static Image braceImage = Properties.Resources.Brace;

        public static float SCREEN_WIDTH;
        public static float PAGE_WIDTH;
        public static float _SCALE;
        public static float TOP_MARGIN;
        public static float LEFT_MARGIN;
        public static float RIGHT_MARGIN;
        public static float STAFF_SPACING;
        public static float INSTRUMENT_SPACING;

        public static int TOTAL_INSTRUMENTS;
        public static int TOTAL_STAVES;

        public static float cursorY;
        public static float cursorX;

        public static Key KEY = Key.C;
        public static Time TIME = Time.Common;

        public static List<float> mBarlines;

        List<Instrument> mInstruments = new List<Instrument>();

        public Song(float panelWidth)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            TOP_MARGIN = 300 * _SCALE;
            LEFT_MARGIN = 100 * _SCALE;
            RIGHT_MARGIN = 50 * _SCALE;
            STAFF_SPACING = 60 * _SCALE;
            INSTRUMENT_SPACING = 80 * _SCALE;

            TOTAL_INSTRUMENTS = 0;
            TOTAL_STAVES = 0;

            cursorY = TOP_MARGIN;
            cursorX = LEFT_MARGIN;

            area = new Rectangle(0, (int)TOP_MARGIN - 5, (int)PAGE_WIDTH, (int)cursorY);

            mBarlines = new List<float>();
        }

        public Song(float panelWidth, Key k, Time t)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = panelWidth;
            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;
            TOP_MARGIN = 300 * _SCALE;
            LEFT_MARGIN = 100 * _SCALE;
            RIGHT_MARGIN = 50 * _SCALE;
            STAFF_SPACING = 60 * _SCALE;
            INSTRUMENT_SPACING = 80 * _SCALE;
            TOTAL_INSTRUMENTS = 0;
            TOTAL_STAVES = 0;
            mBarlines = new List<float>();

            area = new Rectangle(0, (int)TOP_MARGIN - 5, (int)PAGE_WIDTH, (int)cursorY);

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
        
        public Rectangle GetArea()
        {
            return area;
        }

        public void Transpose(Key k)
        {
            KEY = k;
        }

        public void EditTimeSignature(Time t)
        {
            TIME = t;
        }

        public void AddInstrument(List<Clef> clefs, Grouping g)
        {
            mInstruments.Add(new Instrument(clefs, g));
            if (TOTAL_INSTRUMENTS == 1)
                GetInstrument(0).GetStaff(0).SetActive(true);
        }

        public void RemoveInstrument(int i)
        {
            if (i < mInstruments.Count && i >= 0)
                mInstruments.Remove(mInstruments[i]);

       //   If the active staff was in the instrument

       /*\  if (mInstruments.Count > 0)
        *   {
        *       if (i != mInstruments.Count)
        *           mInstruments[i].GetStaff(0).SetActive(true);
        *       else
        *           mInstruments[i - 1].GetStaff(0).SetActive(true);
        *   }
       \*/ 
        }

        public void DrawBarLines(List<float> barlines, PaintEventArgs e)
        {
            Pen barLinePen = new Pen(Color.Black, 3.5f * _SCALE);

            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES -
                STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING;

            float btm_inst_line;
            float top_inst_line = TOP_MARGIN;

            for (int j = 0; j < mInstruments.Count; j++)
            {
                PointF start;
                PointF end;

                btm_inst_line = top_inst_line + (Staff.HEIGHT + STAFF_SPACING) * mInstruments[j].GetNumberOfStaves() - STAFF_SPACING;
                DrawGrouping(mInstruments[j].GetGrouping(), top_inst_line, btm_inst_line, e);

                for (int i = 0; i < barlines.Count; i++)
                {
                    start = new PointF(barlines[i], top_inst_line);
                    end = new PointF(barlines[i], btm_inst_line);

                    if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                        e.Graphics.DrawLine(barLinePen, start, end);
                }

                start = new PointF(PAGE_WIDTH - RIGHT_MARGIN, top_inst_line);
                end = new PointF(PAGE_WIDTH - RIGHT_MARGIN, btm_inst_line);

                if (e.Graphics.IsVisible(new RectangleF(start.X, start.Y, 1, end.Y - start.Y)))
                    e.Graphics.DrawLine(barLinePen, start, end);

                top_inst_line = btm_inst_line + STAFF_SPACING + INSTRUMENT_SPACING;
            }

            barLinePen.Dispose();
        }

        public void DrawGrouping(Grouping g, float instTop, float instBtm, PaintEventArgs e)
        {
            PointF location = new PointF();
            SizeF size = new SizeF();
            RectangleF rect;

            switch (g)
            {
                case Grouping.Bracket:
                    location.X = LEFT_MARGIN - 35 * _SCALE;
                    location.Y = instTop - 15 * _SCALE;
                    size.Width = 40 * _SCALE;
                    size.Height = instBtm - instTop + 30 * _SCALE;

                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(bracketImage, rect);
                    break;
                case Grouping.Brace:
                    location.X = LEFT_MARGIN - 50 * _SCALE;
                    location.Y = instTop;
                    size.Width = 50 * _SCALE;
                    size.Height = instBtm - instTop;

                    rect = new RectangleF(location, size);

                    if (e.Graphics.IsVisible(rect))
                        e.Graphics.DrawImage(braceImage , rect);
                    break;
            }
        }

        public void Draw(PaintEventArgs e)
        {
            Pen barLinePen = new Pen(Color.Black, 3.4f * _SCALE);

            float btm_song_line = TOP_MARGIN + (Staff.HEIGHT + STAFF_SPACING) * TOTAL_STAVES - STAFF_SPACING + (TOTAL_INSTRUMENTS - 1) * INSTRUMENT_SPACING;

            PointF start = new PointF(LEFT_MARGIN, TOP_MARGIN);
            PointF end = new PointF(LEFT_MARGIN, btm_song_line);

            if (e.Graphics.IsVisible(new RectangleF(start.X,start.Y, 1, end.Y - start.Y)))
                e.Graphics.DrawLine(barLinePen, start, end);

            barLinePen.Dispose();

            float top_inst_line = TOP_MARGIN;

            foreach (Instrument instrument in mInstruments)
                instrument.Draw(e);

            DrawBarLines(mBarlines, e);
        }
    }
}

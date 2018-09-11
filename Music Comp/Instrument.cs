﻿using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    class Instrument
    {
        RectangleF area;

        Staff[] mStaves;
        Clef[] mClefs;
        Grouping mGrouping;

        public Instrument(List<Clef> clefs, Grouping g)
        {
            area = new RectangleF();
            if (clefs.Count > 4)
                mClefs = new Clef[4];
            else
                mClefs = new Clef[clefs.Count];
            for (int i = 0; i < mClefs.Length; i++)
            {
                mClefs[i] = clefs[i];
            }
            AddStaves(mClefs.Length);
            mGrouping = g;
        }

        public int GetNumberOfStaves()
        {
            return mStaves.Length;
        }

        public Staff GetStaff(int i)
        {
            return mStaves[i];
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public int GetNumberOfClefs()
        {
            return mClefs.Length;
        }

        public Clef GetClef(int i)
        {
            return mClefs[i];
        }

        public Grouping GetGrouping()
        {
            return mGrouping;
        }

        public void AddStaves(int numberOfStaves)
        {
            mStaves = new Staff[numberOfStaves];
            for (int i = 0; i < mStaves.Length; i++)
            {
                mStaves[i] = new Staff(mClefs[i], Song.TOTAL_INSTRUMENTS, Song.TOTAL_STAVES);
                Song.cursorY += Staff.HEIGHT + Song.STAFF_SPACING;
                if (i == mStaves.Length - 1)
                    Song.TOTAL_INSTRUMENTS++;
            }
        }

        public void Update()
        {
            foreach (Staff staff in mStaves)
                staff.Update();
            area.Location = mStaves[0].GetArea().Location;
            Staff last = mStaves[mStaves.Length - 1];
            area.Width = last.GetArea().Right - area.Left;
            area.Height = last.GetArea().Bottom - area.Top;

            area.Y -= Song.INSTRUMENT_SPACING;
            area.Height += Song.INSTRUMENT_SPACING * 2;
        }

        public void Draw(PaintEventArgs e)
        {
            foreach (Staff staff in mStaves)
                staff.Draw(e);
        }

        public void Play()
        {

        }
    }
}

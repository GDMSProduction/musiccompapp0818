using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
namespace Music_Comp
{
    class Instrument
    {
        Staff[] mStaves;
        Clef[] mClefs;

        public Instrument(Clef c)
        {
            mClefs = new Clef[1];
            mClefs[0] = c;
            AddStaves(1);
        }
        public Instrument(Clef c1, Clef c2)
        {
            mClefs = new Clef[2];
            mClefs[0] = c1;
            mClefs[1] = c2;
            AddStaves(2);
        }
        public Instrument(Clef c1, Clef c2, Clef c3)
        {
            mClefs = new Clef[3];
            mClefs[0] = c1;
            mClefs[1] = c2;
            mClefs[2] = c3;
            AddStaves(3);
        }
        public Instrument(Clef c1, Clef c2, Clef c3, Clef c4)
        {
            mClefs = new Clef[4];
            mClefs[0] = c1;
            mClefs[1] = c2;
            mClefs[2] = c3;
            mClefs[3] = c4;
            AddStaves(4);
        }
        public int GetNumberOfStaves()
        {
            return mStaves.Length;
        }
        public Staff GetStaff(int i)
        {
            return mStaves[i];
        }
        public int GetNumberOfClefs()
        {
            return mClefs.Length;
        }
        public Clef GetClef(int i)
        {
            return mClefs[i];
        }
        public void AddStaves(int numberOfStaves)
        {
            mStaves = new Staff[numberOfStaves];
            for (int i = 0; i < mStaves.Length; i++)
            {
                mStaves[i] = new Staff(mClefs[i], Song.cursorY);
                Song.cursorY += Staff.HEIGHT + Song.STAFF_SPACING;
            }
        }
        public void Draw(PaintEventArgs e)
        {
            foreach (Staff staff in mStaves)
                staff.Draw(e);
        }
    }
}

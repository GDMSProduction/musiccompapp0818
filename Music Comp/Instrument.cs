using System.Collections.Generic;

namespace Music_Comp
{
    class Instrument
    {
        public List<Staff> staves = new List<Staff>();

        public Instrument(int numberOfStaves, Clef c, Key k, Time t)
        {
            for (int i = 0; i < numberOfStaves; i++)
            {
                Staff s = new Staff(i, c, k, t);
                staves.Add(s);
            }
        }
        public Staff getStaff(int i)
        {
            return staves[i];
        }
    }
}

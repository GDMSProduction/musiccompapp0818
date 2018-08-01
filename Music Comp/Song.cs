using System.Collections.Generic;

namespace Music_Comp
{
    class Song
    {
        public List<Instrument> instruments = new List<Instrument>();
        Key key;
        Time time;

        public Song(Key k, Time t)
        {
            key = k;
            time = t;
        }
        public Instrument getInstrument(int i)
        {
            return instruments[i];
        }
        public void addInstrument(int numberOfStaves, Clef clef)
        {
            instruments.Add(new Instrument(numberOfStaves, clef, key, time));
        }
        public void drawSong()
        {
            int inum = 0;
            int snum = 0;
            foreach (Instrument i in instruments)
            {
                foreach (Staff j in i.staves)
                {
                    j.drawStaff(inum, instruments.Count);
                }
                inum++;
            }
            foreach (Instrument i in instruments)
            {
                foreach (Staff j in i.staves)
                {
                    Measure m = new Measure(snum, j.getCursor());
                    snum++;
                }
            }
        }
    }
}

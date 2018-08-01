namespace Music_Comp
{
    class Note
    {
        Pitch pitch;
        Accidental accidental;
        Durration durration;
        byte octave = 4;

        public Note(Pitch p, Accidental a, Durration d, byte o)
        {
            pitch = p;
            accidental = a;
            durration = d;
            octave = o;
        }
        public Pitch getPitch()
        {
            return pitch;
        }
        public Accidental getAccidental()
        {
            return accidental;
        }
        public Durration getDurration()
        {
            return durration;
        }
    }
}

namespace Music_Comp
{
    class Note
    {
        Pitch mPitch;
        Accidental mAccidental;
        Durration mDurration;

        sbyte mOctave = 4;

        public Note(Pitch p, Accidental a, Durration d, sbyte o)
        {
            mPitch = p;
            mAccidental = a;
            mDurration = d;

            mOctave = o;
        }
        public Pitch GetPitch()
        {
            return mPitch;
        }
        public Accidental GetAccidental()
        {
            return mAccidental;
        }
        public Durration GetDurration()
        {
            return mDurration;
        }
        public sbyte GetOctave()
        {
            return mOctave;
        }
    }
}

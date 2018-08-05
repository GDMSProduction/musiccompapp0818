namespace Music_Comp
{
    class Note
    {
        Pitch mPitch;
        Accidental mAccidental;
        Duration mDuration;

        sbyte mOctave = 4;

        public Note(Pitch p, Accidental a, Duration d, sbyte o)
        {
            mPitch = p;
            mAccidental = a;
            mDuration = d;

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
        public Duration GetDurration()
        {
            return mDuration;
        }
        public sbyte GetOctave()
        {
            return mOctave;
        }
    }
}

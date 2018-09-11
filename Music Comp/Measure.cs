using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Measure
    {
        RectangleF area;

        List<Chord> mChords;

        float mLength;

        int mTotalDuration;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float yPosition)
        {
            mClef = clef;
            mChords = new List<Chord>();
        }

        public int GetChordCount()
        {
            return mChords.Count;
        }

        public float GetLength()
        {
            return mLength;
        }

        public Chord GetChord(int i)
        {
            return mChords[i];
        }

        public RectangleF GetArea()
        {
            return area;
        }

        public bool IsFull()
        {
            if (mTotalDuration >= (int)Song.TIME)
                return true;
            return false;
        }

        public Chord AddChord(Chord chord)
        {
            if (chord == null)
                return null;
            if (mChords.Count == 0)
            {
                mChords.Add(chord);
                mTotalDuration += (int)chord.GetDuration();
                return null;
            }
            else
            {
                mTotalDuration += (int)chord.GetDuration();
                if (mTotalDuration > (int)Song.TIME)
                {
                    Chord split = new Chord();
                    Chord remainder = new Chord();

                    int remainderDuration = mTotalDuration - (int)Song.TIME;
                    int splitDuration = (int)chord.GetDuration() - remainderDuration;

                    split = chord.Clone();
                    remainder = chord.Clone();

                    split.SetDuration((Duration)splitDuration);
                    remainder.SetDuration((Duration)remainderDuration);

                    mChords.Add(split);
                    return remainder;
                }
                else if (mTotalDuration == (int)Song.TIME)
                {
                    mChords.Add(chord);
                    return null;
                }
                else
                {
                    mChords.Add(chord);
                    return null;
                }
            }
        }

        public void Update(float barline, float yPosition)
        {
            float cursor = 0;
            foreach (Chord chord in mChords)
            {
                chord.Update(barline + cursor, yPosition, mClef);
                cursor += chord.GetWidth();
            }
            mLength = cursor;

            area.X = barline;
            area.Y = yPosition + Song.STAFF_SPACING;
            area.Width = mLength;
            area.Height = Staff.HEIGHT + Song.STAFF_SPACING * 2;
        }
        public void Draw(PaintEventArgs e)
        {
            foreach (Chord chord in mChords)
                chord.Draw(e);
        }

        public void Play()
        {

        }

        public void Remove(Chord c)
        {
            mTotalDuration -= (int)mChords[mChords.Count - 1].GetDuration();
            mChords.Remove(c);
        }

        public bool IsEmpty()
        {
            if (mChords.Count == 0 || mTotalDuration == 0)
                return true;
            return false;
        }
    }
}

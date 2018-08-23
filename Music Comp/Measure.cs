using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public class Measure
    {
        Rectangle area;

        List<Chord> mChords;

        float mLength;

        bool isFull;
        int mTotalDuration;

        Clef mClef;
        Key mKey;
        Time mTime;

        public Measure(Clef clef, float yPosition)
        {
            mClef = clef;
            isFull = false;
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

        public Rectangle GetArea()
        {
            return area;
        }

        public bool GetFull()
        {
            return isFull;
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

                    split = chord;
                    remainder = chord;

                    split.SetDuration((Duration)splitDuration);
                    remainder.SetDuration((Duration)remainderDuration);

                    mChords.Add(split);
                    isFull = true;
                    return remainder;
                }
                else if (mTotalDuration == (int)Song.TIME)
                {
                    mChords.Add(chord);
                    isFull = true;
                    return null;
                }
                else
                {
                    mChords.Add(chord);
                    return null;
                }
            }
        }

        public void UpdateLength()
        {
            mLength = 60 * Song._SCALE * mChords.Count;
        }

        public void Draw(float cursorX, float yPosition, PaintEventArgs e)
        {
            float cursor = 0;
            foreach (Chord chord in mChords)
            {
                chord.Draw(cursorX + cursor, yPosition, mClef, e);
                cursor += 60 * Song._SCALE;
            }
        }
    }
}

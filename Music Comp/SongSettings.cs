using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace Music_Comp
{
    [Serializable]
    class SongSettings
    {
        // Song

        Image bracketImage = Properties.Resources.Bracket;
        Image braceImage = Properties.Resources.Brace;

        public ImageAttributes _REDSHIFT;

        public float SCREEN_WIDTH;
        public float PAGE_WIDTH;
        public float _SCALE;
        public float TOP_MARGIN;
        public float LEFT_MARGIN;
        public float RIGHT_MARGIN;
        public float STAFF_SPACING;
        public float INSTRUMENT_SPACING;

        public int TOTAL_INSTRUMENTS;
        public int TOTAL_STAVES;
        public int TOTAL_MEASURES;
        public int TOTAL_CHORDS;
        public int TOTAL_NOTES;

        public float cursorY;
        public float cursorX;

        public Key KEY = Key.C;
        public Time TIME = Time.Common;
        public int BPM;
        public sbyte OCTAVE = 4;

        public List<float> BARLINES;
        public List<SongComponent> SELECTABLES;

        // Staff

        Image doubleflatImage = Properties.Resources.DoubleFlat;
        Image flatImage = Properties.Resources.Flat;
        Image naturalImage = Properties.Resources.Natural;
        Image sharpImage = Properties.Resources.Sharp;
        Image doublesharpImage = Properties.Resources.DoubleSharp;
        Image trebleClefImage = Properties.Resources.TrebleClef;
        Image cClefImage = Properties.Resources.C_Clef;
        Image bassClefImage = Properties.Resources.BassClef;
        Image fourFourImage = Properties.Resources.FourFour;
        Image sixEightImage = Properties.Resources.SixEight;

        public float LINE_SPACING;
        public float LENGTH;
        public float HEIGHT;
    }
}

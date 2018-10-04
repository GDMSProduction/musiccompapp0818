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

        public SongSettings()
        {
            SCREEN_WIDTH = Song.SCREEN_WIDTH;
            PAGE_WIDTH = Song.PAGE_WIDTH;
            _SCALE = Song._SCALE;
            TOP_MARGIN = Song.TOP_MARGIN;
            LEFT_MARGIN = Song.LEFT_MARGIN;
            RIGHT_MARGIN = Song.RIGHT_MARGIN;
            STAFF_SPACING = Song.STAFF_SPACING;
            INSTRUMENT_SPACING = Song.INSTRUMENT_SPACING;

            TOTAL_INSTRUMENTS = Song.TOTAL_INSTRUMENTS;
            TOTAL_STAVES = Song.TOTAL_STAVES;
            TOTAL_MEASURES = Song.TOTAL_MEASURES;
            TOTAL_CHORDS = Song.TOTAL_CHORDS;
            TOTAL_NOTES = Song.TOTAL_NOTES;

            cursorY = Song.cursorY;
            cursorX = Song.cursorX;

            KEY = Song.KEY;
            TIME = Song.TIME;
            BPM = Song.BPM;
            OCTAVE = Song.OCTAVE;

            BARLINES = Song.BARLINES;
            SELECTABLES = Song.SELECTABLES;

            LINE_SPACING = Staff.LINE_SPACING;
            LENGTH = Staff.LENGTH;
            HEIGHT = Staff.HEIGHT;
        }

        internal void Apply()
        {
            Song.SCREEN_WIDTH = SCREEN_WIDTH;
            Song.PAGE_WIDTH = PAGE_WIDTH;
            Song._SCALE = _SCALE;
            Song.TOP_MARGIN = TOP_MARGIN;
            Song.LEFT_MARGIN = LEFT_MARGIN;
            Song.RIGHT_MARGIN = RIGHT_MARGIN;
            Song.STAFF_SPACING = STAFF_SPACING;
            Song.INSTRUMENT_SPACING = INSTRUMENT_SPACING;

            Song.TOTAL_INSTRUMENTS = TOTAL_INSTRUMENTS;
            Song.TOTAL_STAVES = TOTAL_STAVES;
            Song.TOTAL_MEASURES = TOTAL_MEASURES;
            Song.TOTAL_CHORDS = TOTAL_CHORDS;
            Song.TOTAL_NOTES = TOTAL_NOTES;

            Song.cursorY = cursorY;
            Song.cursorX = cursorX;

            Song.KEY = KEY;
            Song.TIME = TIME;
            Song.BPM = BPM;
            Song.OCTAVE = OCTAVE;

            Song.BARLINES = BARLINES;
            Song.SELECTABLES = SELECTABLES;

            Staff.LINE_SPACING = LINE_SPACING;
            Staff.LENGTH = LENGTH;
            Staff.HEIGHT = HEIGHT;
        }
    }
}

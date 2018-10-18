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

        int stanzas;

        float SCREEN_WIDTH;
        float PAGE_WIDTH;
        float _SCALE;
        float TOP_MARGIN;
        float LEFT_MARGIN;
        float RIGHT_MARGIN;
        float STAFF_SPACING;
        float INSTRUMENT_SPACING;

        int TOTAL_INSTRUMENTS;
        int TOTAL_STAVES;
        int TOTAL_MEASURES;
        int TOTAL_CHORDS;
        int TOTAL_NOTES;

        float cursorY;
        float cursorX;

        Key KEY = Key.C;
        Time TIME = Time.Common;
        int BPM;
        sbyte OCTAVE = 4;

        List<float> BARLINES;
        List<float[]> CHORD_POSITIONS;

        List<Note[]> LASTNOTES;

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
            stanzas = Song.stanzas;

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
            CHORD_POSITIONS = Song.CHORD_POSITIONS;

            LASTNOTES = Song.LASTNOTES;

            LINE_SPACING = Staff.LINE_SPACING;
            LENGTH = Staff.LENGTH;
            HEIGHT = Staff.HEIGHT;
        }

        public void Apply()
        {
            Song.stanzas = stanzas;

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
            Song.CHORD_POSITIONS = CHORD_POSITIONS;

            Song.LASTNOTES = LASTNOTES;

            Staff.LINE_SPACING = LINE_SPACING;
            Staff.LENGTH = LENGTH;
            Staff.HEIGHT = HEIGHT;

            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
            {
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 0, 0, 0, 1}
            });

            Song._REDSHIFT = new ImageAttributes();
            Song._REDSHIFT.SetColorMatrix(colorMatrix);

            Song.needsFullUpdate = true;
        }
    }
}

using System;
using System.Windows.Forms;

namespace Music_Comp
{
    public partial class AddInstrumentForm : Form
    {
        public float mainSCREEN_WIDTH = Song.SCREEN_WIDTH;
        public float mainPAGE_WIDTH = Song.PAGE_WIDTH;
        public float main_SCALE = Song._SCALE;
        public float mainTOP_MARGIN = Song.TOP_MARGIN;
        public float mainLEFT_MARGIN = Song.LEFT_MARGIN;
        public float mainRIGHT_MARGIN = Song.RIGHT_MARGIN;
        public float mainSTAFF_SPACING = Song.STAFF_SPACING;
        public float mainINSTRUMENT_SPACING = Song.INSTRUMENT_SPACING;

        public int mainTOTAL_INSTRUMENTS = Song.TOTAL_INSTRUMENTS;
        public int mainTOTAL_STAVES = Song.TOTAL_STAVES;

        public float maincursorY = Song.cursorY;
        public float maincursorX = Song.cursorX;

        public float mainLINE_SPACING = Staff.LINE_SPACING;
        public float mainLENGTH = Staff.LENGTH;
        public float mainHEIGHT = Staff.HEIGHT;
        
        public Clef[] clefs = new Clef[4];
        public Grouping grouping = Grouping.None;

        Song song;

        private int staveCount = 1;
        public int StaveCount
        {
            get { return staveCount; }
            set { staveCount = value; }
        }
        public AddInstrumentForm()
        {
            InitializeComponent();

            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            song.AddInstrument(Clef.Treble, Grouping.None);
        }

        private void AddInstrumentForm_Paint(object sender, PaintEventArgs e)
        {
            song.Draw(e);
        }

        private void Brace_CheckedChanged(object sender, EventArgs e)
        {
            if (Brace.Checked == true)
            {
                grouping = Grouping.Brace;
                song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                switch (StaveNumeric.Value)
                {
                    case 2:
                        song.AddInstrument(clefs[0], clefs[1], grouping);
                        break;
                    case 3:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                        break;
                    case 4:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                        break;
                }
                graphicsPanel.Invalidate();
            }
            else return;
        }

        private void Bracket_CheckedChanged(object sender, EventArgs e)
        {
            if (Bracket.Checked == true)
            {
                grouping = Grouping.Bracket;
                song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                switch (StaveNumeric.Value)
                {
                    case 2:
                        song.AddInstrument(clefs[0], clefs[1], grouping);
                        break;
                    case 3:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                        break;
                    case 4:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                        break;
                }
                graphicsPanel.Invalidate();
            }
            else return;
        }

        private void None_CheckedChanged(object sender, EventArgs e)
        {
            if (None.Checked == true)
            {
                grouping = Grouping.None;
                song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                switch (StaveNumeric.Value)
                {
                    case 2:
                        song.AddInstrument(clefs[0], clefs[1], grouping);
                        break;
                    case 3:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                        break;
                    case 4:
                        song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                        break;
                }
                graphicsPanel.Invalidate();
            }
            else return;
        }

        private void StaveNumeric_ValueChanged(object sender, EventArgs e)
        {
            switch (StaveNumeric.Value)
            {
                case 1:
                    staveCount = 1;
                    St2Clef.Enabled = false;
                    St3Clef.Enabled = false;
                    St4Clef.Enabled = false;
                    St2Clef.Text = " ";
                    St3Clef.Text = " ";
                    St4Clef.Text = " ";
                    song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                    song.AddInstrument(Clef.Treble, grouping);
                    break;
                case 2:
                    staveCount = 2;
                    St2Clef.Enabled = true;
                    St3Clef.Enabled = false;
                    St4Clef.Enabled = false;
                    St3Clef.Text = " ";
                    St4Clef.Text = " ";
                    song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                    song.AddInstrument(Clef.Treble, Clef.Treble, grouping);
                    break;
                case 3:
                    staveCount = 3;
                    St2Clef.Enabled = true;
                    St3Clef.Enabled = true;
                    St4Clef.Enabled = false;
                    St4Clef.Text = " ";
                    song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                    song.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, grouping);
                    break;
                case 4:
                    staveCount = 4;
                    St2Clef.Enabled = true;
                    St3Clef.Enabled = true;
                    St4Clef.Enabled = true;
                    song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
                    song.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, Clef.Treble, grouping);
                    break;
            }
            graphicsPanel.Invalidate();
        }

        private void St1Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (St1Clef.Text)
            {
                case "Treble":
                    clefs[0] = Clef.Treble;
                    break;
                case "Alto":
                    clefs[0] = Clef.Alto;
                    break;
                case "Tenor":
                    clefs[0] = Clef.Tenor;
                    break;
                case "Bass":
                    clefs[0] = Clef.Bass;
                    break;
            }
            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            switch (StaveNumeric.Value)
            {
                case 1:
                    song.AddInstrument(clefs[0], grouping);
                    break;
                case 2:
                    song.AddInstrument(clefs[0], clefs[1], grouping);
                    break;
                case 3:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                    break;
                case 4:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                    break;
            }
            graphicsPanel.Invalidate();
        }

        private void St2Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (St2Clef.Text)
            {
                case "Treble":
                    clefs[1] = Clef.Treble;
                    break;
                case "Alto":
                    clefs[1] = Clef.Alto;
                    break;
                case "Tenor":
                    clefs[1] = Clef.Tenor;
                    break;
                case "Bass":
                    clefs[1] = Clef.Bass;
                    break;
            }
            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            switch (StaveNumeric.Value)
            {
                case 2:
                    song.AddInstrument(clefs[0], clefs[1], grouping);
                    break;
                case 3:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                    break;
                case 4:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                    break;
            }
            graphicsPanel.Invalidate();
        }

        private void St3Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (St3Clef.Text)
            {
                case "Treble":
                    clefs[2] = Clef.Treble;
                    break;
                case "Alto":
                    clefs[2] = Clef.Alto;
                    break;
                case "Tenor":
                    clefs[2] = Clef.Tenor;
                    break;
                case "Bass":
                    clefs[2] = Clef.Bass;
                    break;
            }
            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            switch (StaveNumeric.Value)
            {
                case 3:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], grouping);
                    break;
                case 4:
                    song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);
                    break;
            }
            graphicsPanel.Invalidate();
        }

        private void St4Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (St4Clef.Text)
            {
                case "Treble":
                    clefs[3] = Clef.Treble;
                    break;
                case "Alto":
                    clefs[3] = Clef.Alto;
                    break;
                case "Tenor":
                    clefs[3] = Clef.Tenor;
                    break;
                case "Bass":
                    clefs[3] = Clef.Bass;
                    break;
            }
            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            song.AddInstrument(clefs[0], clefs[1], clefs[2], clefs[3], grouping);

            graphicsPanel.Invalidate();
        }
    }
}

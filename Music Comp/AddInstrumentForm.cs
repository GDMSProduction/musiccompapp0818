using System;
using System.Collections.Generic;
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

        public List<Clef> clefs = new List<Clef>();
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

            clefs.Add(Clef.Treble);
            RefreshSong();
        }

        private void RefreshSong()
        {
            song = new Song(graphicsPanel.Width * 2, Song.KEY, Song.TIME);
            song.AddInstrument(clefs, grouping);
            song.GetInstrument(0).GetStaff(0).SetActive(false);
            graphicsPanel.Invalidate();
        }

        private void AddInstrumentForm_Paint(object sender, PaintEventArgs e)
        {
            song.Update();
            song.Draw(e);
        }

        private void Brace_CheckedChanged(object sender, EventArgs e)
        {
            if (Brace.Checked == true)
            {
                grouping = Grouping.Brace;
                RefreshSong();
            }
            else return;
        }

        private void Bracket_CheckedChanged(object sender, EventArgs e)
        {
            if (Bracket.Checked == true)
            {
                grouping = Grouping.Bracket;
                RefreshSong();
            }
            else return;
        }

        private void None_CheckedChanged(object sender, EventArgs e)
        {
            if (None.Checked == true)
            {
                grouping = Grouping.None;
                RefreshSong();
            }
            else return;
        }

        private void StaveNumeric_ValueChanged(object sender, EventArgs e)
        {
            clefs = new List<Clef>();
            for (int i = 0; i < StaveNumeric.Value; i++)
                clefs.Add(Clef.Treble);
            staveCount = (int)StaveNumeric.Value;
            switch (StaveNumeric.Value)
            {
                case 1:
                    St2Clef.Enabled = false;
                    St2Clef.Text = "";
                    St3Clef.Enabled = false;
                    St3Clef.Text = "";
                    St4Clef.Enabled = false;
                    St4Clef.Text = "";
                    break;
                case 2:
                    St2Clef.Enabled = true;
                    if (St2Clef.Text == "")
                        St2Clef.Text = "Treble";
                    St3Clef.Enabled = false;
                    St3Clef.Text = "";
                    St4Clef.Enabled = false;
                    St4Clef.Text = "";
                    break;
                case 3:
                    St2Clef.Enabled = true;
                    if (St2Clef.Text == "")
                        St2Clef.Text = "Treble";
                    St3Clef.Enabled = true;
                    if (St3Clef.Text == "")
                        St3Clef.Text = "Treble";
                    St4Clef.Enabled = false;
                    St4Clef.Text = "";
                    break;
                case 4:
                    St2Clef.Enabled = true;
                    if (St2Clef.Text == "")
                        St2Clef.Text = "Treble";
                    St3Clef.Enabled = true;
                    if (St3Clef.Text == "")
                        St3Clef.Text = "Treble";
                    St4Clef.Enabled = true;
                    St4Clef.Text = "Treble";
                    break;
            }
            RefreshSong();
        }

        private void St1Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(St1Clef.Text, out Clef c);
            clefs[0] = c;
            RefreshSong();
        }

        private void St2Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(St2Clef.Text, out Clef c);
            clefs[1] = c;
            RefreshSong();
        }

        private void St3Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(St3Clef.Text, out Clef c);
            clefs[2] = c;
            RefreshSong();
        }

        private void St4Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(St4Clef.Text, out Clef c);
            clefs[3] = c;
            RefreshSong();
        }
    }
}
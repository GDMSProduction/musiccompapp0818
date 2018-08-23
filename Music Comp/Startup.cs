using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Music_Comp
{
    public partial class Startup : Form
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

        public Key key;
        public Time time;

        public string title;
        public string composer;

        Song song;

        public List<instrumentTemplate> instruments;

        public bool checkexit;

        public struct instrumentTemplate
        {
            public List<Clef> clefs;
            public Grouping grouping;
            public int StaveCount;

            public instrumentTemplate(List<Clef> _clefs, Grouping _grouping, int _StaveCount)
            {
                clefs = _clefs;
                grouping = _grouping;
                StaveCount = _StaveCount;
            }
        }

        public Startup()
        {
            InitializeComponent();

            key = Key.C;
            time = Time.FourFour;

            instruments = new List<instrumentTemplate>();

            checkexit = true;
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (song != null)
                song.Draw(e);
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e)
        {
            song = new Song(graphicsPanel1.Width * 2, key, time);
            AddInstrumentForm options = new AddInstrumentForm();
            options.ShowDialog();

            bool OK = options.DialogResult == DialogResult.OK;

                Song.SCREEN_WIDTH = options.mainSCREEN_WIDTH;
                Song.PAGE_WIDTH = options.mainPAGE_WIDTH;
                Song._SCALE = options.main_SCALE;
                Song.TOP_MARGIN = options.mainTOP_MARGIN;
                Song.LEFT_MARGIN = options.mainLEFT_MARGIN;
                Song.RIGHT_MARGIN = options.mainRIGHT_MARGIN;
                Song.STAFF_SPACING = options.mainSTAFF_SPACING;
                Song.INSTRUMENT_SPACING = options.mainINSTRUMENT_SPACING;

                Song.TOTAL_INSTRUMENTS = options.mainTOTAL_INSTRUMENTS;
                Song.TOTAL_STAVES = options.mainTOTAL_STAVES;

                Song.cursorY = options.maincursorY;
                Song.cursorX = options.maincursorX;

                Staff.LINE_SPACING = options.mainLINE_SPACING;
                Staff.LENGTH = options.mainLENGTH;
                Staff.HEIGHT = options.mainHEIGHT;

            if (OK)
            {
                song.AddInstrument(options.clefs, options.grouping);
                instruments.Add(new instrumentTemplate(options.clefs, options.grouping, options.StaveCount));
                song.GetInstrument(0).GetStaff(0).SetActive(false);
            }
            else
                song = null;

            graphicsPanel1.Invalidate();
            
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            titleLabel.Text = titleTextBox.Text;
            title = titleTextBox.Text;
        }

        private void composerTextBox_TextChanged(object sender, EventArgs e)
        {
            composerLabel.Text = composerTextBox.Text;
            composer = composerTextBox.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            song.RemoveInstrument(0);
            graphicsPanel1.Invalidate();
        }

        private void KeyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(KeyBox.Text, out Key k);
            key = k;
        }

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(TimeBox.Text, out Time t);
            time = t;
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkexit == true)
            {
                Application.Exit();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            checkexit = false;
        }
    }
}

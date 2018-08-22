using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<instrumentTemplate> instruments = new List<instrumentTemplate>();
        public Key key;
        public Time time;
        public string title;
        public string composer;
        public int x = 0;
        public bool checkexit = true;

        Song song;

        public struct instrumentTemplate
        {
            public Clef[] clefs;
            public Grouping grouping;
            public int StaveCount;

            public instrumentTemplate(Clef[] _clefs, Grouping _grouping, int _StaveCount)
            {
                clefs = _clefs;
                grouping = _grouping;
                StaveCount = _StaveCount;
            }
        }

        public Startup()
        {
            InitializeComponent();
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (song != null)
            {
                song.Draw(e);
            }
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e)
        {
            AddInstrumentForm options = new AddInstrumentForm();
            options.ShowDialog();
            if (options.DialogResult == DialogResult.OK)
            {
                song = new Song(graphicsPanel1.Width * 2, key, time);
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

                switch (options.StaveCount)
                {
                    case 1:
                        song.AddInstrument(options.clefs[0], options.grouping);
                        break;
                    case 2:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.grouping);
                        break;
                    case 3:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.clefs[2], options.grouping);
                        break;
                    case 4:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.clefs[2], options.clefs[3], options.grouping);
                        break;
                }
                instruments.Add(new instrumentTemplate(options.clefs, options.grouping, options.StaveCount));
                ++x;
                song.GetInstrument(0).GetStaff(0).SetActive(false);
            }
            else
            {
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
            }
            graphicsPanel1.Invalidate();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label6.Text = textBox1.Text;
            title = textBox1.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label7.Text = textBox4.Text;
            composer = textBox4.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            song.RemoveInstrument(0);
            --x;
            graphicsPanel1.Invalidate();
        }

        private void KeyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (KeyBox.Text)
            {
                case "Cflat":
                    key = Key.Cflat;
                    break;
                case "Gflat":
                    key = Key.Gflat;
                    break;
                case "Dflat":
                    key = Key.Dflat;
                    break;
                case "Aflat":
                    key = Key.Aflat;
                    break;
                case "Eflat":
                    key = Key.Eflat;
                    break;
                case "Bflat":
                    key = Key.Bflat;
                    break;
                case "F":
                    key = Key.F;
                    break;
                case "C":
                    key = Key.C;
                    break;
                case "G":
                    key = Key.G;
                    break;
                case "D":
                    key = Key.G;
                    break;
                case "A":
                    key = Key.A;
                    break;
                case "E":
                    key = Key.E;
                    break;
                case "B":
                    key = Key.B;
                    break;
                case "Fsharp":
                    key = Key.Fsharp;
                    break;
                case "Csharp":
                    key = Key.Csharp;
                    break;
            }
            song = new Song(graphicsPanel1.Width * 2, key, time);
        }

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TimeBox.Text)
            {
                case "FourFour":
                    time = Time.FourFour;
                    break;
                case "SixEight":
                    time = Time.SixEight;
                    break;
            }
            song = new Song(graphicsPanel1.Width * 2, key, time);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkexit == true)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkexit = false;
        }
    }
}

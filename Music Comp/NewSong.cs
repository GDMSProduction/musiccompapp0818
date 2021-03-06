﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Music_Comp
{
    public partial class NewSong : Form
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
            public List<WaveForm> waveForms;

            public instrumentTemplate(List<Clef> _clefs, Grouping _grouping, int _StaveCount, List<WaveForm> _waveForm)
            {
                clefs = _clefs;
                grouping = _grouping;
                StaveCount = _StaveCount;
                waveForms = _waveForm;
            }
        }

        public NewSong()
        {
            InitializeComponent();
            ActiveControl = AddInstrumentButton;

            key = Key.C;
            time = Time.FourFour;

            instruments = new List<instrumentTemplate>();

            checkexit = true;
        }

        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (song != null && Song.TOTAL_INSTRUMENTS != 0)
            {
                song.Update();
                song.Paint(e.Graphics);
            }
            graphicsPanel.Height = Height - graphicsPanel.Location.Y - 48;
            graphicsPanel.Width = Width - graphicsPanel.Location.X - (cancelButton.Width + okButton.Width) - 90;
            okButton.Location = new Point((Width - 22) - okButton.Width, Height - 75);
            cancelButton.Location = new Point((Width - (cancelButton.Width + 25)) - cancelButton.Width, Height - 75);
            AddInstrumentButton.Location = new Point(Width - (AddInstrumentButton.Width + 40) - AddInstrumentButton.Width, 45);
            removeInstrumentButton.Location = new Point(Width - removeInstrumentButton.Width - 34, 45);
            label5.Location = new Point(Width - label5.Width - 70, 25);
            label2.Location = new Point(Width - 176, 95);
            KeyBox.Location = new  Point(Width - 96, 93);
            label3.Location = new  Point(Width - 176, 125);
            TimeBox.Location = new Point(Width - 96, 123);
            titleBoxLabel.Location = new Point(Width - 176, 155);
            titleTextBox.Location = new Point(Width - 96, 153);
            composerBoxLabel.Location = new Point(Width - 176, 185);
            composerTextBox.Location = new Point(Width - 96, 183);
        }

        private void AddInstrumentButton_Click(object sender, EventArgs e)
        {
            if (song == null)
                song = new Song(graphicsPanel.Width, key, time);
            AddInstrumentForm options = new AddInstrumentForm();
            options.ShowDialog();

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

            if (options.DialogResult == DialogResult.OK)
            {
                if (song == null)
                {
                    song = new Song(graphicsPanel.Width, key, time);
                }
                song.AddInstrument(options.clefs, options.waveForms, options.grouping);
                instruments.Add(new instrumentTemplate(options.clefs, options.grouping, options.StaveCount, options.waveForms));
                song.GetInstrument(0).GetStaff(0).Deselect();
            }

            graphicsPanel.Invalidate();
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            titleLabel.Text = titleTextBox.Text;
            title = titleTextBox.Text;
            if (titleTextBox.TextLength > 0 && composerTextBox.TextLength > 0)
            {
                okButton.DialogResult = DialogResult.OK;
            }
            else
            {
                okButton.DialogResult = DialogResult.None;
            }
            if (titleTextBox.TextLength > 0)
            {
                titleBoxLabel.ForeColor = Color.Black;
            }
        }

        private void composerTextBox_TextChanged(object sender, EventArgs e)
        {
            composerLabel.Text = composerTextBox.Text;
            composer = composerTextBox.Text;
            if (titleTextBox.TextLength > 0 && composerTextBox.TextLength > 0)
            {
                okButton.DialogResult = DialogResult.OK;
            }
            else
            {
                okButton.DialogResult = DialogResult.None;
            }
            if (composerTextBox.TextLength > 0)
            {
                composerBoxLabel.ForeColor = Color.Black;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            instruments.RemoveAt(instruments.Count - 1);
            song.RemoveInstrument(song.GetInstrumentCount() - 1);
            graphicsPanel.Invalidate();
        }

        private void KeyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(KeyBox.Text, out Key k);
            song.SetKeySignature(k);
            key = k;

            graphicsPanel.Invalidate();
        }

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(TimeBox.Text, out Time t);
            song.SetTimeSignature(t);
            time = t;

            graphicsPanel.Invalidate();
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkexit && !Properties.Settings.Default.Loaded)
            {
                Application.Exit();
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (titleTextBox.TextLength == 0 && composerTextBox.TextLength == 0)
            {
                titleBoxLabel.ForeColor = Color.Red;
                composerBoxLabel.ForeColor = Color.Red;
            }
            else if (titleTextBox.TextLength == 0)
            {
                titleBoxLabel.ForeColor = Color.Red;
            }
            else if (composerTextBox.TextLength == 0)
            {
                composerBoxLabel.ForeColor = Color.Red;
            }
            else
            {
                checkexit = false;
            }
        }

        private void composerLabel_SizeChanged(object sender, EventArgs e)
        {
            composerLabel.Location = new Point(graphicsPanel.Width - 45 - composerLabel.Width, 100);
        }

        private void titleLabel_SizeChanged(object sender, EventArgs e)
        {
            titleLabel.Location = new Point((graphicsPanel.Width / 2) - (titleLabel.Width / 2), 25);
        }

        private void Startup_SizeChanged(object sender, EventArgs e)
        {
            graphicsPanel.Invalidate();
        }

        private void Startup_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}

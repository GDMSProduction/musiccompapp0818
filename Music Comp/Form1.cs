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
    public partial class MainForm : Form
    {
        float SCREEN_WIDTH;
        float PAGE_WIDTH;
        float PAGE_HEIGHT;
        float _SCALE;

        Song song;

        public MainForm()
        {
            InitializeComponent();

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * _SCALE);
            titleTextBox.Size = new Size((int)(1470 * _SCALE), (int)(140 * _SCALE));
            titleTextBox.Location = new Point((int)(PAGE_WIDTH / 2 - titleTextBox.Width / 2), (int)(120 * _SCALE));

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * _SCALE);
            composerTextBox.Size = new Size((int)(615 * _SCALE), (int)(50 * _SCALE));
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE));

            song = new Song(PAGE_WIDTH, Key.Eflat, Time.Common);
            song.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, Clef.Bass);
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            zoomInButton.Location = new Point(Width - 110, Height - 200);
            zoomOutButton.Location = new Point(Width - 110, Height - 150);
            drawSong(e);
        }

        private void drawSong(PaintEventArgs e)
        {
            song.Draw(e);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (Width > PAGE_WIDTH)
                graphicsPanel.Location = new Point((int)(Width / 2 - PAGE_WIDTH / 2), 0);
            else
                graphicsPanel.Location = new Point(0, 0);
        }

        private void graphicsPanel_Resize(object sender, EventArgs e)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * _SCALE);
            titleTextBox.Size = new Size((int)(1470 * _SCALE), (int)(140 * _SCALE));
            titleTextBox.Location = new Point((int)(PAGE_WIDTH / 2 - titleTextBox.Width / 2), (int)(120 * _SCALE));

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * _SCALE);
            composerTextBox.Size = new Size((int)(615 * _SCALE), (int)(50 * _SCALE));
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE));

            Song.PAGE_WIDTH = graphicsPanel.Width;
            Song._SCALE = _SCALE;
            Song.TOP_MARGIN = 300 * _SCALE;
            Song.LEFT_MARGIN = 100 * _SCALE;
            Song.RIGHT_MARGIN = 50 * _SCALE;
            Song.STAFF_SPACING = 60 * _SCALE;
            Song.INSTRUMENT_SPACING = 80 * _SCALE;

            Staff.LINE_SPACING = 30 * _SCALE;
            Staff.LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            Staff.HEIGHT = 4 * Staff.LINE_SPACING;

            if (Width > graphicsPanel.Width)
                graphicsPanel.Location = new Point(Width / 2 - graphicsPanel.Width / 2, 0);
            else
                graphicsPanel.Location = new Point(0, 0);

            graphicsPanel.Invalidate();
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            if (Song.PAGE_WIDTH < 5000)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH + 100), (int)(PAGE_HEIGHT + 100 * PAGE_HEIGHT / PAGE_WIDTH));
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (Song.PAGE_WIDTH > 500)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH - 100), (int)(PAGE_HEIGHT - 100 * PAGE_HEIGHT / PAGE_WIDTH));
        }

        private void Add_Instrument_Click(object sender, EventArgs e)
        {
            AddInstrument options = new AddInstrument();
            options.ShowDialog();
            if (options.DialogResult == DialogResult.OK)
            {
                
            }
        }
    }
}

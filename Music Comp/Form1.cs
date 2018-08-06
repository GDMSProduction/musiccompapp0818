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
        public static int SCREEN_WIDTH;
        public static int PAGE_WIDTH;
        public static int PAGE_HEIGHT;

        Song song;

        public MainForm()
        {
            InitializeComponent();

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Size = new Size(1470 * PAGE_WIDTH / SCREEN_WIDTH, 140 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Location = new Point(PAGE_WIDTH / 2 - titleTextBox.Width / 2, 120 * PAGE_WIDTH / SCREEN_WIDTH);

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Size = new Size(615 * PAGE_WIDTH / SCREEN_WIDTH, 50 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Location = new Point(PAGE_WIDTH - composerTextBox.Width - 100 * PAGE_WIDTH / SCREEN_WIDTH, 220 * PAGE_WIDTH / SCREEN_WIDTH);

            song = new Song(PAGE_WIDTH);
            song.AddInstrument(Clef.Treble, Clef.Bass);
            song.AddInstrument(Clef.Treble);
            song.EditTimeSignature(Time.CompoundDuple);
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
                graphicsPanel.Location = new Point(Width / 2 - PAGE_WIDTH / 2, 0);
            else
                graphicsPanel.Location = new Point(0, 0);
        }

        private void graphicsPanel_Resize(object sender, EventArgs e)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Size = new Size(1470 * PAGE_WIDTH / SCREEN_WIDTH, 140 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Location = new Point(PAGE_WIDTH / 2 - titleTextBox.Width / 2, 120 * PAGE_WIDTH / SCREEN_WIDTH);

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Size = new Size(615 * PAGE_WIDTH / SCREEN_WIDTH, 50 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Location = new Point(PAGE_WIDTH - composerTextBox.Width - 100 * PAGE_WIDTH / SCREEN_WIDTH, 220 * PAGE_WIDTH / SCREEN_WIDTH);

            Song.PAGE_WIDTH = graphicsPanel.Width;
            Song.TOP_MARGIN = 300 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            Song.LEFT_MARGIN = 100 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            Song.RIGHT_MARGIN = 50 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            Song.STAFF_SPACING = 60 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
            Song.INSTRUMENT_SPACING = 80 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;

            Staff.LINE_SPACING = 30 * Song.PAGE_WIDTH / Song.SCREEN_WIDTH;
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
                graphicsPanel.Size = new Size(PAGE_WIDTH + 100, PAGE_HEIGHT + 100 * PAGE_HEIGHT / PAGE_WIDTH);
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (Song.PAGE_WIDTH > 500)
                graphicsPanel.Size = new Size(PAGE_WIDTH - 100, PAGE_HEIGHT - 100 * PAGE_HEIGHT / PAGE_WIDTH);
        }
    }
}

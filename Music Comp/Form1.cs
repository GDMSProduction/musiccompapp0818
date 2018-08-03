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

        Song song;

        public MainForm()
        {
            InitializeComponent();

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Size = new Size(1470 * PAGE_WIDTH / SCREEN_WIDTH, 140 * PAGE_WIDTH / SCREEN_WIDTH);
            titleTextBox.Location = new Point(PAGE_WIDTH / 2 - titleTextBox.Width / 2, 120 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Size = new Size(615 * PAGE_WIDTH / SCREEN_WIDTH, 50 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Location = new Point(PAGE_WIDTH - composerTextBox.Width - 100 * PAGE_WIDTH / SCREEN_WIDTH, 220 * PAGE_WIDTH / SCREEN_WIDTH);

            song = new Song(graphicsPanel.Width);
            song.AddInstrument(Clef.Treble);
            //song.AddInstrument(Clef.Bass);
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            drawSong(e);
        }

        private void drawSong(PaintEventArgs e)
        {
            song.Draw(e);
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        private void composerTextBox_TextChanged(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (Width > graphicsPanel.Width)
                graphicsPanel.Location = new Point(Width / 2 - graphicsPanel.Width / 2, 0);
            else
                graphicsPanel.Location = new Point(0, 0);
        }

        private void graphicsPanel_Resize(object sender, EventArgs e)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;

            titleTextBox.Location = new Point(PAGE_WIDTH / 2 - titleTextBox.Width / 2, 120 * PAGE_WIDTH / SCREEN_WIDTH);
            composerTextBox.Location = new Point(PAGE_WIDTH - composerTextBox.Width - 100 * PAGE_WIDTH / SCREEN_WIDTH, 220 * PAGE_WIDTH / SCREEN_WIDTH);

            graphicsPanel.Invalidate();
        }
    }
}

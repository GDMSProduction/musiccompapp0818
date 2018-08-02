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
        Song song;

        public MainForm()
        {
            InitializeComponent();
            titleTextBox.Location = new Point(graphicsPanel.Width / 2 - titleTextBox.Width / 2, 120);
            composerTextBox.Location = new Point(graphicsPanel.Width - composerTextBox.Width * 2, 200);
            song = new Song();
            song.AddInstrument(Clef.Treble);
            //song.AddInstrument(Clef.Bass);
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen drawPen = new Pen(Color.Black, 2);
            Rectangle rectangle = new Rectangle(10, 50, Screen.PrimaryScreen.Bounds.Width - 20, Screen.PrimaryScreen.Bounds.Height / 2);
            drawSong(e);
            drawPen.Dispose();
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
    }
}

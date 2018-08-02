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
            song = new Song();
            song.AddInstrument(Clef.Treble);
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            Pen drawPen = new Pen(Color.Black, 2);
            Rectangle rectangle = new Rectangle(10, 50, graphicsPanel.Width - 20, graphicsPanel.Height / 2);
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
    }
}

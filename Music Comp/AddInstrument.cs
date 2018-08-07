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
    public partial class AddInstrument : Form
    {
        Song song;
        public int stavecount
        {
            get
            {
                return stavecount;
            }
            set
            {

            }
        }
        public AddInstrument()
        {
            InitializeComponent();
            song = new Song(panel1.Width, Key.C, Time.Common);
            song.AddInstrument(Clef.Treble);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            song.Draw(e);
        }

        private void Brace_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Bracket_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void None_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void StaveNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (StaveNumeric.Value == 1)
            {
                stavecount = 1;
                St2Clef.Enabled = false;
                St3Clef.Enabled = false;
                St4Clef.Enabled = false;
                St2Clef.Text = " ";
                St3Clef.Text = " ";
                St4Clef.Text = " ";
                song = new Song(panel1.Width, Key.C, Time.Common);
                song.AddInstrument(Clef.Treble);
            }
            else if (StaveNumeric.Value == 2)
            {
                stavecount = 2;
                St2Clef.Enabled = true;
                St3Clef.Enabled = false;
                St4Clef.Enabled = false;
                St2Clef.Text = "Treble";
                St3Clef.Text = " ";
                St4Clef.Text = " ";
                song = new Song(panel1.Width, Key.C, Time.Common);
                song.AddInstrument(Clef.Treble, Clef.Treble);
            }
            else if (StaveNumeric.Value == 3)
            {
                stavecount = 3;
                St2Clef.Enabled = true;
                St3Clef.Enabled = true;
                St4Clef.Enabled = false;
                St2Clef.Text = "Treble";
                St3Clef.Text = "Treble";
                St4Clef.Text = " ";
                song = new Song(panel1.Width, Key.C, Time.Common);
                song.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble);
            }
            else if (StaveNumeric.Value == 4)
            {
                stavecount = 4;
                St2Clef.Enabled = true;
                St3Clef.Enabled = true;
                St4Clef.Enabled = true;
                St2Clef.Text = "Treble";
                St3Clef.Text = "Treble";
                St4Clef.Text = "Treble";
                song = new Song(panel1.Width, Key.C, Time.Common);
                song.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, Clef.Treble);
            }
            panel1.Refresh();
        }

        private void St1Clef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void St2Clef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void St3Clef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void St4Clef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

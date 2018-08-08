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
        Song tempsong;
        public Clef clef1 = new Clef();
        public Clef clef2 = new Clef();
        public Clef clef3 = new Clef();
        public Clef clef4 = new Clef();
        private int staveCount = 1;
        public int stavecount
        {
            get
            {
                return staveCount;
            }
            set
            {
                staveCount = value;
            }
        }
        public AddInstrument()
        {
            InitializeComponent();
            tempsong = new Song(panel1.Width, Key.C, Time.Common);
            tempsong.AddInstrument(Clef.Treble, Grouping.None);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            tempsong.Draw(e);
        }

        private void Brace_CheckedChanged(object sender, EventArgs e)
        {
            if (Brace.Checked == true)
            {

            }
            else return;
        }

        private void Bracket_CheckedChanged(object sender, EventArgs e)
        {
            if (Bracket.Checked == true)
            {

            }
            else return;
        }

        private void None_CheckedChanged(object sender, EventArgs e)
        {
            if (None.Checked == true)
            {

            }
            else return;
        }

        private void StaveNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (StaveNumeric.Value == 1)
            {
                staveCount = 1;
                St2Clef.Enabled = false;
                St3Clef.Enabled = false;
                St4Clef.Enabled = false;
                St2Clef.Text = " ";
                St3Clef.Text = " ";
                St4Clef.Text = " ";
                tempsong = new Song(panel1.Width, Key.C, Time.Common);
                tempsong.AddInstrument(Clef.Treble, Grouping.None);
            }
            else if (StaveNumeric.Value == 2)
            {
                staveCount = 2;
                St2Clef.Enabled = true;
                St3Clef.Enabled = false;
                St4Clef.Enabled = false;
                St3Clef.Text = " ";
                St4Clef.Text = " ";
                tempsong = new Song(panel1.Width, Key.C, Time.Common);
                tempsong.AddInstrument(Clef.Treble, Clef.Treble, Grouping.None);
            }
            else if (StaveNumeric.Value == 3)
            {
                staveCount = 3;
                St2Clef.Enabled = true;
                St3Clef.Enabled = true;
                St4Clef.Enabled = false;
                St4Clef.Text = " ";
                tempsong = new Song(panel1.Width, Key.C, Time.Common);
                tempsong.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, Grouping.None);
            }
            else if (StaveNumeric.Value == 4)
            {
                staveCount = 4;
                St2Clef.Enabled = true;
                St3Clef.Enabled = true;
                St4Clef.Enabled = true;
                tempsong = new Song(panel1.Width, Key.C, Time.Common);
                tempsong.AddInstrument(Clef.Treble, Clef.Treble, Clef.Treble, Clef.Treble, Grouping.None);
            }
            panel1.Refresh();
        }

        private void St1Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (St1Clef.Text == "Treble")
            {
                clef1 = Clef.Treble;
            }
            else if (St1Clef.Text == "Alto")
            {
                clef1 = Clef.Alto;
            }
            else if (St1Clef.Text == "Tenor")
            {
                clef1 = Clef.Tenor;
            }
            else if (St1Clef.Text == "Bass")
            {
                clef1 = Clef.Bass;
            }
            tempsong = new Song(panel1.Width, Key.C, Time.Common);
            if (StaveNumeric.Value == 1)
            {
                tempsong.AddInstrument(clef1, Grouping.None);
            }
            else if (StaveNumeric.Value == 2)
            {
                tempsong.AddInstrument(clef1, clef2, Grouping.None);
            }
            else if (StaveNumeric.Value == 3)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, Grouping.None);
            }
            else if (StaveNumeric.Value == 4)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, clef4, Grouping.None);
            }
            panel1.Invalidate();
        }

        private void St2Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (St2Clef.Text == "Treble")
            {
                clef2 = Clef.Treble;
            }
            else if (St2Clef.Text == "Alto")
            {
                clef2 = Clef.Alto;
            }
            else if (St2Clef.Text == "Tenor")
            {
                clef2 = Clef.Tenor;
            }
            else if (St2Clef.Text == "Bass")
            {
                clef2 = Clef.Bass;
            }
            tempsong = new Song(panel1.Width, Key.C, Time.Common);
            if (StaveNumeric.Value == 2)
            {
                tempsong.AddInstrument(clef1, clef2, Grouping.None);
            }
            else if (StaveNumeric.Value == 3)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, Grouping.None);
            }
            else if (StaveNumeric.Value == 4)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, clef4, Grouping.None);
            }
            panel1.Invalidate();
        }

        private void St3Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (St3Clef.Text == "Treble")
            {
                clef3 = Clef.Treble;
            }
            else if (St3Clef.Text == "Alto")
            {
                clef3 = Clef.Alto;
            }
            else if (St3Clef.Text == "Tenor")
            {
                clef3 = Clef.Tenor;
            }
            else if (St3Clef.Text == "Bass")
            {
                clef3 = Clef.Bass;
            }
            tempsong = new Song(panel1.Width, Key.C, Time.Common);
            if (StaveNumeric.Value == 3)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, Grouping.None);
            }
            else if (StaveNumeric.Value == 4)
            {
                tempsong.AddInstrument(clef1, clef2, clef3, clef4, Grouping.None);
            }
            panel1.Invalidate();
        }

        private void St4Clef_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (St4Clef.Text == "Treble")
            {
                clef4 = Clef.Treble;
            }
            else if (St4Clef.Text == "Alto")
            {
                clef4 = Clef.Alto;
            }
            else if (St4Clef.Text == "Tenor")
            {
                clef4 = Clef.Tenor;
            }
            else if (St4Clef.Text == "Bass")
            {
                clef4 = Clef.Bass;
            }
            tempsong = new Song(panel1.Width, Key.C, Time.Common);
            tempsong.AddInstrument(clef1, clef2, clef3, clef4, Grouping.None);
            panel1.Invalidate();
        }
    }
}

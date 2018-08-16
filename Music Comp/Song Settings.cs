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
    public partial class Song_Settings : Form
    {
        public Song_Settings()
        {
            InitializeComponent();
        }

        public int GetKeySignature()
        {
            int key;

            if (cFlat_Button.Checked)
            {
                cFlat_Button.Checked = true;
                key = -7;
            }

            else if (gFlat_Button.Checked)
            {
                key = -6;
            }

            else if (dFlat_Button.Checked)
            {
                key = -5;
            }

            else if (aFlat_Button.Checked)
            {
                key = -4;
            }

            else if (eFlat_Button.Checked)
            {
                key = -3;
            }

            else if (bFlat_Button.Checked)
            {
                key = -2;
            }

            else if (F_Button.Checked)
            {
                key = -1;
            }

            else if (C_Button.Checked)
            {
                key = 0;
            }

            else if (G_Button.Checked)
            {
                key = 1;
            }

            else if (D_Button.Checked)
            {
                key = 2;
            }

            else if (A_Button.Checked)
            {
                key = 3;
            }

            else if (E_Button.Checked)
            {
                key = 4;
            }

            else if (B_Button.Checked)
            {
                key = 5;
            }
            else if (fSharp_Button.Checked)
            {
                key = 6;
            }
            else
            {
                // cSharp_Button Checked

                key = 7;   
            }

            return key;
        }

        public int GetTimeSignature()
        {
            int time;

            if (SixEight_Button.Checked)
            {
                time = -2;
            }

            else
            {
                time = 0;
            }

            return time;
        }
    }
}

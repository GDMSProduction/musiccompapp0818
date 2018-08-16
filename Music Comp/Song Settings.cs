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
            int check;

            if (cFlat_Button.Checked)
            {
                cFlat_Button.Checked = true;
                check = -7;
            }

            else if (gFlat_Button.Checked)
            {
                check = -6;
            }

            else if (dFlat_Button.Checked)
            {
                check = -5;
            }

            else if (aFlat_Button.Checked)
            {
                check = -4;
            }

            else if (eFlat_Button.Checked)
            {
                check = -3;
            }

            else if (bFlat_Button.Checked)
            {
                check = -2;
            }

            else if (F_Button.Checked)
            {
                check = -1;
            }

            else if (C_Button.Checked)
            {
                check = 0;
            }

            else if (G_Button.Checked)
            {
                check = 1;
            }

            else if (D_Button.Checked)
            {
                check = 2;
            }

            else if (A_Button.Checked)
            {
                check = 3;
            }

            else if (E_Button.Checked)
            {
                check = 4;
            }

            else if (B_Button.Checked)
            {
                check = 5;
            }
            else if (fSharp_Button.Checked)
            {
                check = 6;
            }
            else
            {
                // cSharp_Button Checked

                check = 7;   
            }

            return check;
        }

    }
}

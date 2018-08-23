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

        public Key GetKeySignature()
        {
            Key key;

            if (cFlat_Button.Checked)
            {
                key = Key.Cflat;
            }

            else if (gFlat_Button.Checked)
            {
                key = Key.Gflat;
            }

            else if (dFlat_Button.Checked)
            {
                key = Key.Dflat;
            }

            else if (aFlat_Button.Checked)
            {
                key = Key.Aflat;
            }

            else if (eFlat_Button.Checked)
            {
                key = Key.Eflat;
            }

            else if (bFlat_Button.Checked)
            {
                key = Key.Bflat;
            }

            else if (F_Button.Checked)
            {
                key = Key.F;
            }

            else if (C_Button.Checked)
            {
                key = Key.C;
            }

            else if (G_Button.Checked)
            {
                key = Key.G;
            }

            else if (D_Button.Checked)
            {
                key = Key.D;
            }

            else if (A_Button.Checked)
            {
                key = Key.A;
            }

            else if (E_Button.Checked)
            {
                key = Key.E;
            }

            else if (B_Button.Checked)
            {
                key = Key.B;
            }
            else if (fSharp_Button.Checked)
            {
                key = Key.Fsharp;
            }

            else if (cSharp_Button.Checked)
            {
                key = Key.Csharp;   
            }

            else
            {
                key = Key.C;
            }

            return key;
        }

        public Time GetTimeSignature()
        {
            Time time;

            if (SixEight_Button.Checked)
            {
                time = Time.SixEight;
            }

            else if (FourFour_Button.Checked)
            {
                time = Time.FourFour;
            }
            else
            {
                time = Time.FourFour;
            }

            return time;
        }

        public void SetKeySignatureButton(Key key)
        {
            
            if (key == Key.Cflat)
            {
                cFlat_Button.Checked = true;
            }

            else if (key == Key.Gflat)
            {
                gFlat_Button.Checked = true;
            }

            else if (key == Key.Dflat)
            {
                dFlat_Button.Checked = true;
            }

            else if (key == Key.Aflat)
            {
                aFlat_Button.Checked = true;
            }

            else if (key == Key.Eflat)
            {
                eFlat_Button.Checked = true;
            }

            else if (key == Key.Bflat)
            {
                bFlat_Button.Checked = true;
            }

            else if (key == Key.F)
            {
                F_Button.Checked = true;
            }

            else if (key == Key.C)
            {
                C_Button.Checked = true;
            }

            else if (key == Key.G)
            {
                G_Button.Checked = true;
            }

            else if (key == Key.D)
            {
                D_Button.Checked = true;
            }

            else if (key == Key.A)
            {
                A_Button.Checked = true;
            }

            else if (key == Key.E)
            {
                E_Button.Checked = true;
            }

            else if (key == Key.B)
            {
                B_Button.Checked = true;
            }
            else if (key == Key.Fsharp)
            {
                fSharp_Button.Checked = true;
            }
            else if (key == Key.Csharp)
            {
                cSharp_Button.Checked = true;
            }
        }

        public void SetTimeSignatureButton(Time time)
        {

            if (time == Time.SixEight)
            {
                SixEight_Button.Checked = true;
            }

            else if (time == Time.FourFour)
            {
                FourFour_Button.Checked = true;
            }

        }
    }
}

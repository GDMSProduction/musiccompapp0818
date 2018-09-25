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
    public partial class ViewTutorial : Form
    {
        public bool checktrue;
        public ViewTutorial()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checktrue = false;
            }
            else
            {
                checktrue = true;
            }
        }
    }
}

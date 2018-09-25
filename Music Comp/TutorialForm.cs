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
    public partial class TutorialForm : Form
    {
        public TutorialForm()
        {
            Bitmap rightarrow = new Bitmap(Properties.Resources.rightarrow, new Size(70, 70));
            Bitmap leftarrow = new Bitmap(Properties.Resources.rightarrow, new Size(70, 70));
            leftarrow.RotateFlip(RotateFlipType.RotateNoneFlipX);
            InitializeComponent();
            NextButton.Image = rightarrow;
            button5.Image = leftarrow;
            button1.Image = rightarrow;
            button2.Image = leftarrow;
            button4.Image = rightarrow;
            button3.Image = leftarrow;
            button6.Image = rightarrow;
            button7.Image = leftarrow;
            button8.Image = rightarrow;
            button9.Image = leftarrow;
            checkBox1.Checked = Properties.Settings.Default.AskForTutorial;
        }

        private void Page1_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
        }

        private void Page2_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
        }

        private void Page3_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
        }

        private void Page4_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
        }

        private void Page5_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
        }

        private void Page6_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectTab(5);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            Page2.Select();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(0);
            Page1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            Page3.Select();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(1);
            Page2.Select();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            Page4.Select();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(2);
            Page3.Select();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(4);
            Page5.Select();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl1.SelectTab(3);
            Page4.Select();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AskForTutorial = checkBox1.Checked;
        }
    }
}

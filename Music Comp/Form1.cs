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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen drawpen = new Pen(Color.Black, 2);
            Rectangle rectangle = new Rectangle(10, 50, panel1.Width - 20, panel1.Height / 2);
            e.Graphics.DrawRectangle(drawpen, rectangle);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
        }
    }
}

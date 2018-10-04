using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Music_Comp
{
    public partial class Startup : Form
    {
        public bool newinstrument;
        public int selected;

        public FileInfo[] files;

        DirectoryInfo info;

        public Startup()
        {
            InitializeComponent();
            info = new DirectoryInfo("songs");
            files = info.GetFiles("*.png");
            PictureBox[] pictureBoxes = new PictureBox[files.Length];
            int x = 120;
            int y = 40;
            for (int i = 0; i < files.Length; i++)
            {
                string filename = files[i].Name;
                pictureBoxes[i] = new PictureBox();
                pictureBoxes[i].Image = Image.FromFile("songs\\" + filename);
                pictureBoxes[i].Size = new Size(100, 100);
                pictureBoxes[i].Location = new Point(x, y);
                pictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBoxes[i].Name = "pictureBox" + i;
                if (filename == "new.png")
                {
                    pictureBoxes[i].Location = new Point(10, 40);
                }
                this.Controls.Add(pictureBoxes[i]);
                x = x + 110;
                if (x + 100 > Width)
                {
                    x = 10;
                    y = y + 110;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

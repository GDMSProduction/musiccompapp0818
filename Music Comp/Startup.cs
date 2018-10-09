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
        public string filePath;

        public FileInfo[] files;
        PictureBox[] pictureBoxes;

        DirectoryInfo info;

        public Startup()
        {
            InitializeComponent();
            Properties.Settings.Default.FirstLaunch = true;
            if (Properties.Settings.Default.FirstLaunch)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ValidateNames = false;
                fileDialog.CheckFileExists = false;
                fileDialog.CheckPathExists = true;
                fileDialog.FileName = "Folder Selection";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.directory = Path.GetDirectoryName(fileDialog.FileName);
                    Properties.Settings.Default.FirstLaunch = false;
                }
            }
            filePath = Properties.Settings.Default.directory;
            info = new DirectoryInfo(filePath);
            files = info.GetFiles("*.png");
            pictureBoxes = new PictureBox[files.Length];
            int x = 120;
            int y = 40;
            for (int i = 0; i < files.Length; i++)
            {
                string filename = files[i].Name;
                pictureBoxes[i] = new PictureBox();
                pictureBoxes[i].Image = Image.FromFile(filePath + "\\" + filename);
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
                if ((pictureBoxes[i].Location.X + 100 > button2.Location.X) && (pictureBoxes[i].Location.Y + 100 >= button2.Location.Y))
                {
                    button1.Location = new Point(button1.Location.X, button1.Location.Y + 120);
                    button2.Location = new Point(button2.Location.X, button2.Location.Y + 120);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Startup_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}

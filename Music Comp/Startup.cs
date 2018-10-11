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
        public string filename;

        public FileInfo[] files;
        Image[] images;
        Bitmap[] bitmaps;
        Button[] buttons;

        DirectoryInfo info;

        private Timer timer1;

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 20;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReturnButton();
        }

        public Startup()
        {
            InitTimer();
            InitializeComponent();
            if (Properties.Settings.Default.FirstLaunch || !new DirectoryInfo(Properties.Settings.Default.directory).Exists)
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
            images = new Image[files.Length];
            bitmaps = new Bitmap[files.Length];
            buttons = new Button[images.Length];
            int x = 105;
            int y = 10;
            Button newbutton = new Button();
            newbutton.Name = "newbutton";
            newbutton.Font = new Font("Microsoft Sans Serif", 60);
            newbutton.Text = "+";
            newbutton.TextAlign = ContentAlignment.MiddleLeft;
            newbutton.Size = new Size(85, 110);
            newbutton.Location = new Point(8, 8);
            this.Controls.Add(newbutton);
            for (int i = 0; i < images.Length; i++)
            {
                filename = files[i].Name;
                images[i] = Image.FromFile(filePath + "\\" + filename);
                buttons[i] = new Button();
                bitmaps[i] = new Bitmap(images[i], new Size(85, 110));
                buttons[i].Size = new Size(85, 110);
                buttons[i].Location = new Point(x, y);
                buttons[i].Name = filename;
                this.Controls.Add(buttons[i]);
                buttons[i].Image = bitmaps[i];
                x = x + 95;
                if (x + 88 > Width)
                {
                    x = 10;
                    y = y + 120;
                }
            }
            for (int i = 0; i < buttons.Length; ++i)
            {
                if (buttons[i].Location.Y + 110 > Height)
                {
                    Height = buttons[i].Location.Y + 150;
                    MinimumSize = new Size(Width, Height);
                }
                if (buttons[i].Location.X + buttons[i].Width > Width - 20)
                {
                    Width = buttons[i].Location.X + buttons[i].Width + 20;
                }
            }
            filename = "newbutton";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.height = Height;
            //Properties.Settings.Default.width = Width;
        }

        private void Startup_Resize(object sender, EventArgs e)
        {
            MinimumSize = new Size(0, 0);
            for (int i = 0; i < buttons.Length; ++i)
            {
                if (buttons[i].Location.Y + 160 > Height)
                {
                    Height = buttons[i].Location.Y + 150;
                    MinimumSize = new Size(Width, Height);
                }
                if (true)
                {

                }
            }
            Relocate();
            Invalidate();
        }

        private void Relocate()
        {
            int x = 105;
            int y = 10;
            for (int i = 0; i < buttons.Length; ++i)
            {
                buttons[i].Location = new Point(x, y);
                buttons[i].Location = new Point(x - 3, y - 3);
                x = x + 95;
                if (x + 88 > Width)
                {
                    x = 10;
                    y = y + 120;
                }
                if ((buttons[i].Location.X + 85 > button1.Location.X) && (buttons[i].Location.Y + 85 >= button1.Location.Y))
                {
                    Height = Height + 120;
                }
                button1.Location = new Point(Width - 199, Height - 90);
                button2.Location = new Point(Width - 110, Height - 90);
            }
        }

        private string ReturnButton()
        {
            if (ActiveControl != null && ActiveControl.Name != "button2" && ActiveControl.Name != "button1")
            {
                filename = ActiveControl.Name;
                if (filename != "newbutton")
                {
                    filename = filename.Remove(filename.Length - 4);
                }
                return filename;
            }
            else
            {
                return "";
            }
        }
    }
}

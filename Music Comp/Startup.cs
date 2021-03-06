﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace Music_Comp
{
    public partial class Startup : Form
    {
        public bool newinstrument;
        public int selected;
        public string filePath;
        public string filename;

        private bool checkexit = true;

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
            InitializeComponent();
            InitTimer();
            Image playimage = Properties.Resources.play;
            Bitmap playbitmap = new Bitmap(playimage, new Size(25, 25));
            PlayButton.Image = playbitmap;
            PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), Height - 90);
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
            groupBox1.Controls.Add(newbutton);
            for (int i = 0; i < images.Length; i++)
            {
                filename = files[i].Name;
                images[i] = Image.FromFile(filePath + "\\" + filename);
                buttons[i] = new Button();
                bitmaps[i] = new Bitmap(images[i], new Size(85, 110));
                buttons[i].Size = new Size(85, 110);
                buttons[i].Location = new Point(x, y);
                buttons[i].Name = filename;
                buttons[i].Image = bitmaps[i];
                groupBox1.Controls.Add(buttons[i]);
                x = x + 95;
                if (x + 88 > groupBox1.Width)
                {
                    x = 10;
                    y = y + 120;
                }
            }
            for (int i = 0; i < buttons.Length; ++i)
            {
                if (buttons[i].Location.Y >= button3.Location.Y - 5)
                {
                    Height = buttons[i].Location.Y + 150;
                    groupBox1.Height = Height - 95;
                    MinimumSize = new Size(Width, Height);
                }
                if (buttons[i].Location.X + buttons[i].Width > Width - 20)
                {
                    Width = buttons[i].Location.X + buttons[i].Width + 20;
                    groupBox1.Width = Width;
                }
            }
            filename = "newbutton";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            checkexit = false;
        }

        private void Startup_Resize(object sender, EventArgs e)
        {
            if (buttons != null)
            {
                MinimumSize = new Size(0, 0);
                for (int i = 0; i < buttons.Length; ++i)
                {
                    if (buttons[i].Location.Y + buttons[i].Height >= groupBox1.Height - 5)
                    {
                        Height = buttons[i].Location.Y + buttons[i].Height + 100;
                        groupBox1.Height = Height - 95;
                        groupBox1.Width = Width;
                        MinimumSize = new Size(Width, Height);
                        Relocate();
                        Invalidate();
                        return;
                    }
                }
                groupBox1.Height = Height - 95;
                groupBox1.Width = Width;
            }
            Relocate();
            Invalidate();
        }

        private void Relocate()
        {
            if (buttons != null)
            {
                int x = 105;
                int y = 10;
                for (int i = 0; i < buttons.Length; ++i)
                {
                    buttons[i].Location = new Point(x, y);
                    buttons[i].Location = new Point(x - 3, y - 3);
                    x = x + 95;
                    if (x + 100 > groupBox1.Width)
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
                    button3.Location = new Point(12, Height - 90);
                }
            }
        }

        private string ReturnButton()
        {
            if (ActiveControl != null && ActiveControl.Name != "button2" && ActiveControl.Name != "button1" && ActiveControl.Name != "button3" && ActiveControl.Name != "PlayButton")
            {
                filename = ActiveControl.Name;
                if (filename != "newbutton")
                    filename = filename.Remove(filename.Length - 4);
                return filename;
            }
            return "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ValidateNames = false;
            fileDialog.CheckFileExists = false;
            fileDialog.CheckPathExists = true;
            fileDialog.FileName = "Folder Selection";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.directory = Path.GetDirectoryName(fileDialog.FileName);
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Dispose();
                    bitmaps[i].Dispose();
                    images[i].Dispose();
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
            groupBox1.Controls.Add(newbutton);
            for (int i = 0; i < images.Length; i++)
            {
                filename = files[i].Name;
                images[i] = Image.FromFile(filePath + "\\" + filename);
                buttons[i] = new Button();
                bitmaps[i] = new Bitmap(images[i], new Size(85, 110));
                buttons[i].Size = new Size(85, 110);
                buttons[i].Location = new Point(x, y);
                buttons[i].Name = filename;
                buttons[i].Parent = groupBox1;
                groupBox1.Controls.Add(buttons[i]);
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
                if (buttons[i].Location.Y >= button3.Location.Y - 5)
                {
                    Height = buttons[i].Location.Y + 150;
                    groupBox1.Height = Height - 95;
                    MinimumSize = new Size(Width, Height);
                }
                if (buttons[i].Location.X + buttons[i].Width > Width - 20)
                {
                    Width = buttons[i].Location.X + buttons[i].Width + 20;
                    groupBox1.Width = Width;
                }
            }
            filename = "newbutton";
            Invalidate();
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkexit && !Properties.Settings.Default.Loaded)
                Application.Exit();
        }

        private void Startup_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Dispose();
                bitmaps[i].Dispose();
                images[i].Dispose();
            }
            Properties.Settings.Default.Save();
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].Name == filename + ".png")
                {
                    ActiveControl = buttons[i];
                }
            }
            if (filename != "newbutton" && filename != "button1" && filename != "button2" && filename != "button3" && filename != "PlayButton")
            {
                SoundPlayer sound = new SoundPlayer(filePath + "\\" + filename + ".wav");
                sound.Play();
            }
        }
    }
}

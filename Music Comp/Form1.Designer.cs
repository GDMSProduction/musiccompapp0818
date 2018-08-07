namespace Music_Comp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.graphicsPanel = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Add_Instrument = new System.Windows.Forms.ToolStripButton();
            this.composerTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.graphicsPanel.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.BackColor = System.Drawing.Color.White;
            this.graphicsPanel.Controls.Add(this.toolStrip1);
            this.graphicsPanel.Controls.Add(this.composerTextBox);
            this.graphicsPanel.Controls.Add(this.titleTextBox);
            this.graphicsPanel.Location = new System.Drawing.Point(0, 0);
            this.graphicsPanel.Margin = new System.Windows.Forms.Padding(4);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(3400, 4125);
            this.graphicsPanel.TabIndex = 0;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphicsPanel_Paint);
            this.graphicsPanel.Resize += new System.EventHandler(this.graphicsPanel_Resize);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_Instrument});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(3400, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Add_Instrument
            // 
            this.Add_Instrument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Add_Instrument.Image = ((System.Drawing.Image)(resources.GetObject("Add_Instrument.Image")));
            this.Add_Instrument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_Instrument.Name = "Add_Instrument";
            this.Add_Instrument.Size = new System.Drawing.Size(28, 28);
            this.Add_Instrument.Text = "toolStripButton1";
            this.Add_Instrument.Click += new System.EventHandler(this.Add_Instrument_Click);
            // 
            // composerTextBox
            // 
            this.composerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.composerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composerTextBox.Location = new System.Drawing.Point(637, 106);
            this.composerTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.composerTextBox.MaxLength = 20;
            this.composerTextBox.Name = "composerTextBox";
            this.composerTextBox.Size = new System.Drawing.Size(271, 61);
            this.composerTextBox.TabIndex = 1;
            this.composerTextBox.TabStop = false;
            this.composerTextBox.Text = "John Doe";
            this.composerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // titleTextBox
            // 
            this.titleTextBox.AcceptsReturn = true;
            this.titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTextBox.Location = new System.Drawing.Point(16, 15);
            this.titleTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.titleTextBox.MaximumSize = new System.Drawing.Size(2667, 200);
            this.titleTextBox.MaxLength = 20;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(680, 182);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.TabStop = false;
            this.titleTextBox.Text = "My Song 1";
            this.titleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zoomInButton
            // 
            this.zoomInButton.BackColor = System.Drawing.Color.White;
            this.zoomInButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.zoomInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomInButton.Location = new System.Drawing.Point(1200, 562);
            this.zoomInButton.Margin = new System.Windows.Forms.Padding(4);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(100, 94);
            this.zoomInButton.TabIndex = 1;
            this.zoomInButton.Text = "+";
            this.zoomInButton.UseVisualStyleBackColor = false;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.BackColor = System.Drawing.Color.White;
            this.zoomOutButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.zoomOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomOutButton.Location = new System.Drawing.Point(1200, 688);
            this.zoomOutButton.Margin = new System.Windows.Forms.Padding(4);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(100, 94);
            this.zoomOutButton.TabIndex = 2;
            this.zoomOutButton.Text = "-";
            this.zoomOutButton.UseVisualStyleBackColor = false;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1472, 848);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.zoomInButton);
            this.Controls.Add(this.graphicsPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Music Composition App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.graphicsPanel.ResumeLayout(false);
            this.graphicsPanel.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel graphicsPanel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox composerTextBox;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Add_Instrument;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
    }
}


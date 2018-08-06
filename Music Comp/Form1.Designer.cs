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
            this.graphicsPanel = new System.Windows.Forms.Panel();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.composerTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.graphicsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.BackColor = System.Drawing.Color.White;
            this.graphicsPanel.Controls.Add(this.composerTextBox);
            this.graphicsPanel.Controls.Add(this.titleTextBox);
            this.graphicsPanel.Location = new System.Drawing.Point(0, 0);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(2500, 2500);
            this.graphicsPanel.TabIndex = 0;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphicsPanel_Paint);
            this.graphicsPanel.Resize += new System.EventHandler(this.graphicsPanel_Resize);
            // 
            // zoomInButton
            // 
            this.zoomInButton.BackColor = System.Drawing.Color.White;
            this.zoomInButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.zoomInButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomInButton.Location = new System.Drawing.Point(900, 450);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(75, 75);
            this.zoomInButton.TabIndex = 1;
            this.zoomInButton.Text = "+";
            this.zoomInButton.UseVisualStyleBackColor = false;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // composerTextBox
            // 
            this.composerTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.composerTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composerTextBox.Location = new System.Drawing.Point(478, 85);
            this.composerTextBox.MaxLength = 20;
            this.composerTextBox.Name = "composerTextBox";
            this.composerTextBox.Size = new System.Drawing.Size(203, 46);
            this.composerTextBox.TabIndex = 1;
            this.composerTextBox.TabStop = false;
            this.composerTextBox.Text = "Composer";
            this.composerTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // titleTextBox
            // 
            this.titleTextBox.AcceptsReturn = true;
            this.titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleTextBox.Location = new System.Drawing.Point(12, 12);
            this.titleTextBox.MaximumSize = new System.Drawing.Size(2000, 200);
            this.titleTextBox.MaxLength = 20;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(460, 136);
            this.titleTextBox.TabIndex = 0;
            this.titleTextBox.TabStop = false;
            this.titleTextBox.Text = "TITLE";
            this.titleTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.BackColor = System.Drawing.Color.White;
            this.zoomOutButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.zoomOutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoomOutButton.Location = new System.Drawing.Point(900, 550);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(75, 75);
            this.zoomOutButton.TabIndex = 2;
            this.zoomOutButton.Text = "-";
            this.zoomOutButton.UseVisualStyleBackColor = false;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1078, 652);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.zoomInButton);
            this.Controls.Add(this.graphicsPanel);
            this.Name = "MainForm";
            this.Text = "Music Composition App";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.graphicsPanel.ResumeLayout(false);
            this.graphicsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel graphicsPanel;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox composerTextBox;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.Button zoomOutButton;
    }
}


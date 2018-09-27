namespace Music_Comp
{
    partial class Startup
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
            this.okButton = new System.Windows.Forms.Button();
            this.titleBoxLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.composerBoxLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AddInstrumentButton = new System.Windows.Forms.Button();
            this.removeInstrumentButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.composerTextBox = new System.Windows.Forms.TextBox();
            this.KeyBox = new System.Windows.Forms.ComboBox();
            this.TimeBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.graphicsPanel = new Music_Comp.GraphicsPanel();
            this.composerLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.graphicsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.okButton.Location = new System.Drawing.Point(1628, 939);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(94, 48);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // titleBoxLabel
            // 
            this.titleBoxLabel.AutoSize = true;
            this.titleBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBoxLabel.Location = new System.Drawing.Point(1495, 240);
            this.titleBoxLabel.Name = "titleBoxLabel";
            this.titleBoxLabel.Size = new System.Drawing.Size(62, 25);
            this.titleBoxLabel.TabIndex = 1;
            this.titleBoxLabel.Text = "Title *";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1495, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1495, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time";
            // 
            // composerBoxLabel
            // 
            this.composerBoxLabel.AutoSize = true;
            this.composerBoxLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composerBoxLabel.Location = new System.Drawing.Point(1495, 293);
            this.composerBoxLabel.Name = "composerBoxLabel";
            this.composerBoxLabel.Size = new System.Drawing.Size(116, 25);
            this.composerBoxLabel.TabIndex = 4;
            this.composerBoxLabel.Text = "Composer *";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1559, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Instruments";
            // 
            // AddInstrumentButton
            // 
            this.AddInstrumentButton.Location = new System.Drawing.Point(1500, 63);
            this.AddInstrumentButton.Name = "AddInstrumentButton";
            this.AddInstrumentButton.Size = new System.Drawing.Size(108, 38);
            this.AddInstrumentButton.TabIndex = 6;
            this.AddInstrumentButton.Text = "Add";
            this.AddInstrumentButton.UseVisualStyleBackColor = true;
            this.AddInstrumentButton.Click += new System.EventHandler(this.AddInstrumentButton_Click);
            // 
            // removeInstrumentButton
            // 
            this.removeInstrumentButton.Location = new System.Drawing.Point(1614, 63);
            this.removeInstrumentButton.Name = "removeInstrumentButton";
            this.removeInstrumentButton.Size = new System.Drawing.Size(108, 38);
            this.removeInstrumentButton.TabIndex = 7;
            this.removeInstrumentButton.Text = "Remove";
            this.removeInstrumentButton.UseVisualStyleBackColor = true;
            this.removeInstrumentButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(1614, 241);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(100, 26);
            this.titleTextBox.TabIndex = 8;
            this.titleTextBox.TextChanged += new System.EventHandler(this.titleTextBox_TextChanged);
            // 
            // composerTextBox
            // 
            this.composerTextBox.Location = new System.Drawing.Point(1614, 294);
            this.composerTextBox.Name = "composerTextBox";
            this.composerTextBox.Size = new System.Drawing.Size(100, 26);
            this.composerTextBox.TabIndex = 11;
            this.composerTextBox.TextChanged += new System.EventHandler(this.composerTextBox_TextChanged);
            // 
            // KeyBox
            // 
            this.KeyBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KeyBox.FormattingEnabled = true;
            this.KeyBox.Items.AddRange(new object[] {
            "Cflat",
            "Gflat",
            "Dflat",
            "Aflat",
            "Eflat",
            "Bflat",
            "F",
            "C",
            "G",
            "D",
            "A",
            "E",
            "B",
            "Fsharp",
            "Csharp"});
            this.KeyBox.Location = new System.Drawing.Point(1614, 145);
            this.KeyBox.Name = "KeyBox";
            this.KeyBox.Size = new System.Drawing.Size(100, 28);
            this.KeyBox.TabIndex = 4;
            this.KeyBox.Text = "C";
            this.KeyBox.SelectedIndexChanged += new System.EventHandler(this.KeyBox_SelectedIndexChanged);
            // 
            // TimeBox
            // 
            this.TimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeBox.FormattingEnabled = true;
            this.TimeBox.Items.AddRange(new object[] {
            "FourFour",
            "SixEight"});
            this.TimeBox.Location = new System.Drawing.Point(1614, 191);
            this.TimeBox.Name = "TimeBox";
            this.TimeBox.Size = new System.Drawing.Size(100, 28);
            this.TimeBox.TabIndex = 13;
            this.TimeBox.Text = "FourFour";
            this.TimeBox.SelectedIndexChanged += new System.EventHandler(this.TimeBox_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(1528, 939);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(94, 48);
            this.cancelButton.TabIndex = 14;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.BackColor = System.Drawing.SystemColors.Window;
            this.graphicsPanel.Controls.Add(this.composerLabel);
            this.graphicsPanel.Controls.Add(this.titleLabel);
            this.graphicsPanel.Location = new System.Drawing.Point(12, 12);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(1431, 975);
            this.graphicsPanel.TabIndex = 12;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.graphicsPanel1_Paint);
            // 
            // composerLabel
            // 
            this.composerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.composerLabel.AutoSize = true;
            this.composerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composerLabel.Location = new System.Drawing.Point(1299, 148);
            this.composerLabel.Name = "composerLabel";
            this.composerLabel.Size = new System.Drawing.Size(0, 37);
            this.composerLabel.TabIndex = 3;
            this.composerLabel.SizeChanged += new System.EventHandler(this.composerLabel_SizeChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(606, 70);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(0, 82);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.SizeChanged += new System.EventHandler(this.titleLabel_SizeChanged);
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(1734, 999);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.KeyBox);
            this.Controls.Add(this.graphicsPanel);
            this.Controls.Add(this.composerTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.removeInstrumentButton);
            this.Controls.Add(this.AddInstrumentButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.composerBoxLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.titleBoxLabel);
            this.MinimumSize = new System.Drawing.Size(970, 580);
            this.Name = "Startup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Startup_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Startup_FormClosed);
            this.SizeChanged += new System.EventHandler(this.Startup_SizeChanged);
            this.graphicsPanel.ResumeLayout(false);
            this.graphicsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label titleBoxLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label composerBoxLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AddInstrumentButton;
        private System.Windows.Forms.Button removeInstrumentButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox composerTextBox;
        private GraphicsPanel graphicsPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.ComboBox KeyBox;
        private System.Windows.Forms.ComboBox TimeBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label composerLabel;
    }
}
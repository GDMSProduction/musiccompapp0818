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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.AddInstrumentButton = new System.Windows.Forms.Button();
            this.removeInstrumentButton = new System.Windows.Forms.Button();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.composerTextBox = new System.Windows.Forms.TextBox();
            this.KeyBox = new System.Windows.Forms.ComboBox();
            this.TimeBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.graphicsPanel1 = new Music_Comp.GraphicsPanel();
            this.composerLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.graphicsPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(1628, 939);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(94, 48);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Key";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Time";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(255, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Composer";
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
            this.titleTextBox.Location = new System.Drawing.Point(102, 36);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(100, 26);
            this.titleTextBox.TabIndex = 8;
            this.titleTextBox.TextChanged += new System.EventHandler(this.titleTextBox_TextChanged);
            // 
            // composerTextBox
            // 
            this.composerTextBox.Location = new System.Drawing.Point(383, 36);
            this.composerTextBox.Name = "composerTextBox";
            this.composerTextBox.Size = new System.Drawing.Size(100, 26);
            this.composerTextBox.TabIndex = 11;
            this.composerTextBox.TextChanged += new System.EventHandler(this.composerTextBox_TextChanged);
            // 
            // KeyBox
            // 
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
            this.KeyBox.Location = new System.Drawing.Point(102, 77);
            this.KeyBox.Name = "KeyBox";
            this.KeyBox.Size = new System.Drawing.Size(100, 28);
            this.KeyBox.TabIndex = 4;
            this.KeyBox.Text = "C";
            this.KeyBox.SelectedIndexChanged += new System.EventHandler(this.KeyBox_SelectedIndexChanged);
            // 
            // TimeBox
            // 
            this.TimeBox.FormattingEnabled = true;
            this.TimeBox.Items.AddRange(new object[] {
            "FourFour",
            "SixEight"});
            this.TimeBox.Location = new System.Drawing.Point(102, 120);
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
            // graphicsPanel1
            // 
            this.graphicsPanel1.AutoScroll = true;
            this.graphicsPanel1.Controls.Add(this.composerLabel);
            this.graphicsPanel1.Controls.Add(this.titleLabel);
            this.graphicsPanel1.Location = new System.Drawing.Point(208, 119);
            this.graphicsPanel1.Name = "graphicsPanel1";
            this.graphicsPanel1.Size = new System.Drawing.Size(1314, 868);
            this.graphicsPanel1.TabIndex = 12;
            this.graphicsPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.graphicsPanel1_Paint);
            // 
            // composerLabel
            // 
            this.composerLabel.AutoSize = true;
            this.composerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.composerLabel.Location = new System.Drawing.Point(1229, 70);
            this.composerLabel.Name = "composerLabel";
            this.composerLabel.Size = new System.Drawing.Size(0, 55);
            this.composerLabel.TabIndex = 3;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(606, 70);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(0, 82);
            this.titleLabel.TabIndex = 2;
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(1734, 999);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.TimeBox);
            this.Controls.Add(this.KeyBox);
            this.Controls.Add(this.graphicsPanel1);
            this.Controls.Add(this.composerTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.removeInstrumentButton);
            this.Controls.Add(this.AddInstrumentButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.okButton);
            this.Name = "Startup";
            this.Text = "Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Startup_FormClosing);
            this.graphicsPanel1.ResumeLayout(false);
            this.graphicsPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button AddInstrumentButton;
        private System.Windows.Forms.Button removeInstrumentButton;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox composerTextBox;
        private GraphicsPanel graphicsPanel1;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label composerLabel;
        private System.Windows.Forms.ComboBox KeyBox;
        private System.Windows.Forms.ComboBox TimeBox;
        private System.Windows.Forms.Button cancelButton;
    }
}
namespace Music_Comp
{
    partial class AddInstrumentForm
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
            this.Brace = new System.Windows.Forms.RadioButton();
            this.Bracket = new System.Windows.Forms.RadioButton();
            this.None = new System.Windows.Forms.RadioButton();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.lStaves = new System.Windows.Forms.Label();
            this.StaveNumeric = new System.Windows.Forms.NumericUpDown();
            this.Stave1clef = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.St1Clef = new System.Windows.Forms.ComboBox();
            this.St2Clef = new System.Windows.Forms.ComboBox();
            this.St3Clef = new System.Windows.Forms.ComboBox();
            this.St4Clef = new System.Windows.Forms.ComboBox();
            this.Wave1 = new System.Windows.Forms.Label();
            this.Wave3 = new System.Windows.Forms.Label();
            this.Wave4 = new System.Windows.Forms.Label();
            this.Wave2 = new System.Windows.Forms.Label();
            this.Wavebox1 = new System.Windows.Forms.ComboBox();
            this.Wavebox2 = new System.Windows.Forms.ComboBox();
            this.Wavebox3 = new System.Windows.Forms.ComboBox();
            this.Wavebox4 = new System.Windows.Forms.ComboBox();
            this.graphicsPanel = new Music_Comp.GraphicsPanel();
            ((System.ComponentModel.ISupportInitialize)(this.StaveNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // Brace
            // 
            this.Brace.AutoSize = true;
            this.Brace.Location = new System.Drawing.Point(634, 48);
            this.Brace.Name = "Brace";
            this.Brace.Size = new System.Drawing.Size(76, 24);
            this.Brace.TabIndex = 0;
            this.Brace.Text = "Brace";
            this.Brace.UseVisualStyleBackColor = true;
            this.Brace.CheckedChanged += new System.EventHandler(this.Brace_CheckedChanged);
            // 
            // Bracket
            // 
            this.Bracket.AutoSize = true;
            this.Bracket.Location = new System.Drawing.Point(634, 106);
            this.Bracket.Name = "Bracket";
            this.Bracket.Size = new System.Drawing.Size(89, 24);
            this.Bracket.TabIndex = 1;
            this.Bracket.Text = "Bracket";
            this.Bracket.UseVisualStyleBackColor = true;
            this.Bracket.CheckedChanged += new System.EventHandler(this.Bracket_CheckedChanged);
            // 
            // None
            // 
            this.None.AutoSize = true;
            this.None.Checked = true;
            this.None.Location = new System.Drawing.Point(634, 170);
            this.None.Name = "None";
            this.None.Size = new System.Drawing.Size(72, 24);
            this.None.TabIndex = 2;
            this.None.TabStop = true;
            this.None.Text = "None";
            this.None.UseVisualStyleBackColor = true;
            this.None.CheckedChanged += new System.EventHandler(this.None_CheckedChanged);
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(693, 395);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(95, 43);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.Location = new System.Drawing.Point(592, 395);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(95, 43);
            this.Cancel.TabIndex = 3;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // lStaves
            // 
            this.lStaves.AutoSize = true;
            this.lStaves.Location = new System.Drawing.Point(485, 50);
            this.lStaves.Name = "lStaves";
            this.lStaves.Size = new System.Drawing.Size(58, 20);
            this.lStaves.TabIndex = 7;
            this.lStaves.Text = "Staves";
            // 
            // StaveNumeric
            // 
            this.StaveNumeric.Location = new System.Drawing.Point(344, 48);
            this.StaveNumeric.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.StaveNumeric.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StaveNumeric.Name = "StaveNumeric";
            this.StaveNumeric.Size = new System.Drawing.Size(121, 26);
            this.StaveNumeric.TabIndex = 8;
            this.StaveNumeric.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StaveNumeric.ValueChanged += new System.EventHandler(this.StaveNumeric_ValueChanged);
            // 
            // Stave1clef
            // 
            this.Stave1clef.AutoSize = true;
            this.Stave1clef.Location = new System.Drawing.Point(340, 81);
            this.Stave1clef.Name = "Stave1clef";
            this.Stave1clef.Size = new System.Drawing.Size(95, 20);
            this.Stave1clef.TabIndex = 9;
            this.Stave1clef.Text = "Stave 1 Clef";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Stave 2 Clef";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Stave 3 Clef";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(340, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Stave 4 Clef";
            // 
            // St1Clef
            // 
            this.St1Clef.FormattingEnabled = true;
            this.St1Clef.Items.AddRange(new object[] {
            "Treble",
            "Alto",
            "Tenor",
            "Bass"});
            this.St1Clef.Location = new System.Drawing.Point(489, 73);
            this.St1Clef.Name = "St1Clef";
            this.St1Clef.Size = new System.Drawing.Size(121, 28);
            this.St1Clef.TabIndex = 13;
            this.St1Clef.Text = "Treble";
            this.St1Clef.SelectedIndexChanged += new System.EventHandler(this.St1Clef_SelectedIndexChanged);
            // 
            // St2Clef
            // 
            this.St2Clef.Enabled = false;
            this.St2Clef.FormattingEnabled = true;
            this.St2Clef.Items.AddRange(new object[] {
            "Treble",
            "Alto",
            "Tenor",
            "Bass"});
            this.St2Clef.Location = new System.Drawing.Point(489, 106);
            this.St2Clef.Name = "St2Clef";
            this.St2Clef.Size = new System.Drawing.Size(121, 28);
            this.St2Clef.TabIndex = 14;
            this.St2Clef.SelectedIndexChanged += new System.EventHandler(this.St2Clef_SelectedIndexChanged);
            // 
            // St3Clef
            // 
            this.St3Clef.Enabled = false;
            this.St3Clef.FormattingEnabled = true;
            this.St3Clef.Items.AddRange(new object[] {
            "Treble",
            "Alto",
            "Tenor",
            "Bass"});
            this.St3Clef.Location = new System.Drawing.Point(489, 140);
            this.St3Clef.Name = "St3Clef";
            this.St3Clef.Size = new System.Drawing.Size(121, 28);
            this.St3Clef.TabIndex = 15;
            this.St3Clef.SelectedIndexChanged += new System.EventHandler(this.St3Clef_SelectedIndexChanged);
            // 
            // St4Clef
            // 
            this.St4Clef.Enabled = false;
            this.St4Clef.FormattingEnabled = true;
            this.St4Clef.Items.AddRange(new object[] {
            "Treble",
            "Alto",
            "Tenor",
            "Bass"});
            this.St4Clef.Location = new System.Drawing.Point(489, 175);
            this.St4Clef.Name = "St4Clef";
            this.St4Clef.Size = new System.Drawing.Size(121, 28);
            this.St4Clef.TabIndex = 16;
            this.St4Clef.SelectedIndexChanged += new System.EventHandler(this.St4Clef_SelectedIndexChanged);
            // 
            // Wave1
            // 
            this.Wave1.AutoSize = true;
            this.Wave1.Location = new System.Drawing.Point(340, 217);
            this.Wave1.Name = "Wave1";
            this.Wave1.Size = new System.Drawing.Size(139, 20);
            this.Wave1.TabIndex = 18;
            this.Wave1.Text = "Stave 1 Waveform";
            // 
            // Wave3
            // 
            this.Wave3.AutoSize = true;
            this.Wave3.Location = new System.Drawing.Point(340, 285);
            this.Wave3.Name = "Wave3";
            this.Wave3.Size = new System.Drawing.Size(139, 20);
            this.Wave3.TabIndex = 19;
            this.Wave3.Text = "Stave 3 Waveform";
            // 
            // Wave4
            // 
            this.Wave4.AutoSize = true;
            this.Wave4.Location = new System.Drawing.Point(340, 319);
            this.Wave4.Name = "Wave4";
            this.Wave4.Size = new System.Drawing.Size(139, 20);
            this.Wave4.TabIndex = 20;
            this.Wave4.Text = "Stave 4 Waveform";
            // 
            // Wave2
            // 
            this.Wave2.AutoSize = true;
            this.Wave2.Location = new System.Drawing.Point(340, 251);
            this.Wave2.Name = "Wave2";
            this.Wave2.Size = new System.Drawing.Size(139, 20);
            this.Wave2.TabIndex = 21;
            this.Wave2.Text = "Stave 2 Waveform";
            // 
            // Wavebox1
            // 
            this.Wavebox1.FormattingEnabled = true;
            this.Wavebox1.Items.AddRange(new object[] {
            "Sine",
            "Square",
            "Sawtooth",
            "Triangle",
            "Noise"});
            this.Wavebox1.Location = new System.Drawing.Point(489, 209);
            this.Wavebox1.Name = "Wavebox1";
            this.Wavebox1.Size = new System.Drawing.Size(121, 28);
            this.Wavebox1.TabIndex = 24;
            this.Wavebox1.Text = "Sine";
            this.Wavebox1.SelectedIndexChanged += new System.EventHandler(this.Wavebox1_SelectedIndexChanged);
            // 
            // Wavebox2
            // 
            this.Wavebox2.Enabled = false;
            this.Wavebox2.FormattingEnabled = true;
            this.Wavebox2.Items.AddRange(new object[] {
            "Sine",
            "Square",
            "Sawtooth",
            "Triangle",
            "Noise"});
            this.Wavebox2.Location = new System.Drawing.Point(489, 243);
            this.Wavebox2.Name = "Wavebox2";
            this.Wavebox2.Size = new System.Drawing.Size(121, 28);
            this.Wavebox2.TabIndex = 25;
            this.Wavebox2.SelectedIndexChanged += new System.EventHandler(this.Wavebox2_SelectedIndexChanged);
            // 
            // Wavebox3
            // 
            this.Wavebox3.Enabled = false;
            this.Wavebox3.FormattingEnabled = true;
            this.Wavebox3.Items.AddRange(new object[] {
            "Sine",
            "Square",
            "Sawtooth",
            "Triangle",
            "Noise"});
            this.Wavebox3.Location = new System.Drawing.Point(489, 277);
            this.Wavebox3.Name = "Wavebox3";
            this.Wavebox3.Size = new System.Drawing.Size(121, 28);
            this.Wavebox3.TabIndex = 26;
            this.Wavebox3.SelectedIndexChanged += new System.EventHandler(this.Wavebox3_SelectedIndexChanged);
            // 
            // Wavebox4
            // 
            this.Wavebox4.Enabled = false;
            this.Wavebox4.FormattingEnabled = true;
            this.Wavebox4.Items.AddRange(new object[] {
            "Sine",
            "Square",
            "Sawtooth",
            "Triangle",
            "Noise"});
            this.Wavebox4.Location = new System.Drawing.Point(489, 311);
            this.Wavebox4.Name = "Wavebox4";
            this.Wavebox4.Size = new System.Drawing.Size(121, 28);
            this.Wavebox4.TabIndex = 27;
            this.Wavebox4.SelectedIndexChanged += new System.EventHandler(this.Wavebox4_SelectedIndexChanged);
            // 
            // graphicsPanel
            // 
            this.graphicsPanel.BackColor = System.Drawing.SystemColors.Control;
            this.graphicsPanel.Location = new System.Drawing.Point(0, 0);
            this.graphicsPanel.Name = "graphicsPanel";
            this.graphicsPanel.Size = new System.Drawing.Size(313, 380);
            this.graphicsPanel.TabIndex = 0;
            this.graphicsPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.AddInstrumentForm_Paint);
            // 
            // AddInstrumentForm
            // 
            this.AcceptButton = this.OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Wavebox4);
            this.Controls.Add(this.Wavebox3);
            this.Controls.Add(this.Wavebox2);
            this.Controls.Add(this.Wavebox1);
            this.Controls.Add(this.Wave2);
            this.Controls.Add(this.Wave4);
            this.Controls.Add(this.Wave3);
            this.Controls.Add(this.Wave1);
            this.Controls.Add(this.St4Clef);
            this.Controls.Add(this.St3Clef);
            this.Controls.Add(this.St2Clef);
            this.Controls.Add(this.St1Clef);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Stave1clef);
            this.Controls.Add(this.StaveNumeric);
            this.Controls.Add(this.lStaves);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.None);
            this.Controls.Add(this.Bracket);
            this.Controls.Add(this.Brace);
            this.Controls.Add(this.graphicsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(822, 506);
            this.MinimumSize = new System.Drawing.Size(822, 506);
            this.Name = "AddInstrumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddInstrument";
            this.SizeChanged += new System.EventHandler(this.AddInstrumentForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.StaveNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Music_Comp.GraphicsPanel graphicsPanel;
        private System.Windows.Forms.RadioButton Brace;
        private System.Windows.Forms.RadioButton Bracket;
        private System.Windows.Forms.RadioButton None;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label lStaves;
        private System.Windows.Forms.NumericUpDown StaveNumeric;
        private System.Windows.Forms.Label Stave1clef;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox St1Clef;
        private System.Windows.Forms.ComboBox St2Clef;
        private System.Windows.Forms.ComboBox St3Clef;
        private System.Windows.Forms.ComboBox St4Clef;
        private System.Windows.Forms.Label Wave1;
        private System.Windows.Forms.Label Wave3;
        private System.Windows.Forms.Label Wave4;
        private System.Windows.Forms.Label Wave2;
        private System.Windows.Forms.ComboBox Wavebox1;
        private System.Windows.Forms.ComboBox Wavebox2;
        private System.Windows.Forms.ComboBox Wavebox3;
        private System.Windows.Forms.ComboBox Wavebox4;
    }
}
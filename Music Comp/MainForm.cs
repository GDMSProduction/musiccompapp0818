using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Music_Comp
{
    public partial class MainForm : Form
    {
        float SCREEN_WIDTH;
        float PAGE_WIDTH;
        float PAGE_HEIGHT;
        float _SCALE;

        Song song;

        int selectedStaff = 0;
        int selectedInstrument = 0;

        Chord mChord = new Chord();
        int noteIndex = 0;

        Duration currentDuration = Duration.Quarter;

        public MainForm()
        {
            InitializeComponent();

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * _SCALE);
            titleTextBox.Size = new Size((int)(1470 * _SCALE), (int)(140 * _SCALE));
            titleTextBox.Location = new Point((int)(PAGE_WIDTH / 2 - titleTextBox.Width / 2), (int)(120 * _SCALE));

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * _SCALE);
            composerTextBox.Size = new Size((int)(615 * _SCALE), (int)(50 * _SCALE));
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE));

            ActiveControl = graphicsPanel;

            (graphicsPanel as Control).KeyUp += new KeyEventHandler(graphicsPanel_KeyUp);

            Startup startup = new Startup();
            startup.ShowDialog();

            if (startup.DialogResult == DialogResult.OK)
            {
                Song.SCREEN_WIDTH = startup.mainSCREEN_WIDTH;
                Song.PAGE_WIDTH = startup.mainPAGE_WIDTH;
                Song._SCALE = startup.main_SCALE;
                Song.TOP_MARGIN = startup.mainTOP_MARGIN;
                Song.LEFT_MARGIN = startup.mainLEFT_MARGIN;
                Song.RIGHT_MARGIN = startup.mainRIGHT_MARGIN;
                Song.STAFF_SPACING = startup.mainSTAFF_SPACING;
                Song.INSTRUMENT_SPACING = startup.mainINSTRUMENT_SPACING;

                Song.TOTAL_INSTRUMENTS = startup.mainTOTAL_INSTRUMENTS;
                Song.TOTAL_STAVES = startup.mainTOTAL_STAVES;

                Song.cursorY = startup.maincursorY;
                Song.cursorX = startup.maincursorX;

                Staff.LINE_SPACING = startup.mainLINE_SPACING;
                Staff.LENGTH = startup.mainLENGTH;
                Staff.HEIGHT = startup.mainHEIGHT;

                song = new Song(PAGE_WIDTH, startup.key, startup.time);
                for (int i = 0; i < startup.instruments.Count; i++)
                    song.AddInstrument(startup.instruments[i].clefs, startup.instruments[i].grouping);
                titleTextBox.Text = startup.title;
                composerTextBox.Text = startup.composer;
            }
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            zoomInButton.Location = new Point(Width - 110, Height - 200);
            zoomOutButton.Location = new Point(Width - 110, Height - 150);
            if (song != null && Song.TOTAL_INSTRUMENTS != 0)
            {
                song.Update();
                song.Draw(e);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (Width > PAGE_WIDTH)
                graphicsPanel.Location = new Point((int)(Width / 2 - PAGE_WIDTH / 2), 0);
            else
                graphicsPanel.Location = new Point(0, 0);
        }

        private void graphicsPanel_Resize(object sender, EventArgs e)
        {
            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * _SCALE);
            titleTextBox.Size = new Size((int)(1470 * _SCALE), (int)(140 * _SCALE));
            titleTextBox.Location = new Point((int)(PAGE_WIDTH / 2 - titleTextBox.Width / 2), (int)(120 * _SCALE));

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * _SCALE);
            composerTextBox.Size = new Size((int)(615 * _SCALE), (int)(50 * _SCALE));
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE));

            Song.PAGE_WIDTH = graphicsPanel.Width;
            Song._SCALE = _SCALE;
            Song.TOP_MARGIN = 300 * _SCALE;
            Song.LEFT_MARGIN = 100 * _SCALE;
            Song.RIGHT_MARGIN = 50 * _SCALE;
            Song.STAFF_SPACING = 60 * _SCALE;
            Song.INSTRUMENT_SPACING = 80 * _SCALE;

            Staff.LINE_SPACING = 30 * _SCALE;
            Staff.LENGTH = Song.PAGE_WIDTH - Song.LEFT_MARGIN - Song.RIGHT_MARGIN;
            Staff.HEIGHT = 4 * Staff.LINE_SPACING;

            if (Width > graphicsPanel.Width)
                graphicsPanel.Location = new Point(Width / 2 - graphicsPanel.Width / 2, 0);
            else
                graphicsPanel.Location = new Point(0, 0);

            graphicsPanel.Invalidate();
        }

        private void zoomInButton_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            if (Song.PAGE_WIDTH < 5000)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH + 100), (int)(PAGE_HEIGHT + 100 * PAGE_HEIGHT / PAGE_WIDTH));
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            if (Song.PAGE_WIDTH > 500)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH - 100), (int)(PAGE_HEIGHT - 100 * PAGE_HEIGHT / PAGE_WIDTH));
            if (Song.mBarlines != null)
                for (int i = 0; i < Song.mBarlines.Count; i++)
                    if (_SCALE < 1)
                        Song.mBarlines[i] *= _SCALE;
                    else
                        Song.mBarlines[i] /= _SCALE;
        }

        private void Add_Instrument_Click(object sender, EventArgs e)
        {
            AddInstrumentForm options = new AddInstrumentForm();
            options.ShowDialog();

            Song.SCREEN_WIDTH = options.mainSCREEN_WIDTH;
            Song.PAGE_WIDTH = options.mainPAGE_WIDTH;
            Song._SCALE = options.main_SCALE;
            Song.TOP_MARGIN = options.mainTOP_MARGIN;
            Song.LEFT_MARGIN = options.mainLEFT_MARGIN;
            Song.RIGHT_MARGIN = options.mainRIGHT_MARGIN;
            Song.STAFF_SPACING = options.mainSTAFF_SPACING;
            Song.INSTRUMENT_SPACING = options.mainINSTRUMENT_SPACING;

            Song.TOTAL_INSTRUMENTS = options.mainTOTAL_INSTRUMENTS;
            Song.TOTAL_STAVES = options.mainTOTAL_STAVES;

            Song.cursorY = options.maincursorY;
            Song.cursorX = options.maincursorX;

            Staff.LINE_SPACING = options.mainLINE_SPACING;
            Staff.LENGTH = options.mainLENGTH;
            Staff.HEIGHT = options.mainHEIGHT;

            if (options.DialogResult == DialogResult.OK)
                song.AddInstrument(options.clefs, options.grouping);

            graphicsPanel.Invalidate();
        }

        private void graphicsPanel_KeyUp(object sender, KeyEventArgs e)
        {
            bool valid = false;

            if (!ControlCheck())
            {
                Chord chord = new Chord();
                chord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));

                switch (e.KeyCode)
                {
                    case Keys.Tab:
                        if (!ShiftCheck())
                        {
                            switch (currentDuration)
                            {
                                case Duration.Sixteenth:
                                    currentDuration = Duration.Eighth;
                                    break;
                                case Duration.Eighth:
                                    currentDuration = Duration.Quarter;
                                    break;
                                case Duration.Quarter:
                                    currentDuration = Duration.Half;
                                    break;
                                case Duration.Half:
                                    currentDuration = Duration.Whole;
                                    break;
                                case Duration.Whole:
                                    currentDuration = Duration.Sixteenth;
                                    break;
                            }
                        }
                        else if (ShiftCheck())
                        {
                            switch (currentDuration)
                            {
                                case Duration.Sixteenth:
                                    currentDuration = Duration.Whole;
                                    break;
                                case Duration.Eighth:
                                    currentDuration = Duration.Sixteenth;
                                    break;
                                case Duration.Quarter:
                                    currentDuration = Duration.Eighth;
                                    break;
                                case Duration.Half:
                                    currentDuration = Duration.Quarter;
                                    break;
                                case Duration.Whole:
                                    currentDuration = Duration.Half;
                                    break;
                            }
                        }
                            break;

                    case Keys.Up:
                        if (selectedStaff > 0)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff--).SetActive(false);
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate();
                        }
                        else if (selectedStaff == 0 && selectedInstrument != 0)
                        {
                            song.GetInstrument(selectedInstrument--).GetStaff(selectedStaff).SetActive(false);
                            selectedStaff = song.GetInstrument(selectedInstrument).GetNumberOfStaves() - 1;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate();
                        }
                        break;
                    case Keys.Down:
                        if (selectedStaff < song.GetInstrument(selectedInstrument).GetNumberOfStaves() - 1)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff++).SetActive(false);
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate();
                        }
                        else if (selectedStaff == song.GetInstrument(selectedInstrument).GetNumberOfStaves() - 1 &&
                                 selectedInstrument != Song.TOTAL_INSTRUMENTS - 1)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            selectedStaff = 0;
                            selectedInstrument++;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate();
                        }
                        break;

                    case Keys.A:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.A);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.B:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.B);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.C:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.C);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.D:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.D);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.E:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.E);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.F:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.F);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.G:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.G);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                    case Keys.R:
                        valid = true;
                        chord.GetNote(0).SetPitch(Pitch.Rest);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    chord.GetNote(0).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    chord.GetNote(0).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    chord.GetNote(0).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        break;
                }
                if (valid)
                {
                    Chord remainder =
                    song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetNextMeasure().AddChord(chord);
                    song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetNextMeasure().AddChord(remainder);
                    graphicsPanel.Invalidate();
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.A);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.B:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.B);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.C:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.C);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.D:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.D);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.E:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.E);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.F:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.F);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                    case Keys.G:
                        valid = true;
                        mChord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));
                        mChord.GetNote(noteIndex).SetPitch(Pitch.G);
                        if (ShiftCheck())
                            switch (currentDuration)
                            {
                                case Duration.Eighth:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedEighth);
                                    break;
                                case Duration.Quarter:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedQuarter);
                                    break;
                                case Duration.Half:
                                    mChord.GetNote(noteIndex).SetDuration(Duration.DottedHalf);
                                    break;
                                default:
                                    break;
                            }
                        noteIndex++;
                        break;
                }
            }
            if (!ControlCheck() && noteIndex != 0)
            {
                Chord remainder =
                song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetNextMeasure().AddChord(mChord);
                song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetNextMeasure().AddChord(remainder);
                graphicsPanel.Invalidate();

                mChord = new Chord();
                noteIndex = 0;
            }
        }

        private bool ControlCheck()
        {
            return (ModifierKeys & Keys.Control) != 0;
        }
        private bool ShiftCheck()
        {
            return (ModifierKeys & Keys.Shift) != 0;
        }

        private void graphicsPanel_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
                WindowState = FormWindowState.Normal;
            else
                WindowState = FormWindowState.Maximized;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm options = new AboutForm();
            options.ShowDialog();
        }

        private void songToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Song_Settings SongSettingsMenu = new Song_Settings();

            SongSettingsMenu.SetKeySignatureButton(Song.KEY);
            SongSettingsMenu.SetTimeSignatureButton(Song.TIME);

            if (SongSettingsMenu.ShowDialog() == DialogResult.OK)
            {
                song.Transpose(SongSettingsMenu.GetKeySignature());
                song.EditTimeSignature(SongSettingsMenu.GetTimeSignature());
                graphicsPanel.Invalidate();
            }
        }
    }
}

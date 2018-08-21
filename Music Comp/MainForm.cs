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

        Song_Settings KeySignatureMenu = new Song_Settings();

        List<Note> notes = new List<Note>();

        int noteIndex = 0;

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

            song = new Song(PAGE_WIDTH, Key.Eflat, Time.Common);
            song.AddInstrument(Clef.Treble, Clef.Bass, Grouping.Brace);
            ActiveControl = graphicsPanel;

            (graphicsPanel as Control).KeyUp += new KeyEventHandler(graphicsPanel_KeyUp);
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            zoomInButton.Location = new Point(Width - 110, Height - 200);
            zoomOutButton.Location = new Point(Width - 110, Height - 150);
            song.Draw(e);
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
            if (Song.PAGE_WIDTH < 5000)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH + 100), (int)(PAGE_HEIGHT + 100 * PAGE_HEIGHT / PAGE_WIDTH));
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            if (Song.PAGE_WIDTH > 500)
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH - 100), (int)(PAGE_HEIGHT - 100 * PAGE_HEIGHT / PAGE_WIDTH));
        }

        private void Add_Instrument_Click(object sender, EventArgs e)
        {
            AddInstrumentForm options = new AddInstrumentForm();
            options.ShowDialog();
            if (options.DialogResult == DialogResult.OK)
            {
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

                switch (options.StaveCount)
                {
                    case 1:
                        song.AddInstrument(options.clefs[0], options.grouping);
                        break;
                    case 2:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.grouping);
                        break;
                    case 3:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.clefs[2], options.grouping);
                        break;
                    case 4:
                        song.AddInstrument(options.clefs[0], options.clefs[1], options.clefs[2], options.clefs[3], options.grouping);
                        break;
                }
            }
            else
            {
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
            }
            graphicsPanel.Invalidate();
        }

        private void graphicsPanel_KeyUp(object sender, KeyEventArgs e)
        {
            bool valid = false;

            if (!ControlCheck())
            {
                Note[] notes = new Note[1];
                notes[0] = new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4);
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (selectedStaff > 0)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff--).SetActive(false);
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate();
                        }
                        else if (selectedStaff == 0 && selectedInstrument != 0)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            selectedInstrument--;
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
                        else if (selectedStaff == song.GetInstrument(selectedInstrument).GetNumberOfStaves() - 1 && selectedInstrument != Song.TOTAL_INSTRUMENTS - 1)
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
                        notes[0].SetPitch(Pitch.A);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.B:
                        valid = true;
                        notes[0].SetPitch(Pitch.B);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.C:
                        valid = true;
                        notes[0].SetPitch(Pitch.C);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.D:
                        valid = true;
                        notes[0].SetPitch(Pitch.D);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.E:
                        valid = true;
                        notes[0].SetPitch(Pitch.E);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.F:
                        valid = true;
                        notes[0].SetPitch(Pitch.F);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.G:
                        valid = true;
                        notes[0].SetPitch(Pitch.G);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                    case Keys.R:
                        valid = true;
                        notes[0].SetPitch(Pitch.Rest);
                        if (ShiftCheck())
                            notes[0].SetDuration(Duration.Half);
                        break;
                }

                if (valid)
                {
                    song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetMeasure(0).AddNote(notes);
                    graphicsPanel.Invalidate();
                }
            }
            else if (ControlCheck())
            {

                    switch (e.KeyCode)
                    {
                        case Keys.A:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.A);
                        if (ShiftCheck())
                            notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                            break;
                        case Keys.B:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.B);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                        case Keys.C:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.C);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                        case Keys.D:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.D);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                        case Keys.E:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.E);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                        case Keys.F:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.F);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                        case Keys.G:
                            valid = true;
                        notes.Add(new Note(Pitch.C, Accidental.Natural, Duration.Quarter, 4));
                        notes[noteIndex].SetPitch(Pitch.G);
                            if (ShiftCheck())
                                notes[noteIndex].SetDuration(Duration.Half);
                            noteIndex++;
                        break;
                    } 
            }

            if (!ControlCheck() && noteIndex != 0)
            {
                Note[] notesArr = new Note[notes.Count];
                for (int i = 0; i < notes.Count; i++)
                    notesArr[i] = notes[i];
                notes = new List<Note>();
                noteIndex = 0;
                song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetMeasure(0).AddNote(notesArr);
                graphicsPanel.Invalidate();
            }
        }

        private bool ControlCheck()
        {
            return ModifierKeys == Keys.Control;
        }
        private bool ShiftCheck()
        {
            return ModifierKeys == Keys.Shift;
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
            if (DialogResult.OK == KeySignatureMenu.ShowDialog())
            {
                song.Transpose((Key)KeySignatureMenu.GetKeySignature());
                song.EditTimeSignature((Time)KeySignatureMenu.GetTimeSignature());
                graphicsPanel.Invalidate();
            }
        }

    }
}

using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace Music_Comp
{
    public partial class MainForm : Form
    {
        float SCREEN_WIDTH;
        float PAGE_WIDTH;
        float PAGE_HEIGHT;
        float _SCALE;

        bool lcheck = false;
        bool playcheck = false;

        Song song;

        int selectedStaff = 0;
        int selectedInstrument = 0;

        Chord mChord = new Chord();
        int noteIndex = 0;

        Duration currentDuration = Duration.Quarter;

        Bitmap quarter = new Bitmap(Properties.Resources.Note, new Size(65, 100));
        Bitmap half = new Bitmap(Properties.Resources.HalfNote, new Size(80, 80));
        Bitmap eighth = new Bitmap(Properties.Resources.EighthNote, new Size(55, 80));
        Bitmap sixteenth = new Bitmap(Properties.Resources.SixteenthNote, new Size(80, 80));
        Bitmap whole = new Bitmap(Properties.Resources.WholeNote, new Size(70, 40));

        Bitmap play = new Bitmap(Properties.Resources.play, new Size(13, 13));
        Bitmap pause = new Bitmap(Properties.Resources.pause, new Size(13, 13));

        public MainForm()
        {
            InitializeComponent();

            ViewTutorial tutorial = new ViewTutorial();
            tutorial.ShowDialog();
            if (tutorial.DialogResult == DialogResult.OK)
            {
                TutorialForm tutform = new TutorialForm();
                tutform.ShowDialog();
            }

            PlayButton.Image = play;
            PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), 0);

            songdur1.Size = SongDuration.Size;
            songdur1.Image = half;
            songdur2.Size = SongDuration.Size;
            songdur2.Image = whole;
            songdur3.Size = SongDuration.Size;
            songdur3.Image = eighth;
            songdur4.Size = SongDuration.Size;
            songdur4.Image = sixteenth;

            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);

            SCREEN_WIDTH = Screen.PrimaryScreen.Bounds.Width;
            PAGE_WIDTH = graphicsPanel.Width;
            PAGE_HEIGHT = graphicsPanel.Height;

            _SCALE = PAGE_WIDTH / SCREEN_WIDTH;

            titleTextBox.Font = new Font("Microsoft Sans Serif", 70 * _SCALE);
            titleTextBox.Size = new Size((int)(1470 * _SCALE), (int)(140 * _SCALE));
            titleTextBox.Location = new Point((int)(PAGE_WIDTH / 2 - titleTextBox.Width / 2), (int)(120 * _SCALE));

            composerTextBox.Font = new Font("Microsoft Sans Serif", 25 * _SCALE);
            composerTextBox.Size = new Size((int)(615 * _SCALE), (int)(50 * _SCALE));
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE)          );

            ActiveControl = graphicsPanel;

            (graphicsPanel as Control).KeyUp += new KeyEventHandler(graphicsPanel_KeyUp);

            SongDuration.Image = quarter;

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
        
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }

        private void graphicsPanel_Paint(object sender, PaintEventArgs e)
        {
            zoomInButton.Location = new Point(Width - 110, Height - 200);
            zoomOutButton.Location = new Point(Width - 110, Height - 150);
            SongDuration.Location = new Point(20, Height - 220);
            PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), 0);
            if (lcheck)
            {
                songdur1.Location = new Point(SongDuration.Width + 20, Height - 220);
                songdur2.Location = new Point(SongDuration.Width * 2 + 20, Height - 220);
                songdur3.Location = new Point(SongDuration.Width * 3 + 20, Height - 220);
                songdur4.Location = new Point(SongDuration.Width * 4 + 20, Height - 220);
            }
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
            if (Song.BARLINES != null)
                for (int i = 0; i < Song.BARLINES.Count; i++)
                    if (_SCALE < 1)
                        Song.BARLINES[i] *= _SCALE;
                    else
                        Song.BARLINES[i] /= _SCALE;
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
            if (playcheck == true && e.KeyCode != Keys.Space)
            {
                return;
            }

            bool valid = false;

            if ((song == null || Song.TOTAL_INSTRUMENTS == 0) && e.KeyCode != Keys.Tab && e.KeyCode != Keys.L)
            {
                return;
            }

            if (!ControlCheck())
            {
                Chord chord = new Chord();
                chord.Add(new Note(Pitch.C, Accidental.Natural, currentDuration, 4));

                switch (e.KeyCode)
                {
                    case Keys.L:
                        {
                            if (lcheck == false)
                            {
                                switch (currentDuration)
                                {
                                    case Duration.Sixteenth:
                                        songdur1.Image = eighth;
                                        songdur2.Image = quarter;
                                        songdur3.Image = half;
                                        songdur4.Image = whole;
                                        break;
                                    case Duration.Eighth:
                                        songdur1.Image = quarter;
                                        songdur2.Image = half;
                                        songdur3.Image = whole;
                                        songdur4.Image = sixteenth;
                                        break;
                                    case Duration.Quarter:
                                        songdur1.Image = half;
                                        songdur2.Image = whole;
                                        songdur3.Image = sixteenth;
                                        songdur4.Image = eighth;
                                        break;
                                    case Duration.Half:
                                        songdur1.Image = whole;
                                        songdur2.Image = sixteenth;
                                        songdur3.Image = eighth;
                                        songdur4.Image = quarter;
                                        break;
                                    case Duration.Whole:
                                        songdur1.Image = sixteenth;
                                        songdur2.Image = eighth;
                                        songdur3.Image = quarter;
                                        songdur4.Image = half;
                                        break;
                                }
                                songdur1.Location = new Point(SongDuration.Width + 20, Height - 220);
                                songdur2.Location = new Point(songdur1.Width * 2 + 20, Height - 220);
                                songdur3.Location = new Point(songdur1.Width * 3 + 20, Height - 220);
                                songdur4.Location = new Point(songdur1.Width * 4 + 20, Height - 220);
                                lcheck = true;
                            }
                            else if (lcheck)
                            {
                                switch (currentDuration)
                                {
                                    case Duration.Sixteenth:
                                        SongDuration.Image = sixteenth;
                                        break;
                                    case Duration.Eighth:
                                        SongDuration.Image = eighth;
                                        break;
                                    case Duration.Quarter:
                                        SongDuration.Image = quarter;
                                        break;
                                    case Duration.Half:
                                        SongDuration.Image = half;
                                        break;
                                    case Duration.Whole:
                                        SongDuration.Image = whole;
                                        break;
                                }
                                songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
                                lcheck = false;
                            }
                            break;
                        }
                    case Keys.Tab:
                        if (!ShiftCheck())
                        {
                            switch (currentDuration)
                            {
                                case Duration.Sixteenth:
                                    currentDuration = Duration.Eighth;
                                    SongDuration.Image = eighth;
                                    break;
                                case Duration.Eighth:
                                    currentDuration = Duration.Quarter;
                                    SongDuration.Image = quarter;
                                    break;
                                case Duration.Quarter:
                                    currentDuration = Duration.Half;
                                    SongDuration.Image = half;
                                    break;
                                case Duration.Half:
                                    currentDuration = Duration.Whole;
                                    SongDuration.Image = whole;
                                    break;
                                case Duration.Whole:
                                    currentDuration = Duration.Sixteenth;
                                    SongDuration.Image = sixteenth;
                                    break;
                            }
                        }
                        else if (ShiftCheck())
                        {
                            switch (currentDuration)
                            {
                                case Duration.Sixteenth:
                                    currentDuration = Duration.Whole;
                                    SongDuration.Image = whole;
                                    break;
                                case Duration.Eighth:
                                    currentDuration = Duration.Sixteenth;
                                    SongDuration.Image = sixteenth;
                                    break;
                                case Duration.Quarter:
                                    currentDuration = Duration.Eighth;
                                    SongDuration.Image = eighth;
                                    break;
                                case Duration.Half:
                                    currentDuration = Duration.Quarter;
                                    SongDuration.Image = quarter;
                                    break;
                                case Duration.Whole:
                                    currentDuration = Duration.Half;
                                    SongDuration.Image = half;
                                    break;
                            }
                        }
                        break;

                    case Keys.Up:
                        if (selectedStaff > 0)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                            selectedStaff--;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                        }
                        else if (selectedStaff == 0 && selectedInstrument != 0)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                            selectedInstrument--;
                            selectedStaff = song.GetInstrument(selectedInstrument).GetStaffCount() - 1;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                        }
                        break;
                    case Keys.Down:
                        if (selectedStaff < song.GetInstrument(selectedInstrument).GetStaffCount() - 1)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                            selectedStaff++;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                        }
                        else if (selectedStaff == song.GetInstrument(selectedInstrument).GetStaffCount() - 1 &&
                                 selectedInstrument != Song.TOTAL_INSTRUMENTS - 1)
                        {
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(false);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                            selectedStaff = 0;
                            selectedInstrument++;
                            song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).SetActive(true);
                            graphicsPanel.Invalidate(new Region(song.GetInstrument(selectedInstrument).GetStaff(selectedStaff).GetArea()));
                        }
                        break;
                    case Keys.Back:
                        Staff staff = song.GetInstrument(selectedInstrument).GetStaff(selectedStaff);
                        if (!staff.GetCurrentMeasure().IsEmpty())
                        {
                            staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                            staff.Update();
                            graphicsPanel.Invalidate(new Region(staff.GetArea()));
                        }
                        else if (staff.GetCurrentMeasure().IsEmpty() && staff.GetMeasuresCount() != 1)
                        {
                            staff.RemoveMeasure(staff.GetCurrentMeasure());
                            staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                            //Song.BARLINES.Remove(Song.BARLINES.Count - 1);
                            staff.Update();
                            graphicsPanel.Invalidate(new Region(staff.GetArea()));
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
                    case Keys.Space:
                        {
                            Play();
                            break;
                        }
                    
                }
                if (valid)
                {
                    Staff staff = song.GetInstrument(selectedInstrument).GetStaff(selectedStaff);
                    Chord remainder = staff.GetNextMeasure().AddChord(chord);
                    chord.Play();
                    if (remainder != null)
                        staff.GetNextMeasure().AddChord(remainder);
                    staff.Update();
                    graphicsPanel.Invalidate(new Region(staff.GetArea()));
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
                Staff staff = song.GetInstrument(selectedInstrument).GetStaff(selectedStaff);
                Chord remainder = staff.GetNextMeasure().AddChord(mChord);
                mChord.Play();
                if (remainder != null)
                    staff.GetNextMeasure().AddChord(remainder);
                staff.Update();
                graphicsPanel.Invalidate(new Region(staff.GetArea()));

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
            foreach (SongComponent selectable in Song.SELECTABLES)
            {
                selectable.Deselect();
                if (selectable.GetArea().Contains(graphicsPanel.PointToClient(Cursor.Position)))
                    selectable.Select();
            }
            graphicsPanel.Invalidate();
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
                graphicsPanel.Invalidate(new Region(song.GetArea()));
            }
        }

        private void SongDuration_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void SongDuration_MouseClick(object sender, MouseEventArgs e)
        {
            ActiveControl = graphicsPanel;

            if (playcheck == true)
            {
                return;
            }

            if (lcheck)
            {
                songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
                lcheck = false;
                return;
            }

            if (!ShiftCheck())
            {
                switch (currentDuration)
                {
                    case Duration.Sixteenth:
                        currentDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Eighth:
                        currentDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Duration.Quarter:
                        currentDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                    case Duration.Half:
                        currentDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Whole:
                        currentDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                }
            }
            else if (ShiftCheck())
            {
                switch (currentDuration)
                {
                    case Duration.Sixteenth:
                        currentDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Eighth:
                        currentDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                    case Duration.Quarter:
                        currentDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Half:
                        currentDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Duration.Whole:
                        currentDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                }
            }
        }

        private void songdur1_Click(object sender, EventArgs e)
        {
            //eighth
            //quarter
            //half
            //whole
            //sixteenth
            ActiveControl = graphicsPanel;
            switch (currentDuration)
            {
                case Duration.Sixteenth:
                    currentDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    break;
                case Duration.Eighth:
                    currentDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    break;
                case Duration.Quarter:
                    currentDuration = Duration.Half;
                    SongDuration.Image = half;
                    break;
                case Duration.Half:
                    currentDuration = Duration.Whole;
                    SongDuration.Image = whole;
                    break;
                case Duration.Whole:
                    currentDuration = Duration.Sixteenth;
                    SongDuration.Image = sixteenth;
                    break;
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur2_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            switch (currentDuration)
            {
                //eighth
                //quarter
                //half
                //whole
                //sixteenth
                case Duration.Sixteenth:
                    currentDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    break;
                case Duration.Eighth:
                    currentDuration = Duration.Half;
                    SongDuration.Image = half;
                    break;
                case Duration.Quarter:
                    currentDuration = Duration.Whole;
                    SongDuration.Image = whole;
                    break;
                case Duration.Half:
                    currentDuration = Duration.Sixteenth;
                    SongDuration.Image = sixteenth;
                    break;
                case Duration.Whole:
                    currentDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    break;
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur3_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            switch (currentDuration)
            {
                case Duration.Sixteenth:
                    currentDuration = Duration.Half;
                    SongDuration.Image = half;
                    break;
                case Duration.Eighth:
                    currentDuration = Duration.Whole;
                    SongDuration.Image = whole;
                    break;
                case Duration.Quarter:
                    currentDuration = Duration.Sixteenth;
                    SongDuration.Image = sixteenth;
                    break;
                case Duration.Half:
                    currentDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    break;
                case Duration.Whole:
                    currentDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    break;
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur4_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            switch (currentDuration)
            {
                case Duration.Sixteenth:
                    currentDuration = Duration.Whole;
                    SongDuration.Image = whole;
                    break;
                case Duration.Eighth:
                    currentDuration = Duration.Sixteenth;
                    SongDuration.Image = sixteenth;
                    break;
                case Duration.Quarter:
                    currentDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    break;
                case Duration.Half:
                    currentDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    break;
                case Duration.Whole:
                    currentDuration = Duration.Half;
                    SongDuration.Image = half;
                    break;
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void composerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void titleTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TutorialForm tutorial = new TutorialForm();
            tutorial.ShowDialog();
        }

        private void newSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            song = new Song(PAGE_WIDTH);
            graphicsPanel.Invalidate();
        }

        private void Play()
        {
            ActiveControl = graphicsPanel;
            if (playcheck == false)
            {
                PlayButton.Image = pause;
                titleTextBox.Enabled = false;
                composerTextBox.Enabled = false;
                playcheck = true;
                song.Play();
            }
            else
            {
                PlayButton.Image = play;
                titleTextBox.Enabled = true;
                composerTextBox.Enabled = true;
                playcheck = false;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            Play();
        }
    }
}

using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Timers;
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

        System.Timers.Timer timer;

        int noteIndex = 0;
        Chord mChord = new Chord(0);

        Duration currentNoteDuration = Duration.Quarter;


        Bitmap quarter = new Bitmap(Properties.Resources.Note, new Size(65, 100));
        Bitmap half = new Bitmap(Properties.Resources.HalfNote, new Size(80, 80));
        Bitmap eighth = new Bitmap(Properties.Resources.EighthNote, new Size(55, 80));
        Bitmap sixteenth = new Bitmap(Properties.Resources.SixteenthNote, new Size(80, 80));
        Bitmap whole = new Bitmap(Properties.Resources.WholeNote, new Size(70, 40));
        Bitmap dottedquarter = new Bitmap(Properties.Resources.dottedquarter, new Size(65, 105));
        Bitmap dottedhalf = new Bitmap(Properties.Resources.dottedhalf, new Size(55, 90));
        Bitmap dottedeighth = new Bitmap(Properties.Resources.dottedeighth, new Size(105, 100));

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

            timer = new System.Timers.Timer();
            timer.Elapsed += soundPlayer_Finished;

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
            composerTextBox.Location = new Point((int)(PAGE_WIDTH - composerTextBox.Width - 100 * _SCALE), (int)(220 * _SCALE));

            ActiveControl = graphicsPanel;

            (graphicsPanel as Control).KeyUp += new KeyEventHandler(graphicsPanel_KeyUp);
            (graphicsPanel as Control).KeyDown += new KeyEventHandler(graphicsPanel_KeyDown);

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

                switch (Song.TIME)
                {
                    case Time.FourFour:
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Time.SixEight:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                }

                for (int i = 0; i < startup.instruments.Count; i++)
                    song.AddInstrument(startup.instruments[i].clefs, startup.instruments[i].waveForms, startup.instruments[i].grouping);
                titleTextBox.Text = startup.title;
                composerTextBox.Text = startup.composer;
            }
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
                if (Song.TIME == Time.SixEight)
                {
                    songdur4.Location = new Point(0, -200);
                }
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

            Song.BARLINES = options.mainBARLINES;
            Song.SELECTABLES = options.mainSELECTABLES;

            if (options.DialogResult == DialogResult.OK)
                song.AddInstrument(options.clefs, options.waveForms, options.grouping);

            graphicsPanel.Invalidate();
        }

        private void graphicsPanel_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            PointF CursorPosition = graphicsPanel.PointToClient(Cursor.Position);

            Instrument newInstrument = null;
            Staff newStaff = null;
            Measure newMeasure = null;
            Chord newChord = null;

            for (int i = 0; i < Song.SELECTABLES.Count; i++)
            {
                if (Song.SELECTABLES[i].GetArea().Width == 0)
                {
                    Song.SELECTABLES.RemoveAt(i--);
                    continue;
                }

                Song.SELECTABLES[i].Deselect();
                if (Song.SELECTABLES[i].GetArea().Contains(CursorPosition))
                {
                    if (Song.SELECTABLES[i].GetType() == typeof(Instrument))
                        newInstrument = (Instrument)Song.SELECTABLES[i];
                    else if (Song.SELECTABLES[i].GetType() == typeof(Staff))
                        newStaff = (Staff)Song.SELECTABLES[i];
                    else if (Song.SELECTABLES[i].GetType() == typeof(Measure))
                        newMeasure = (Measure)Song.SELECTABLES[i];
                    else if (Song.SELECTABLES[i].GetType() == typeof(Chord))
                        newChord = (Chord)Song.SELECTABLES[i];
                }
            }
            if (newInstrument != null)
                song.SetSelection(newInstrument);
            if (newStaff != null)
                newInstrument.SetSelection(newStaff);
            if (newMeasure != null)
                newStaff.SetSelection(newMeasure);
            if (newChord != null)
                newMeasure.SetSelection(newChord);

            graphicsPanel.Invalidate();
        }

        private void graphicsPanel_KeyUp(object sender, KeyEventArgs e)
        {
            if (playcheck == true && e.KeyCode != Keys.Space)
                return;

            if ((song == null || Song.TOTAL_INSTRUMENTS == 0) && e.KeyCode != Keys.Tab && e.KeyCode != Keys.L && e.KeyCode != Keys.ShiftKey)
                return;

            bool valid = false;

            if (!ControlCheck())
            {
                Chord chord = new Chord(0);
                chord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, 4, 0));

                switch (e.KeyCode)
                {
                    case Keys.ShiftKey:
                        {
                            if (Song.TIME == Time.FourFour)
                            {
                                switch (currentNoteDuration)
                                {
                                    case Duration.Sixteenth:
                                        {
                                            SongDuration.Image = sixteenth;
                                            songdur1.Image = eighth;
                                            songdur2.Image = quarter;
                                            songdur3.Image = half;
                                            songdur4.Image = whole;
                                            break;
                                        }
                                    case Duration.Eighth:
                                        {
                                            SongDuration.Image = eighth;
                                            songdur1.Image = quarter;
                                            songdur2.Image = half;
                                            songdur3.Image = whole;
                                            songdur4.Image = sixteenth;
                                            break;
                                        }
                                    case Duration.Quarter:
                                        {
                                            SongDuration.Image = quarter;
                                            songdur1.Image = half;
                                            songdur2.Image = whole;
                                            songdur3.Image = sixteenth;
                                            songdur4.Image = eighth;
                                            break;
                                        }
                                    case Duration.Half:
                                        {
                                            SongDuration.Image = half;
                                            songdur1.Image = whole;
                                            songdur2.Image = sixteenth;
                                            songdur3.Image = eighth;
                                            songdur4.Image = quarter;
                                            break;
                                        }
                                    case Duration.Whole:
                                        {
                                            SongDuration.Image = whole;
                                            songdur1.Image = sixteenth;
                                            songdur2.Image = eighth;
                                            songdur3.Image = quarter;
                                            songdur4.Image = half;
                                            break;
                                        }
                                    case Duration.DottedHalf:
                                        {
                                            SongDuration.Image = half;
                                            songdur1.Image = whole;
                                            songdur2.Image = sixteenth;
                                            songdur3.Image = eighth;
                                            songdur4.Image = quarter;
                                            break;
                                        }
                                    case Duration.DottedEighth:
                                        {
                                            SongDuration.Image = eighth;
                                            songdur1.Image = quarter;
                                            songdur2.Image = half;
                                            songdur3.Image = whole;
                                            songdur4.Image = sixteenth;
                                            break;
                                        }
                                    case Duration.DottedQuarter:
                                        {
                                            SongDuration.Image = quarter;
                                            songdur1.Image = half;
                                            songdur2.Image = whole;
                                            songdur3.Image = sixteenth;
                                            songdur4.Image = eighth;
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                switch (currentNoteDuration)
                                {
                                    case Duration.Sixteenth:
                                        {
                                            SongDuration.Image = sixteenth;
                                            songdur1.Image = eighth;
                                            songdur2.Image = dottedquarter;
                                            songdur3.Image = dottedhalf;
                                            break;
                                        }
                                    case Duration.Eighth:
                                        {
                                            SongDuration.Image = eighth;
                                            songdur1.Image = dottedquarter;
                                            songdur2.Image = dottedhalf;
                                            songdur3.Image = sixteenth;
                                            break;
                                        }
                                    case Duration.Quarter:
                                        {
                                            SongDuration.Image = dottedquarter;
                                            songdur1.Image = dottedhalf;
                                            songdur2.Image = sixteenth;
                                            songdur3.Image = eighth;
                                            break;
                                        }
                                    case Duration.Half:
                                        {
                                            SongDuration.Image = dottedhalf;
                                            songdur1.Image = sixteenth;
                                            songdur2.Image = eighth;
                                            songdur3.Image = dottedquarter;
                                            break;
                                        }
                                    case Duration.DottedQuarter:
                                        {
                                            SongDuration.Image = dottedquarter;
                                            songdur1.Image = dottedhalf;
                                            songdur2.Image = sixteenth;
                                            songdur3.Image = eighth;
                                            break;
                                        }
                                    case Duration.DottedHalf:
                                        {
                                            SongDuration.Image = dottedhalf;
                                            songdur1.Image = sixteenth;
                                            songdur2.Image = eighth;
                                            songdur3.Image = dottedquarter;
                                            break;
                                        }
                                    case Duration.DottedEighth:
                                        {
                                            SongDuration.Image = eighth;
                                            songdur1.Image = dottedquarter;
                                            songdur2.Image = dottedhalf;
                                            songdur3.Image = sixteenth;
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case Keys.L:
                        {
                            if (!lcheck)
                            {
                                if (Song.TIME == Time.FourFour)
                                {
                                    if (!ShiftCheck())
                                    {

                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                songdur1.Image = eighth;
                                                songdur2.Image = quarter;
                                                songdur3.Image = half;
                                                songdur4.Image = whole;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = eighth;
                                                songdur1.Image = quarter;
                                                songdur2.Image = half;
                                                songdur3.Image = whole;
                                                songdur4.Image = sixteenth;
                                                break;
                                            case Duration.Quarter:
                                                SongDuration.Image = quarter;
                                                songdur1.Image = half;
                                                songdur2.Image = whole;
                                                songdur3.Image = sixteenth;
                                                songdur4.Image = eighth;
                                                break;
                                            case Duration.Half:
                                                SongDuration.Image = half;
                                                songdur1.Image = whole;
                                                songdur2.Image = sixteenth;
                                                songdur3.Image = eighth;
                                                songdur4.Image = quarter;
                                                break;
                                            case Duration.Whole:
                                                SongDuration.Image = whole;
                                                songdur1.Image = sixteenth;
                                                songdur2.Image = eighth;
                                                songdur3.Image = quarter;
                                                songdur4.Image = half;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                songdur1.Image = dottedeighth;
                                                songdur2.Image = dottedquarter;
                                                songdur3.Image = dottedhalf;
                                                songdur4.Image = whole;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = dottedeighth;
                                                songdur1.Image = dottedquarter;
                                                songdur2.Image = dottedhalf;
                                                songdur3.Image = whole;
                                                songdur4.Image = sixteenth;
                                                break;
                                            case Duration.Quarter:
                                                SongDuration.Image = dottedquarter;
                                                songdur1.Image = dottedhalf;
                                                songdur2.Image = whole;
                                                songdur3.Image = sixteenth;
                                                songdur4.Image = dottedeighth;
                                                break;
                                            case Duration.Half:
                                                SongDuration.Image = dottedhalf;
                                                songdur1.Image = whole;
                                                songdur2.Image = sixteenth;
                                                songdur3.Image = dottedeighth;
                                                songdur4.Image = dottedquarter;
                                                break;
                                            case Duration.Whole:
                                                SongDuration.Image = whole;
                                                songdur1.Image = sixteenth;
                                                songdur2.Image = dottedeighth;
                                                songdur3.Image = dottedquarter;
                                                songdur4.Image = dottedhalf;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!ShiftCheck())
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                songdur1.Image = eighth;
                                                songdur2.Image = dottedquarter;
                                                songdur3.Image = dottedhalf;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = eighth;
                                                songdur1.Image = dottedquarter;
                                                songdur2.Image = dottedhalf;
                                                songdur3.Image = sixteenth;
                                                break;
                                            case Duration.DottedQuarter:
                                                SongDuration.Image = dottedquarter;
                                                songdur1.Image = dottedhalf;
                                                songdur2.Image = sixteenth;
                                                songdur3.Image = eighth;
                                                break;
                                            case Duration.DottedHalf:
                                                SongDuration.Image = dottedhalf;
                                                songdur1.Image = sixteenth;
                                                songdur2.Image = eighth;
                                                songdur3.Image = dottedquarter;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                songdur1.Image = dottedeighth;
                                                songdur2.Image = quarter;
                                                songdur3.Image = half;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = dottedeighth;
                                                songdur1.Image = quarter;
                                                songdur2.Image = half;
                                                songdur3.Image = sixteenth;
                                                break;
                                            case Duration.DottedQuarter:
                                                SongDuration.Image = quarter;
                                                songdur1.Image = half;
                                                songdur2.Image = sixteenth;
                                                songdur3.Image = dottedeighth;
                                                break;
                                            case Duration.DottedHalf:
                                                SongDuration.Image = half;
                                                songdur1.Image = sixteenth;
                                                songdur2.Image = dottedeighth;
                                                songdur3.Image = quarter;
                                                break;
                                        }
                                    }
                                }
                                songdur1.Location = new Point(SongDuration.Width + 20, Height - 220);
                                songdur2.Location = new Point(songdur1.Width * 2 + 20, Height - 220);
                                songdur3.Location = new Point(songdur1.Width * 3 + 20, Height - 220);
                                songdur4.Location = new Point(songdur1.Width * 4 + 20, Height - 220);
                                if (Song.TIME == Time.SixEight)
                                {
                                    songdur4.Location = new Point(0, -200);
                                }
                                lcheck = true;
                            }
                            else
                            {
                                if (Song.TIME == Time.FourFour)
                                {
                                    if (!ShiftCheck())
                                    {
                                        switch (currentNoteDuration)
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
                                    }
                                    else
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = dottedeighth;
                                                break;
                                            case Duration.Quarter:
                                                SongDuration.Image = dottedquarter;
                                                break;
                                            case Duration.Half:
                                                SongDuration.Image = dottedhalf;
                                                break;
                                            case Duration.Whole:
                                                SongDuration.Image = whole;
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    if (!ShiftCheck())
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = eighth;
                                                break;
                                            case Duration.DottedQuarter:
                                                SongDuration.Image = dottedquarter;
                                                break;
                                            case Duration.DottedHalf:
                                                SongDuration.Image = dottedhalf;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (currentNoteDuration)
                                        {
                                            case Duration.Sixteenth:
                                                SongDuration.Image = sixteenth;
                                                break;
                                            case Duration.Eighth:
                                                SongDuration.Image = dottedeighth;
                                                break;
                                            case Duration.DottedQuarter:
                                                SongDuration.Image = quarter;
                                                break;
                                            case Duration.DottedHalf:
                                                SongDuration.Image = half;
                                                break;
                                        }
                                    }
                                }
                                songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
                                lcheck = false;
                            }
                            break;
                        }
                    case Keys.Tab:
                        {
                            if (Song.TIME == Time.FourFour && !lcheck)
                            {
                                if ((int)currentNoteDuration % 9 == 0)
                                    currentNoteDuration = Duration.Quarter;
                                if (!ShiftCheck())
                                {
                                    currentNoteDuration = (Duration)((int)currentNoteDuration * 2);
                                    if ((int)currentNoteDuration > 48)
                                        currentNoteDuration = Duration.Sixteenth;
                                }
                                else
                                {
                                    currentNoteDuration = (Duration)((int)currentNoteDuration / 2);
                                    if ((int)currentNoteDuration < 3)
                                        currentNoteDuration = Duration.Whole;
                                }
                            }
                            else if (Song.TIME == Time.SixEight && !lcheck)
                            {
                                if (!ShiftCheck())
                                {
                                    switch (currentNoteDuration)
                                    {
                                        case Duration.Eighth:
                                            currentNoteDuration = Duration.DottedQuarter;
                                            break;
                                        case Duration.Sixteenth:
                                        //currentNoteDuration = Duration.Eighth;
                                        //break;
                                        case Duration.DottedQuarter:
                                            currentNoteDuration = (Duration)((int)currentNoteDuration * 2);
                                            break;
                                        case Duration.DottedHalf:
                                            currentNoteDuration = Duration.Sixteenth;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (currentNoteDuration)
                                    {
                                        case Duration.Eighth:
                                        //currentNoteDuration = Duration.Sixteenth;
                                        //break;
                                        case Duration.DottedHalf:
                                            currentNoteDuration = (Duration)((int)currentNoteDuration / 2);
                                            break;
                                        case Duration.Sixteenth:
                                            currentNoteDuration = Duration.DottedHalf;
                                            break;
                                        case Duration.DottedQuarter:
                                            currentNoteDuration = Duration.Eighth;
                                            break;
                                    }
                                }
                            }
                            switch (currentNoteDuration)
                            {
                                case Duration.Sixteenth:
                                    SongDuration.Image = sixteenth;
                                    break;
                                case Duration.Eighth:
                                    SongDuration.Image = eighth;
                                    break;
                                case Duration.DottedEighth:
                                    SongDuration.Image = dottedeighth;
                                    break;
                                case Duration.Quarter:
                                    SongDuration.Image = quarter;
                                    break;
                                case Duration.DottedQuarter:
                                    SongDuration.Image = dottedquarter;
                                    break;
                                case Duration.Half:
                                    SongDuration.Image = half;
                                    break;
                                case Duration.DottedHalf:
                                    SongDuration.Image = dottedhalf;
                                    break;
                                case Duration.Whole:
                                    SongDuration.Image = whole;
                                    break;
                            }
                            break;
                        }
                    case Keys.Up:
                        {
                            if (song.GetSelection().GetSelection().GetStaffNumber() > 0)
                            {
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                                song.GetSelection().SetSelection(song.GetSelection().GetSelection().GetStaffNumber() - 1);
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                            }
                            else if (song.GetSelection().GetInstrumentNumber() != 0)
                            {
                                song.GetSelection().GetSelection().Deselect();
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                                song.SetSelection(song.GetSelection().GetInstrumentNumber() - 1);
                                song.GetSelection().SetSelection(song.GetInstrument(song.GetSelection().GetInstrumentNumber()).GetStaffCount() - 1);
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                            }
                            break;
                        }
                    case Keys.Down:
                        {
                            if (song.GetSelection().GetSelection().GetStaffNumber() < song.GetSelection().GetStaffCount() - 1)
                            {
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                                song.GetSelection().SetSelection(song.GetSelection().GetSelection().GetStaffNumber() + 1);
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                            }
                            else if (song.GetSelection().GetInstrumentNumber() != Song.TOTAL_INSTRUMENTS - 1)
                            {
                                song.GetSelection().GetSelection().Deselect();
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                                song.SetSelection(song.GetSelection().GetInstrumentNumber() + 1);
                                song.GetSelection().SetSelection(0);
                                graphicsPanel.Invalidate(new Region(song.GetSelection().GetSelection().GetArea()));
                            }
                            break;
                        }
                    case Keys.Back:
                        {
                            Staff staff = song.GetSelection().GetSelection();
                            if (!staff.GetCurrentMeasure().IsEmpty())
                            {
                                staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                staff.Update();
                                graphicsPanel.Invalidate(new Region(staff.GetArea()));
                            }
                            else if (staff.GetCurrentMeasure().IsEmpty() && staff.GetMeasureCount() != 1)
                            {
                                staff.RemoveMeasure(staff.GetCurrentMeasure());
                                staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                //Song.BARLINES.Remove(Song.BARLINES.Count - 1);
                                staff.Update();
                                graphicsPanel.Invalidate(new Region(staff.GetArea()));
                            }
                            break;
                        }
                    case Keys.Space:
                        Play();
                        break;
                    case Keys.A:
                    case Keys.B:
                    case Keys.C:
                    case Keys.D:
                    case Keys.E:
                    case Keys.F:
                    case Keys.G:
                    case Keys.R:
                        {
                            valid = true;
                            if (e.KeyCode != Keys.R)
                            {
                                Enum.TryParse(e.KeyCode.ToString(), out Pitch p);
                                chord.GetNote(0).SetPitch(p);
                            }
                            else
                                chord.GetNote(0).SetPitch(Pitch.Rest);
                            if (ShiftCheck())
                            {
                                switch (currentNoteDuration)
                                {
                                    case Duration.Eighth:
                                    case Duration.Quarter:
                                    case Duration.Half:
                                        chord.GetNote(0).Duration = (Duration)((int)chord.GetNote(0).Duration * 1.5f);
                                        break;
                                    case Duration.DottedQuarter:
                                    case Duration.DottedHalf:
                                        chord.GetNote(0).Duration = (Duration)((int)chord.GetNote(0).Duration * 2 / 3);
                                        break;
                                }
                            }
                            break;
                        }
                }
                if (valid)
                {
                    Staff staff = song.GetInstrument(song.GetSelection().GetInstrumentNumber()).GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
                    Chord remainder = staff.GetNextMeasure().Add(chord);
                    chord.Play();
                    if (remainder != null)
                        staff.GetNextMeasure().Add(remainder);
                    staff.Update();
                    graphicsPanel.Invalidate(new Region(staff.GetArea()));
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                    case Keys.B:
                    case Keys.C:
                    case Keys.D:
                    case Keys.E:
                    case Keys.F:
                    case Keys.G:
                        {
                            valid = true;
                            mChord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, 4, 0));
                            Enum.TryParse(e.KeyCode.ToString(), out Pitch p);
                            mChord.GetNote(noteIndex).SetPitch(p);
                            if (ShiftCheck())
                            {
                                switch (currentNoteDuration)
                                {
                                    case Duration.Eighth:
                                    case Duration.Quarter:
                                    case Duration.Half:
                                        mChord.GetNote(0).Duration = (Duration)((int)mChord.GetNote(0).Duration * 1.5f);
                                        break;
                                    case Duration.DottedQuarter:
                                    case Duration.DottedHalf:
                                        mChord.GetNote(0).Duration = (Duration)((int)mChord.GetNote(0).Duration * 2 / 3);
                                        break;
                                }
                            }
                            noteIndex++;
                            break;
                        }
                }
            }
            if (!ControlCheck() && noteIndex != 0)
            {
                Staff staff = song.GetInstrument(song.GetSelection().GetInstrumentNumber()).GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
                Chord remainder = staff.GetNextMeasure().Add(mChord.Clone());
                mChord.Play();
                if (remainder != null)
                    staff.GetNextMeasure().Add(remainder);
                staff.Update();
                graphicsPanel.Invalidate(new Region(staff.GetArea()));

                mChord = new Chord(0);
                Song.SELECTABLES.Remove(mChord);
                noteIndex = 0;
            }
        }

        private void graphicsPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                if (Song.TIME == Time.FourFour)
                {
                    switch (currentNoteDuration)
                    {
                        case Duration.Sixteenth:
                            SongDuration.Image = sixteenth;
                            songdur1.Image = dottedeighth;
                            songdur2.Image = dottedquarter;
                            songdur3.Image = dottedhalf;
                            songdur4.Image = whole;
                            break;
                        case Duration.Eighth:
                            SongDuration.Image = dottedeighth;
                            songdur1.Image = dottedquarter;
                            songdur2.Image = dottedhalf;
                            songdur3.Image = whole;
                            songdur4.Image = sixteenth;
                            break;
                        case Duration.Quarter:
                            SongDuration.Image = dottedquarter;
                            songdur1.Image = dottedhalf;
                            songdur2.Image = whole;
                            songdur3.Image = sixteenth;
                            songdur4.Image = dottedeighth;
                            break;
                        case Duration.Half:
                            SongDuration.Image = dottedhalf;
                            songdur1.Image = whole;
                            songdur2.Image = sixteenth;
                            songdur3.Image = dottedeighth;
                            songdur4.Image = dottedquarter;
                            break;
                        case Duration.Whole:
                            SongDuration.Image = whole;
                            songdur1.Image = sixteenth;
                            songdur2.Image = dottedeighth;
                            songdur3.Image = dottedquarter;
                            songdur4.Image = dottedhalf;
                            break;
                    }
                }
                else if (Song.TIME == Time.SixEight)
                {
                    switch (currentNoteDuration)
                    {
                        case Duration.Sixteenth:
                            SongDuration.Image = sixteenth;
                            songdur1.Image = dottedeighth;
                            songdur2.Image = quarter;
                            songdur3.Image = half;
                            break;
                        case Duration.Eighth:
                            SongDuration.Image = dottedeighth;
                            songdur1.Image = quarter;
                            songdur2.Image = half;
                            songdur3.Image = sixteenth;
                            break;
                        case Duration.DottedQuarter:
                            SongDuration.Image = quarter;
                            songdur1.Image = half;
                            songdur2.Image = sixteenth;
                            songdur3.Image = dottedeighth;
                            break;
                        case Duration.DottedHalf:
                            SongDuration.Image = half;
                            songdur1.Image = sixteenth;
                            songdur2.Image = dottedeighth;
                            songdur3.Image = quarter;
                            break;
                    }
                }
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
                song.SetKeySignature(SongSettingsMenu.GetKeySignature());
                song.SetTimeSignature(SongSettingsMenu.GetTimeSignature());
                if (Song.TIME == Time.FourFour)
                {
                    currentNoteDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    songdur1.Image = half;
                    songdur2.Image = whole;
                    songdur3.Image = sixteenth;
                    songdur4.Image = eighth;
                }
                else if (Song.TIME == Time.SixEight)
                {
                    currentNoteDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    songdur1.Image = dottedquarter;
                    songdur2.Image = dottedhalf;
                    songdur3.Image = sixteenth;
                }
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

            if (Song.TIME == Time.FourFour)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Duration.Quarter:
                        currentNoteDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                    case Duration.Half:
                        currentNoteDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Whole:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                }
            }
            else if (Song.TIME == Time.SixEight)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.DottedQuarter;
                        SongDuration.Image = dottedquarter;
                        break;
                    case Duration.DottedQuarter:
                        currentNoteDuration = Duration.DottedHalf;
                        SongDuration.Image = dottedhalf;
                        break;
                    case Duration.DottedHalf:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                }
            }
        }

        private void songdur1_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            if (Song.TIME == Time.FourFour)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Duration.Quarter:
                        currentNoteDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                    case Duration.Half:
                        currentNoteDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Whole:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                }
            }
            else if (Song.TIME == Time.SixEight)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.DottedQuarter;
                        SongDuration.Image = dottedquarter;
                        break;
                    case Duration.DottedQuarter:
                        currentNoteDuration = Duration.DottedHalf;
                        SongDuration.Image = dottedhalf;
                        break;
                    case Duration.DottedHalf:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                }
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur2_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            if (Song.TIME == Time.FourFour)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                    case Duration.Quarter:
                        currentNoteDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Half:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                    case Duration.Whole:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                }
            }
            else if (Song.TIME == Time.SixEight)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.DottedQuarter;
                        SongDuration.Image = dottedquarter;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.DottedHalf;
                        SongDuration.Image = dottedhalf;
                        break;
                    case Duration.DottedQuarter:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                    case Duration.DottedHalf:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                }
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur3_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            if (Song.TIME == Time.FourFour)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.Half;
                        SongDuration.Image = half;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.Whole;
                        SongDuration.Image = whole;
                        break;
                    case Duration.Quarter:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                    case Duration.Half:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.Whole:
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                        break;
                }
            }
            else if (Song.TIME == Time.SixEight)
            {
                switch (currentNoteDuration)
                {
                    case Duration.Sixteenth:
                        currentNoteDuration = Duration.DottedHalf;
                        SongDuration.Image = dottedhalf;
                        break;
                    case Duration.Eighth:
                        currentNoteDuration = Duration.Sixteenth;
                        SongDuration.Image = sixteenth;
                        break;
                    case Duration.DottedQuarter:
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                        break;
                    case Duration.DottedHalf:
                        currentNoteDuration = Duration.DottedQuarter;
                        SongDuration.Image = dottedquarter;
                        break;
                }
            }
            songdur1.Location = songdur2.Location = songdur3.Location = songdur4.Location = new Point(-200, 0);
            lcheck = false;
        }

        private void songdur4_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            switch (currentNoteDuration)
            {
                case Duration.Sixteenth:
                    currentNoteDuration = Duration.Whole;
                    SongDuration.Image = whole;
                    break;
                case Duration.Eighth:
                    currentNoteDuration = Duration.Sixteenth;
                    SongDuration.Image = sixteenth;
                    break;
                case Duration.Quarter:
                    currentNoteDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                    break;
                case Duration.Half:
                    currentNoteDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                    break;
                case Duration.Whole:
                    currentNoteDuration = Duration.Half;
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

        delegate void VoidDelegateBool(bool enabled);
        private void SetEnabled(bool enabled)
        {
            titleTextBox.Enabled = enabled;
            composerTextBox.Enabled = enabled;
        }

        private void Play()
        {
            if (!playcheck)
            {
                PlayButton.Image = pause;
                if (titleTextBox.InvokeRequired)
                {
                    VoidDelegateBool d = new VoidDelegateBool(SetEnabled);
                    Invoke(d, false);
                }
                else
                {
                    titleTextBox.Enabled = false;
                    composerTextBox.Enabled = false;
                }
                playcheck = true;
                int interval = song.Play();
                if (interval == 0)
                    return;
                timer.Interval = interval;
                timer.Enabled = true;
            }
            else
            {
                PlayButton.Image = play;
                if (titleTextBox.InvokeRequired)
                {
                    VoidDelegateBool d = new VoidDelegateBool(SetEnabled);
                    Invoke(d, true);
                }
                else
                {
                    titleTextBox.Enabled = true;
                    composerTextBox.Enabled = true;
                }
                song.Stop();
                playcheck = false;
            }
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;
            Play();
        }

        private void soundPlayer_Finished(object source, ElapsedEventArgs e)
        {
            timer.Enabled = false;
            Play();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }
    }
}

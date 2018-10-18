using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Timers;
using System.IO;
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

        bool isLetter = true;
        bool EightOrNine = false;

        bool isdown = false;

        bool songloaded = false;

        Point toolpoint;

        Song song;

        System.Timers.Timer timer;

        int noteIndex = 0;
        Chord mChord = new Chord(0);

        sbyte octaveDifference = 0;

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
            if (Properties.Settings.Default.AskForTutorial)
            {
                ViewTutorial tutorial = new ViewTutorial();
                if (tutorial.ShowDialog() == DialogResult.OK)
                {
                    TutorialForm tutform = new TutorialForm();
                    tutform.ShowDialog();
                }
            }

            panel2.Location = new Point(-200, -200);

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
            
            ActiveControl = graphicsPanel;

            (graphicsPanel as Control).KeyUp += new KeyEventHandler(graphicsPanel_KeyUp);
            (graphicsPanel as Control).KeyDown += new KeyEventHandler(graphicsPanel_KeyDown);

            Startup startup = new Startup();
            startup.ShowDialog();

            if (startup.filename == "" || startup.filename == "button2" || startup.filename == "newbutton" || startup.filename == "button3")
            {
                NewSong newsong = new NewSong();
                newsong.ShowDialog();

                if (newsong.DialogResult == DialogResult.OK)
                {
                    Song.SCREEN_WIDTH = newsong.mainSCREEN_WIDTH;
                    Song.PAGE_WIDTH = newsong.mainPAGE_WIDTH;
                    Song._SCALE = newsong.main_SCALE;
                    Song.TOP_MARGIN = newsong.mainTOP_MARGIN;
                    Song.LEFT_MARGIN = newsong.mainLEFT_MARGIN;
                    Song.RIGHT_MARGIN = newsong.mainRIGHT_MARGIN;
                    Song.STAFF_SPACING = newsong.mainSTAFF_SPACING;
                    Song.INSTRUMENT_SPACING = newsong.mainINSTRUMENT_SPACING;

                    Song.TOTAL_INSTRUMENTS = newsong.mainTOTAL_INSTRUMENTS;
                    Song.TOTAL_STAVES = newsong.mainTOTAL_STAVES;

                    Song.cursorY = newsong.maincursorY;
                    Song.cursorX = newsong.maincursorX;

                    Staff.LINE_SPACING = newsong.mainLINE_SPACING;
                    Staff.LENGTH = newsong.mainLENGTH;
                    Staff.HEIGHT = newsong.mainHEIGHT;

                    song = new Song(PAGE_WIDTH, newsong.key, newsong.time);

                    if (Song.TIME > 0)
                    {
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                    }
                    else if (Song.TIME < 0)
                    {
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                    }

                    for (int i = 0; i < newsong.instruments.Count; i++)
                        song.AddInstrument(newsong.instruments[i].clefs, newsong.instruments[i].waveForms, newsong.instruments[i].grouping);
                    song.Title = newsong.title;
                    song.Composer = newsong.composer;

                    PlayButton.Image = play;
                    PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), 0);

                    menuStrip1.BringToFront();

                    PlayButton.BringToFront();

                    song.Update();
                    graphicsPanel.Invalidate();
                }
            }
            else
            {
                songloaded = true;
                byte[] buffer;
                FileStream stream = new FileStream(startup.filePath + "\\" + startup.filename + ".bcf", FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                BinaryFormatter formatter = new BinaryFormatter();

                stream.Seek(-1 * sizeof(long), SeekOrigin.End);
                long sSongSize = reader.ReadInt64();
                stream.Seek(0, SeekOrigin.Begin);

                
                buffer = new byte[sSongSize];
                reader.Read(buffer, 0, buffer.Length);
                MemoryStream sSong = new MemoryStream(buffer);

                buffer = new byte[stream.Length - sSongSize - sizeof(long)];
                reader.Read(buffer, 0, buffer.Length);
                MemoryStream sSettings = new MemoryStream(buffer);

                SongSettings settings = (SongSettings)formatter.Deserialize(sSettings);
                settings.Apply();
                song = (Song)formatter.Deserialize(sSong);

                Song.SELECTABLES = new List<SongComponent>();

                for (int i = 0; i < song.GetInstrumentCount(); i++)
                {
                    Instrument instrument = song.GetInstrument(i);
                    Song.SELECTABLES.Add(instrument);
                    for (int s = 0; s < instrument.GetStaffCount(); s++)
                    {
                        Staff staff = instrument.GetStaff(s);
                        Song.SELECTABLES.Add(staff);
                        for (int m = 0; m < staff.GetMeasureCount(); m++)
                        {
                            Measure measure = staff.GetMeasure(m);
                            Song.SELECTABLES.Add(measure);
                            for (int c = 0; c < measure.GetChordCount(); c++)
                            {
                                Chord chord = measure.GetChord(c);
                                Song.SELECTABLES.Add(chord);
                                for (int n = 0; n < chord.GetNoteCount(); n++)
                                    Song.SELECTABLES.Add(chord.GetNote(n));
                            }
                        }
                    }
                }

                foreach (SongComponent component in Song.SELECTABLES)
                    component.Deselect();

                if (Song.TIME > 0)
                {
                    currentNoteDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                }
                else if (Song.TIME < 0)
                {
                    currentNoteDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                }

                textBox1.Text = song.Title;
                textBox2.Text = song.Composer;

                graphicsPanel.Invalidate();
                Properties.Settings.Default.Loaded = true;
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
                    songdur4.Location = new Point(0, -200);
            }
            if (song != null && Song.TOTAL_INSTRUMENTS != 0)
                song.Paint(e.Graphics);
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

            PlayButton.Image = play;
            PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), 0);

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
            {
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH + 100), (int)(PAGE_HEIGHT + 100 * PAGE_HEIGHT / PAGE_WIDTH));
                Song.needsFullUpdate = true;
                song.Update();

                if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint != null)
                    panel2.Location = toolpoint;
                else if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint == null)
                    if (songloaded)
                        panel2.Location = new Point(0, 0);
                    else
                        panel2.Location = new Point(0, menuStrip1.Height);
            }
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            ActiveControl = graphicsPanel;

            if (Song.PAGE_WIDTH > 500)
            {
                graphicsPanel.Size = new Size((int)(PAGE_WIDTH - 100), (int)(PAGE_HEIGHT - 100 * PAGE_HEIGHT / PAGE_WIDTH));

                /*if (Song.BARLINES != null)
                    for (int i = 0; i < Song.BARLINES.Count; i++)
                        if (_SCALE < 1)
                            Song.BARLINES[i] *= _SCALE;
                        else
                            Song.BARLINES[i] /= _SCALE;*/

                Song.needsFullUpdate = true;
                song.Update();

                if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint != null)
                    panel2.Location = toolpoint;
                else if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint == null)
                    if (songloaded)
                        panel2.Location = new Point(0, 0);
                    else
                        panel2.Location = new Point(0, menuStrip1.Height);
            }
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
            Song.CHORD_POSITIONS = options.mainCHORD_POSITIONS;

            Song.SELECTABLES = options.mainSELECTABLES;
            Song.LASTNOTES = options.mainLASTNOTES;

            if (options.DialogResult == DialogResult.OK)
                song.AddInstrument(options.clefs, options.waveForms, options.grouping);

            song.Update();
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
                    else ((Note)Song.SELECTABLES[i]).Select();
                }
            }
            if (newInstrument != null)
                song.SetSelection(newInstrument);
            if (newStaff != null)
                newInstrument.SetSelection(newStaff);
            if (newMeasure != null)
            {
                newStaff.SetSelection(newMeasure);
                if (newChord != null)
                    newMeasure.SetSelection(newChord);
            }

            song.Update();
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
                    chord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, Song.OCTAVE));

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
                                song.GetSelection().SetSelection(song.GetSelection().GetSelection().GetStaffNumber() - 1);
                                song.Update();
                                graphicsPanel.Invalidate();
                            }
                            else if (song.GetSelection().GetInstrumentNumber() != 0)
                            {
                                song.GetSelection().GetSelection().Deselect();
                                song.SetSelection(song.GetSelection().GetInstrumentNumber() - 1);
                                song.GetSelection().SetSelection(song.GetInstrument(song.GetSelection().GetInstrumentNumber()).GetStaffCount() - 1);
                                song.Update();
                                graphicsPanel.Invalidate();
                            }
                            break;
                        }
                    case Keys.Down:
                        {
                            if (song.GetSelection().GetSelection().GetStaffNumber() < song.GetSelection().GetStaffCount() - 1)
                            {
                                song.GetSelection().SetSelection(song.GetSelection().GetSelection().GetStaffNumber() + 1);
                                song.Update();
                                graphicsPanel.Invalidate();
                            }
                            else if (song.GetSelection().GetInstrumentNumber() != Song.TOTAL_INSTRUMENTS - 1)
                            {
                                song.GetSelection().GetSelection().Deselect();
                                song.SetSelection(song.GetSelection().GetInstrumentNumber() + 1);
                                song.GetSelection().SetSelection(0);
                                song.Update();
                                graphicsPanel.Invalidate();
                            }
                            break;
                        }
                    case Keys.OemMinus:
                        octaveDifference -= 1;
                        break;
                    case Keys.Oemplus:
                        octaveDifference += 1;
                        break;
                    case Keys.Back:
                        {
                            Staff staff = song.GetSelection().GetSelection();
                            Instrument instrument = song.GetInstrument(song.GetSelection().GetInstrumentNumber());

                            if (!CheckChordSelection())
                            {
                                if (!staff.GetCurrentMeasure().IsEmpty())
                                {
                                    Chord c = staff.GetSelection().GetSelection();
                                    staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                    if (!staff.GetCurrentMeasure().IsEmpty() && staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1).GetNoteCount() == 1)
                                        Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1).GetNote(0);
                                    else if (!staff.GetCurrentMeasure().IsEmpty() && staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1).GetNoteCount() != 1)
                                        Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = GetAverageNote(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                    Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;
                                    song.Update();
                                    graphicsPanel.Invalidate();
                                }
                                else if (staff.GetCurrentMeasure().IsEmpty() && staff.GetMeasureCount() != 1)
                                {
                                    staff.RemoveMeasure(staff.GetCurrentMeasure());
                                    staff.GetCurrentMeasure().Remove(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                    if (staff.GetCurrentMeasure().IsEmpty())
                                        staff.RemoveMeasure(staff.GetCurrentMeasure());
                                    if (staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1).GetNoteCount() == 1)
                                        Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = staff.GetMeasure(staff.GetMeasureCount() - 1).GetChord(staff.GetCurrentMeasure().GetChordCount() - 1).GetNote(0);
                                    else
                                        Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = GetAverageNote(staff.GetCurrentMeasure().GetChord(staff.GetCurrentMeasure().GetChordCount() - 1));
                                    Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;

                                    song.Update();
                                    graphicsPanel.Invalidate();
                                }
                            }
                            else
                                 ChangeSingleNoteChord(Pitch.Rest);
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
                            isLetter = true;
                            EightOrNine = false;
                            if (e.KeyCode != Keys.R)
                            {
                                Enum.TryParse(e.KeyCode.ToString(), out Pitch p);
                                chord.GetNote(0).SetPitch(p);
                            }
                            else
                                chord.GetNote(0).SetPitch(Pitch.Rest);
                            if (ShiftCheck())
                                ShiftNoteDuration(chord);
                            break;
                        }
                    case Keys.D8:
                        {
                            valid = true;
                            isLetter = false;
                            EightOrNine = true;
                            chord.GetNote(0).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(chord);
                            break;
                        }
                    case Keys.D9:
                        {
                            valid = true;
                            isLetter = false;
                            EightOrNine = true;
                            chord.GetNote(0).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(chord);
                            break;
                        }
                    case Keys.D1:
                    case Keys.D2:
                    case Keys.D3:
                    case Keys.D4:
                    case Keys.D5:
                    case Keys.D6:
                    case Keys.D7:
                    case Keys.D0:
                        {
                            valid = true;
                            isLetter = false;
                            EightOrNine = false;
                            chord.GetNote(0).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(chord);
                            break;
                        }
                }
                if (valid && !CheckChordSelection())
                {
                    Instrument instrument = song.GetInstrument(song.GetSelection().GetInstrumentNumber());
                    Staff staff = instrument.GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
                    chord.SetWaveForm(staff.GetWaveForm());
                    if (isLetter)
                        chord.GetNote(0).Octave = CalculateOctave(chord.GetNote(0), instrument);
                    octaveDifference = 0;
                    if (EightOrNine)
                        chord.GetNote(0).Octave += 1;
                    CheckOctaveRange(staff, chord);
                    int chordNumber = GetChordNumber(staff);
                    chord.SetChordNumber(chordNumber++);
                    Chord remainder = staff.GetNextMeasure().Add(chord);
                    chord.Play();
                    if (remainder != null)
                    {
                        remainder.SetChordNumber(chordNumber);
                        staff.GetNextMeasure().Add(remainder);
                    }
                    song.Update();
                    graphicsPanel.Invalidate();

                    Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = chord.GetNote(0);
                    Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;
                }
                else if (valid)
                {
                    ChangeSingleNoteChord(chord.GetNote(0).GetPitch());
                    for (int i = 0; i < chord.GetNoteCount(); i++)
                    {
                        Song.SELECTABLES.Remove(chord.GetNote(i));
                    }
                    Song.SELECTABLES.Remove(chord);
                }
            }
            else // (ControlCheck())
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
                            isLetter = true;
                            EightOrNine = false;
                            mChord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, Song.OCTAVE));
                            Enum.TryParse(e.KeyCode.ToString(), out Pitch p);
                            mChord.GetNote(noteIndex).SetPitch(p);
                            if (ShiftCheck())
                                ShiftNoteDuration(mChord);
                            noteIndex++;
                            break;
                        }
                    case Keys.D8:
                        {
                            isLetter = false;
                            EightOrNine = true;
                            mChord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, Song.OCTAVE));
                            mChord.GetNote(noteIndex).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(mChord);
                            noteIndex++;
                            break;
                        }
                    case Keys.D9:
                        {
                            isLetter = false;
                            EightOrNine = true;
                            mChord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, Song.OCTAVE));
                            mChord.GetNote(noteIndex).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(mChord);
                            noteIndex++;
                            break;
                        }
                    case Keys.D1:
                    case Keys.D2:
                    case Keys.D3:
                    case Keys.D4:
                    case Keys.D5:
                    case Keys.D6:
                    case Keys.D7:
                        {
                            isLetter = false;
                            EightOrNine = false;
                            mChord.Add(new Note(Pitch.C, Accidental.Natural, currentNoteDuration, Song.OCTAVE));
                            mChord.GetNote(noteIndex).SetPitch(CheckPitch(e));
                            if (ShiftCheck())
                                ShiftNoteDuration(mChord);
                            noteIndex++;
                            break;
                        }
                }
            }
            if (!ControlCheck() && noteIndex != 0)
            {
                Instrument instrument = song.GetInstrument(song.GetSelection().GetInstrumentNumber());
                Staff staff = instrument.GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
                mChord.SetWaveForm(staff.GetWaveForm());
                if (isLetter)
                     for (int i = 0; i < noteIndex; i++)
                        mChord.GetNote(i).Octave = CalculateOctave(mChord.GetNote(i), instrument);
                if (EightOrNine)
                    mChord.GetNote(0).Octave += 1;
                CheckOctaveRange(staff, mChord);
                Chord remainder = staff.GetNextMeasure().Add(mChord.Clone());
                mChord.Play();
                if (remainder != null)
                    staff.GetNextMeasure().Add(remainder);
                song.Update();
                graphicsPanel.Invalidate();

                Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = GetAverageNote(mChord);
                Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;

                mChord = new Chord(0);
                Song.SELECTABLES.Remove(mChord);
                noteIndex = 0;
            }
        }

        private void ChangeSingleNoteChord(Pitch p)
        {
            Instrument instrument = song.GetInstrument(song.GetSelection().GetInstrumentNumber());
            Staff staff = instrument.GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
            int chordNumber = 0;
            for (int i = 0; i < Song.SELECTABLES.Count; i++)
            {
                if (Song.SELECTABLES[i].GetType() == typeof(Chord))
                {
                    if (Song.SELECTABLES[i].isItSelected())
                    {
                        (Song.SELECTABLES[i] as Chord).GetNote(0).SetPitch(p);
                        (Song.SELECTABLES[i] as Chord).SetWaveForm(staff.GetWaveForm());
                        if (isLetter)
                            (Song.SELECTABLES[i] as Chord).GetNote(0).Octave = CalculateOctave((Song.SELECTABLES[i] as Chord).GetNote(0), instrument);
                        octaveDifference = 0;
                        if (EightOrNine)
                            (Song.SELECTABLES[i] as Chord).GetNote(0).Octave += 1;
                        CheckOctaveRange(staff, (Song.SELECTABLES[i] as Chord));
                        (Song.SELECTABLES[i] as Chord).Play();
                        song.Update();
                        graphicsPanel.Invalidate();

                        if (GetChordNumber(staff) == chordNumber)
                        {
                            Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = (Song.SELECTABLES[i] as Chord).GetNote(0);
                            Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;
                        }
                        break;
                    }
                    chordNumber++;
                }
            }
        }

        private void ChangeMultipleNoteChord(Pitch p)
        {
            Instrument instrument = song.GetInstrument(song.GetSelection().GetInstrumentNumber());
            Staff staff = instrument.GetStaff(song.GetSelection().GetSelection().GetStaffNumber());
            int chordNumber = 0;
            int iValue = 0;
            for (int i = 0; i < Song.SELECTABLES.Count; i++)
            {
                if (Song.SELECTABLES[i].GetType() == typeof(Chord))
                {
                    for (int j = 0; j < (Song.SELECTABLES[i] as Chord).GetNoteCount() ; j++)
                    {
                        if ((Song.SELECTABLES[i] as Chord).GetNote(j).isItSelected() )
                        {
                            iValue = i;
                            break;
                        }
                    }
                    chordNumber++;
                }
            }
            for (int i = 0; i < Song.SELECTABLES.Count; i++)
            {
                if (Song.SELECTABLES[i].GetType() == typeof(Note))
                {
                    if (Song.SELECTABLES[i].isItSelected())
                    {
                        (Song.SELECTABLES[i] as Note).SetPitch(p);
                        (Song.SELECTABLES[iValue] as Chord).SetWaveForm(staff.GetWaveForm());
                        if (isLetter)
                            (Song.SELECTABLES[i] as Note).Octave = CalculateOctave((Song.SELECTABLES[i] as Note), instrument);
                        octaveDifference = 0;
                        if (EightOrNine)
                            (Song.SELECTABLES[i] as Note).Octave += 1;
                        CheckOctaveRange(staff, (Song.SELECTABLES[iValue] as Chord));
                        (Song.SELECTABLES[iValue] as Chord).Play();
                        song.Update();
                        graphicsPanel.Invalidate();

                        if (GetChordNumber(staff) == chordNumber)
                        {
                            Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()] = (Song.SELECTABLES[i] as Chord).GetNote(0);
                            Song.OCTAVE = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()].Octave;
                        }
                        break;
                    }
                }
            }
        }

        private bool CheckChordSelection()
        {
            for (int i = 0; i < Song.SELECTABLES.Count; i++)
            {
                if (Song.SELECTABLES[i].GetType() == typeof(Chord))
                {
                    if (Song.SELECTABLES[i].isItSelected())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private int GetChordNumber(Staff s)
        {
            int number = 0;
            for (int i = 0; i < s.GetMeasureCount(); i++)
                number += s.GetMeasure(i).GetChordCount();

            return number;
        }

        private sbyte CalculateOctave(Note n, Instrument instrument)
        {
            Staff staff = instrument.GetSelection();
            int index = 0;
            
            Note lastNote = Song.LASTNOTES[instrument.GetInstrumentNumber()][staff.GetStaffNumber()];
            n.Octave = lastNote.Octave;

            int[] pitchDiff = new int[3];
            for (int i = 0; i < pitchDiff.Length; i++)
            {
                pitchDiff[i] = (int)lastNote.GetPitch() - (int)n.GetPitch();
                int octaveDiff = n.Octave + i - 1 - lastNote.Octave;
                pitchDiff[i] -= 7 * octaveDiff;
            }

            int min = pitchDiff[0];
            for (int i = 1; i < pitchDiff.Length; i++)
                if (Math.Abs(pitchDiff[i]) < min)
                {
                    min = pitchDiff[i];
                    index = i;
                }
            return (sbyte)(n.Octave - index + 1 + octaveDifference);
        }

        private Note GetAverageNote(Chord c)
        {
            int newPitch = 0;

            for (int i = 0; i < c.GetNoteCount(); i++)
                newPitch += (c.GetNote(i).Octave * 8 - (int)c.GetNote(i).GetPitch() + 8);

            newPitch = newPitch / c.GetNoteCount();


            int newOctave = 0;
            while (newPitch > 0)
            {
                newPitch -= 8;
                newOctave++;
            }

            newPitch *= -1;
            newOctave--;

            if (newPitch == 7)
                newPitch = 6;
           
            Note averageNote = new Note((Pitch)newPitch,Accidental.Natural,Duration.Quarter,(sbyte)newOctave);

            return averageNote;
        }

        private void ShiftNoteDuration(Chord c)
        {
            switch (currentNoteDuration)
            {
                case Duration.Eighth:
                case Duration.Quarter:
                case Duration.Half:
                    c.GetNote(0).Duration = (Duration)((int)c.GetNote(0).Duration * 1.5f);
                    break;
                case Duration.DottedQuarter:
                case Duration.DottedHalf:
                    c.GetNote(0).Duration = (Duration)((int)c.GetNote(0).Duration * 2 / 3);
                    break;
            }
        }

        private Pitch CheckPitch(KeyEventArgs e)
        {
            int i;

            if (e.KeyCode == Keys.D0)
                return Pitch.Rest;

            if (e.KeyCode == Keys.D8)
            {
                Enum.TryParse(Song.KEY.ToString(), out Pitch p);
                i = 49 - 49;
                if ((int)p - i < 0)
                    i -= 7;
                return (Pitch)((int)p - i);
            }
            else if (e.KeyCode == Keys.D9)
            {
                Enum.TryParse(Song.KEY.ToString(), out Pitch p);
                i = 50 - 49;
                if ((int)p - i < 0)
                    i -= 7;
                return (Pitch)((int)p - i);
            }
            else
            {
                Enum.TryParse(Song.KEY.ToString(), out Pitch p);
                i = (int)e.KeyCode - 49;
                if ((int)p - i < 0)
                    i -= 7;
                return (Pitch)((int)p - i);
            }
        }

        private void CheckOctaveRange(Staff s, Chord c)
        {
            for (int i = 0; i < c.GetNoteCount(); i++)
            {
                Note n = c.GetNote(i);
                switch (s.GetClef())
                {
                    case Clef.Treble:
                        if (n.GetPitch() <= Pitch.A || n.GetPitch() == Pitch.C)
                        {
                            if (n.Octave > 5)
                                n.Octave = 5;
                            else if (n.Octave < 3)
                                n.Octave = 3;
                        }
                        else
                        {
                            if (n.Octave > 5)
                                n.Octave = 5;
                            else if (n.Octave < 4)
                                n.Octave = 4;
                        }
                        break;
                    case Clef.Alto:
                        if (n.GetPitch() == Pitch.D)
                        {
                            if (n.Octave > 6)
                                n.Octave = 6;
                            else if (n.Octave < 4)
                                n.Octave = 4;
                        }
                        else if (n.GetPitch() == Pitch.B || n.GetPitch() == Pitch.C)
                        {
                            if (n.Octave > 5)
                                n.Octave = 5;
                            else if (n.Octave < 3)
                                n.Octave = 3;
                        }
                        else
                        {
                            if (n.Octave > 5)
                                n.Octave = 5;
                            else if (n.Octave < 4)
                                n.Octave = 4;
                        }
                        break;
                    case Clef.Bass:
                        if (n.GetPitch() >= Pitch.E && n.GetPitch() < Pitch.C)
                        {
                            if (n.Octave > 4)
                                n.Octave = 4;
                            else if (n.Octave < 2)
                                n.Octave = 2;
                        }
                        else if (n.GetPitch() == Pitch.C)
                        {
                            if (n.Octave > 3)
                                n.Octave = 3;
                            else if (n.Octave < 1)
                                n.Octave = 1;
                        }
                        else
                        {
                            if (n.Octave > 3)
                                n.Octave = 3;
                            else if (n.Octave < 2)
                                n.Octave = 2;
                        }
                        break;
                    case Clef.Tenor:
                        if (n.GetPitch() <= Pitch.G || n.GetPitch() == Pitch.C)
                        {
                            if (n.GetPitch() == Pitch.C)
                                if (n.Octave > 4)
                                    n.Octave = 4;
                            if (n.Octave < 3)
                                n.Octave = 3;
                        }
                        else
                        {
                            if (n.Octave > 5)
                                n.Octave = 5;
                            else if (n.Octave < 4)
                                n.Octave = 4;
                        }
                        break;
                }
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

        private void tutorialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TutorialForm tutorial = new TutorialForm();
            tutorial.Show();
        }

        private void newSongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            song = new Song(PAGE_WIDTH);
            graphicsPanel.Invalidate();
        }

        private void Play()
        {
            if (!playcheck)
            {
                PlayButton.Image = pause;
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
                song.Stop();
                timer.Enabled = false;
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Song.BPM = (int)numericUpDown1.Value;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = textBox1.Text;
            dlg.DefaultExt = ".wav";
            dlg.Filter = "wave file (*.wav)|*.wav|png file (*.png)|*.png|image & audio (*.wav, *.png)|*.wav-png";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                string extension = Path.GetExtension(dlg.FileName);
                switch (extension.ToLower())
                {
                    case ".wav":
                        song.ExportAudio(dlg.FileName);
                        break;
                    case ".png":
                        song.ExportImage(dlg.FileName);
                        break;
                    case ".wav-png":
                        string pathWithoutExt = dlg.FileName.Remove(dlg.FileName.Length - extension.Length, extension.Length);
                        song.ExportAudio(pathWithoutExt + ".wav");
                        song.ExportImage(pathWithoutExt + ".png");
                        break;
                }
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Loaded = false;
            Properties.Settings.Default.Save();
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = textBox1.Text;
            dlg.DefaultExt = "bcf";
            dlg.Filter = "bitComposer file (*.bcf)|*.bcf";
            if (DialogResult.OK == dlg.ShowDialog())
            {
                song.Save(dlg.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (song.GetFileName() == null)
                saveAsToolStripMenuItem_Click(sender, e);
            else
                song.Save();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Startup startup = new Startup();
            startup.ShowDialog();

            if (startup.filename == "" || startup.filename == "button2" || startup.filename == "newbutton" || startup.filename == "button3")
            {
                NewSong newsong = new NewSong();
                newsong.ShowDialog();

                if (newsong.DialogResult == DialogResult.OK)
                {
                    Song.SCREEN_WIDTH = newsong.mainSCREEN_WIDTH;
                    Song.PAGE_WIDTH = newsong.mainPAGE_WIDTH;
                    Song._SCALE = newsong.main_SCALE;
                    Song.TOP_MARGIN = newsong.mainTOP_MARGIN;
                    Song.LEFT_MARGIN = newsong.mainLEFT_MARGIN;
                    Song.RIGHT_MARGIN = newsong.mainRIGHT_MARGIN;
                    Song.STAFF_SPACING = newsong.mainSTAFF_SPACING;
                    Song.INSTRUMENT_SPACING = newsong.mainINSTRUMENT_SPACING;

                    Song.TOTAL_INSTRUMENTS = newsong.mainTOTAL_INSTRUMENTS;
                    Song.TOTAL_STAVES = newsong.mainTOTAL_STAVES;

                    Song.cursorY = newsong.maincursorY;
                    Song.cursorX = newsong.maincursorX;

                    Staff.LINE_SPACING = newsong.mainLINE_SPACING;
                    Staff.LENGTH = newsong.mainLENGTH;
                    Staff.HEIGHT = newsong.mainHEIGHT;

                    song = new Song(PAGE_WIDTH, newsong.key, newsong.time);

                    if (Song.TIME > 0)
                    {
                        currentNoteDuration = Duration.Quarter;
                        SongDuration.Image = quarter;
                    }
                    else if (Song.TIME < 0)
                    {
                        currentNoteDuration = Duration.Eighth;
                        SongDuration.Image = eighth;
                    }

                    for (int i = 0; i < newsong.instruments.Count; i++)
                        song.AddInstrument(newsong.instruments[i].clefs, newsong.instruments[i].waveForms, newsong.instruments[i].grouping);
                    song.Title = newsong.title;
                    song.Composer = newsong.composer;

                    PlayButton.Image = play;
                    PlayButton.Location = new Point((Width / 2) - (PlayButton.Width / 2), 0);

                    menuStrip1.BringToFront();

                    PlayButton.BringToFront();

                    song.Update();
                    graphicsPanel.Invalidate();
                }
            }
            else
            {
                songloaded = true;
                byte[] buffer;
                FileStream stream = new FileStream(startup.filePath + "\\" + startup.filename + ".bcf", FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);
                BinaryFormatter formatter = new BinaryFormatter();

                stream.Seek(-1 * sizeof(long), SeekOrigin.End);
                long sSongSize = reader.ReadInt64();
                stream.Seek(0, SeekOrigin.Begin);


                buffer = new byte[sSongSize];
                reader.Read(buffer, 0, buffer.Length);
                MemoryStream sSong = new MemoryStream(buffer);

                buffer = new byte[stream.Length - sSongSize - sizeof(long)];
                reader.Read(buffer, 0, buffer.Length);
                MemoryStream sSettings = new MemoryStream(buffer);

                SongSettings settings = (SongSettings)formatter.Deserialize(sSettings);
                settings.Apply();
                song = (Song)formatter.Deserialize(sSong);

                Song.SELECTABLES = new List<SongComponent>();
                song.GetInstrument(0).Select();
                song.GetSelection().GetStaff(0).Select();

                if (Song.TIME > 0)
                {
                    currentNoteDuration = Duration.Quarter;
                    SongDuration.Image = quarter;
                }
                else if (Song.TIME < 0)
                {
                    currentNoteDuration = Duration.Eighth;
                    SongDuration.Image = eighth;
                }

                graphicsPanel.Invalidate();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            song.Title = textBox1.Text;
            graphicsPanel.Invalidate();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            song.Composer = textBox2.Text;
            graphicsPanel.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolboxToolStripMenuItem.Text == "Show Toolbox")
            {
                if (songloaded)
                {
                    panel2.Location = new Point(0, 0);
                }
                else
                {
                    panel2.Location = new Point(0, menuStrip1.Height);
                }
                toolboxToolStripMenuItem.Text = "Hide Toolbox";
            }
            else if (toolboxToolStripMenuItem.Text == "Hide Toolbox")
            {
                panel2.Location = new Point(-200, -200);
                toolboxToolStripMenuItem.Text = "Show Toolbox";
            }
        }

        private void KeyBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(KeyBox.Text, out Key k);
            song.SetKeySignature(k);

            graphicsPanel.Invalidate();
        }

        private void TimeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Enum.TryParse(TimeBox.Text, out Time t);
            song.SetTimeSignature(t);

            graphicsPanel.Invalidate();
        }

        int left, top, right, bottom;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            left = MousePosition.X - panel2.Location.X;
            top = MousePosition.Y - panel2.Location.Y;
            right = panel2.Location.X + panel2.Width - MousePosition.X;
            bottom = panel2.Location.Y + panel2.Height - MousePosition.Y;
            isdown = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void KeyBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void TimeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ActiveControl = graphicsPanel;
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            isdown = false;
            toolpoint = panel2.Location;
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdown)
            {
                if (!songloaded)
                {
                    if (MousePosition.X - left >= 0 && MousePosition.Y - top >= menuStrip1.Height && MousePosition.X + right <= Width - 25 && MousePosition.Y + bottom <= Height - 40)
                    {
                        panel2.Location = new Point(MousePosition.X - left, MousePosition.Y - top);
                    }
                    if (MousePosition.X - left < 0 && MousePosition.Y - top >= menuStrip1.Height && MousePosition.Y + bottom <= Height - 40)
                    {
                        panel2.Location = new Point(0, MousePosition.Y - top);
                    }
                    if (MousePosition.X + right > Width - 25 && MousePosition.Y - top > menuStrip1.Height && MousePosition.Y + bottom <= Height - 40)
                    {
                        panel2.Location = new Point(Width - panel2.Width - 25, MousePosition.Y - top);
                    }
                    if (MousePosition.Y - top < menuStrip1.Height && MousePosition.X - left >= 0 && MousePosition.X + right <= Width - 25)
                    {
                        panel2.Location = new Point(MousePosition.X - left, menuStrip1.Height);
                    }
                    if (MousePosition.Y + bottom > Height - 40 && MousePosition.X - left >= 0 && MousePosition.X + right <= Width - 25)
                    {
                        panel2.Location = new Point(MousePosition.X - left, Height - panel2.Height - 40);
                    }
                }
                else
                {
                    if (MousePosition.X - left >= 0 && MousePosition.Y - top >= 0 && MousePosition.X + right <= Width - 25 && MousePosition.Y + bottom <= Height - 55)
                    {
                        panel2.Location = new Point(MousePosition.X - left, MousePosition.Y - top);
                    }
                    if (MousePosition.X - left < 0 && MousePosition.Y - top >= 0 && MousePosition.Y + bottom <= Height - 55)
                    {
                        panel2.Location = new Point(0, MousePosition.Y - top);
                    }
                    if (MousePosition.X + right > Width - 25 && MousePosition.Y - top > 0 && MousePosition.Y + bottom <= Height - 55)
                    {
                        panel2.Location = new Point(Width - panel2.Width - 25, MousePosition.Y - top);
                    }
                    if (MousePosition.Y - top < 0 && MousePosition.X - left >= 0 && MousePosition.X + right <= Width - 25)
                    {
                        panel2.Location = new Point(MousePosition.X - left, 0);
                    }
                    if (MousePosition.Y + bottom > Height - 55 && MousePosition.X - left >= 0 && MousePosition.X + right <= Width - 25)
                    {
                        panel2.Location = new Point(MousePosition.X - left, Height - panel2.Height - 55);
                    }
                }
            }
        }

        private void panel2_LocationChanged(object sender, EventArgs e)
        {
            if (!isdown)
            {
                if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint != null)
                {
                    panel2.Location = toolpoint;
                }
                else if (toolboxToolStripMenuItem.Text == "Hide Toolbox" && toolpoint == null)
                {
                    if (songloaded)
                    {
                        panel2.Location = new Point(0, 0);
                    }
                    else
                    {
                        panel2.Location = new Point(0, menuStrip1.Height);
                    }
                }
            }
        }
    }
}
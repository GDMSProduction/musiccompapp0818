using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Comp
{
    public enum Key
    {
        Cflat = -7,
        Gflat,
        Dflat,
        Aflat,
        Eflat,
        Bflat,
        F,
        C,
        G,
        D,
        A,
        E,
        B,
        Fsharp,
        Csharp
    }
    public enum Mode
    {
        Ionian,
        Dorian,
        Phrygian,
        Lydian,
        Mixolydian,
        Aeolian,
        Locrian
    }
    public enum Time
    {
        NineEight = -3,
        SixEight,
        ThreeEight,
        CompoundDuple = -2,
        CompoundTripple,
        SimpleQuadruple,
        SimpleDuple,
        SimpleTriple,
        Common = 0,
        TwoFour,
        ThreeFour,
        FourFour = 0,
    }
    public enum Clef
    {
        Treble,
        Alto,
        Bass,
        Tenor = -1,
        G = 0,
        C,
        F
    }
    public enum Accidental
    {
        DoubleFlat = -2,
        Flat,
        Natural,
        Sharp,
        DoubleSharp
    }
    public enum Pitch
    {
        E,
        D,
        C,
        B,
        A,
        G,
        F,
        Rest
    }
    public enum Duration
    {
        Whole,
        Half,
        Quarter,
        Eighth,
        Sixteenth,
        Triplet,
        Drag
    }
    public enum Grouping
    {
        None,
        Bracket,
        Brace
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

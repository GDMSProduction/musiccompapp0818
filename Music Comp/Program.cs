using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;

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
        F, // Fat Cats Go Down Alleys Eating Birds
        C,
        G,
        D,
        A,
        E,
        B, // BEAD Greatest Common Factor
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
        NineEight = -54,

        SixEight = -36, //
        CompoundDuple = -36,

        ThreeEight = -18,
        CompoundTripple = -18,

        FourFour = 48, //
        SimpleQuadruple = 48,
        Common = 48,

        SimpleDuple = 24,

        SimpleTriple = 36,
        TwoFour,
        ThreeFour,
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
        Sixteenth = 3,
        Triplet = 4,
        Eighth = 6,
        DottedEighth = 9,
        Quarter = 12,
        DottedQuarter = 18,
        Drag = 16,
        Half = 24,
        DottedHalf = 36,
        Whole = 48
    }

    public enum WaveForm
    {
        Sine,
        Square,
        Sawtooth,
        Triangle,
        Noise
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Change the namespace to your project's namespace.
namespace Music_Comp
{
    class GraphicsPanel : Panel
    {
        // Default constructor
        public GraphicsPanel()
        {
            // Turn on double buffering.
            DoubleBuffered = true;

            // Allow repainting when the windows is resized.
            // SetStyle(ControlStyles.ResizeRedraw, true);
        }
    }
}

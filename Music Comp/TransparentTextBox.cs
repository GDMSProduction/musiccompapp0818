using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Music_Comp
{
    class TransparentTextBox : TextBox
    {
        public TransparentTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
    }
}

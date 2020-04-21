using System.Drawing;
using System.Windows.Forms;

namespace turing
{
    internal class DoubleButton : Button
    {
        public DoubleButton()
        {
            this.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
        }
    }
}
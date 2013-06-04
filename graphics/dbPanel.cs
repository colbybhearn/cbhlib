using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CbhLib.graphics
{
    public class dbPanel : Panel
    {
        public dbPanel()
        {
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.ResizeRedraw, true);

            this.UpdateStyles();
        }
    }
}

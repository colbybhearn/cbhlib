using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace CbhLib.Interface
{
    public class myListBox : System.Windows.Forms.ListBox
    {
        public myListBox()
        {
            this.SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint,
                true);
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Items.Count > 0)
            {
                e.DrawBackground(); 
                e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(this.ForeColor), new PointF(e.Bounds.X, e.Bounds.Y));
            }
            base.OnDrawItem(e);
        }

        //protected override void beg
        
        protected override void OnPaint(PaintEventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();
            bool start = false;
            bool stop = false;
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                System.Drawing.Rectangle irect = this.GetItemRectangle(0);

                
                
                RectangleF clipR = iRegion.GetBounds(e.Graphics);
                int offset = (int)(-irect.Y / (float)irect.Height);

                irect.Y -= (irect.Height);
                irect.Y += (irect.Height*offset);

                for (int i = offset; i < this.Items.Count; ++i)
                {
                    irect.Y += irect.Height;
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        start = true;
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                        || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                        || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                    else
                    {
                        if (start)
                            stop = true;
                    }
                    if (stop)
                        break;
                }
                
            }
            base.OnPaint(e);
            sw.Stop();
            //Trace.WriteLine("StopWatch: " + sw.ElapsedTicks); // 60,000 100-nanoseconds => 6 milli
        }

       /*
        protected override void OnPaint(PaintEventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            Region iRegion = new Region(e.ClipRectangle);
            e.Graphics.FillRegion(new SolidBrush(this.BackColor), iRegion);
            if (this.Items.Count > 0)
            {
                sw.Start();
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    System.Drawing.Rectangle irect = this.GetItemRectangle(i);
                    if (e.ClipRectangle.IntersectsWith(irect))
                    {
                        if ((this.SelectionMode == SelectionMode.One && this.SelectedIndex == i)
                        || (this.SelectionMode == SelectionMode.MultiSimple && this.SelectedIndices.Contains(i))
                        || (this.SelectionMode == SelectionMode.MultiExtended && this.SelectedIndices.Contains(i)))
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Selected, this.ForeColor,
                                this.BackColor));
                        }
                        else
                        {
                            OnDrawItem(new DrawItemEventArgs(e.Graphics, this.Font,
                                irect, i,
                                DrawItemState.Default, this.ForeColor,
                                this.BackColor));
                        }
                        iRegion.Complement(irect);
                    }
                }
                sw.Stop();
                Trace.WriteLine("StopWatch: " + sw.ElapsedTicks); // 130,000 100-nanoseconds => 13 milli
            }

            base.OnPaint(e);
        }
        */
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;

namespace MyPad
{
    public class InformationBar : ContainerControl
    {
        int yOffset = 0;

        public InformationBar ()
            : base ()
        {
            SetStyle (ControlStyles.AllPaintingInWmPaint, true);
            SetStyle (ControlStyles.FixedHeight, true);
            SetStyle (ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle (ControlStyles.ResizeRedraw, true);
            SetStyle (ControlStyles.UserPaint, true);
        }

        public void Show (bool animate)
        {
            if (!animate)
                this.Show ();
            else {
                this.Show ();
                int origHeight = this.Height;
                this.Height = 1;

                for (int i = this.Height; i < origHeight; i++) {
                    yOffset = (origHeight - i);
                    this.Height = i;
                    this.Refresh ();

                    Thread.Sleep (15);
                }

                this.Height = origHeight;
            }
        }

        public void Hide (bool animate)
        {
            if (!animate)
                this.Hide ();
            else {
                int origHeight = this.Height;

                for (int i = this.Height; i > 1; i--) {
                    yOffset = (origHeight - i);
                    this.Height = i;
                    this.Refresh ();

                    Thread.Sleep (15);
                }

                this.Hide ();
                this.Height = origHeight;
            }
        }

        protected override void OnMouseEnter (EventArgs e)
        {
            this.ForeColor = SystemColors.HighlightText;
            this.BackColor = SystemColors.Highlight;
            this.Refresh ();

            base.OnMouseEnter (e);
        }

        protected override void OnMouseLeave (EventArgs e)
        {
            this.ForeColor = SystemColors.InfoText;
            this.BackColor = SystemColors.Info;
            this.Refresh ();

            base.OnMouseLeave (e);
        }

        protected override void OnMouseClick (MouseEventArgs e)
        {
            if (e.X >= this.Width - 20 && e.X <= this.Width - 5 &&
                e.Y >= 2 && e.Y <= 16)
                this.Hide ();

            base.OnMouseClick (e);
        }

        protected void DrawCloseButton (PaintEventArgs e)
        {
            Rectangle rect = new Rectangle ();
            rect.Location = new Point (e.ClipRectangle.Right - 20, e.ClipRectangle.Top + 5);
            rect.Size = new Size (12, 12);

            e.Graphics.DrawImage (MyPad.Resources.Resources.cross, rect);
        }

        protected override void OnPaint (PaintEventArgs e)
        {
            Rectangle rect = new Rectangle ();
            rect.Location = new Point (5, 0 - yOffset);
            rect.Size = new Size (e.ClipRectangle.Width - 25, e.ClipRectangle.Height);

            TextRenderer.DrawText (e.Graphics, this.Text, this.Font, rect, this.ForeColor, this.BackColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            DrawCloseButton (e);

            base.OnPaint (e);
        }
    }
}

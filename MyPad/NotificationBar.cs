/*
 * Copyright (c) 2009 Cory Borrow
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace MyPad
{
    public class NotificationBar : Control
    {
        Button button = null;

        int displayTimeout = 5000;
        bool useAnimation = true;

        int animationOffset = 0;

        public int DisplayTimeout
        {
            get
            {
                return displayTimeout;
            }
            set
            {
                displayTimeout = value;
            }
        }

        public bool UseAnimation
        {
            get
            {
                return useAnimation;
            }
            set
            {
                useAnimation = value;
            }
        }

        public bool ShowButton
        {
            get
            {
                return button.Visible;
            }
            set
            {
                button.Visible = value;
            }
        }

        public string ButtonText
        {
            get
            {
                return button.Text;
            }
            set
            {
                button.Text = value;
            }
        }

        public EventHandler ButtonClick
        {
            set
            {
                button.Click += value;
            }
        }

        public NotificationBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.ForeColor = SystemColors.InfoText;
            this.BackColor = SystemColors.Info;

            button = new Button();
            button.Size = new Size(80, 23);
            button.Location = new Point(this.Width - (button.Width + 20));

            this.Controls.Add(button);
        }

        public void Show(bool useAnimation)
        {
            Size originalSize = this.Size;
            this.Size = new Size(this.Width, 0);

            if (useAnimation)
            {
                this.Show();

                for(int i = 0; i < originalSize.Height; i++)
                {
                    animationOffset = (originalSize.Height - i);
                    this.Height = i;
                    this.Refresh();

                    Thread.Sleep(15);
                }
            }
            else
            {
                this.Size = originalSize;
                this.Show();
            }
        }

        public void Hide(bool useAnimation)
        {
            Size originalSize = this.Size;

            if (useAnimation)
            {
                for (int i = this.Height; i > 0; i--)
                {
                    animationOffset = (originalSize.Height - i);
                    this.Height = i;
                    this.Refresh();

                    Thread.Sleep(15);
                }

                this.Hide();
                this.Size = originalSize;
            }
            else
            {
                this.Hide();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.X >= (this.Width - 18) && e.X <= this.Width - 2 && e.Y >= 4 && e.Y <= 16)
            {
                this.Hide(useAnimation);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this.ForeColor = SystemColors.HighlightText;
            this.BackColor = SystemColors.Highlight;
            this.Refresh();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.ForeColor = SystemColors.InfoText;
            this.BackColor = SystemColors.Info;
            this.Refresh();

            base.OnMouseLeave(e);
        }

        protected void DrawCloseButton(PaintEventArgs e)
        {
            Rectangle rect = new Rectangle();
            rect.Location = new Point(this.Width - 18, 4 - animationOffset);
            rect.Size = new Size(10, 10);

            e.Graphics.DrawLine(new Pen(this.ForeColor, 2), new Point(rect.Left, rect.Top), new Point(rect.Right, rect.Bottom));
            e.Graphics.DrawLine(new Pen(this.ForeColor, 2), new Point(rect.Right, rect.Top), new Point(rect.Left, rect.Bottom));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color borderColor = Color.FromArgb(this.BackColor.R - 25, this.BackColor.G - 25, this.BackColor.B - 25);

            Rectangle rect = new Rectangle();
            rect.Location = new Point(2, 2 - animationOffset);

            if (button.Visible)
            {
                rect.Width -= button.Width + 10;
            }

            Size textSize = TextRenderer.MeasureText(e.Graphics, this.Text, this.Font, new Size(int.MaxValue, int.MaxValue), 
                TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak);

            rect.Size = textSize;
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rect, this.ForeColor, this.BackColor, 
                TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak);

            e.Graphics.DrawLine(new Pen(borderColor, 2), new Point(0, this.Height - 2), new Point(this.Width, this.Height - 2));

            DrawCloseButton(e);

            base.OnPaint(e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyPad
{
    public class MyTabControl : Control
    {
        public delegate void SelectedTabChangedEventHandler(object sender, EventArgs e);
        public delegate void TabClosingEventHandler(object sender, ref bool closing);
        public delegate void TabTextChangedEventHandler(object sender, EventArgs e);

        public event SelectedTabChangedEventHandler SelectedTabChanged;
        public event TabClosingEventHandler TabClosing;
        public event TabTextChangedEventHandler TabTextChanged;

        MyTabPageCollection tabPages;
        MyTabPage selectedTabPage;

        public MyTabPageCollection TabPages
        {
            get
            {
                return tabPages;
            }
        }

        public MyTabPage SelectedTabPage
        {
            get
            {
                return selectedTabPage;
            }
            set
            {
                selectedTabPage = value;

                if (selectedTabPage != null)
                {
                    selectedTabPage.BringToFront();
                    SelectedTabChanged(selectedTabPage, EventArgs.Empty);
                    this.Refresh();
                }
            }
        }

        public MyTabControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            tabPages = new MyTabPageCollection();
            tabPages.TabContainer = this;

            SelectedTabChanged = new SelectedTabChangedEventHandler(OnSelectedTabPageChanged);
            TabClosing = new TabClosingEventHandler(OnTabPageClosing);
            TabTextChanged = new TabTextChangedEventHandler(OnTabTextChanged);
        }

        public void TabPage_TabTextChanged(object sender, EventArgs e)
        {
            TabTextChanged(sender, e);
            this.Refresh();
        }

        protected virtual void OnSelectedTabPageChanged(object sender, EventArgs e)
        {

        }

        protected virtual void OnTabPageClosing(object sender, ref bool closing)
        {
            
        }

        protected virtual void OnTabTextChanged(object sender, EventArgs e)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                Rectangle rect = GetTabRect(i);
                Rectangle mouseRect = new Rectangle(e.X, e.Y, 1, 1);

                if (e.Y >= rect.Top && e.Y <= rect.Bottom)
                {
                    if (e.X >= rect.Left && e.X <= (rect.Right - 18))
                        this.SelectedTabPage = this.TabPages[i];
                    else if (e.X >= (rect.Right - 18) && e.X <= rect.Right)
                    {
                        MyTabPage tab = this.TabPages[i];

                        if (tab == selectedTabPage)
                        {
                            bool closeTab = true;
                            TabClosing(tab, ref closeTab);

                            if (closeTab)
                            {
                                this.TabPages.Remove(tab);
                                tab.Dispose();
                            }
                        }
                    }

                    this.Refresh();
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnResize(EventArgs e)
        {
            foreach (MyTabPage tab in tabPages)
            {
                tab.Location = new Point(0, 32);
                tab.Size = new Size(this.Width, this.Height - 32);
            }

            base.OnResize(e);
        }

        protected void DrawItem(DrawItemEventArgs e)
        {
            Image closeButton = MyPad.Properties.Resources.CloseIcon;
            MyTabPage tab = this.TabPages[e.Index];

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Control), e.Bounds);
                e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight, 1), new Point(e.Bounds.Left, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Top));
                e.Graphics.DrawLine(new Pen(SystemColors.ControlDarkDark, 1), new Point(e.Bounds.Right, e.Bounds.Top), new Point(e.Bounds.Right, e.Bounds.Bottom - 2));

                e.Graphics.DrawImage(closeButton, new Rectangle(e.Bounds.Right - 18, e.Bounds.Top + 5, 14, 14));
            }

            TextRenderer.DrawText(e.Graphics, tab.Text, e.Font, e.Bounds, this.ForeColor, Color.Transparent,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(SystemColors.Control, 2), new Point(0, 30), new Point(this.Width, 30));
            //e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(0, 0, this.Width, 30));

            int xpos = 5;
            int index = 0;

            foreach (MyTabPage tabPage in tabPages)
            {
                DrawItemState drawState = DrawItemState.Default;
                Font font = this.Font;

                if (selectedTabPage == tabPage)
                {
                    drawState = DrawItemState.Selected;
                    font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
                }

                Size textSize = TextRenderer.MeasureText(e.Graphics, tabPage.Text, font, new Size(int.MaxValue, int.MaxValue));

                Rectangle rect = new Rectangle();
                rect.Location = new Point(xpos, 5);
                rect.Size = new Size(textSize.Width + 18, 25);

                DrawItemEventArgs de = new DrawItemEventArgs(e.Graphics, font, rect, index, drawState);
                DrawItem(de);

                tabPage.Bounds = rect;
                xpos += rect.Width + 5;
                index++;
            }

            base.OnPaint(e);
        }

        private Rectangle GetTabRect(int index)
        {
            if (this.TabPages.Count > index)
                return this.TabPages[index].Bounds;
            return Rectangle.Empty;
        }
    }
}

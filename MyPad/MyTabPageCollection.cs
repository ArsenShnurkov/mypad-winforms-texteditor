using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MyPad
{
    public class MyTabPageCollection
    {
        List<MyTabPage> tabPages;
        MyTabControl tabContainer;

        public MyTabPage this[int index]
        {
            get
            {
                if (tabPages.Count > index)
                    return tabPages[index];
                return null;
            }
            set
            {
                if (tabPages.Count > index)
                    tabPages[index] = value;
            }
        }

        public MyTabControl TabContainer
        {
            get
            {
                return tabContainer;
            }
            set
            {
                tabContainer = value;
            }
        }

        public int Count
        {
            get
            {
                return tabPages.Count;
            }
        }

        public MyTabPageCollection()
        {
            tabPages = new List<MyTabPage>();
        }

        public void Add(string text)
        {
            MyTabPage tp = new MyTabPage();
            tp.Text = text;
            Add(tp);
        }

        public void Add(MyTabPage page)
        {
            tabPages.Add(page);

            page.Size = new Size(tabContainer.Width, tabContainer.Height - 32);
            page.Location = new Point(0, 32);
            page.TextChanged += new MyTabPage.TextChangedEventHandler(tabContainer.TabPage_TabTextChanged);
            page.Show();

            tabContainer.Controls.Add(page);
            tabContainer.Refresh();
        }

        public int IndexOf(MyTabPage page)
        {
            return tabPages.IndexOf(page);
        }

        public void Remove(MyTabPage page)
        {
            tabPages.Remove(page);
        }

        public void RemoveAt(int index)
        {
            if (tabPages.Count > index)
                tabPages.RemoveAt(index);
        }

        public IEnumerator<MyTabPage> GetEnumerator()
        {
            return tabPages.GetEnumerator();
        }
    }
}

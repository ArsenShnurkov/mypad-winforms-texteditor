using System;
using System.Windows.Forms;

namespace MyPad
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.ToolStripMenuItem showIndexedSearchTabPageToolStripMenuItem;

        private void showIndexedSearchTabPageToolStripMenuItem_Click (object sender, EventArgs e)
        {
            var newSearchTab = new IndexedSearchTabPage ();
            newSearchTab.Text = "Поиск по индексу";
            tabControl1.Controls.Add (newSearchTab);
            tabControl1.SelectTab (newSearchTab);
        }

        private void InitializeIndexedSearchTabPageMenuItem ()
        {
            this.showIndexedSearchTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            // 
            // showSearchTabPageToolStripMenuItem
            // 
            this.showIndexedSearchTabPageToolStripMenuItem.Name = "showSearchTabPageToolStripMenuItem";
            this.showIndexedSearchTabPageToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.showIndexedSearchTabPageToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.showIndexedSearchTabPageToolStripMenuItem.Text = "Search by index";
            this.showIndexedSearchTabPageToolStripMenuItem.Click += new System.EventHandler (this.showIndexedSearchTabPageToolStripMenuItem_Click);

            this.toolsToolStripMenuItem.DropDownItems.Insert (0, showIndexedSearchTabPageToolStripMenuItem);
        }

    }
}

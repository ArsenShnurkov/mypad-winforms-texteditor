using System;
using System.Windows.Forms;

namespace MyPad
{
    public partial class MainForm : Form
    {
        private void showSearchTabPageToolStripMenuItem_Click (object sender, EventArgs e)
        {
            var newSearchTab = new SearchTabPage ();
            newSearchTab.Text = "Поиск";
            tabControl1.Controls.Add (newSearchTab);
            tabControl1.SelectTab (newSearchTab);
        }
    }
}

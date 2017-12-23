using System;
using System.Windows.Forms;

namespace MyPad
{
    public partial class MainForm : Form
    {
        private void toolStripButtonInsertImageURL_Click (object sender, EventArgs e)
        {
            enchanceImagelinkToolStripMenuItem_Click (sender, e);
        }

        private void enchanceImagelinkToolStripMenuItem_Click (object sender, EventArgs e)
        {
            TabPage tb = tabControl1.SelectedTab;
            if (tb as EditorTabPage == null) {
                return;
            }
            EditorTabPage etb = tb as EditorTabPage;
            etb.EnchanceImagelink ();
        }

        public string InputFileFromURL ()
        {
            throw new NotImplementedException();
            //return "<img src=\"./test_of_image.png\" />";
        }
    }
}

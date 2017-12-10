using System;
using System.Windows.Forms;

namespace MyPad
{
    public partial class SearchTabPage : TabPage
    {
        public SearchTabPage ()
        {
            InitializeComponent ();
        }

        private void buttonStartSearch_Click (object sender, EventArgs e)
        {
                        MessageBox.Show ("Start");
        }

        private void buttonStopSearch_Click (object sender, EventArgs e)
        {
            MessageBox.Show ("Stop");
        }
    }
}

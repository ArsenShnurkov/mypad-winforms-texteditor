using System;
using System.ComponentModel;
using System.Diagnostics;
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
            this.backgroundWorker1.RunWorkerAsync ();
            for (int i = 0; i < 1000; i++) {
            }
        }

        private void buttonStopSearch_Click (object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy) {

                // Notify the worker thread that a cancel has been requested.
                // The cancel will not actually happen until the thread in the
                // DoWork checks the m_oWorker.CancellationPending flag. 
                this.backgroundWorker1.CancelAsync ();
            }
        }

        void backgroundWorker1_ProgressChanged (object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine (e.ProgressPercentage);

            int i = e.ProgressPercentage;
            var res = e.UserState as SearchResult;
            TreeNode newNode = new TreeNode ();
            newNode.Name = res.line.ToString ();
            newNode.Text = res.fi.FullName;
            this.treeViewResults.Nodes.Add (newNode);
        }

        void backgroundWorker1_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled) {
                MessageBox.Show ("Cancelled");
                return;
            }
            if (e.Error != null) {
                MessageBox.Show ("Error while performing background operation.");
                return;
            }
        }
    }
}

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            SearchRequest req = new SearchRequest ();
            req.SearchDirectory = Globals.LoadConfiguration ().AppSettings.Settings ["SearchDirectory"]?.Value;
            req.query = this.textBoxSearchString.Text;
            this.backgroundWorker1.RunWorkerAsync (req);
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

            var res = e.UserState as SearchResult;
            if (res == null) {
                return;
            }
            TreeNode fileNode = GetTreeNodeFromName (res.filePathName);
            TreeNode newNode = fileNode.Nodes.Add (res.line.ToString ("000000"), res.filePathName + "(" + res.line.ToString () + ")");
            newNode.Tag = res;
        }
        TreeNode GetTreeNodeFromName (string name)
        {
            FileInfo fi = new FileInfo (name);
            // Build list of parts
            Stack<string> parts = new Stack<string> ();
            parts.Push (fi.Name);
            DirectoryInfo dir = fi.Directory;
            do {
                parts.Push (dir.Name);
                dir = dir.Parent;
            }
            while (dir.FullName != dir.Root.FullName);
            // create such path in tree
            TreeNode res = null;
            var currentSet = treeViewResults.Nodes;
            while (parts.Count > 0) {
                string part = parts.Pop ();
                res = null;
                for (int nc = 0; nc < currentSet.Count; ++nc) {
                    if (string.Compare (currentSet [nc].Text, part) == 0) {
                        res = currentSet [nc];
                        break;
                    }
                }
                if (res == null) {
                    res = new TreeNode (part);
                    if (parts.Count > 0) {
                        res.Expand ();
                    }
                    currentSet.Add (res);
                }
                currentSet = res.Nodes;
            }
            return res;
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

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

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
            // set tab caption
            this.Text = this.textBoxSearchString.Text;
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
            FileInfo fi = new FileInfo (res.filePathName);
            TreeNode fileNode = GetTreeNodeFromName (fi.FullName);
            try {
                string nodeText = res.lineNumber.ToString () + ":" + res.lineContent;
                string tag = "#L" + res.lineNumber.ToString ();
                TreeNode newNode = new TreeNode (tag);
                newNode.Text = nodeText.Substring (0, Math.Min (nodeText.Length, 100));
                newNode.Tag = new TreeNodeInfo (fi.Name, fi.FullName, res.lineNumber);
                fileNode.Nodes.Add (newNode);
                //fileNode.Expand ();
                //newNode.EnsureVisible ();
                fileNode.EnsureVisible ();
            } catch (Exception) {
                fileNode.BackColor = Color.Red;
            }
        }

        //TreeNode fileNode = GetTreeNodeFromName (res.filePathName);
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
                    if (string.Compare (((TreeNodeInfo)(currentSet [nc].Tag)).ShortName, part) == 0) {
                        res = currentSet [nc];
                        break;
                    }
                }
                if (res == null) {
                    res = new TreeNode (part);
                    res.Tag = new TreeNodeInfo (part);
                    if (parts.Count > 0) {
                        res.Expand ();
                    } else {
                        ((TreeNodeInfo)res.Tag).LongName = fi.FullName;
                        string title = Globals.GetTextTitleFromFile (fi.FullName);
                        if (string.IsNullOrWhiteSpace (title) == false) {
                            res.Text = fi.LastWriteTimeUtc.ToString ("yyyy") + ", \"" + title + "\", " + part;
                        }
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

        // If a node is double-clicked, open the file indicated by the TreeNode.
        void treeViewResults_NodeMouseDoubleClick (object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNodeInfo tag = (TreeNodeInfo)e.Node.Tag;
            if (string.IsNullOrWhiteSpace (tag.LongName) == false) {
                Globals.GetMainForm ().InternalOpenFile (tag.LongName);
            }
        }

        void treeViewResults_KeyPress (object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar (Keys.Enter)) {
                TreeViewEventArgs args = new TreeViewEventArgs (treeViewResults.SelectedNode, TreeViewAction.ByKeyboard);
                if (treeViewResults.SelectedNode == null) {
                    return;
                }
                TreeNodeInfo tag = (TreeNodeInfo)treeViewResults.SelectedNode.Tag;
                if (string.IsNullOrWhiteSpace (tag.LongName) == false) {
                    Globals.GetMainForm ().InternalOpenFile (tag.LongName);
                }
                e.Handled = true;
            }
            base.OnKeyPress (e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace MyPad
{
    public partial class IndexedSearchTabPage : TabPage
    {
        FileSearcherBackgroundWorker backgroundWorker1;

        private void InitializeBackgroundWorker ()
        {
            backgroundWorker1 = new FileSearcherBackgroundWorker ();
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void SwitchToEditMode (bool bEditMode)
        {
            if (bEditMode) {
                buttonStartSearch.Left = buttonStartSearch.Parent.Right - buttonStartSearch.Width - 10;
                this.textBoxSearchString.Width = buttonStartSearch.Left - textBoxSearchString.Left - 10;
            } else {
                buttonStopSearch.Left = buttonStopSearch.Parent.Right - buttonStopSearch.Width - 10;
                progressBarSearch.Width = buttonStopSearch.Left - progressBarSearch.Left - 10;
            }
            this.textBoxSearchString.Visible = bEditMode;
            this.buttonStartSearch.Visible = bEditMode;
            this.progressBarSearch.Visible = !bEditMode;
            this.buttonStopSearch.Visible = !bEditMode;
        }

        private void buttonStartSearch_Click (object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy) {
                return;
            }
            SearchRequest req = new SearchRequest ();
            req.SearchDirectory = Globals.LoadConfiguration ().AppSettings.Settings ["SearchDirectory"]?.Value;
            req.query = this.textBoxSearchString.Text;
            // set tab caption
            this.Text = this.textBoxSearchString.Text;
            SwitchToEditMode (false);
            this.backgroundWorker1.RunWorkerAsync (req);
        }

        private void buttonStopSearch_Click (object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy) {

                // Notify the worker thread that a cancel has been requested.
                // The cancel will not actually happen until the thread in the
                // DoWork checks the m_oWorker.CancellationPending flag. 
                this.backgroundWorker1.CancelAsync ();
                while (this.backgroundWorker1.IsBusy) {
                    Application.DoEvents ();
                }
                SwitchToEditMode (true);
            }
        }

        void backgroundWorker1_ProgressChanged (object sender, ProgressChangedEventArgs e)
        {
            var res = e.UserState as SearchResult;
            if (res == null) {
                return;
            }
            this.progressBarSearch.Value = e.ProgressPercentage;
            FileInfo fi = new FileInfo (res.filePathName);
            Debug.WriteLine (res.filePathName);
            TreeNode fileNode = GetTreeNodeFromName (fi.FullName);
            try {
                string nodeText = res.lineNumber.ToString () + ":" + res.lineContent;
                string tag = "#L" + res.lineNumber.ToString ();
                TreeNode newNode = new TreeNode (tag);
                newNode.Text = nodeText.Substring (0, Math.Min (nodeText.Length, 100));
                newNode.Tag = new TreeNodeInfo (fi.Name, fi.FullName, res.lineNumber);
                fileNode.Nodes.Add (newNode);
            } catch (Exception) {
                fileNode.BackColor = Color.Red;
            }
        }

        void backgroundWorker1_RunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)
        {
            SwitchToEditMode (true);
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled) {
                //MessageBox.Show ("Cancelled");
                return;
            }
            if (e.Error != null) {
                MessageBox.Show ("Error while performing background operation.");
                return;
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
            string currentPath = dir.FullName;
            while (parts.Count > 0) {
                string part = parts.Pop ();
                currentPath = Path.Combine (currentPath, part);
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
                        res.ContextMenuStrip = directoryMenu;
                        ((TreeNodeInfo)res.Tag).LongName = currentPath;
                        res.ToolTipText = currentPath;
                        res.Expand ();
                        treeViewResults.Invalidate ();
                        Thread.Sleep (10);
                    } else {
                        res.ContextMenuStrip = docMenu;
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
    }
}


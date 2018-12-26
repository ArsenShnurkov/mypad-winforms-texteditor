using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace MyPad
{
    public partial class IndexedSearchTabPage : TabPage, IInitializable
    {
        ContextMenuStrip docMenu;
        ToolStripMenuItem editLabel;
        ToolStripSeparator separatorLabel;
        ToolStripMenuItem firefoxLabel;

        ContextMenuStrip directoryMenu;
        ToolStripMenuItem labelExploreDirectory;

        public IndexedSearchTabPage ()
        {
            InitializeComponent ();

            // Populate directory context menu
            directoryMenu = new ContextMenuStrip ();

            labelExploreDirectory = new ToolStripMenuItem ();
            labelExploreDirectory.Text = "Explore";
            labelExploreDirectory.Click += labelExploreDirectory_Click;

            directoryMenu.Items.AddRange (
                new ToolStripItem [] { labelExploreDirectory });

            // Populate file context menu
            docMenu = new ContextMenuStrip ();

            //Create some menu items.
            editLabel = new ToolStripMenuItem ();
            editLabel.Text = "Edit";
            editLabel.Click += editLabel_Click;
            separatorLabel = new ToolStripSeparator ();
            firefoxLabel = new ToolStripMenuItem ();
            firefoxLabel.Text = "Firefox";
            firefoxLabel.Click += firefoxLabel_Click;

            //Add the menu items to the menu.
            docMenu.Items.AddRange (
                new ToolStripItem [] { editLabel, separatorLabel, firefoxLabel });

        }

        private bool _isInitialized;

        public void Initialize ()
        {
            if (_isInitialized) {
                return;
            }
            textBoxSearchString.Select ();
            _isInitialized = true;
        }

        private void buttonStartSearch_Click (object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy) {
                this.tabControlOutput.SelectedTab = this.tabPageSearchStatistics;
                return;
            }
            SearchRequest req = new SearchRequest ();
            req.SearchDirectory = Globals.LoadConfiguration ().AppSettings.Settings ["SearchDirectory"]?.Value;
            req.query = this.textBoxSearchString.Text;
            // set tab caption
            this.Text = this.textBoxSearchString.Text;
            this.backgroundWorker1.RunWorkerAsync (req);
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
            var res = e.UserState as SearchResult;
            if (res == null) {
                return;
            }
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

        protected override bool ProcessCmdKey (ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter) {
                buttonStartSearch.PerformClick ();
                treeViewResults.Select ();
                return true;
            }
            return base.ProcessCmdKey (ref msg, keyData);
        }

        private void editLabel_Click (object sender, EventArgs e)
        {
            TreeNode item = this.treeViewResults.SelectedNode;
            TreeNodeInfo info = item.Tag as TreeNodeInfo;
            if (info == null) {
                return;
            }
            var filename = info.LongName;
            if (string.IsNullOrEmpty (filename)) {

                return;
            }
            Globals.GetMainForm ().InternalOpenFile (filename);
        }

        private void firefoxLabel_Click (object sender, EventArgs e)
        {
            TreeNode item = this.treeViewResults.SelectedNode;
            TreeNodeInfo info = item.Tag as TreeNodeInfo;
            if (info == null) {
                return;
            }
            var filename = info.LongName;

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo ();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = filename;
            // Enter the executable to run, including the complete path
            start.FileName = "/usr/bin/firefox";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start (start)) {
                proc.WaitForExit ();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }

        private void labelExploreDirectory_Click (object sender, EventArgs e)
        {
            TreeNode item = this.treeViewResults.SelectedNode;
            TreeNodeInfo info = item.Tag as TreeNodeInfo;
            if (info == null) {
                return;
            }
            var directory = info.LongName;

            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo ();
            // Enter in the command line arguments, everything you would enter after the executable name itself
            start.Arguments = directory;
            // Enter the executable to run, including the complete path
            start.FileName = "/usr/bin/caja";
            // Do you want to show a console window?
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            // Run the external process & wait for it to finish
            using (Process proc = Process.Start (start)) {
                proc.WaitForExit ();

                // Retrieve the app's exit code
                exitCode = proc.ExitCode;
            }
        }
    }
}

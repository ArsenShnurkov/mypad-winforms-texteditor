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
            InitializeBackgroundWorker ();
            InitializeComponent ();
            panelContainer.ActiveControl = textBoxSearchString;

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

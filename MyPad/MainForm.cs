using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

using MyPad.Dialogs;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

//using LuaInterface;
using System.Diagnostics;

namespace MyPad
{
    public partial class MainForm : Form
    {
        OptionsDialog optionsDialog;
        EntriesListDialog entriesListDialog;
        AboutDialog aboutDialog;

        FindReplaceDialog findReplaceDialog = new FindReplaceDialog();
        FindDialog findDialog = new FindDialog();
        string fileToLoad = String.Empty;

        public MainForm()
        {
            InitializeComponent();

            SettingsManager.Init();

            if (SettingsManager.MRUList.Count > 0)
            {
                foreach (string file in SettingsManager.MRUList)
                {
                    ToolStripItem tsi = new ToolStripMenuItem(file);
                    tsi.Click += new EventHandler(RecentFiles_Click);
                    recentFilesToolStripMenuItem.DropDown.Items.Add(tsi);
                }
            }

            unsavedDocumentsDialog = new UnsavedDocumentsDialog();
            optionsDialog = new OptionsDialog();
            entriesListDialog = new EntriesListDialog();
            aboutDialog = new AboutDialog();

            int x = SettingsManager.ReadValue<int>("MainWindowX");
            int y = SettingsManager.ReadValue<int>("MainWindowY");
            int width = SettingsManager.ReadValue<int>("MainWindowWidth");
            int height = SettingsManager.ReadValue<int>("MainWindowHeight");

            if (width < 100)
            {
                width = 800;
            }
            if (height < 100)
            {
                height = 600;
            }

            this.Location = new Point(x, y);
            this.Size = new Size(width, height);

            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            tabControl1.DragEnter += new DragEventHandler(tabControl1_DragEnter);
            tabControl1.DragDrop += new DragEventHandler(tabControl1_DragDrop);
        }

        public MainForm(string file)
            : this()
        {
            if (File.Exists(file))
            {
                fileToLoad = file;
            }
        }


        public void UpdateMainWindowTitle()
        {
            EditorTabPage etb = GetActiveTab();
            if (etb != null)
            {
                this.Text = string.Format("MyPad - {0} [{1}]", etb.Text, etb.GetFileFullPathAndName());
            }
        }

        public void SetupActiveTab()
        {
            EditorTabPage etb = GetActiveTab();
            if (etb != null)
            {
                etb.Show();

                UpdateMainWindowTitle();
                etb.Editor.Focus();

                toolStripStatusLabel2.Text = string.Format("Line: {0}", (etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line + 1).ToString());
                toolStripStatusLabel3.Text = string.Format("Col: {0}", etb.Editor.ActiveTextAreaControl.TextArea.Caret.Column.ToString());

                string highlighter = etb.Editor.Document.HighlightingStrategy.Name;

                foreach (ToolStripMenuItem tsi in highlightingToolStripMenuItem.DropDownItems)
                {
                    if (tsi.Name == highlighter)
                    {
                        tsi.Checked = true;
                    }
                    else
                    {
                        tsi.Checked = false;
                    }
                }
            }
            else
            {
                this.Text = "MyPad";
            }
        }

        public EditorTabPage GetActiveTab()
        {
            if (tabControl1.SelectedTab != null)
            {
                return (EditorTabPage)tabControl1.SelectedTab;
            }
            return null;
        }

        public EditorTabPage GetTabByTitle(string title)
        {
            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (etb.Text == title || etb.Text.StartsWith(title))
                {
                    return etb;
                }
            }
            return null;
        }

        public TextEditorControl GetActiveEditor()
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                return etb.Editor;
            }
            return null;
        }

        public void ClearCheckedHighlighters()
        {
            foreach (ToolStripMenuItem item in highlightingToolStripMenuItem.DropDown.Items)
            {
                item.Checked = false;
            }
        }

        public ToolStripMenuItem GetRecentMenuItem(string text)
        {
            ToolStripMenuItem tsi = null;

            foreach (ToolStripMenuItem tsmi in recentFilesToolStripMenuItem.DropDownItems)
            {
                if (tsmi.Text == text)
                    tsi = tsmi;
            }

            return tsi;
        }


        public EditorTabPage FindTabByPath(string fileToLoad)
        {
            foreach (var tab in tabControl1.TabPages)
            {
                var t = (EditorTabPage)tab;
                string file = t.GetFileFullPathAndName ();
                if (string.Compare(file, fileToLoad) == 0)
                {
                    return t;
                }
            }
            return null;
        }

        delegate void InternalOpenFileDelegate(string fileToLoad);

        internal void InvokeOpenFile(string fileToLoad)
        {
            if (this.InvokeRequired)
            {
                base.Invoke (new InternalOpenFileDelegate (this.InternalOpenFile), new object[]{ fileToLoad });
            } else
            {
                this.InternalOpenFile (fileToLoad);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            FileSyntaxModeProvider provider = new FileSyntaxModeProvider(Path.Combine(Application.StartupPath, "Modes"));
            HighlightingManager.Manager.AddSyntaxModeFileProvider(provider);

            foreach (string hl in HighlightingManager.Manager.HighlightingDefinitions.Keys)
            {
                ToolStripItem tsi = new ToolStripMenuItem(hl);
                tsi.Click += new EventHandler(Highlighting_Click);
                highlightingToolStripMenuItem.DropDown.Items.Add(tsi);
            }

            base.OnLoad(e);

            if (string.IsNullOrWhiteSpace(fileToLoad) == false && File.Exists(fileToLoad))
            {
                InternalOpenFile(fileToLoad);
            }
            else
            {
                newToolStripMenuItem_Click(null, null);
            }
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);

            if (SettingsManager.Settings != null)
            {
                SettingsManager.Settings["MainWindowX"] = this.Location.X;
                SettingsManager.Settings["MainWindowY"] = this.Location.Y;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (SettingsManager.Settings != null)
            {
                SettingsManager.Settings["MainWindowWidth"] = this.Size.Width;
                SettingsManager.Settings["MainWindowHeight"] = this.Size.Height;
            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            if ((drgevent.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                drgevent.Effect = DragDropEffects.Link;
            }

            base.OnDragEnter(drgevent);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if ((drgevent.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
            {
                drgevent.Effect = DragDropEffects.Link;
            }

            IDataObject iData = drgevent.Data;

            if (iData.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])iData.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        InternalOpenFile(file);
                    }
                }
            }

            base.OnDragDrop(drgevent);
        }

        void RecentFiles_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;

            InternalOpenFile(tsi.Text);

            recentFilesToolStripMenuItem.DropDown.Items.Remove(tsi);
            recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);

            SettingsManager.MRUList.Remove(tsi.Text);
            SettingsManager.MRUList.Insert(0, tsi.Text);
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetupActiveTab();
        }

        void tabControl1_DragDrop(object sender, DragEventArgs e)
        {
            OnDragDrop(e);
        }

        void tabControl1_DragEnter(object sender, DragEventArgs e)
        {
            OnDragEnter(e);
        }

        private void Highlighting_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;

            if (etb != null)
            {
                etb.SetHighlighting(tsi.Text);
                string name = etb.GetHighlighting();

                if (tsi.Text.Equals(name))
                {
                    tsi.Checked = true;
                }
            }
        }

        void etb_TextChanged(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                toolStripStatusLabel2.Text = string.Format("Line: {0}", (etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line + 1).ToString());
                toolStripStatusLabel3.Text = string.Format("Col: {0}", etb.Editor.ActiveTextAreaControl.TextArea.Caret.Column.ToString());
            }
        }

        void TabControl_TabCaptionUpdate(object sender, EventArgs e)
        {
            tabControl1.Invalidate(); // redraw tab captions if required
            UpdateMainWindowTitle();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                InternalOpenFile(file);

                if (!SettingsManager.MRUList.Contains(file))
                {
                    if (SettingsManager.MRUList.Count >= 15)
                        SettingsManager.MRUList.RemoveAt(14);
                    SettingsManager.MRUList.Insert(0, file);

                    ToolStripMenuItem tsi = new ToolStripMenuItem(file, null, new EventHandler(RecentFiles_Click));
                    recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);
                }
                else
                {
                    SettingsManager.MRUList.Remove(file);
                    SettingsManager.MRUList.Insert(0, file);

                    ToolStripMenuItem tsi = GetRecentMenuItem(file);
                    recentFilesToolStripMenuItem.DropDown.Items.Remove(tsi);
                    recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);
                }
            }
        }

        private void reloadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*EditorTabPage etb = */
            GetActiveTab();
        }


        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Delete();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.SelectAll();
        }

        private void enchanceHyperlinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.EnchanceHyperlink();
            }
        }
        private void enchanceImagelinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.EnchanceImagelink();
            }
        }
        private void MakeBoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeBold();
            }
        }
        private void MakeSelectionRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeSelectionRed();
            }
        }
        private void MakeBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeBR();
            }
        }
        private void MakeH1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("1");
            }
        }
        private void MakeH2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("2");
            }
        }
        private void MakeH3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("3");
            }
        }
        private void MakeH4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("4");
            }
        }
        private void MakeH5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("5");
            }
        }
        private void MakeH6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MakeH("6");
            }
        }
        private void insertNbspToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.InsertTextAtCursor("&nbsp;");
            }
        }

        private void moveLineUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MoveLineUp(etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line);
            }
        }

        private void moveLineDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.MoveLineDown(etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line);
            }
        }

        private void cutLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.SelectLine(etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line);
                etb.Cut();
            }
        }

        private void copyLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.SelectLine(etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line);
                etb.Copy();

                clearSelectionToolStripMenuItem_Click(null, null);
            }
        }

        private void commentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {

            }
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                etb.Editor.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
            }
        }

        private void wrapInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {

            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                findDialog.SetFocusOnSearchTextField();
                if (findDialog.ShowDialog() == DialogResult.OK)
                {
                    int index = etb.Find(findDialog.Search, findDialog.Options);
                    if (index >= 0)
                    {
                        etb.ScrollToOffset(index);
                    }
                }
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                int index = etb.Find(findDialog.Search, findDialog.Options);
                if (index >= 0)
                {
                    etb.ScrollToOffset(index);
                }
            }
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                findReplaceDialog.SetFocusOnSearchTextField();
                if (findReplaceDialog.ShowDialog() == DialogResult.OK)
                {
                    etb.FindAndReplace(findReplaceDialog.Search, findReplaceDialog.Replacement, findReplaceDialog.Options);
                }
            }
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Visible)
                toolStrip1.Hide();
            else
                toolStrip1.Show();
        }

        private void statusbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStrip1.Visible)
                statusStrip1.Hide();
            else
                statusStrip1.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (optionsDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (EditorTabPage etb in tabControl1.TabPages)
                {
                    etb.ReloadSettings();
                }
            }
        }

        private void atomFeedEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (entriesListDialog.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutDialog.ShowDialog();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            redoToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            findToolStripMenuItem_Click(null, null);
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var tab = (sender as TabControl).TabPages[e.Index];
            //e.DrawBackground();
            Color c = tab.ForeColor;
            using (Brush fore = new SolidBrush(tab.ForeColor))
            {
                using (Brush back = new SolidBrush(tab.BackColor))
                {
                    e.Graphics.FillRectangle(back, e.Bounds);
                    SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, fore, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                    Rectangle rect = e.Bounds;
                    rect.Offset(0, 1);
                    rect.Inflate(0, -1);
                    e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    e.DrawFocusRectangle();
                }
            }
        }

        string GetFullFilename (string filename)
        {
            // Tro to find in tabs
            try {
                filename = new FileInfo (filename).FullName;
            }
            catch (Exception ex) {
                Trace.WriteLine (ex.ToString ());
            }
            return filename;
        }

        public bool AlreadyOpen(string filename)
        {
            if (string.IsNullOrWhiteSpace (filename))
            {
                return false;
            }

            filename = GetFullFilename (filename);

            foreach (EditorTabPage etb in tabControl1.TabPages) {
                if (etb.GetFileFullPathAndName ().CompareTo (filename) == 0) {
                    return true;
                }
            }

            return false;
        }

        public bool Exists(string filename)
        {
            if (string.IsNullOrWhiteSpace (filename))
            {
                return false;
            }

            if (AlreadyOpen (filename))
            {
                return true;
            }

            filename = GetFullFilename (filename);

            // try to find on disk
            if (File.Exists (filename))
            {
                return true;
            }

            return false;
        }
    }
}

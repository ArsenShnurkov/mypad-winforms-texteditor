using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

namespace MyPad
{
    public partial class MainForm : Form
    {
        UnsavedDocumentsDialog unsavedDocumentsDialog;
        OptionsDialog optionsDialog;
        AboutDialog aboutDialog;

        FindReplaceDialog findReplaceDialog;
        FindDialog findDialog;

        string fileToLoad = "";

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
            aboutDialog = new AboutDialog();

            findReplaceDialog = new FindReplaceDialog();
            findDialog = new FindDialog();

            int x = SettingsManager.ReadValue<int>("MainWindowX");
            int y = SettingsManager.ReadValue<int>("MainWindowY");
            int width = SettingsManager.ReadValue<int>("MainWindowWidth");
            int height = SettingsManager.ReadValue<int>("MainWindowHeight");

            if (width < 100)
                width = 800;
            if (height < 100)
                height = 600;

            this.Location = new Point(x, y);
            this.Size = new Size(width, height);

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

        public EditorTabPage GetActiveTab()
        {
            if (tabControl1.SelectedTab != null)
                return (EditorTabPage)tabControl1.SelectedTab;
            return null;
        }

        public EditorTabPage GetTabByTitle(string title)
        {
            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (etb.Text == title || etb.Text.StartsWith(title))
                    return etb;
            }
            return null;
        }

        public TextEditorControl GetActiveEditor()
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                return etb.Editor;
            return null;
        }

        public int GetUntitledTabCount()
        {
            int count = 0;

            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (etb.Text.StartsWith("Untitled"))
                    count++;
            }

            return count;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            SettingsManager.Save();

            unsavedDocumentsDialog.ClearDocuments();

            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (!etb.Saved)
                    unsavedDocumentsDialog.AddDocument(etb.ToolTipText);
            }

            if (unsavedDocumentsDialog.SelectedDocumentsCount > 0)
            {
                DialogResult dr = unsavedDocumentsDialog.ShowDialog();

                if (dr == DialogResult.Yes)
                {
                    IEnumerable<string> documents = unsavedDocumentsDialog.SelectedDocuments;

                    foreach (string document in documents)
                    {
                        string file = Path.GetFileName(document);

                        EditorTabPage etb = GetTabByTitle(file);

                        if (etb != null)
                        {
                            tabControl1.SelectedTab = etb;
                            saveToolStripMenuItem_Click(null, null);
                        }
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }

            base.OnClosing(e);
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

            if (fileToLoad != null && File.Exists(fileToLoad))
            {
                EditorTabPage etb = new EditorTabPage();
                etb.LoadFile(fileToLoad);
                etb.Editor.DragEnter += new DragEventHandler(tabControl1_DragEnter);
                etb.Editor.DragDrop += new DragEventHandler(tabControl1_DragDrop);
                etb.EditorTextChanged += new EventHandler(etb_TextChanged);
                etb.Show();

                tabControl1.TabPages.Add(etb);
                tabControl1.SelectedTab = etb;

                if (!SettingsManager.MRUList.Contains(fileToLoad))
                {
                    if (SettingsManager.MRUList.Count >= 15)
                        SettingsManager.MRUList.RemoveAt(14);
                    SettingsManager.MRUList.Insert(0, fileToLoad);

                    ToolStripMenuItem tsi = new ToolStripMenuItem(fileToLoad, null, new EventHandler(RecentFiles_Click));
                    recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);
                }
                else
                {
                    SettingsManager.MRUList.Remove(fileToLoad);
                    SettingsManager.MRUList.Insert(0, fileToLoad);

                    ToolStripMenuItem tsi = GetRecentMenuItem(fileToLoad);
                    recentFilesToolStripMenuItem.DropDown.Items.Remove(tsi);
                    recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);
                }
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
                        EditorTabPage etb = new EditorTabPage();
                        etb.LoadFile(file);
                        etb.Show();

                        tabControl1.TabPages.Add(etb);
                        tabControl1.SelectedTab = etb;
                    }
                }
            }

            base.OnDragDrop(drgevent);
        }

        void RecentFiles_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;

            EditorTabPage etb = new EditorTabPage();
            etb.LoadFile(tsi.Text);
            etb.Show();

            tabControl1.TabPages.Add(etb);
            tabControl1.SelectedTab = etb;

            recentFilesToolStripMenuItem.DropDown.Items.Remove(tsi);
            recentFilesToolStripMenuItem.DropDown.Items.Insert(0, tsi);

            SettingsManager.MRUList.Remove(tsi.Text);
            SettingsManager.MRUList.Insert(0, tsi.Text);
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (e.Button == MouseButtons.Middle)
            {
                if (etb != null)
                    closeToolStripMenuItem_Click(null, null);
            }
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                this.Text = string.Format("MyPad - {0} [{1}]", etb.Text, etb.ToolTipText);
                etb.Editor.Focus();

                toolStripStatusLabel2.Text = string.Format("Line: {0}", (etb.Editor.ActiveTextAreaControl.TextArea.Caret.Line + 1).ToString());
                toolStripStatusLabel3.Text = string.Format("Col: {0}", etb.Editor.ActiveTextAreaControl.TextArea.Caret.Column.ToString());

                string highlighter = etb.Editor.Document.HighlightingStrategy.Name;

                foreach (ToolStripMenuItem tsi in highlightingToolStripMenuItem.DropDownItems)
                {
                    if (tsi.Name == highlighter)
                        tsi.Checked = true;
                    else
                        tsi.Checked = false;
                }
            }
            else
            {
                this.Text = "MyPad";
            }
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = new EditorTabPage();
            etb.Text = string.Format("Untitled{0}", GetUntitledTabCount());
            etb.ToolTipText = etb.Text;
            etb.Editor.DragEnter += new DragEventHandler(tabControl1_DragEnter);
            etb.Editor.DragDrop += new DragEventHandler(tabControl1_DragDrop);
            etb.EditorTextChanged += new EventHandler(etb_TextChanged);
            etb.Show();

            tabControl1.TabPages.Add(etb);
            tabControl1.SelectedTab = etb;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                EditorTabPage etb = new EditorTabPage();
                etb.LoadFile(file);
                etb.Editor.DragEnter += new DragEventHandler(tabControl1_DragEnter);
                etb.Editor.DragDrop += new DragEventHandler(tabControl1_DragDrop);
                etb.EditorTextChanged += new EventHandler(etb_TextChanged);
                etb.Show();

                tabControl1.TabPages.Add(etb);
                tabControl1.SelectedTab = etb;

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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();
            string file = "";

            if (etb != null)
            {
                if (File.Exists(etb.ToolTipText))
                {
                    etb.SaveFile(etb.ToolTipText);
                    file = etb.ToolTipText;
                }
                else
                {
                    saveAsToolStripMenuItem_Click(null, null);
                    file = etb.Text;
                }

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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string file = saveFileDialog1.FileName;
                    etb.SaveFile(file);
                }
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage currentActiveTab = GetActiveTab();

            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                tabControl1.SelectedTab = etb;
                saveToolStripMenuItem_Click(null, null);
            }

            tabControl1.SelectedTab = currentActiveTab;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                if (!etb.Saved)
                {
                    DialogResult dr = MessageBox.Show("You are about to close an unsaved document, do you want to save it now?",
                                          "MyPad - Save document", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        saveToolStripMenuItem_Click(null, null);
                        etb.Dispose();
                    }
                    else if (dr == DialogResult.No)
                        etb.Dispose();
                }
                else
                {
                    etb.Dispose();
                }
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                tabControl1.SelectedTab = etb;
                closeToolStripMenuItem_Click(null, null);
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                if (findDialog.ShowDialog() == DialogResult.OK)
                {
                    etb.Find(findDialog.Search, findDialog.Options);
                }
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
                etb.Find(findDialog.Search, findDialog.Options);
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
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

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(null, null);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            saveAllToolStripMenuItem_Click(null, null);
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
    }
}

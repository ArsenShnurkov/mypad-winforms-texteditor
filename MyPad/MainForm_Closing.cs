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

namespace MyPad
{
    public partial class MainForm : Form
    {
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (etb != null)
            {
                if (etb.IsSavingNecessary == true)
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
                SetupActiveTab();
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

        protected override void OnClosing(CancelEventArgs e)
        {
            SettingsManager.Save();

            unsavedDocumentsDialog.ClearDocuments();

            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (!etb.Saved)
                    unsavedDocumentsDialog.AddDocument(etb.GetFileFullPathAndName());
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
    }
}


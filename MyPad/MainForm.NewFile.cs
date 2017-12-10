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
//using System.Web.Helpers; 

using MyPad.Dialogs;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

//using LuaInterface;

namespace MyPad
{
    public partial class MainForm : Form
    {
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage currentActiveTab = GetActiveTab();

            bool bSavingNecessary = true;
            EditorTabPage etb = new EditorTabPage();
            string newTabName = null;

            string nearestInternalLink = GetNearestInternalLink(etb.Editor);
            if (string.IsNullOrWhiteSpace(nearestInternalLink))
            {
                newTabName = string.Format("Untitled{0}", tabControl1.GetUntitledTabCount());
                bSavingNecessary = false; // это совершенно новый файл и его можно не сохранять
            }
            else
            {
                string name = newTabName = string.Format("Untitled{0}", tabControl1.GetUntitledTabCount());
                // name = nearestInternalLink;
                string fullPath = Path.Combine(etb.GetFileFullPathAndName(), name);
                if (File.Exists(fullPath))
                {
                    InternalOpenFile(fullPath);
                    return;
                }
                newTabName = fullPath;
                // А здесь уже есть полезная информация от пользователя - имя файла,
                // поэтому не сохранять молча уже нельзя
            }

            var oldName = currentActiveTab == null ? String.Empty : currentActiveTab.GetFileFullPathAndName();
            etb.Editor.Text = Globals.GetDefaultTemplateText(newTabName, oldName, newTabName);
            etb.IsSavingNecessary = bSavingNecessary;
            etb.SetFileName(newTabName);


            etb.Editor.DragEnter += new DragEventHandler(tabControl1_DragEnter);
            etb.Editor.DragDrop += new DragEventHandler(tabControl1_DragDrop);
            etb.OnEditorTextChanged += new EventHandler(etb_TextChanged);
            etb.OnEditorTabFilenameChanged += new EventHandler(TabControl_TabCaptionUpdate);
            etb.OnEditorTabStateChanged += new EventHandler(TabControl_TabCaptionUpdate);
            etb.Show();

            tabControl1.TabPages.Add(etb);
            tabControl1.SelectedTab = etb;

        }
        private string GetCurrentLineText(TextEditorControl textEditorControl)
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Save selected text
            string text = textArea.SelectionManager.SelectedText;

            int line;

            if (textArea.SelectionManager.HasSomethingSelected)
            {
                // Get point at start of selection
                line = textArea.SelectionManager.SelectionCollection [0].StartPosition.Line;
                // deselect text
                //textArea.SelectionManager.ClearSelection();
            } 
            else
            {
                line = textArea.Caret.Position.Line;
            }

            var lineSeg = textArea.TextView.Document.GetLineSegment(line);
            var linetext = textArea.TextView.Document.GetText(lineSeg);

            return linetext;
        }

        private string GetNearestInternalLink(TextEditorControl editor)
        {
            string str = GetCurrentLineText(editor);
            Regex ex = new Regex (@">(.*)</a");
            var m = ex.Match (str);
            if (m != null && m.Captures.Count > 1)
            {
                var res = m.Captures[1].Value;
                return res;
            }
            return null;
        }

    }
}


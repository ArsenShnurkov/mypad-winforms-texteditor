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
                string fullPath = Path.Combine(etb.ToolTipText, nearestInternalLink);
                if (File.Exists(fullPath))
                {
                    InternalOpenFile(fullPath);
                    return;
                }
                newTabName = fullPath;
                // А здесь уже есть полезная информация от пользователя - имя файла,
                // поэтому не сохранять молча уже нельзя
            }

            etb.Editor.Text = GetDefaultTemplateText();
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
        private string GetCurrentLineText(TextEditorControl editor)
        {
            return string.Empty;
        }

        private string GetNearestInternalLink(TextEditorControl editor)
        {
            string str = GetCurrentLineText(editor);
            return null;
        }

        private string GetDefaultTemplateText()
        {
            var links = new StringBuilder();
            links.AppendFormat("<a href=\"{0}\">{1}</a>", "index.htm", "topic1");
            links.AppendFormat("<a href=\"{0}\">{1}</a>", "../index.htm", "topic2");

            var par = new StringBuilder();
            par.AppendFormat("title={0}", Uri.EscapeDataString("Здравствуй мир"));
            par.Append("&");
            par.AppendFormat("header={0}", Uri.EscapeDataString("Вот он и парсинг параметров"));
            par.Append("&");

            var escapedString = Uri.EscapeDataString (links.ToString ());
            par.AppendFormat("links={0}", escapedString);
            // A potentially dangerous Request.QueryString value was detected from the client

            string text = TemplateEngine2.ProcessRequest("Text1.aspx", par.ToString());
            return text;
        }
    }
}


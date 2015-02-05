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
        void InternalOpenFile(string fileToLoad)
        {
            if (string.IsNullOrWhiteSpace(fileToLoad))
            {
                return;
            }
            if (this.WindowState != FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.BringToFront();

            var tab = FindTabByPath(fileToLoad);
            if (tab != null)
            {
                tabControl1.SelectedTab = tab;
                SetupActiveTab();
                return;
            }

            EditorTabPage etb = new EditorTabPage();
            etb.LoadFile(fileToLoad);
            etb.Editor.DragEnter += new DragEventHandler(tabControl1_DragEnter);
            etb.Editor.DragDrop += new DragEventHandler(tabControl1_DragDrop);
            etb.OnEditorTextChanged += new EventHandler(etb_TextChanged);
            etb.OnEditorTabFilenameChanged += new EventHandler(TabControl_TabCaptionUpdate);
            etb.OnEditorTabStateChanged += new EventHandler(TabControl_TabCaptionUpdate);
            tabControl1.TabPages.Add(etb);
            etb.Show();
            tabControl1.SelectedTab = etb;
            etb.Update();

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

    }
}


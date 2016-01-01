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
        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            EditorTabPage etb = GetActiveTab();

            if (e.Button == MouseButtons.Middle)
            {
                if (etb != null)
                    closeToolStripMenuItem_Click(null, null);
            }
            if (e.Button == MouseButtons.Right)
            {
                //Point pt = new Point(e.X, e.Y);
                Point pt = Cursor.Position;
                Point p = this.tabControl1.PointToClient(pt);
                for (int i = 0; i < this.tabControl1.TabCount; i++)
                {
                    Rectangle r = this.tabControl1.GetTabRect(i);
                    if (r.Contains(p))
                    {
                        this.tabControl1.SelectedIndex = i; // i is the index of tab under cursor
                        var menu = new ContextMenu();
                        menu.MenuItems.Add("Закрыть", closeToolStripMenuItem_Click);
                        menu.MenuItems.Add("-");
                        menu.MenuItems.Add("Имя и путь файла скопировать", filepathnameToolStripMenuItem_Click);
                        menu.MenuItems.Add("папку файла скопировать", foldernameToolStripMenuItem_Click);
                        menu.Show(this.tabControl1, p);
                        SetupActiveTab();
                        return;
                    }
                }
                //e.Cancel = true;
            }
        }

        private void filepathnameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();
            if (etb != null)
            {
                Globals.TextClipboard.CopyTextToClipboard(etb.GetFileFullPathAndName(), false);
            }
        }

        private void foldernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditorTabPage etb = GetActiveTab();
            if (etb != null)
            {
                var fileFullPathAndName = etb.GetFileFullPathAndName ();
                FileInfo fi = new FileInfo(fileFullPathAndName);
                if (fileFullPathAndName.Contains(Path.DirectorySeparatorChar))
                {
                    Globals.TextClipboard.CopyTextToClipboard(fi.DirectoryName, false);
                }
                else
                {
                    Globals.TextClipboard.CopyTextToClipboard("." + Path.DirectorySeparatorChar, false);
                }
            }
        }
    }
}


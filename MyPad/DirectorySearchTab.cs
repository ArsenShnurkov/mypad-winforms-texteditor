/*
 * Copyright (c) 2009 Cory Borrow
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace MyPad
{
    public class DirectorySearchTab : TabPage
    {
        private delegate void AddTreeNodeDel(TreeNode node);

        FolderBrowserDialog folderBrowserDialog;
        ComboBox comboBox;
        TextBox textBox;
        Button button;
        TreeView treeView;

        Thread searchThread;
        string startDir;
        string search;

        public DirectorySearchTab()
        {
            comboBox = new ComboBox();
            comboBox.Location = new Point(10, 10);
            comboBox.Size = new Size(this.Width - 120, 23);
            comboBox.Show();

            button = new Button();
            button.Location = new Point(this.Width - 100, 10);
            button.Size = new Size(80, 23);
            button.Text = "Browse...";
            button.Click += new EventHandler(button_Click);
            button.Show();

            textBox = new TextBox();
            textBox.Location = new Point(10, 40);
            textBox.Size = new Size(this.Width - 20, 23);
            textBox.TextChanged += new EventHandler(textBox_TextChanged);
            textBox.Show();

            treeView = new TreeView();
            treeView.Location = new Point(10, 70);
            treeView.Size = new Size(this.Width - 20, this.Height - 80);
            treeView.Show();

            this.Controls.Add(textBox);
            this.Controls.Add(treeView);

            searchThread = new Thread(new ThreadStart(BeginSearch));
        }

        private void BeginSearch()
        {
            SearchDir(startDir);
        }

        private void SearchDir(string dir)
        {
            DirectoryInfo di = new DirectoryInfo(dir);

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                SearchDir(d.FullName);
            }

            foreach (FileInfo fi in di.GetFiles())
            {
                TreeNode node = new TreeNode();
                node.Text = fi.FullName;

                StreamReader sr = new StreamReader(fi.FullName);
                string text = sr.ReadToEnd();
                sr.Close();

                string[] lines = text.Split('\n');
                int lineNum = 0;

                foreach (string line in lines)
                {
                    MatchCollection matches = Regex.Matches(line, search);
                    node.Text = string.Format("{0} ({1}", fi.FullName, matches.Count);

                    foreach (Match m in matches)
                    {
                        node.Nodes.Add(string.Format("Line {0} : {1}", lineNum, line));
                    }
                    lineNum++;
                }

                if(this.InvokeRequired) {
                    this.Invoke(new AddTreeNodeDel(AddTreeNode), new object[] { node });
                }
            }
        }

        private void AddTreeNode(TreeNode node)
        {
            treeView.Nodes.Add(node);
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();

            searchThread.Abort();
            searchThread = new Thread(new ThreadStart(BeginSearch));
            searchThread.Start();
        }

        void button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                startDir = folderBrowserDialog.SelectedPath;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (searchThread != null && searchThread.IsAlive)
            {
                searchThread.Abort();
            }

            base.Dispose(disposing);
        }
    }
}

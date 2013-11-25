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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace MyPad.Dialogs
{
    public partial class SearchDialog : Form
    {
        RegexOptions regexOptions;

        public RegexOptions RegexOptions
        {
            get
            {
                return regexOptions;
            }
        }

        public string SearchFor
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public SearchDialog()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                regexOptions = RegexOptions.None;
            }
            else
            {
                if (checkBox1.Checked)
                {
                    regexOptions |= RegexOptions.IgnoreCase;
                }
                if (checkBox2.Checked)
                {
                    regexOptions |= RegexOptions.IgnorePatternWhitespace;
                }
                if (checkBox3.Checked)
                {
                    regexOptions |= RegexOptions.ECMAScript;
                }
            }

            DialogResult = DialogResult.OK;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                panel1.BackColor = Color.Fuchsia;

                Margins margin = new Margins();
                margin.cyBottomHeight = panel1.Height;

                Win32.ExtendGlass(this, margin);
            }

            base.OnHandleCreated(e);
        }

        private void SearchDialog_Load(object sender, EventArgs e)
        {

        }
    }
}

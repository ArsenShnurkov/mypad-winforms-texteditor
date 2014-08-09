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
    public partial class FindReplaceDialog : Form
    {
        RegexOptions options = RegexOptions.None;

        public string Search
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

        public string Replacement
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }

        public RegexOptions Options
        {
            get
            {
                return options;
            }
        }

        public FindReplaceDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                options |= RegexOptions.Multiline;
            else
                options |= RegexOptions.Singleline;

            if (checkBox2.Checked)
                options |= RegexOptions.ECMAScript;
            if (checkBox3.Checked)
                options |= RegexOptions.IgnorePatternWhitespace;
            if (checkBox4.Checked)
                options |= RegexOptions.IgnoreCase;

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

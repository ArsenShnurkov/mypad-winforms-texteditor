﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPad
{
    public partial class OutputForm : Form
    {
        public string OutputText
        {
            get
            {
                return richTextBox1.Text;
            }
            set
            {
                richTextBox1.Text = value;
            }
        }

        public OutputForm()
        {
            InitializeComponent();
        }
    }
}

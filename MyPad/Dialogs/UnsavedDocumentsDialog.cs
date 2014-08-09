using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPad.Dialogs
{
    public partial class UnsavedDocumentsDialog : Form
    {
        public IEnumerable<string> SelectedDocuments
        {
            get
            {
                return checkedListBox1.CheckedItems.Cast<string>();
            }
        }

        public int SelectedDocumentsCount
        {
            get
            {
                return checkedListBox1.Items.Count;
            }
        }

        public UnsavedDocumentsDialog()
        {
            InitializeComponent();
        }

        public void ClearDocuments()
        {
            checkedListBox1.Items.Clear();
        }

        public void AddDocument(string item)
        {
            checkedListBox1.Items.Add(item);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

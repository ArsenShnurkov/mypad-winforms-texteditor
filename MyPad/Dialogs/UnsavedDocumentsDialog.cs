using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;

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
            int index = checkedListBox1.Items.Add(item);
            checkedListBox1.SetItemChecked(index, true);
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

namespace MyPad
{
    using System;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.ServiceModel.Syndication;

    partial class EntriesListDialog : Form
    {
        public EntriesListDialog ()
        {
            InitializeComponent();
        }

        void CreateNewEntry_Click (object sender, EventArgs e)
        {
            var dlg = new EntryCreateOrEditDialog ();
            dlg.ShowDialog ();
        }

        void EditExistingEntry_Click (object sender, EventArgs e)
        {
            var dlg = new EntryCreateOrEditDialog ();
            dlg.ShowDialog ();
        }

        void DeleteEntry_Click (object sender, EventArgs e)
        {
            var itemsToDelete = new  List<ListViewItem> ();
            // message box
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Selected)
                {
                    itemsToDelete.Add (item);
                }
            }
            foreach (var item in itemsToDelete)
            {
                listView1.Items.Remove (item);
            }
        }

        void SaveButton_Click (object sender, EventArgs e)
        {
        }
    }
}

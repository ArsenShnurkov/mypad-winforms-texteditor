namespace MyPad {
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    partial class EntriesListDialog : Form {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        Panel panel1;
        Label label1;
        ListView listView1;
        Button createNewEntry;
        Button editExistingEntry;
        Button deleteEntry;
        Button saveButton;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            var resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
            Debug.Assert (resources != null);
            this.SuspendLayout();

            panel1 = new Panel ();
            label1 = new Label ();
            listView1 = new ListView ();
            createNewEntry = new Button ();
            editExistingEntry = new Button ();
            deleteEntry = new Button ();
            saveButton = new Button ();

            panel1.Controls.Add (label1);
            panel1.Controls.Add (listView1);
            panel1.Controls.Add (createNewEntry);
            panel1.Controls.Add (editExistingEntry);
            panel1.Controls.Add (deleteEntry);
            panel1.Controls.Add (saveButton);

            this.Controls.Add (panel1);

            panel1.Dock = DockStyle.Fill;

            int formSpacing = 6;

            label1.Top = formSpacing;
            label1.Left = formSpacing;
            label1.AutoSize = true;
            label1.Text = "News list:";
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            int topGuide = label1.Top + label1.Height + formSpacing;

            createNewEntry.Height = 20;
            createNewEntry.Top = topGuide;
            createNewEntry.Width = 80;
            createNewEntry.Left = createNewEntry.Parent.Width - createNewEntry.Width - formSpacing;
            createNewEntry.Text = "Create";
            createNewEntry.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            editExistingEntry.Height = 20;
            editExistingEntry.Top = createNewEntry.Top + createNewEntry.Height + formSpacing;
            editExistingEntry.Width = 80;
            editExistingEntry.Left = editExistingEntry.Parent.Width - editExistingEntry.Width - formSpacing;
            editExistingEntry.Text = "Edit";
            editExistingEntry.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            deleteEntry.Height = 20;
            deleteEntry.Top = editExistingEntry.Top + editExistingEntry.Height + formSpacing;
            deleteEntry.Width = 80;
            deleteEntry.Left = deleteEntry.Parent.Width - deleteEntry.Width - formSpacing;
            deleteEntry.Text = "Delete";
            deleteEntry.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            int maxWidth = Math.Max (Math.Max (createNewEntry.Width, editExistingEntry.Width), deleteEntry.Width);

            saveButton.Height = 20;
            saveButton.Top = saveButton.Parent.Height - saveButton.Height - formSpacing;
            saveButton.Width = 80;
            saveButton.Left = saveButton.Parent.Width - saveButton.Width - formSpacing;
            saveButton.Text = "Save";
            saveButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

            int bottomGuide = saveButton.Top - formSpacing;

            listView1.Top = topGuide;
            listView1.Height = bottomGuide - listView1.Top;
            listView1.Left = formSpacing;
            listView1.Width = listView1.Parent.Width - 3 * formSpacing - maxWidth;
            listView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            listView1.View = View.Details;

            var chSeq = new ColumnHeader ();
            chSeq.Tag = "Seq";;
            chSeq.Text = "Seq";
            listView1.Columns.Add(chSeq);

            var chPublished = new ColumnHeader ();
            chPublished.Tag = "Published";;
            chPublished.Text = "Published";
            listView1.Columns.Add(chPublished);

            var chUpdated = new ColumnHeader ();
            chUpdated.Tag = "Updated";;
            chUpdated.Text = "Updated";
            listView1.Columns.Add(chUpdated);

            var chTitle = new ColumnHeader ();
            chTitle.Tag = "Title";
            chTitle.Text = "Title";
            listView1.Columns.Add(chTitle);

            var chURL = new ColumnHeader ();
            chURL.Tag = "URL";
            chURL.Text = "URL";
            listView1.Columns.Add(chURL);

            listView1.FullRowSelect = true;
            listView1.HideSelection = false;
            listView1.ClientSizeChanged += (object sender, EventArgs e) => {
                int total = 0;
                ColumnHeader last = null;
                foreach (ColumnHeader c in listView1.Columns)
                    {
                    c.AutoResize (ColumnHeaderAutoResizeStyle.ColumnContent);
                    //c.Width = -1;
                    total += c.Width;
                    last = c;
                    }
                if (last != null)
                    {
                    total -= last.Width;
                    last.Width = listView1.ClientSize.Width - total;
                    }
            };


            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable; // FixedDialog
            this.Name = "EntriesListDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EntriesList Dialog";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ResumeLayout(false);
            this.PerformLayout();

            createNewEntry.Click += CreateNewEntry_Click;;
            editExistingEntry.Click += EditExistingEntry_Click;
            deleteEntry.Click += DeleteEntry_Click;
            saveButton.Click += SaveButton_Click;
        }

        #endregion
    }
}

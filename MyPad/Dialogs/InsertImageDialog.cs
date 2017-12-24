using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;

namespace MyPad.Dialogs
{
    public partial class InsertImageDialog : Form
    {
        public string URL { get; set; }
        public string FileName { get; set; }
        public string ALT { get; set; }

        public InsertImageDialog ()
        {
            InitializeComponent ();
            this.CenterToScreen ();
        }

        protected override void OnLoad (EventArgs e)
        {
            base.OnLoad (e);
            this.textBoxURL.Text = this.URL;
            this.textBoxFileName.Text = this.FileName;
            this.textBoxALT.Text = this.ALT;
        }

        private void textBoxURL_TextChanged (object sender, EventArgs e)
        {
            int idx = textBoxURL.Text.LastIndexOf (".", StringComparison.InvariantCulture);
            if (idx >= 0) {
                string extension = textBoxURL.Text.Substring (idx);
                this.textBoxFileName.Text = this.FileName + extension;
            }
        }

        private void buttonOK_Click (object sender, EventArgs e)
        {
            this.URL = this.textBoxURL.Text;
            this.FileName = this.textBoxFileName.Text;
            this.ALT = this.textBoxALT.Text;
        }
    }
}

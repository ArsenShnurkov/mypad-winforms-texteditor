using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Net;

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
            WebResponse response;
            try {
                HttpWebRequest webrequest = WebRequest.Create (textBoxURL.Text.ToString ()) as HttpWebRequest;
                webrequest.Method = "HEAD";
                response = webrequest.GetResponse ();
            } catch {
                return;
            }

            switch (response.ContentType) {
            case "image/png":
                this.textBoxFileName.Text = this.FileName + ".png";
                return;
            case "image/gif":
                this.textBoxFileName.Text = this.FileName + ".gif";
                return;
            case "image/jpg":
                this.textBoxFileName.Text = this.FileName + ".jpg";
                return;
            case "image/svg":
                this.textBoxFileName.Text = this.FileName + ".svg";
                return;
            }

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

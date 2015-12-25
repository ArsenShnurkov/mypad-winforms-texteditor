namespace MyPad
{
    using System;
    using System.Windows.Forms;

    partial class EntryCreateOrEditDialog : Form
    {
        public EntryCreateOrEditDialog ()
        {
            InitializeComponent();
            // this.urlText.Text = url;
            // this.msgText.Text = msg;
            // this is not necessary, because member variables are empty strings
            this.urlText.TextChanged += (object sender, EventArgs e) => { url = urlText.Text; };
            this.titleText.TextChanged += (object sender, EventArgs e) => { title = titleText.Text; };
            this.msgText.TextChanged += (object sender, EventArgs e) => { msg = msgText.Text; };
        }

        private string url = string.Empty;
        public string URL { 
            set
            {
                url = value;
                if (this.urlText != null && string.Compare (url, this.urlText.Text) != 0)
                {
                    this.urlText.Text = url;
                }
            }
            get
            {
                return url;
            }
        }

        private string title = string.Empty;
        public string TITLE { 
            set
            {
                title = value;
                if (this.titleText != null && string.Compare (title, this.titleText.Text) != 0)
                {
                    this.titleText.Text = title;
                }
            }
            get
            {
                return title;
            }
        }

        private string msg = string.Empty;
        public string MSG { 
            set
            {
                msg = value;
                if (this.msgText != null && string.Compare (msg, this.msgText.Text) != 0)
                {
                    this.msgText.Text = msg;
                }
            }
            get
            {
                return msg;
            }
        }
    }
}


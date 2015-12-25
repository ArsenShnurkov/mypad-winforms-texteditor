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
                if (this.urlText != null)
                {
                    return this.urlText.Text;
                }
                else
                {
                    return url;
                }
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
                if (this.titleText != null)
                {
                    return this.titleText.Text;
                }
                else
                {
                    return title;
                }
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
                if (this.msgText != null)
                {
                    return this.msgText.Text;
                }
                else
                {
                    return msg;
                }
            }
        }
    }
}


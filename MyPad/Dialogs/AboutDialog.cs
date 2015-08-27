using System;
using System.Windows.Forms;
using System.Reflection;

namespace MyPad.Dialogs
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            var ver = Assembly.GetEntryAssembly ().GetName ().Version;
            this.Text = string.Format ("{0} v{1}", this.Text, ver);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}

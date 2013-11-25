using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MyPad.Plugins;

namespace MyPad.Dialogs
{
    public partial class PluginManagerDialog : Form
    {
        public PluginManagerDialog()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                panel1.BackColor = Color.Fuchsia;

                Margins margin = new Margins();
                margin.cyBottomHeight = panel1.Height;

                Win32.ExtendGlass(this, margin);
            }

            base.OnHandleCreated(e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)listBox1.SelectedItem;

            foreach (IPlugin plugin in PluginManager.Plugins)
            {
                if (plugin.Name == name)
                {
                    textBox1.Text = plugin.Name;
                    textBox2.Text = plugin.Author;
                    textBox3.Text = plugin.Website;
                    textBox4.Text = plugin.Email;
                    richTextBox1.Text = plugin.Description;
                }
            }
        }

        private void PluginManagerDialog_Load(object sender, EventArgs e)
        {
            foreach (IPlugin plugin in PluginManager.Plugins)
            {
                listBox1.Items.Add(plugin.Name);
            }
        }
    }
}

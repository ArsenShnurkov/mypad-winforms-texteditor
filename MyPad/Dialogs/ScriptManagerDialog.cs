using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyPad.Dialogs
{
    public partial class ScriptManagerDialog : Form
    {
        public ScriptManager ScriptManager;

        public ScriptManagerDialog()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialogEx()
        {
            listBox1.Items.Clear();

            if (ScriptManager != null)
            {
                /*foreach (LuaScript script in ScriptManager.Scripts)
                {
                    listBox1.Items.Add(script.Name);
                }*/
            }

            return this.ShowDialog();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Environment.OSVersion.Version.Major > 5)
            {
                panel1.BackColor = Color.Fuchsia;

                Margins margins = new Margins();
                margins.cyBottomHeight = panel1.Height;

                if (!Win32.ExtendGlass(this, margins))
                    panel1.BackColor = SystemColors.Control;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string scriptName = (string)listBox1.SelectedItem;

            if (scriptName != null)
            {
                LuaScript script = ScriptManager.GetScriptByName(scriptName);

                if (script != null)
                {
                    textBox1.Text = script.Name;
                    textBox2.Text = script.Path;
                    textBox3.Text = script.Description;
                    comboBox1.Text = script.Type.ToString();
                    checkBox1.Checked = script.Enabled;
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    checkBox1.Checked = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string scriptName = (string)listBox1.SelectedItem;

            if (scriptName != null)
            {
                LuaScript script = ScriptManager.GetScriptByName(scriptName);

                if (script != null)
                {
                    script.Name = textBox1.Text;
                    script.Type = (ScriptType)Enum.Parse(typeof(ScriptType), comboBox1.Text);
                    script.Path = textBox2.Text;
                    script.FullPath = Path.Combine(Path.GetDirectoryName(script.FullPath), script.Path);
                    script.Description = textBox3.Text;
                    script.Enabled = checkBox1.Checked;
                    script.Save();
                }
            }
        }
    }
}

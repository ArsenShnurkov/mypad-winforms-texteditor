using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPad.Dialogs
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
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

        protected override void OnLoad(EventArgs e)
        {
            FontFamily[] fonts = FontFamily.Families;

            foreach (FontFamily font in fonts)
            {
                comboBox1.Items.Add(font.Name);
            }

            comboBox2.Items.AddRange(new object[] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 18, 20, 22 });

            base.OnLoad(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                checkBox1.Checked = SettingsManager.ReadValue<bool>("AllowCaretBeyondEOL");
                checkBox2.Checked = SettingsManager.ReadValue<bool>("ShowEOLMarkers");
                checkBox3.Checked = SettingsManager.ReadValue<bool>("ShowSpaces");
                checkBox4.Checked = SettingsManager.ReadValue<bool>("ShowTabs");
                checkBox5.Checked = SettingsManager.ReadValue<bool>("ShowVRuler");
                checkBox6.Checked = SettingsManager.ReadValue<bool>("ShowHRuler");
                checkBox7.Checked = SettingsManager.ReadValue<bool>("ShowInvalidLines");
                checkBox8.Checked = SettingsManager.ReadValue<bool>("ShowLineNumbers");
                checkBox9.Checked = SettingsManager.ReadValue<bool>("ShowMatchingBrackets");
                checkBox10.Checked = SettingsManager.ReadValue<bool>("AutoInsertBrackets");
                checkBox11.Checked = SettingsManager.ReadValue<bool>("EnableFolding");
                checkBox12.Checked = SettingsManager.ReadValue<bool>("ConvertTabsToSpaces");
                comboBox1.Text = SettingsManager.ReadValue<string>("FontName");
                comboBox2.Text = SettingsManager.ReadValue<string>("FontSize");
            }

            base.OnVisibleChanged(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsManager.Settings["AllowCaretBeyondEOL"] = checkBox1.Checked;
            SettingsManager.Settings["ShowEOLMarker"] = checkBox2.Checked;
            SettingsManager.Settings["ShowSpaces"] = checkBox3.Checked;
            SettingsManager.Settings["ShowTabs"] = checkBox4.Checked;
            SettingsManager.Settings["ShowVRuler"] = checkBox5.Checked;
            SettingsManager.Settings["ShowHRuler"] = checkBox6.Checked;
            SettingsManager.Settings["ShowInvalidLines"] = checkBox7.Checked;
            SettingsManager.Settings["ShowLineNumbers"] = checkBox8.Checked;
            SettingsManager.Settings["ShowMatchingBrackets"] = checkBox9.Checked;
            SettingsManager.Settings["AutoInsertBrackets"] = checkBox10.Checked;
            SettingsManager.Settings["EnableFolding"] = checkBox11.Checked;
            SettingsManager.Settings["ConvertTabsToSpaces"] = checkBox12.Checked;

            if (comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
            {
                try
                {
                    Font font = new Font(new FontFamily(comboBox1.Text), (float)Convert.ToDouble(comboBox2.Text), FontStyle.Regular);
                    SettingsManager.Settings["FontName"] = comboBox1.Text;
                    SettingsManager.Settings["FontSize"] = (float)Convert.ToDouble(comboBox2.Text);
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
            }

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

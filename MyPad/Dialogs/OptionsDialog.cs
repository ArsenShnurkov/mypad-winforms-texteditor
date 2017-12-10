using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace MyPad.Dialogs
{
    public partial class OptionsDialog : Form
    {
        Configuration cfg;
        public OptionsDialog ()
        {
            InitializeComponent ();

            cfg = Globals.LoadConfiguration ();

            CopySettingsIntoControls ();
        }

        protected override void OnLoad (EventArgs e)
        {
            comboBox1.Items.Clear();
            FontFamily [] fonts = FontFamily.Families;

            var sortedList = new SortedList<string, FontFamily> ();
            foreach (FontFamily font in fonts) {
                if (sortedList.ContainsKey (font.Name) == false) {
                    sortedList.Add (font.Name, font);
                }
            }

            foreach (var item in sortedList.Keys) {
                int index = comboBox1.Items.Add (item);
            }

            comboBox2.Items.Clear();
            comboBox2.Items.AddRange (new object [] { 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 18, 20, 22 });

            base.OnLoad (e);
        }

        void CopySettingsIntoControls ()
        {
            EditorConfigurationSection section = cfg.GetEditorConfiguration ();
            checkBox1.Checked = section.AllowCaretBeyondEOL.Value;
            checkBox2.Checked = section.ShowEOLMarkers.Value;
            checkBox3.Checked = section.ShowSpaces.Value;
            checkBox4.Checked = section.ShowTabs.Value;
            checkBox5.Checked = section.ShowVRuler.Value;
            checkBox6.Checked = section.ShowHRuler.Value;
            checkBox7.Checked = section.ShowInvalidLines.Value;
            checkBox8.Checked = section.ShowLineNumbers.Value;
            checkBox9.Checked = section.ShowMatchingBrackets.Value;
            checkBox10.Checked = section.AutoInsertBrackets.Value;
            checkBox11.Checked = section.EnableFolding.Value;
            checkBox12.Checked = section.ConvertTabsToSpaces.Value;
            comboBox1.Text = section.FontName.Value;
            comboBox2.Text = section.FontSize.Value.ToString (CultureInfo.InvariantCulture);
            this.textBoxAtomFeedFileLocation.Text = cfg.AppSettings.Settings ["AtomFeedLocation"]?.Value;
            this.textBoxSearchDirectory.Text = cfg.AppSettings.Settings ["SearchDirectory"]?.Value;
        }

        protected override void OnVisibleChanged (EventArgs e)
        {
            if (this.Visible) {
                CopySettingsIntoControls ();
            }

            base.OnVisibleChanged (e);
        }

        private void button1_Click (object sender, EventArgs e)
        {
            EditorConfigurationSection section = cfg.GetEditorConfiguration ();
            section.AllowCaretBeyondEOL.Value = checkBox1.Checked;
            section.ShowEOLMarkers.Value = checkBox2.Checked;
            section.ShowSpaces.Value = checkBox3.Checked;
            section.ShowTabs.Value = checkBox4.Checked;
            section.ShowVRuler.Value = checkBox5.Checked;
            section.ShowHRuler.Value = checkBox6.Checked;
            section.ShowInvalidLines.Value = checkBox7.Checked;
            section.ShowLineNumbers.Value = checkBox8.Checked;
            section.ShowMatchingBrackets.Value = checkBox9.Checked;
            section.AutoInsertBrackets.Value = checkBox10.Checked;
            section.EnableFolding.Value = checkBox11.Checked;
            section.ConvertTabsToSpaces.Value = checkBox12.Checked;
            cfg.AppSettings.Settings.Add ("AtomFeedLocation", this.textBoxAtomFeedFileLocation.Text);
            cfg.AppSettings.Settings.Add ("SearchDirectory", this.textBoxSearchDirectory.Text);

            if (comboBox1.Text != string.Empty && comboBox2.Text != string.Empty) {
                try {
                    var fontSize = (float)Convert.ToDouble (comboBox2.Text, CultureInfo.InvariantCulture);
                    var f = new Font (new FontFamily (comboBox1.Text), fontSize, FontStyle.Regular);
                    section.FontName.Value = f.Name;
                    section.FontSize.Value = fontSize;
                } catch (Exception ex) {
                    string err = ex.Message;
                }
            }
            cfg.SaveAll ();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click (object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

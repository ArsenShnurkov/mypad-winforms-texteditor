using System.Windows.Forms;
using System.Text.RegularExpressions;
using System;
using System.Drawing;
using System.Text;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace MyPad.Dialogs
{
    public partial class OptionsDialog : Form
    {
        Configuration cfg;
        public OptionsDialog()
        {
            InitializeComponent();

            // вычисление пути до файла конфига в пользовательской директории
            StringBuilder location = new StringBuilder("${SpecialFolder.LocalApplicationData}/${AssemblyProduct}/${AssemblyTitle}.config");
            // roaming
            location.Replace ("${SpecialFolder.ApplicationData}", System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData));
            // local
            location.Replace ("${SpecialFolder.LocalApplicationData}", System.Environment.GetFolderPath (System.Environment.SpecialFolder.LocalApplicationData));
            location.Replace ("${AssemblyProduct}", Globals.AssemblyProduct);
            location.Replace ("${AssemblyTitle}", Globals.AssemblyTitle);
            var filename = location.ToString ();
            Globals.EnsureDirectoryExists (filename);
            if (false == File.Exists (filename))
            {
                CreateDefaultConfig (filename);
            }
            for (;;)
            {
                try
                {
                    cfg = Get (filename);
                    CopySettingsIntoControls();
                    break;
                } catch (Exception ex)
                {
                    Trace.WriteLine (ex.ToString ());
                    CreateDefaultConfig (filename);
                    continue;
                }
            }
        }

        // http://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application
        void CreateDefaultConfig(string filename)
        {
            // ConfigurationManager
            var cfg = Get(filename);
            cfg.AppSettings.Settings.Add ("AtomFeedLocation", "/var/calculate/remote/distfiles/egit-src/blog/notifications.atom");
            cfg.Save ();
        }

        public Configuration Get(string fileName)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fileName };
            var cfg = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return cfg;
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

        void CopySettingsIntoControls ()
        {
            checkBox1.Checked = SettingsManager.ReadValue<bool> ("AllowCaretBeyondEOL");
            checkBox2.Checked = SettingsManager.ReadValue<bool> ("ShowEOLMarkers");
            checkBox3.Checked = SettingsManager.ReadValue<bool> ("ShowSpaces");
            checkBox4.Checked = SettingsManager.ReadValue<bool> ("ShowTabs");
            checkBox5.Checked = SettingsManager.ReadValue<bool> ("ShowVRuler");
            checkBox6.Checked = SettingsManager.ReadValue<bool> ("ShowHRuler");
            checkBox7.Checked = SettingsManager.ReadValue<bool> ("ShowInvalidLines");
            checkBox8.Checked = SettingsManager.ReadValue<bool> ("ShowLineNumbers");
            checkBox9.Checked = SettingsManager.ReadValue<bool> ("ShowMatchingBrackets");
            checkBox10.Checked = SettingsManager.ReadValue<bool> ("AutoInsertBrackets");
            checkBox11.Checked = SettingsManager.ReadValue<bool> ("EnableFolding");
            checkBox12.Checked = SettingsManager.ReadValue<bool> ("ConvertTabsToSpaces");
            comboBox1.Text = SettingsManager.ReadValue<string> ("FontName");
            comboBox2.Text = SettingsManager.ReadValue<string> ("FontSize");
            this.textBoxAtomFeedFileLocation.Text = cfg.AppSettings.Settings ["AtomFeedLocation"].Value;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible)
            {
                CopySettingsIntoControls ();
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
            cfg.AppSettings.Settings ["AtomFeedLocation"].Value = this.textBoxAtomFeedFileLocation.Text;

            if (comboBox1.Text != string.Empty && comboBox2.Text != string.Empty)
            {
                try
                {
                    new Font(new FontFamily(comboBox1.Text), (float)Convert.ToDouble(comboBox2.Text), FontStyle.Regular);
                    SettingsManager.Settings["FontName"] = comboBox1.Text;
                    SettingsManager.Settings["FontSize"] = (float)Convert.ToDouble(comboBox2.Text);
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                }
            }
            cfg.Save ();
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}

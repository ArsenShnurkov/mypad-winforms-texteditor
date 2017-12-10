using System;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MyPad
{
    static partial class Globals
    {
        static Configuration cfg = null;

        public static Configuration LoadConfiguration ()
        {
            if (cfg != null) {
                return cfg;
            }
            var configFile = ConfigurationManager.OpenExeConfiguration (ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            string strUserConfigFilePath = settings ["UserConfigFilePath"].Value;

            Dictionary<string, string> strings = new Dictionary<string, string> ();
            strings.Add ("ApplicationData", System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData));
            strings.Add ("LocalApplicationData", System.Environment.GetFolderPath (System.Environment.SpecialFolder.LocalApplicationData));
            strings.Add ("AssemblyProduct", Globals.AssemblyProduct);
            strings.Add ("AssemblyTitle", Globals.AssemblyTitle);
            // savefile path calculation
            StringBuilder location = new StringBuilder (strUserConfigFilePath);
            foreach (var kvp in strings) {
                location.Replace ("${" + kvp.Key + "}", kvp.Value);
            }
            var filename = location.ToString ();
            Globals.EnsureDirectoryExists (filename);
            if (false == File.Exists (filename)) {
                CreateDefaultConfig (filename);
            }
            for (;;) {
                try {
                    cfg = LoadConfigurationByFilename (filename);
                    break;
                } catch (Exception ex) {
                    Trace.WriteLine (ex.ToString ());
                    CreateDefaultConfig (filename);
                    continue;
                }
            }
            return cfg;
        }

        public static Configuration LoadConfigurationByFilename (string fileName)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fileName };
            var cfg = ConfigurationManager.OpenMappedExeConfiguration (fileMap, ConfigurationUserLevel.None);
            return cfg;
        }

        // http://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application
        public static void CreateDefaultConfig (string filename)
        {
            // ConfigurationManager
            var cfg = LoadConfigurationByFilename (filename);
            // cfg.AppSettings.Settings.Add ("AtomFeedLocation", "/var/calculate/remote/distfiles/egit-src/blog/notifications.atom");
            // it's better to install configuration file from ebuild (they allow apply_user() patches in EAPI-6)
            cfg.SaveAll ();
        }

    }
}

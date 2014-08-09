using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace MyPad
{
    public class SettingsManager
    {
        static Dictionary<string, object> settings;
        static IList<string> mruList;

        static string settingsPath = "";
        static string mruPath = "";

        public static Dictionary<string, object> Settings
        {
            get
            {
                return settings;
            }
        }

        public static IList<string> MRUList
        {
            get
            {
                return mruList;
            }
        }

        public static void Init()
        {
            settings = new Dictionary<string, object>();
            mruList = new List<string>();

            settingsPath = Path.Combine(Application.StartupPath, "Data/Settings.xml");
            mruPath = Path.Combine(Application.StartupPath, "Data/MRUList.xml");

            LoadSettings(settingsPath);
            LoadMRUList(mruPath);
        }

        public static void Save()
        {
            SaveSettings();
            SaveMRUList();
        }

        public static T ReadValue<T>(string name)
        {
            if (settings.ContainsKey(name))
            {
                return (T)Convert.ChangeType(settings[name], typeof(T));
            }
            return default(T);
        }

        public static string ReadAsString(string name)
        {
            if (settings.ContainsKey(name))
            {
                return Convert.ToString(settings[name]);
            }
            return "";
        }

        public static int ReadAsInt(string name)
        {
            if (settings.ContainsKey(name))
            {
                return Convert.ToInt32(settings[name]);
            }
            return 0;
        }

        private static void LoadSettings(string path)
        {
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.GetElementsByTagName("Setting");

                foreach (XmlNode node in nodes)
                {
                    string name = "";
                    object value = null;

                    if (node.Attributes["name"] != null)
                        name = node.Attributes["name"].Value;
                    if (node.Attributes["value"] != null)
                        value = node.Attributes["value"].Value;

                    if (name != string.Empty && value != null)
                    {
                        settings.Add(name, value);
                    }
                }
            }
        }

        private static void SaveSettings()
        {
            if (settingsPath != string.Empty)
            {
                StreamWriter sw = new StreamWriter(settingsPath);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine("<Settings>");

                foreach (KeyValuePair<string, object> s in settings)
                {
                    sw.WriteLine(string.Format("\t<Setting name=\"{0}\" value=\"{1}\" />", s.Key, s.Value));
                }

                sw.WriteLine("</Settings>");
                sw.Close();
            }
        }

        private static void LoadMRUList(string path)
        {
            if (File.Exists(path))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlElement root = doc.DocumentElement;
                XmlNodeList nodes = root.GetElementsByTagName("File");

                foreach (XmlNode node in nodes)
                {
                    string file = "";

                    if (node.Attributes["path"] != null)
                        file = node.Attributes["path"].Value;

                    if (file != string.Empty && File.Exists(file))
                        mruList.Add(file);
                }
            }
        }

        private static void SaveMRUList()
        {
            if (mruPath != string.Empty)
            {
                StreamWriter sw = new StreamWriter(mruPath);
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                sw.WriteLine("<MRUList>");

                foreach (string file in mruList)
                {
                    sw.WriteLine(string.Format("\t<File path=\"{0}\" />", file));
                }

                sw.WriteLine("</MRUList>");
                sw.Close();
            }
        }
    }
}

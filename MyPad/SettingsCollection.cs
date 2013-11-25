/*
 * Copyright (c) 2009 Cory Borrow
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace MyPad
{
    public class SettingsCollection
    {
        Dictionary<string, object> settings;
        DefaultTextEditorProperties defaultSettings;

        public object this[string name]
        {
            get
            {
                return ReadValue(name);
            }
            set
            {
                settings[name] = value;
            }
        }

        public DefaultTextEditorProperties DefaultSettings
        {
            get
            {
                return defaultSettings;
            }
        }

        public SettingsCollection()
        {
            settings = new Dictionary<string, object>();
            defaultSettings = new DefaultTextEditorProperties();
        }

        public void Load(string file)
        {
            if (File.Exists(file))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);

                XmlElement docRoot = doc.DocumentElement;
                XmlNodeList nodes = docRoot.GetElementsByTagName("Setting");

                foreach (XmlNode node in nodes)
                {
                    string name = node.Attributes["name"].Value;
                    //string type = node.Attributes["type"].Value;
                    object value = node.Attributes["value"].Value;

                    settings.Add(name, value);
                }
            }
        }

        public void Save(string file)
        {
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sw.WriteLine("<SettingsManager>");

            foreach (KeyValuePair<string, object> item in settings)
            {
                sw.WriteLine(string.Format("\t<Setting name=\"{0}\" value=\"{1}\" />", item.Key, item.Value));
            }

            sw.WriteLine("</SettingsManager>");
            sw.Close();
        }

        public object ReadValue(string name)
        {
            if (settings.ContainsKey(name))
            {
                return settings[name];
            }

            return null;
        }

        public T ReadValue<T>(string name)
        {
            if (settings.ContainsKey(name))
            {
                object value = settings[name];
                T myVal = (T)Convert.ChangeType(value, typeof(T));
                return myVal;
            }
            return default(T);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return settings.GetEnumerator();
        }
    }
}

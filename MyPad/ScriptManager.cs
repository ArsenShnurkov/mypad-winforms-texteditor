using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

using ICSharpCode.TextEditor;

using LuaInterface;

namespace MyPad
{
    public class ScriptManager
    {
        Lua luaScriptHost;
        IList<LuaScript> scripts;
        ToolStripMenuItem scriptMenuItem;
        MainForm mainForm;

        public Lua LuaScriptHost
        {
            get
            {
                return luaScriptHost;
            }
        }

        public IList<LuaScript> Scripts
        {
            get
            {
                return scripts;
            }
        }

        public ScriptManager(MainForm mainForm, ToolStripMenuItem scriptMenuItem)
        {
            scripts = new List<LuaScript>();
            this.mainForm = mainForm;
            this.scriptMenuItem = scriptMenuItem;
            this.scriptMenuItem.DropDownItemClicked += new ToolStripItemClickedEventHandler(scriptMenuItem_DropDownItemClicked);

            luaScriptHost = new Lua();
            LoadScripts();
        }

        public void Attach(TextEditorControl editor)
        {
            if (editor != null)
            {
                editor.TextChanged += new EventHandler(Editor_TextChanged);
            }
        }

        public LuaScript GetScriptByName(string name)
        {
            foreach (LuaScript script in scripts)
            {
                if (script.Name == name)
                    return script;
            }
            return null;
        }

        public void LoadScripts()
        {
            Console.WriteLine("Start loading scripts...");

            DirectoryInfo di = new DirectoryInfo(Path.Combine(Application.StartupPath, "Scripts"));

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                Console.WriteLine(string.Format("Loading script {0}...", d.Name));

                string dir = d.FullName;

                LuaScript script = new LuaScript();
                string installFile = "";

                if (File.Exists(Path.Combine(dir, "ScriptInstaller.xml")))
                    installFile = Path.Combine(dir, "ScriptInstaller.xml");

                if (!File.Exists(installFile))
                {
                    Console.WriteLine(string.Format("Script installer not found for {0}", d.Name));
                    continue;
                }

                XmlTextReader xml = new XmlTextReader(installFile);

                while (xml.Read())
                {
                    if (xml.NodeType == XmlNodeType.Element)
                    {
                        if (xml.Name == "Name")
                        {
                            script.Name = xml.ReadElementContentAsString();
                        }
                        else if (xml.Name == "File")
                        {
                            script.Path = xml.ReadElementContentAsString();
                            script.FullPath = Path.Combine(dir, script.Path);
                        }
                        else if (xml.Name == "Type")
                        {
                            string type = xml.ReadElementContentAsString();
                            
                            /*if (type == "Menu")
                                script.Type = ScriptType.MenuScript;
                            else if (type == "Text")
                                script.Type = ScriptType.TextScript;*/

                            script.Type = (ScriptType)Enum.Parse(typeof(ScriptType), type);

                            if (Enum.ToObject(typeof(ScriptType), script.Type) == null)
                                script.Type = ScriptType.MenuScript;
                        }
                        else if (xml.Name == "Description")
                        {
                            script.Description = xml.ReadElementContentAsString();
                        }
                    }
                }

                xml.Close();

                if (script != null)
                {
                    Console.WriteLine(string.Format("Script {0} loaded sussecfully", script.Name));

                    script.Enabled = true;
                    script.LuaScriptHost = luaScriptHost;
                    script.InstallerPath = installFile;
                    scripts.Add(script);

                    if (script.Type == ScriptType.MenuScript)
                    {
                        scriptMenuItem.DropDownItems.Add(script.Name);
                    }
                }
                else
                {
                    Console.WriteLine(string.Format("Failed to load script {0}", d.Name));
                }
            }
            Console.WriteLine("Done loading scripts");
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            foreach (LuaScript script in scripts)
            {
                if (script.Type == ScriptType.TextScript)
                {
                    TextEditorControl textControl = (TextEditorControl)sender;

                    if (script.Enabled)
                        script.Execute(textControl);
                }
            }
        }

        private void scriptMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;

            if (tsi != null)
            {
                string text = e.ClickedItem.Text;
                LuaScript script = GetScriptByName(text);
                TextEditorControl textControl = mainForm.GetActiveEditor();

                if (script != null)
                {
                    if (script.Type == ScriptType.MenuScript)
                    {
                        if(script.Enabled)
                            script.Execute(textControl);
                    }
                }
            }
        }
    }
}

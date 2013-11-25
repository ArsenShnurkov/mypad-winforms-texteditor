using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ICSharpCode.TextEditor;

using LuaInterface;
using LuaInterface.Exceptions;

namespace MyPad
{
    public enum ScriptType
    {
        MenuScript,
        TextScript
    };

    public class LuaScript
    {
        public Lua LuaScriptHost;
        public ScriptType Type;
        public string Name;
        public string Description;
        public string Path;
        public string FullPath;
        public string InstallerPath;
        public bool Enabled;

        public LuaScript()
        {
            Enabled = true;
            Type = ScriptType.MenuScript;
            Name = "";
            Path = "";
            FullPath = "";
        }

        public void Execute(TextEditorControl editor)
        {
            if (File.Exists(FullPath))
            {
                Console.WriteLine("Script starting...");

                if (editor != null)
                    LuaScriptHost["TextEditor"] = editor;

                try
                {
                    LuaScriptHost.DoFile(FullPath);
                }
                catch (LuaException lex)
                {
                    ApplicationLog.LogMessage(string.Format("Script error : {0}", lex.ToString()));
                }
                catch (Exception ex)
                {
                    ApplicationLog.LogMessage(string.Format("Error: {0}", ex.ToString()));
                }

                Console.WriteLine("Script finished");
            }
        }

        public void Save()
        {
            StreamWriter sw = new StreamWriter(InstallerPath);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sw.WriteLine("<Script>");
            sw.WriteLine(string.Format("\t<Name>{0}</Name>", this.Name));
            sw.WriteLine(string.Format("\t<Type>{0}</Type>", this.Type));
            sw.WriteLine(string.Format("\t<File>{0}</File>", this.Path));
            sw.WriteLine(string.Format("\t<Description>{0}</Description>", this.Description));
            sw.WriteLine(string.Format("\t<Enabled>{0}</Enabled>", this.Enabled));
            sw.WriteLine("</Script>");
            sw.Close();
        }
    }
}

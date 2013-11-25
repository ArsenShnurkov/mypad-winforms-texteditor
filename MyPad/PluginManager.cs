using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

using MyPad.Plugins;

namespace MyPad
{
    public class PluginManager
    {
        static List<IPlugin> plugins;

        public static List<IPlugin> Plugins
        {
            get
            {
                return plugins;
            }
        }

        public static void LoadPlugins()
        {
            plugins = new List<IPlugin>();

            string[] files = Directory.GetFiles(Path.Combine(Application.StartupPath, "Plugins"), "*.dll");

            foreach (string file in files)
            {
                try
                {
                    Assembly asm = Assembly.LoadFile(file);

                    foreach (Type t in asm.GetTypes())
                    {
                        if (t.GetInterface(typeof(IPlugin).FullName) != null)
                        {
                            plugins.Add((IPlugin)Activator.CreateInstance(t));
                        }
                    }
                }
                catch (ReflectionTypeLoadException rex)
                {
                    Console.WriteLine(string.Format("Cannot load plugin {0} - {1}", file, rex.LoaderExceptions[0].Message));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Cannot load plugin {0} - {1}", file, ex.Message));
                }
            }
        }
    }
}

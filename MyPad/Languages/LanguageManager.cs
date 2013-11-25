using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MyPad.Languages
{
    public class LanguageManager
    {
        static List<ILanguageStrategy> loadedLanguages;

        public static List<ILanguageStrategy> Languages
        {
            get
            {
                return loadedLanguages;
            }
        }

        public static void Init()
        {
            loadedLanguages = new List<ILanguageStrategy>();

            loadedLanguages.Add(new PHPLanguageStrategy());
            loadedLanguages.Add(new CLanguageStrategy());


            //LoadLanguageStrategiesFromAssembly(Path.Combine(Application.StartupPath, "DefaultLanguageStrategies.dll"));
        }

        public static ILanguageStrategy GetLanguageStrategyForFile(string file)
        {
            if (loadedLanguages.Count > 0)
            {
                foreach (ILanguageStrategy lang in loadedLanguages)
                {
                    if (lang.Extensions.Contains(Path.GetExtension(file).ToLower()))
                        return lang;
                }
            }
            return null;
        }

        public static ILanguageStrategy GetLanguageStrategy(string name)
        {
            if (loadedLanguages.Count > 0)
            {
                foreach (ILanguageStrategy lang in loadedLanguages)
                {
                    if (lang.Name == name)
                        return lang;
                }
            }

            return null;
        }

        public static string[] GetAvailableLanguages()
        {
            List<string> langs = new List<string>();

            foreach (ILanguageStrategy lang in loadedLanguages)
            {
                langs.Add(lang.Name);
            }

            return langs.ToArray();
        }

        protected static void LoadLanguageStrategiesFromAssembly(string path)
        {
            Assembly asm = Assembly.LoadFile(path);

            foreach (Type t in asm.GetTypes())
            {
                if (t.GetInterface(t.FullName) != null)
                {
                    ILanguageStrategy lang = (ILanguageStrategy)Activator.CreateInstance(t);
                    loadedLanguages.Add(lang);
                }
            }
        }
    }
}

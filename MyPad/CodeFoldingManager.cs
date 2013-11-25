using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace MyPad
{
    public class CodeFoldingManager
    {
        static Dictionary<string[], IFoldingStrategy> foldingStrategies;

        public static void Init()
        {
            foldingStrategies = new Dictionary<string[], IFoldingStrategy>();
            foldingStrategies.Add(new string[] { ".php", ".php3", ".php4", ".php5", ".c", ".cpp", ".h", ".hpp", ".cxx", ".cs", ".java" },
                new DefaultFoldingStrategy());
        }

        public static void AddStrategy(string[] extensions, IFoldingStrategy strategy)
        {
            foldingStrategies.Add(extensions, strategy);
        }

        public static IFoldingStrategy GetStrategyForFile(string file)
        {
            string ext = Path.GetExtension(file).ToLower();

            foreach (KeyValuePair<string[], IFoldingStrategy> strategy in foldingStrategies)
            {
                if (strategy.Key.Contains(ext))
                {
                    return strategy.Value;
                }
            }

            return null;
        }
    }
}

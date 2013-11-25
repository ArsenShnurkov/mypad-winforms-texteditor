using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MyPad.Languages
{
    public class CLanguageStrategy : ILanguageStrategy
    {
        public string Name
        {
            get { return "C/C++"; }
        }

        public string[] Extensions
        {
            get { return new string[] { ".c", ".cpp", ".cc", ".cxx", ".h", ".hpp"}; }
        }

        public IHighlightingStrategy HighlightingStrategy
        {
            get { return HighlightingManager.Manager.FindHighlighter("C++"); }
        }

        public IFoldingStrategy FoldingStrategy
        {
            get { return new DefaultFoldingStrategy(); }
        }

        public ICompletionDataProvider CompletionData
        {
            get { return null; }
        }
    }
}

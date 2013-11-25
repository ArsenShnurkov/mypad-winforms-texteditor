using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

using MyPad.CodeCompletionProviders;

namespace MyPad.Languages
{
    public class PHPLanguageStrategy : ILanguageStrategy
    {
        public string Name
        {
            get { return "PHP"; }
        }

        public string[] Extensions
        {
            get { return new string[] { ".php", ".php3", ".php4", ".php5", ".php6", ".phtml", ".htm", ".html", ".xhtml" }; }
        }

        public IHighlightingStrategy HighlightingStrategy
        {
            get { return HighlightingManager.Manager.FindHighlighter("Web"); }
        }

        public IFoldingStrategy FoldingStrategy
        {
            get { return new DefaultFoldingStrategy(); }
        }

        public ICompletionDataProvider CompletionData
        {
            get { return new PHPCompletionProvider(); }
        }
    }
}

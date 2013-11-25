using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MyPad.Languages
{
    public interface ILanguageStrategy
    {
        string Name
        {
            get;
        }

        string[] Extensions
        {
            get;
        }

        IHighlightingStrategy HighlightingStrategy
        {
            get;
        }

        IFoldingStrategy FoldingStrategy
        {
            get;
        }

        ICompletionDataProvider CompletionData
        {
            get;
        }
    }
}

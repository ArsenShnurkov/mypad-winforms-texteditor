using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using ICSharpCode.TextEditor;

namespace MyPad
{
    public class ScriptUtil
    {
        public void InsertString(TextEditorControl editor, int offset, string text)
        {
            editor.Document.Insert(offset, text);
        }

        public void RemoveString(TextEditorControl editor, int offset, int length)
        {
            editor.Document.Remove(offset, length);
        }

        public void Replace(TextEditorControl editor, string text, string replacement)
        {
            editor.Document.Replace(editor.Document.TextContent.IndexOf(text), text.Length, replacement);
        }

        public int Find(TextEditorControl editor, string text, int offset)
        {
            Regex regex = new Regex(text, RegexOptions.None);

            if (regex.IsMatch(editor.Document.TextContent, offset))
            {
                Match m = regex.Match(editor.Document.TextContent, offset);

                Select(editor, m.Index, m.Length);
                return m.Index;
            }

            return 0;
        }

        public void Select(TextEditorControl editor, int offset, int length)
        {
            if (!editor.IsInUpdate)
            {
                TextLocation startPos = editor.Document.OffsetToPosition(offset);
                TextLocation endPos = editor.Document.OffsetToPosition(offset + length);
                editor.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPos, endPos);
            }
        }
    }
}

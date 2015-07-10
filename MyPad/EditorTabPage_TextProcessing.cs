using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Actions;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui;
using System.Diagnostics;
using System.Globalization;
using System.Web;

using NDepend.Path;
using NDepend.Path.Interface.Core;

namespace MyPad
{
    public partial class EditorTabPage : TabPage
    {
        public void InsertTextAtCursor(string textToInsert)
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Save selected text
            string text = textArea.SelectionManager.SelectedText;

            // Clear selection
            if (textArea.SelectionManager.HasSomethingSelected)
            {
                // ensure caret is at start of selection
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                // deselect text
                textArea.SelectionManager.ClearSelection();
            }
            // Replace() takes the arguments: start offset to replace, length of the text to remove, new text
            textArea.Document.Replace(textArea.Caret.Offset,
                text.Length,
                textToInsert);

            textArea.Caret.Position = new TextLocation(textArea.Caret.Position.Column + textToInsert.Length, textArea.Caret.Position.Line);

            // Redraw:
            textArea.Refresh(); 

        }

        public void EnchanceHyperlink()
        {

            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            IFilePath hyperLinkAddressPath = null;
            try
            {
                hyperLinkAddressPath = text.ToFilePath();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
            }


            StringBuilder newContentForInsertion = new StringBuilder(text.Length * 2 + 20);

            if (hyperLinkAddressPath != null) // This is an existing local file
            {
                var hyperLinkAddress = hyperLinkAddressPath.ToString(); // Normalized path
                if (hyperLinkAddressPath.IsRelativePath)
                {
                    newContentForInsertion.AppendFormat ("<a href=\"{0}\">{1}</a>", hyperLinkAddress, hyperLinkAddress);
                } else 
                if (hyperLinkAddressPath.IsAbsolutePath)
                {
                    // convert path to relative
                    newContentForInsertion.AppendFormat ("<a href=\"{0}\">{1}</a>", hyperLinkAddress, hyperLinkAddress);
                }
            }
            else // this is a new local file, or URL or mail address
            {
                string innerHtml = (HttpUtility.UrlDecode(text)).Trim(); // Decoded URL
                StringBuilder hyperlink = new StringBuilder(text.Trim(), text.Length * 2 + 20);

                bool endsLikeHtmlPage = hyperlink.ToString ().ToLower ().EndsWith (".htm");

                if (text.Contains("/") == false)
                {
                    if (text.Contains("@"))
                    {
                        hyperlink.Insert(0, "mailto:");
                    }
                    else
                    {
                        if (endsLikeHtmlPage == false)
                        {
                            hyperlink.Append ("/");
                        }
                    }
                }
                var hyperLinkAddress = hyperlink.ToString();

                int idxSeparator = hyperLinkAddress.IndexOfAny (new char[]{'/','\\'});
                int idxPoint = hyperLinkAddress.IndexOfAny (new char[]{'.'});

                if (idxSeparator < idxPoint || hyperLinkAddress.Contains(":") || endsLikeHtmlPage)
                {
                    newContentForInsertion.AppendFormat("<a href=\"{0}\">{1}</a>", hyperLinkAddress, innerHtml);
                }
                else
                {
                    newContentForInsertion.AppendFormat("<a href=\"https://{0}\">{1}</a>", hyperLinkAddress, innerHtml);
                }
            }

            InsertTextAtCursor(newContentForInsertion.ToString());
        }

        public void EnchanceImagelink()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;
            string trimmedText = text.Trim ();

            int lengthEstimation1 = text.Length * 2 + 20;

            StringBuilder hyperlink = new StringBuilder(trimmedText, lengthEstimation1);

            StringBuilder alttext = new StringBuilder(lengthEstimation1);
            alttext.Append((HttpUtility.UrlDecode(trimmedText)).Trim());

            if (Uri.CheckSchemeName (Uri.UriSchemeHttp) || Uri.CheckSchemeName (Uri.UriSchemeHttps)) // полная внешняя ссылка
            {
                // ничего не делаем
            } else // внутренняя ссылка
            {
                // проводим разные преобразования
                bool relativeLink = trimmedText.StartsWith(".");
                // а что если относительный путь начинается не с точки, а прямо с буквы?
                // TODO: проверить, если ли такой файл по факту
                bool justAFilename = string.IsNullOrEmpty (Path.GetDirectoryName (trimmedText));

                /*
                // Как подсчитать колчество точек в строке стандартными библиотечными функциями C# ? (лобовой вариант, как сделать короче)
                // вопрос для hashcode.ru
                int dotcount = 0;
                for (int i = 0; i < path.Length; i++) {
                    if (path [i] == '.') {
                        dotcount++;
                    }
                }
                */
                if (relativeLink || justAFilename)
                {
                    // do nothing
                } else
                {
                    if (text.Contains (":") == false) {
                        hyperlink.Insert (0, "https://");
                    }
                }
            }

            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";
            int lengthEstimation2 = formatString.Length + hyperlink.Length + alttext.Length;

            StringBuilder sb = new StringBuilder(lengthEstimation2);
            sb.AppendFormat (formatString, hyperlink.ToString (), alttext.ToString ());

            InsertTextAtCursor(sb.ToString());
        }

        public void MakeBold()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            int leftspaces = 0;
            for (; leftspaces < text.Length; ++leftspaces)
            {
                if (char.IsWhiteSpace (text [leftspaces]) == false)
                {
                    break;
                }
            }
            int rightspaces = text.Length;
            for (; rightspaces > 0; --rightspaces)
            {
                if (char.IsWhiteSpace (text [rightspaces - 1]) == false)
                {
                    break;
                }
            }

            string trimmedText = text.Substring(leftspaces, rightspaces - leftspaces);

            string formatString = "{0}<strong>{1}</strong>{2}";
            int lengthEstimation2 = formatString.Length + text.Length;

            StringBuilder sb = new StringBuilder(lengthEstimation2);
            sb.AppendFormat (formatString, text.Substring(0, leftspaces), trimmedText, text.Substring(rightspaces));

            InsertTextAtCursor(sb.ToString());
        }

        public void MakeSelectionRed()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            int leftspaces = 0;
            for (; leftspaces < text.Length; ++leftspaces)
            {
                if (char.IsWhiteSpace (text [leftspaces]) == false)
                {
                    break;
                }
            }
            int rightspaces = text.Length;
            for (; rightspaces > 0; --rightspaces)
            {
                if (char.IsWhiteSpace (text [rightspaces - 1]) == false)
                {
                    break;
                }
            }

            string trimmedText = text.Substring(leftspaces, rightspaces - leftspaces);

            string formatString = "{0}<font style=\"color:red\">{1}</font>{2}";
            int lengthEstimation2 = formatString.Length + text.Length;

            StringBuilder sb = new StringBuilder(lengthEstimation2);
            sb.AppendFormat (formatString, text.Substring(0, leftspaces), trimmedText, text.Substring(rightspaces));

            InsertTextAtCursor(sb.ToString());
        }


        public void MakeBR()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Save selected text
            string text = textArea.SelectionManager.SelectedText;

            // Clear selection
            if (textArea.SelectionManager.HasSomethingSelected)
            {
                // ensure caret is at start of selection
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection[0].StartPosition;
                // deselect text
                textArea.SelectionManager.ClearSelection();
            }

            string brtag = "<br />";
            int lengthEstimation = Environment.NewLine.Length * 2 + brtag.Length;
            StringBuilder sb = new StringBuilder(lengthEstimation);
            sb.AppendFormat ("{0}{1}", brtag, Environment.NewLine);
            int delta = 1;

            var line = textArea.TextView.Document.GetLineSegment(textArea.Caret.Position.Line);
            var linetext = textArea.TextView.Document.GetText(line);
            if (string.IsNullOrWhiteSpace (linetext) == false)
            {
                sb.Insert(0, Environment.NewLine);
                delta++;
            }

            string textToInsert = sb.ToString ();

            // Replace() takes the arguments: start offset to replace, length of the text to remove, new text
            textArea.Document.Replace(textArea.Caret.Offset,
                text.Length,
                textToInsert);
            textArea.Caret.Position = new TextLocation(0, textArea.Caret.Position.Line + delta);
            textArea.Refresh ();
        }

        public void MakeH(string level)
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            string trimmedText = text.Trim();

            string formatString = "<h{0}>{1}</h{0}>";
            int lengthEstimation2 = formatString.Length + level.Length * 2 + trimmedText.Length;

            StringBuilder sb = new StringBuilder(lengthEstimation2);
            sb.AppendFormat (formatString, level, trimmedText);

            InsertTextAtCursor(sb.ToString());
        }
    }
}

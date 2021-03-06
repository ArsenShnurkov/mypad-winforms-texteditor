﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

using ICSharpCode.TextEditor;
using System.Diagnostics;
using System.Web;

using NDepend.Path;

namespace MyPad
{
    public partial class EditorTabPage : TabPage
    {
        public void InsertTextAtCursor (string textToInsert)
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;
            textArea.BeginUpdate ();

            // Save selected text
            string text = textArea.SelectionManager.SelectedText;

            // Clear selection
            if (textArea.SelectionManager.HasSomethingSelected) {
                // ensure caret is at start of selection
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection [0].StartPosition;
                // deselect text
                textArea.SelectionManager.ClearSelection ();
            }

            //var startLocation = textArea.Caret.Position;

            // Replace() takes the arguments: start offset to replace, length of the text to remove, new text
            textArea.Document.Replace (textArea.Caret.Offset,
                text.Length,
                textToInsert);

            textArea.Caret.Position = textArea.Document.OffsetToPosition (textArea.Caret.Offset + textToInsert.Length);

            // Redraw:
            textArea.EndUpdate ();
            /*textArea.Caret.ValidateCaretPos ();
            textArea.Refresh(); 
            textArea.Caret.UpdateCaretPosition ();*/
        }

        bool FindAdReplaceByRegexps (string search, string replacement, RegexOptions options)
        {
            textEditorControl.Document.UndoStack.StartUndoGroup ();
            try {
                Regex regex = new Regex (search, options);

                MatchCollection matches = regex.Matches (textEditorControl.Text);

                for (int i = matches.Count - 1; i >= 0; i--) {
                    Match m = matches [i];

                    string find = m.Value;
                    string replace = regex.Replace (find, replacement);
                    textEditorControl.Document.Replace (
                        m.Index,
                        m.Length,
                        replace);
                }
            } catch (ArgumentException ex) {
                // suppress compilation error, search for raw text instead
                Trace.WriteLine (ex.ToString ());
                textEditorControl.Document.UndoStack.EndUndoGroup ();
                textEditorControl.Document.UndoStack.Undo ();
                return false;
            }
            textEditorControl.Document.UndoStack.EndUndoGroup ();
            return true;
        }

        void FindAdReplaceRaw (string search, string replacement, RegexOptions options/*not used*/)
        {
            textEditorControl.Document.UndoStack.StartUndoGroup ();
            var positions = new List<int> ();
            int pos = textEditorControl.Text.IndexOf (search);
            for (; pos >= 0; pos = textEditorControl.Text.IndexOf (search, pos + search.Length)) {
                positions.Add (pos);
            }
            for (int i = positions.Count - 1; i >= 0; i--) {
                textEditorControl.Document.Replace (
                    positions [i],
                    search.Length,
                    replacement);
            }
            textEditorControl.Document.UndoStack.EndUndoGroup ();
        }

        public void EnchanceHyperlink ()
        {

            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string strSelectedText = textArea.SelectionManager.SelectedText;
            string textOfHref = strSelectedText.Trim ();
            // remove quotes which was selected by accident
            while (textOfHref.StartsWith ("\""))
                textOfHref = textOfHref.Substring (1);
            while (textOfHref.EndsWith ("\""))
                textOfHref = textOfHref.Substring (0, textOfHref.Length - 1);

            string textOfWholeTag = (HttpUtility.UrlDecode (strSelectedText)).Trim (); // Decoded URL

            try {
                var url = new Uri (textOfHref);
                //if ("file".CompareTo (url.Scheme.ToLower ()) == 0)
                if (url.IsFile) {
                    string pathToSource = this.GetFileFullPathAndName ();

                    string pathToTarget = url.AbsolutePath;

                    // replace text of link with title
                    if (Globals.GetMainForm ().Exists (pathToTarget)) {
                        textOfWholeTag = Globals.GetTextTitleFromFile (pathToTarget);
                    }

                    var relUri = GetRelativeUriString (pathToSource, pathToTarget);
                    if (string.IsNullOrWhiteSpace (relUri) == false) {
                        textOfHref = relUri; // replace href of link
                    }
                }
            } catch (Exception ex) {
                // invalid url exception during parsing in Uri constructor
                Trace.WriteLine (ex.ToString ());
            }

            IFilePath hyperLinkAddressPath = null;
            try {
                hyperLinkAddressPath = textOfHref.ToFilePath ();
            } catch (Exception ex) {
                Trace.WriteLine (ex.ToString ());
            }


            StringBuilder newContentForInsertion = new StringBuilder (strSelectedText.Length * 2 + 20);

            if (hyperLinkAddressPath != null) // This is an existing local file
            {
                var hyperLinkAddress = hyperLinkAddressPath.ToString (); // Normalized path
                if (hyperLinkAddressPath.IsRelativePath) {
                    newContentForInsertion.AppendFormat ("<a href=\"{0}\">{1}</a>", hyperLinkAddress, textOfWholeTag);
                } else {
                    if (hyperLinkAddressPath.IsAbsolutePath) {
                        // convert path to relative
                        newContentForInsertion.AppendFormat ("<a href=\"{0}\">{1}</a>", hyperLinkAddress, textOfWholeTag);
                    }
                }
            } else // this is a new local file, or URL or mail address
              {
                StringBuilder hyperlink = new StringBuilder (textOfHref, textOfHref.Length * 2 + 20);

                if (textOfHref.IndexOf (".") < 0) {
                    hyperlink.Replace (' ', '-');
                    hyperlink.Replace (':', '_');
                    hyperlink.Append (".htm");
                }

                bool endsLikeHtmlPage = hyperlink.ToString ().ToLower ().EndsWith (".htm");

                if (textOfHref.Contains ("/") == false) {
                    if (textOfHref.Contains ("@")) {
                        hyperlink.Insert (0, "mailto:");
                    } else {
                        if (endsLikeHtmlPage == false) {
                            hyperlink.Append ("/");
                        }
                    }
                }
                textOfHref = hyperlink.ToString ();

                int idxSeparator = textOfHref.IndexOfAny (new char [] { '/', '\\' });
                int idxPoint = textOfHref.IndexOfAny (new char [] { '.' });

                if (idxSeparator < idxPoint || textOfHref.Contains (":") || endsLikeHtmlPage) {
                    newContentForInsertion.AppendFormat ("<a href=\"{0}\">{1}</a>", textOfHref, textOfWholeTag);
                } else {
                    newContentForInsertion.AppendFormat ("<a href=\"https://{0}\">{1}</a>", textOfHref, textOfWholeTag);
                }
            }

            InsertTextAtCursor (newContentForInsertion.ToString ());
        }

        public void EnchanceImagelink ()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;
            if (string.IsNullOrWhiteSpace (text)) {
                string str = Globals.GetMainForm ().InputFileFromURL ();
                InsertTextAtCursor (str);
                return;
            }
            string trimmedText = text.Trim ();

            int lengthEstimation1 = text.Length * 2 + 20;

            StringBuilder hyperlink = new StringBuilder (trimmedText, lengthEstimation1);

            StringBuilder alttext = new StringBuilder (lengthEstimation1);
            alttext.Append ((HttpUtility.UrlDecode (trimmedText)).Trim ());

            if (Uri.CheckSchemeName (Uri.UriSchemeHttp) || Uri.CheckSchemeName (Uri.UriSchemeHttps)) // полная внешняя ссылка
            {
                // ничего не делаем
            } else // внутренняя ссылка
            {
                // проводим разные преобразования
                bool relativeLink = trimmedText.StartsWith (".", StringComparison.InvariantCulture);
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
                if (relativeLink || justAFilename) {
                    // do nothing
                } else {
                    if (text.Contains (":") == false) {
                        hyperlink.Insert (0, "https://");
                    }
                }
            }

            string formatString = "<img src=\"{0}\" alt=\"{1}\" />";
            int lengthEstimation2 = formatString.Length + hyperlink.Length + alttext.Length;

            StringBuilder sb = new StringBuilder (lengthEstimation2);
            sb.AppendFormat (formatString, hyperlink.ToString (), alttext.ToString ());

            InsertTextAtCursor (sb.ToString ());
        }

        public void ConvertTextToHtml ()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            var sb = new StringBuilder (text, text.Length + text.Length / 5); // 20% estimate (not based on facts)

            sb.Replace ("&", "&amp;");
            sb.Replace ("<", "&lt;");
            sb.Replace (">", "&gt;");
            sb.Replace ("\"", "&quot;");

            InsertTextAtCursor (sb.ToString ());
        }


        public void MakeBold ()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            int leftspaces = 0;
            for (; leftspaces < text.Length; ++leftspaces) {
                if (char.IsWhiteSpace (text [leftspaces]) == false) {
                    break;
                }
            }
            int rightspaces = text.Length;
            for (; rightspaces > 0; --rightspaces) {
                if (char.IsWhiteSpace (text [rightspaces - 1]) == false) {
                    break;
                }
            }

            string trimmedText = text.Substring (leftspaces, rightspaces - leftspaces);

            string formatString = "{0}<strong>{1}</strong>{2}";
            int lengthEstimation2 = formatString.Length + text.Length;

            StringBuilder sb = new StringBuilder (lengthEstimation2);
            sb.AppendFormat (formatString, text.Substring (0, leftspaces), trimmedText, text.Substring (rightspaces));

            InsertTextAtCursor (sb.ToString ());
        }

        public void MakeSelectionRed ()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            int leftspaces = 0;
            for (; leftspaces < text.Length; ++leftspaces) {
                if (char.IsWhiteSpace (text [leftspaces]) == false) {
                    break;
                }
            }
            int rightspaces = text.Length;
            for (; rightspaces > 0; --rightspaces) {
                if (char.IsWhiteSpace (text [rightspaces - 1]) == false) {
                    break;
                }
            }

            string trimmedText = text.Substring (leftspaces, rightspaces - leftspaces);

            string formatString = "{0}<font style=\"color:red\">{1}</font>{2}";
            int lengthEstimation2 = formatString.Length + text.Length;

            StringBuilder sb = new StringBuilder (lengthEstimation2);
            sb.AppendFormat (formatString, text.Substring (0, leftspaces), trimmedText, text.Substring (rightspaces));

            InsertTextAtCursor (sb.ToString ());
        }


        public void MakeBR ()
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Save selected text
            string text = textArea.SelectionManager.SelectedText;

            // Clear selection
            if (textArea.SelectionManager.HasSomethingSelected) {
                // ensure caret is at start of selection
                textArea.Caret.Position = textArea.SelectionManager.SelectionCollection [0].StartPosition;
                // deselect text
                textArea.SelectionManager.ClearSelection ();
            }

            string brtag = "<br />";
            int lengthEstimation = Environment.NewLine.Length * 2 + brtag.Length;
            StringBuilder sb = new StringBuilder (lengthEstimation);
            sb.AppendFormat ("{0}{1}", brtag, Environment.NewLine);
            int delta = 1;

            var line = textArea.TextView.Document.GetLineSegment (textArea.Caret.Position.Line);
            var linetext = textArea.TextView.Document.GetText (line);
            if (string.IsNullOrWhiteSpace (linetext) == false) {
                sb.Insert (0, Environment.NewLine);
                delta++;
            }

            string textToInsert = sb.ToString ();

            // Replace() takes the arguments: start offset to replace, length of the text to remove, new text
            textArea.Document.Replace (textArea.Caret.Offset,
                text.Length,
                textToInsert);
            textArea.Caret.Position = new TextLocation (0, textArea.Caret.Position.Line + delta);
            textArea.Refresh ();
        }

        public void MakeH (string level)
        {
            var textArea = textEditorControl.ActiveTextAreaControl.TextArea;

            // Get selected text
            string text = textArea.SelectionManager.SelectedText;

            string trimmedText = text.Trim ();

            string formatString = "<h{0}>{1}</h{0}>";
            int lengthEstimation2 = formatString.Length + level.Length * 2 + trimmedText.Length;

            StringBuilder sb = new StringBuilder (lengthEstimation2);
            sb.AppendFormat (formatString, level, trimmedText);

            InsertTextAtCursor (sb.ToString ());
        }
    }
}

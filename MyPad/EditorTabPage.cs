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

namespace MyPad
{
    public class EditorTabPage : TabPage
    {
        public event EventHandler EditorTextChanged;

        TextEditorControl textEditorControl;
        bool saved = true;

        int lastFindOffset = 0;

        public TextEditorControl Editor
        {
            get
            {
                return textEditorControl;
            }
        }

        public bool Saved
        {
            get
            {
                return saved;
            }
        }

        public EditorTabPage()
        {
            string fontName = string.Empty;
            float fontSize = 0;

            try
            {
                fontName = SettingsManager.ReadValue<string>("FontName");
                string stringFontSize = SettingsManager.ReadAsString("FontSize");
                fontSize = float.Parse(stringFontSize, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            textEditorControl = new TextEditorControl();
            textEditorControl.Name = "textEditorControl";
            textEditorControl.Dock = DockStyle.Fill;
            textEditorControl.TextChanged += new EventHandler(Editor_TextChanged);
            textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectionChanged += new EventHandler(SelectionManager_SelectionChanged);
            textEditorControl.AllowCaretBeyondEOL = SettingsManager.ReadValue<bool>("AllowCaretBeyondEOL");
            textEditorControl.ConvertTabsToSpaces = SettingsManager.ReadValue<bool>("ConvertTabsToSpaces");
            textEditorControl.EnableFolding = SettingsManager.ReadValue<bool>("EnableFolding");
            textEditorControl.ShowEOLMarkers = SettingsManager.ReadValue<bool>("ShowEOLMarkers");
            textEditorControl.ShowHRuler = SettingsManager.ReadValue<bool>("ShowHRuler");
            textEditorControl.ShowInvalidLines = SettingsManager.ReadValue<bool>("ShowInvalidLines");
            textEditorControl.ShowLineNumbers = SettingsManager.ReadValue<bool>("ShowLineNumbers");
            textEditorControl.ShowMatchingBracket = SettingsManager.ReadValue<bool>("ShowMatchingBrackets");
            textEditorControl.ShowSpaces = SettingsManager.ReadValue<bool>("ShowSpaces");
            textEditorControl.ShowTabs = SettingsManager.ReadValue<bool>("ShowTabs");
            textEditorControl.ShowVRuler = SettingsManager.ReadValue<bool>("ShowVRuler");

            if (!string.IsNullOrWhiteSpace(fontName) && fontSize > 0)
            {
                try
                {
                    textEditorControl.Font = new Font(new FontFamily(fontName), fontSize, FontStyle.Regular);
                }
                catch (Exception ex)
                {
                    textEditorControl.Font = new Font(FontFamily.GenericMonospace, 9.75f, FontStyle.Regular);
                    string err = ex.Message;
                }
            }

            if (SettingsManager.ReadValue<bool>("HighlightCurrentLine"))
                textEditorControl.LineViewerStyle = LineViewerStyle.FullRow;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "Smart")
                textEditorControl.IndentStyle = IndentStyle.Smart;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "Auto")
                textEditorControl.IndentStyle = IndentStyle.Auto;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "None")
                textEditorControl.IndentStyle = IndentStyle.None;
            if (SettingsManager.ReadValue<bool>("AutoInsertBrackets"))
                textEditorControl.ActiveTextAreaControl.TextEditorProperties.AutoInsertCurlyBracket = true;
            
            textEditorControl.Show();

            this.Controls.Add(textEditorControl);

            EditorTextChanged = new EventHandler(TextEditorControl_TextChanged);

        }

        public void ReloadSettings()
        {
            string fontName = SettingsManager.ReadValue<string>("FontName");
            float fontSize = SettingsManager.ReadValue<float>("FontSize");

            textEditorControl.AllowCaretBeyondEOL = SettingsManager.ReadValue<bool>("AllowCaretBeyondEOL");
            textEditorControl.ConvertTabsToSpaces = SettingsManager.ReadValue<bool>("ConvertTabsToSpaces");
            textEditorControl.EnableFolding = SettingsManager.ReadValue<bool>("EnableFolding");
            textEditorControl.ShowEOLMarkers = SettingsManager.ReadValue<bool>("ShowEOLMarkers");
            textEditorControl.ShowHRuler = SettingsManager.ReadValue<bool>("ShowHRuler");
            textEditorControl.ShowInvalidLines = SettingsManager.ReadValue<bool>("ShowInvalidLines");
            textEditorControl.ShowLineNumbers = SettingsManager.ReadValue<bool>("ShowLineNumbers");
            textEditorControl.ShowMatchingBracket = SettingsManager.ReadValue<bool>("ShowMatchingBrackets");
            textEditorControl.ShowSpaces = SettingsManager.ReadValue<bool>("ShowSpaces");
            textEditorControl.ShowTabs = SettingsManager.ReadValue<bool>("ShowTabs");
            textEditorControl.ShowVRuler = SettingsManager.ReadValue<bool>("ShowVRuler");

            if (fontName != string.Empty && fontSize > 0)
            {
                try
                {
                    textEditorControl.Font = new Font(new FontFamily(fontName), fontSize, FontStyle.Regular);
                }
                catch (Exception ex)
                {
                    textEditorControl.Font = new Font(FontFamily.GenericMonospace, 9.75f, FontStyle.Regular);
                    string err = ex.Message;
                }
            }

            if (SettingsManager.ReadValue<bool>("HighlightCurrentLine"))
                textEditorControl.LineViewerStyle = LineViewerStyle.FullRow;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "Smart")
                textEditorControl.IndentStyle = IndentStyle.Smart;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "Auto")
                textEditorControl.IndentStyle = IndentStyle.Auto;
            if (SettingsManager.ReadValue<string>("IndentStyle") == "None")
                textEditorControl.IndentStyle = IndentStyle.None;
            if (SettingsManager.ReadValue<bool>("AutoInsertBrackets"))
                textEditorControl.ActiveTextAreaControl.TextEditorProperties.AutoInsertCurlyBracket = true;

            textEditorControl.OptionsChanged();
        }

        public void LoadFile(string path)
        {
            textEditorControl.TextChanged -= new EventHandler(Editor_TextChanged);
            textEditorControl.LoadFile(path, true, true);
            textEditorControl.TextChanged += new EventHandler(Editor_TextChanged);

            this.Text = Path.GetFileName(path);
            this.ToolTipText = path;
        }

        public void SaveFile(string path)
        {
            textEditorControl.SaveFile(path);
            saved = true;

            this.Text = Path.GetFileName(path);
            this.ToolTipText = path;
        }

        public void SetHighlighting(string name)
        {
            textEditorControl.SetHighlighting(name);
        }

        public string GetHighlighting()
        {
            return textEditorControl.Document.HighlightingStrategy.Name;
        }

        public void Undo()
        {
            textEditorControl.Undo();
        }

        public void Redo()
        {
            textEditorControl.Redo();
        }

        public void Cut()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null, null);
        }

        public void Copy()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null, null);
        }

        public void Paste()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null, null);
        }

        public void SelectAll()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll(null, null);
        }

        public void Delete()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete(null, null);
        }

        public int Find(string search, RegexOptions options)
        {
            Regex regex = new Regex(search, options);

            if (regex.IsMatch(textEditorControl.Text, lastFindOffset))
            {
                Match m = regex.Match(textEditorControl.Text, lastFindOffset);

                TextLocation start = textEditorControl.Document.OffsetToPosition(m.Index);
                TextLocation end = textEditorControl.Document.OffsetToPosition((m.Index + m.Length));
                textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(start, end);

                lastFindOffset = (m.Index + m.Length);
            }
            else
                lastFindOffset = -1;

            return lastFindOffset;
        }

        public void FindAndReplace(string search, string replacement, RegexOptions options)
        {
            Regex regex = new Regex(search, options);

            MatchCollection matches = regex.Matches(textEditorControl.Text);

            foreach (Match m in matches)
            {
                string find = m.Value;
                string replace = regex.Replace(find, replacement);
                textEditorControl.Document.Replace(m.Index, m.Length, replace);
            }
        }

        public void HighlightMatchingTokens(string text)
        {
            foreach (LineSegment line in textEditorControl.Document.LineSegmentCollection)
            {
                foreach (TextWord word in line.Words)
                {
                    if (word.Word == text)
                    {
                        if (word.SyntaxColor != null)
                            word.SyntaxColor = new HighlightColor(word.SyntaxColor.Color, Color.PaleGreen, 
                                word.SyntaxColor.Bold, word.SyntaxColor.Italic);
                    }
                }
            }
        }

        public void ClearHighlightedTokens()
        {
            foreach (LineSegment line in textEditorControl.Document.LineSegmentCollection)
            {
                foreach (TextWord word in line.Words)
                {
                    if (word.SyntaxColor != null)
                    {
                        if (word.SyntaxColor.BackgroundColor == Color.PaleGreen)
                            word.SyntaxColor = new HighlightColor(word.SyntaxColor.Color, 
                                textEditorControl.Document.HighlightingStrategy.GetColorFor("Default").BackgroundColor, 
                                word.SyntaxColor.Bold, word.SyntaxColor.Italic);
                    }
                }
            }
        }


        public void ScrollToOffset(int offset)
        {
            var location = textEditorControl.Document.OffsetToPosition(offset);
            this.textEditorControl.ActiveTextAreaControl.Caret.Position = location;
        }

        public void SelectLine(int line)
        {
            LineSegment lineSeg = textEditorControl.Document.GetLineSegment(line);
            TextLocation start = textEditorControl.Document.OffsetToPosition(lineSeg.Offset);
            TextLocation end = textEditorControl.Document.OffsetToPosition(lineSeg.Offset + lineSeg.Length);
            textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(start, end);
        }

        public void MoveLineUp(int line)
        {
            if ((line - 1) > 0)
            {
                LineSegment lineSeg = textEditorControl.Document.GetLineSegment(line);
                LineSegment prevLineSeg = textEditorControl.Document.GetLineSegment(line - 1);
                string lineText = textEditorControl.Document.GetText(lineSeg);

                textEditorControl.Document.Remove(lineSeg.Offset, lineSeg.TotalLength);
                textEditorControl.Document.Insert(prevLineSeg.Offset, lineText + Environment.NewLine);
            }
        }

        public void MoveLineDown(int line)
        {
            if ((line + 1) < textEditorControl.Document.TotalNumberOfLines)
            {
                MoveLineUp(line + 1);
            }
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            saved = false;

            if (!this.Text.Contains("*"))
                this.Text = this.Text + "*";

            EditorTextChanged(this, e);
        }

        private void TextEditorControl_TextChanged(object sender, EventArgs e)
        {

        }

        void SelectionManager_SelectionChanged(object sender, EventArgs e)
        {
            if (textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
            {
                HighlightMatchingTokens(textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText);
            }
            else
            {
                ClearHighlightedTokens();
                this.Refresh();
            }
        }
    }
}

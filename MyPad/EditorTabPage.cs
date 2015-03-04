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

namespace MyPad
{
    public partial class EditorTabPage : TabPage
    {
        public event EventHandler OnEditorTextChanged;
        public event EventHandler OnEditorTabFilenameChanged;
        public event EventHandler OnEditorTabStateChanged;

        TextEditorControl textEditorControl;

        bool hasSomethingOnDisk = false;
        bool isSavingNecessary = false;

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
                if (hasSomethingOnDisk == false && string.IsNullOrWhiteSpace(Text))
                {
                    return true;
                }
                if (hasSomethingOnDisk && (isSavingNecessary == false))
                {
                    return true;
                }
                return false;
            }
        }

        public bool HasSomethingOnDisk
        {
            get
            {
                return hasSomethingOnDisk;
            }
        }

        public bool IsSavingNecessary
        {
            get
            {
                return isSavingNecessary;
            }
            set
            {
                if (isSavingNecessary != value)
                {
                    isSavingNecessary = value;
                    UpdateTabName();
                }
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
            textEditorControl.TextChanged += new EventHandler(Handle_EditorTextChanged);
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
            textEditorControl.TextChanged -= new EventHandler(Handle_EditorTextChanged);
            textEditorControl.LoadFile(path, true, true);
            hasSomethingOnDisk = true;
            textEditorControl.TextChanged += new EventHandler(Handle_EditorTextChanged);

            this.Text = Path.GetFileName(path);
            this.ToolTipText = path;
        }

        public void SaveFile(string path)
        {
            textEditorControl.SaveFile(path);
            hasSomethingOnDisk = true;
            if (IsSavingNecessary == true)
            {
                IsSavingNecessary = false; // will update tab name
            }
            else
            {
                UpdateTabName();
            }
        }

        public void SetFileName(string path)
        {
            this.Text = Path.GetFileName(path);
            if (hasSomethingOnDisk)
            {
                this.ForeColor = Color.Black;
            }
            else
            {
                this.ForeColor = Color.Red;
            }

            this.ToolTipText = path;
            if (Parent != null)
            {
                Parent.Invalidate();
            }

            Fire_EditorTab_FilenameChanged();
            UpdateTabName();
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
            if (lastFindOffset == -1) {
                lastFindOffset = 0; // restart from top, otherwise it will be exception
            }

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

        private void Fire_EditorTab_FilenameChanged()
        {
            if (OnEditorTabFilenameChanged != null)
            {
                OnEditorTabFilenameChanged(this, null);
            }
            Fire_EditorTab_StateChanged();
        }

        private void Fire_EditorTab_StateChanged()
        {
            if (OnEditorTabStateChanged != null)
            {
                OnEditorTabStateChanged(this, null);
            }
        }

        protected void UpdateTabName()
        {
            if (isSavingNecessary)
            {
                if (this.Text.EndsWith("*") == false)
                {
                    this.Text = this.Text + "*";
                }
            }
            else
            {
                if (this.Text.EndsWith("*") == true)
                {
                    int length = this.Text.Length;
                    if (length > 0) // unnecessary check, because it was checked above, that Text.EndsWith("*")
                    {
                        this.Text = this.Text.Substring(0, length - 1);
                    }
                }
            }

            Fire_EditorTab_StateChanged();
        }

        private void Fire_EditorTextChanged(object sender, EventArgs e)
        {
            if (OnEditorTextChanged != null)
            {
                OnEditorTextChanged(this, e);
            }
        }

        private void Handle_EditorTextChanged(object sender, EventArgs e)
        {
            isSavingNecessary = true;
            UpdateTabName();
        }

        private void Handle_TabTextChanged(object sender, EventArgs e)
        {
            Fire_EditorTab_FilenameChanged();
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

    public static class TabControlExtensions
    {
        public static int GetUntitledTabCount(this TabControl tabControl1)
        {
            int count = 0;

            foreach (EditorTabPage etb in tabControl1.TabPages)
            {
                if (etb.HasSomethingOnDisk == false)
                {
                    count++;
                }
            }

            return count;
        }
    }
}

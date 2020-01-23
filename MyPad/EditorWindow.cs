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
using ICSharpCode.TextEditor.Gui.CompletionWindow;

using MyPad.Plugins;
using MyPad.Languages;

namespace MyPad
{
    public class EditorWindow : MyTabPage
    {
        TextEditorControl textEditorControl;
        CodeCompletionKeyHandler completionKeyHandler;

        //bool documentOpen = false;
        string openFile = "";
        bool saved = true;

        int lastSearchOffset = 0;

        public TextEditorControl TextEditorControl {
            get {
                return textEditorControl;
            }
        }

        public string OpenFile {
            get {
                return openFile;
            }
        }

        public bool Saved {
            get {
                return saved;
            }
        }

        public int LastSearchOffset {
            get {
                return lastSearchOffset;
            }
            set {
                lastSearchOffset = value;
            }
        }

        public int ActiveLine {
            get {
                return textEditorControl.ActiveTextAreaControl.TextArea.Caret.Line;
            }
        }

        /*public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;

                if(tabPage != null)
                    tabPage.Text = this.Text;
            }
        }*/

        public EditorWindow (MainForm mainForm)
        {
            //this.Text = "Untitled";
            //this.WindowState = FormWindowState.Maximized;
            //this.ShowIcon = false;
            //this.ShowInTaskbar = false;
            //this.DockAreas = DockAreas.Document | DockAreas.Float;
            var cfg = Globals.LoadConfiguration ();
            EditorConfigurationSection section = cfg.GetEditorConfiguration ();

            textEditorControl = new TextEditorControl ();
            textEditorControl.Name = "textEditorControl";
            textEditorControl.Dock = DockStyle.Fill;
            string fontFamily = section?.FontName?.Value ?? String.Empty;
            int fontSize = (int)(section?.FontSize?.Value ?? 12);
            textEditorControl.Font = new Font (new FontFamily (fontFamily), fontSize, FontStyle.Regular);
            textEditorControl.ShowEOLMarkers = section?.ShowEOLMarkers?.Value ?? false;
            textEditorControl.ShowHRuler = section?.ShowHRuler?.Value ?? false;
            textEditorControl.ShowInvalidLines = section?.ShowInvalidLines?.Value ?? false;
            textEditorControl.ShowLineNumbers = section?.ShowLineNumbers?.Value ?? false;
            textEditorControl.ShowMatchingBracket = section?.ShowMatchingBrackets?.Value ?? false;
            textEditorControl.ShowSpaces = section?.ShowSpaces?.Value ?? false;
            textEditorControl.ShowTabs = section?.ShowTabs?.Value ?? false;
            textEditorControl.ShowVRuler = section?.ShowVRuler?.Value ?? false;
            textEditorControl.EnableFolding = section?.EnableFolding?.Value ?? false;
            textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectionChanged += new EventHandler (SelectionManager_SelectionChanged);
            textEditorControl.ActiveTextAreaControl.TextArea.KeyPress += new KeyPressEventHandler (TextArea_KeyPress);
            textEditorControl.TextChanged += new EventHandler (textEditorControl_TextChanged);
            textEditorControl.Show ();

            this.Controls.Add (textEditorControl);

            completionKeyHandler = CodeCompletionKeyHandler.Attach (mainForm, textEditorControl);
        }

        public void LoadFile (string file)
        {
            if (File.Exists (file)) {
                openFile = file;
                this.Text = Path.GetFileName (file);
                textEditorControl.LoadFile (file);

                ILanguageStrategy lang = LanguageManager.GetLanguageStrategyForFile (file);

                if (lang != null) {
                    textEditorControl.Document.FoldingManager.FoldingStrategy = lang.FoldingStrategy;
                    textEditorControl.Document.HighlightingStrategy = lang.HighlightingStrategy;
                    completionKeyHandler.CompletionDataProvider = lang.CompletionData;
                }

                saved = true;
                this.Text = this.Text.Replace ("*", "");
            }
        }

        public void SaveFile (string file)
        {
            var cfg = Globals.LoadConfiguration ();
            EditorConfigurationSection section = cfg.GetEditorConfiguration ();
            if (section?.TrailingWhitespace?.Value ?? false) {
                TrimTrailingWhitespace ();
            }

            openFile = file;
            saved = true;
            this.Text = this.Text.Replace ("*", "");

            this.Text = Path.GetFileName (file);
            textEditorControl.SaveFile (file);

            ILanguageStrategy lang = LanguageManager.GetLanguageStrategyForFile (file);

            if (lang != null) {
                textEditorControl.Document.FoldingManager.FoldingStrategy = lang.FoldingStrategy;
                textEditorControl.Document.HighlightingStrategy = lang.HighlightingStrategy;
                completionKeyHandler.CompletionDataProvider = lang.CompletionData;
            }
        }

        public void ReloadFile ()
        {

            if (File.Exists (openFile)) {
                textEditorControl.LoadFile (openFile);

                ILanguageStrategy lang = LanguageManager.GetLanguageStrategyForFile (openFile);

                if (lang != null) {
                    textEditorControl.Document.FoldingManager.FoldingStrategy = lang.FoldingStrategy;
                    textEditorControl.Document.HighlightingStrategy = lang.HighlightingStrategy;
                    completionKeyHandler.CompletionDataProvider = lang.CompletionData;
                }
            }
        }

        public void SetHighlighter (string name)
        {
            textEditorControl.SetHighlighting (name);
        }

        public void SetLanguage (string name)
        {
            ILanguageStrategy lang = LanguageManager.GetLanguageStrategy (name);

            if (lang != null) {
                textEditorControl.Document.FoldingManager.FoldingStrategy = lang.FoldingStrategy;
                textEditorControl.Document.HighlightingStrategy = lang.HighlightingStrategy;
                completionKeyHandler.CompletionDataProvider = lang.CompletionData;
            }
        }

        public void ReloadSettings ()
        {
            /*
                textEditorControl.Font = new Font (new FontFamily (SettingsManager.Settings.ReadValue<string> ("Font")),
                    SettingsManager.Settings.ReadValue<int> ("FontSize"), FontStyle.Regular);
                textEditorControl.ShowEOLMarkers = SettingsManager.Settings.ReadValue<bool> ("ShowEOLMarkers");
                textEditorControl.ShowHRuler = SettingsManager.Settings.ReadValue<bool> ("ShowHRuler");
                textEditorControl.ShowInvalidLines = SettingsManager.Settings.ReadValue<bool> ("ShowInvalidLines");
                textEditorControl.ShowLineNumbers = SettingsManager.Settings.ReadValue<bool> ("ShowLineNumbers");
                textEditorControl.ShowMatchingBracket = SettingsManager.Settings.ReadValue<bool> ("HighlightMatchingBrackets");
                textEditorControl.ShowSpaces = SettingsManager.Settings.ReadValue<bool> ("ShowSpaces");
                textEditorControl.ShowTabs = SettingsManager.Settings.ReadValue<bool> ("ShowTabs");
                textEditorControl.ShowVRuler = SettingsManager.Settings.ReadValue<bool> ("ShowVRuler");
                textEditorControl.EnableFolding = SettingsManager.Settings.ReadValue<bool> ("FoldingEnabled");
            */
        }

        public void Undo ()
        {
            textEditorControl.Undo ();
        }

        public void Redo ()
        {
            textEditorControl.Redo ();
        }

        public void Cut ()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut (null, null);
        }

        public void Copy ()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy (null, null);
        }

        public void Paste ()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste (null, null);
        }

        public void SelectAll ()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.SelectAll (null, null);
        }

        public void Delete ()
        {
            textEditorControl.ActiveTextAreaControl.TextArea.ClipboardHandler.Delete (null, null);
        }

        public void SelectLine (int line)
        {
            if (textEditorControl.Document.TotalNumberOfLines > line) {
                LineSegment ls = textEditorControl.Document.GetLineSegment (line);
                TextLocation startPoint = textEditorControl.Document.OffsetToPosition (ls.Offset);
                TextLocation endPoint = textEditorControl.Document.OffsetToPosition (ls.Offset + ls.TotalLength);

                textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection (startPoint, endPoint);
            }
        }

        public void MoveLineUp (int lineNum)
        {
            if (lineNum > 0) {
                LineSegment line = textEditorControl.Document.GetLineSegment (lineNum);
                string lineText = textEditorControl.Document.GetText (line);

                int prevLineNum = lineNum - 1;
                LineSegment prevLine = textEditorControl.Document.GetLineSegment (prevLineNum);

                textEditorControl.Document.Remove (line.Offset, line.TotalLength);
                textEditorControl.Document.Insert (prevLine.Offset, lineText + Environment.NewLine);
                textEditorControl.Document.RequestUpdate (new TextAreaUpdate (TextAreaUpdateType.LinesBetween, prevLineNum, lineNum));
                textEditorControl.Document.CommitUpdate ();
            }
        }

        public void MoveLineDown (int line)
        {
            if (textEditorControl.Document.TotalNumberOfLines > line) {
                MoveLineUp (line + 1);
            }
        }

        public void DuplicateLine (int lineNum)
        {
            if (lineNum > 0 && lineNum < textEditorControl.Document.TotalNumberOfLines) {
                LineSegment line = textEditorControl.Document.GetLineSegment (lineNum);
                string lineText = textEditorControl.Document.GetText (line);

                textEditorControl.Document.Insert (line.Offset + line.TotalLength, lineText + Environment.NewLine);
                textEditorControl.Document.RequestUpdate (new TextAreaUpdate (TextAreaUpdateType.LinesBetween, lineNum, lineNum + 1));
                textEditorControl.Document.CommitUpdate ();
            }
        }

        public void Find (string text, RegexOptions options)
        {
            Regex regex = new Regex (text, options);

            if (regex.IsMatch (textEditorControl.Text)) {
                Match m = regex.Match (textEditorControl.Text);

                TextLocation startPos = textEditorControl.Document.OffsetToPosition (m.Index);
                TextLocation endPos = textEditorControl.Document.OffsetToPosition (m.Index + m.Length);
                textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection (startPos, endPos);
                this.Refresh ();
            }
        }

        public void FindAndReplace (string text, string replacement, RegexOptions options)
        {
            Regex regex = new Regex (text, options);

            if (regex.IsMatch (textEditorControl.Text)) {
                textEditorControl.Text = regex.Replace (textEditorControl.Text, replacement);
            }
        }

        public void HighlightMatchingWords (string text)
        {
            IDocument doc = textEditorControl.Document;

            foreach (LineSegment line in doc.LineSegmentCollection) {
                foreach (TextWord word in line.Words) {
                    if (word.Word == text) {
                        if (word.SyntaxColor != null)
                            word.SyntaxColor = new HighlightColor (word.SyntaxColor.Color, Color.PaleGreen, word.SyntaxColor.Bold, word.SyntaxColor.Italic);
                        else
                            word.SyntaxColor = new HighlightColor (word.Color, Color.PaleGreen, word.Bold, word.Italic);
                    }
                }
            }
        }

        public void ClearHighlightedMatchingWords ()
        {
            IDocument doc = textEditorControl.Document;

            foreach (LineSegment line in doc.LineSegmentCollection) {
                foreach (TextWord word in line.Words) {
                    if (word.SyntaxColor != null) {
                        if (word.SyntaxColor.BackgroundColor == Color.PaleGreen) {
                            HighlightColor color = textEditorControl.Document.HighlightingStrategy.GetColorFor ("Default");
                            word.SyntaxColor = new HighlightColor (word.SyntaxColor.Color, color.BackgroundColor, word.SyntaxColor.Bold, word.SyntaxColor.Italic);
                        }
                    }
                }
            }
        }

        public void TrimTrailingWhitespace ()
        {
            RemoveTrailingWS removeTrailingWS = new RemoveTrailingWS ();
            removeTrailingWS.Execute (textEditorControl.ActiveTextAreaControl.TextArea);
        }

        void SelectionManager_SelectionChanged (object sender, EventArgs e)
        {
            if (textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected) {
                string selectedText = textEditorControl.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                HighlightMatchingWords (selectedText);
            } else {
                ClearHighlightedMatchingWords ();
                textEditorControl.Document.RequestUpdate (new TextAreaUpdate (TextAreaUpdateType.WholeTextArea));
            }
        }

        void textEditorControl_TextChanged (object sender, EventArgs e)
        {
            saved = false;

            if (!this.Text.Contains ("*")) {
                this.Text += "*";
            }
        }

        void TextArea_KeyPress (object sender, KeyPressEventArgs e)
        {
            /*foreach (ITextPlugin plugin in PluginManager.Plugins)
            {
                plugin.KeyPressed(sender, e);
            }*/

            //textEditorControl.Document.FoldingManager.UpdateFoldings(null, null);
        }
    }
}

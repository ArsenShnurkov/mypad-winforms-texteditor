/*
 * Copyright (c) 2009 Cory Borrow
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MyPad
{
    public class CodeCompletionKeyHandler
    {
        MainForm mainForm;
        TextEditorControl editor;
        CodeCompletionWindow codeCompletionWindow;
        ICompletionDataProvider provider;

        public ICompletionDataProvider CompletionDataProvider
        {
            get
            {
                return provider;
            }
            set
            {
                provider = value;
            }
        }

        public CodeCompletionKeyHandler(MainForm mainForm, TextEditorControl editor)
        {
            this.mainForm = mainForm;
            this.editor = editor;
        }

        public static CodeCompletionKeyHandler Attach(MainForm mainForm, TextEditorControl editor)
        {
            CodeCompletionKeyHandler cckh = new CodeCompletionKeyHandler(mainForm, editor);
            editor.ActiveTextAreaControl.TextArea.KeyEventHandler += cckh.TextArea_KeyEventHandler;

            return cckh;
        }

        bool TextArea_KeyEventHandler(char ch)
        {
            if (provider != null)
            {
                if (codeCompletionWindow != null)
                {
                    if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '$')
                    {
                        if (codeCompletionWindow.ProcessKeyEvent(ch))
                            return true;
                    }
                    else
                    {
                        codeCompletionWindow.Close();
                    }
                }

                if (codeCompletionWindow == null && char.IsLetterOrDigit(ch) || ch == '_')
                {
                    codeCompletionWindow = CodeCompletionWindow.ShowCompletionWindow(mainForm, editor, editor.FileName, provider, ch);

                    if (codeCompletionWindow != null)
                    {
                        codeCompletionWindow.FormClosed += new System.Windows.Forms.FormClosedEventHandler(codeCompletionWindow_FormClosed);
                    }
                }
            }
            return false;
        }

        void editor_Disposed(object sender, EventArgs e)
        {

        }

        void codeCompletionWindow_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            if (codeCompletionWindow != null)
            {
                codeCompletionWindow.FormClosed -= new System.Windows.Forms.FormClosedEventHandler(codeCompletionWindow_FormClosed);
                codeCompletionWindow.Dispose();
                codeCompletionWindow = null;
            }
        }
    }
}

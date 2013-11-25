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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Gui;
using ICSharpCode.TextEditor.Gui.CompletionWindow;

namespace MyPad.CodeCompletionProviders
{
    public class CCompletionProvider : ICompletionDataProvider
    {
        ImageList imageList;

        public int DefaultIndex
        {
            get { return 0; }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            return (ICompletionData[])new object[] { null };
        }

        public ImageList ImageList
        {
            get { return imageList; }
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
            return data.InsertAction(textArea, key);
        }

        public string PreSelection
        {
            get { return ""; }
        }

        public CompletionDataProviderKeyResult ProcessKey(char key)
        {
            if (key == '\r' || key == '\t')
            {
                return CompletionDataProviderKeyResult.InsertionKey;
            }
            else
            {
                return CompletionDataProviderKeyResult.NormalKey;
            }
        }

        private bool InString(TextArea textArea)
        {
            int caretPos = textArea.Caret.Offset;
            bool openString = false;

            for (int i = 0; i < textArea.Document.TextLength; i++)
            {
                if (textArea.Document.TextContent[i] == '"')
                {
                    if (openString)
                        openString = false;
                    else
                        openString = true;
                }

                if (i == caretPos)
                {
                    break;
                }
            }

            return openString;
        }
    }
}

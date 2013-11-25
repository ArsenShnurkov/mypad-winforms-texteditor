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
    public class HTMLCompletionDataProvider : ICompletionDataProvider
    {
        List<ICompletionData> completionData;
        ImageList imageList;

        public int DefaultIndex
        {
            get { return 0; }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            List<ICompletionData> customCompletionData = new List<ICompletionData>();

            if (!InString(textArea))
            {
                if (!InTag(textArea))
                {
                    customCompletionData.AddRange(completionData.ToArray());
                }
                else
                {
                    customCompletionData.Add(new DefaultCompletionData("id", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("class", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("style", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("rel", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("src", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("name", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("type", "", 0));
                    customCompletionData.Add(new DefaultCompletionData("value", "", 0));
                    //customCompletionData.Add(new DefaultCompletionData("href", "", 0));
                }
            }
            

            return customCompletionData.ToArray();
        }

        public ImageList ImageList
        {
            get { return imageList; }
        }

        public string PreSelection
        {
            get { return ""; }
        }

        public HTMLCompletionDataProvider()
        {
            completionData = new List<ICompletionData>();

            StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "FunctionLists\\htmltags.txt"));
            string text = sr.ReadToEnd();
            sr.Close();

            string[] tags = text.Split('\n');

            foreach (string tag in tags)
            {
                completionData.Add(new DefaultCompletionData(tag.Trim(), "", 0));
            }

            imageList = new ImageList();
        }

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
            return data.InsertAction(textArea, key);
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

        private bool InTag(TextArea textArea)
        {
            int caretPos = textArea.Caret.Offset;
            int caretLine = textArea.Caret.Line;

            bool inTag = false;

            MatchCollection matches = Regex.Matches(textArea.Document.TextContent, "<([a-zA-Z]+ )");

            foreach (Match m in matches)
            {
                int line = textArea.Document.GetLineNumberForOffset(m.Index);

                if (m.Index < caretPos && line == caretLine)
                {
                    inTag = true;
                }
            }

            return inTag;
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
                    break;
            }

            return openString;
        }
    }
}

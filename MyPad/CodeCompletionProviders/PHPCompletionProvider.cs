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
    public class PHPCompletionProvider : ICompletionDataProvider
    {
        ImageList imageList;
        List<ICompletionData> completionData;

        public int DefaultIndex
        {
            get { return 0; }
        }

        public ICompletionData[] GenerateCompletionData(string fileName, TextArea textArea, char charTyped)
        {
            List<ICompletionData> customCompletionData = new List<ICompletionData>();

            if (!InString(textArea))
            {
                customCompletionData.AddRange(completionData.ToArray());

                MatchCollection funcMatches = Regex.Matches(textArea.Document.TextContent, "function ([_a-zA-Z0-9]+)( |)\\((.*)\\)");
                MatchCollection varMatches = Regex.Matches(textArea.Document.TextContent, "\\$([_a-zA-Z0-9]+)");

                foreach (Match fm in funcMatches)
                {
                    if (fm.Groups[1].Value != string.Empty)
                    {
                        customCompletionData.Add(new DefaultCompletionData(fm.Groups[1].Value.Trim(), "", 1));
                    }
                }

                foreach (Match vm in varMatches)
                {
                    if (vm.Groups[1].Value != string.Empty)
                    {
                        string var = vm.Groups[1].Value.Trim();

                        if (!ContainsKeyword(customCompletionData, var))
                        {
                            customCompletionData.Add(new DefaultCompletionData(var, "", 3));
                        }
                    }
                }
            }

            return customCompletionData.ToArray();
        }

        public System.Windows.Forms.ImageList ImageList
        {
            get { return imageList; }
        }

        public string PreSelection
        {
            get { return ""; }
        }

        public PHPCompletionProvider()
        {
            completionData = new List<ICompletionData>();
            completionData.Add(new DefaultCompletionData("class", "", 0));
            completionData.Add(new DefaultCompletionData("abstract", "", 0));
            completionData.Add(new DefaultCompletionData("interface", "", 0));
            completionData.Add(new DefaultCompletionData("public", "", 0));
            completionData.Add(new DefaultCompletionData("private", "", 0));
            completionData.Add(new DefaultCompletionData("protected", "", 0));
            completionData.Add(new DefaultCompletionData("static", "", 0));
            completionData.Add(new DefaultCompletionData("extends", "", 0));
            completionData.Add(new DefaultCompletionData("implements", "", 0));
            completionData.Add(new DefaultCompletionData("function", "", 0));

            StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "FunctionLists\\phpfunc.txt"));
            string text = sr.ReadToEnd();
            sr.Close();

            string[] funcs = text.Split('\n');

            foreach (string f in funcs)
            {
                string nf = Regex.Replace(f, "(\\(.*\\))", "");
                completionData.Add(new DefaultCompletionData(nf.Trim(), "", 1));
            }

            imageList = new ImageList();
            imageList.Images.Add(MyPad.Properties.Resources.DefaultIcon);
            imageList.Images.Add(MyPad.Properties.Resources.FunctionIcon);
            imageList.Images.Add(MyPad.Properties.Resources.MethodIcon);
            imageList.Images.Add(MyPad.Properties.Resources.VariableIcon);
            imageList.Images.Add(MyPad.Properties.Resources.InterfaceIcon);
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

        public bool InsertAction(ICompletionData data, TextArea textArea, int insertionOffset, char key)
        {
            textArea.Caret.Position = textArea.Document.OffsetToPosition(insertionOffset);
            return data.InsertAction(textArea, key);
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

        private bool ContainsKeyword(List<ICompletionData> data, string word)
        {
            foreach (ICompletionData d in data)
            {
                if (d.Text == word)
                    return true;
            }

            return false;
        }

        private string GetLastWord(TextArea textArea)
        {
            return null;
        }
    }
}

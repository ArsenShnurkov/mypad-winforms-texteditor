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
using ICSharpCode.TextEditor.Document;

namespace MyPad
{
    public class DefaultFoldingStrategy : IFoldingStrategy
    {
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> folds = new List<FoldMarker>();
            List<FoldMarker> openFoldMarkers = new List<FoldMarker>();

            for (int i = 0; i < document.TotalNumberOfLines; i++)
            {
                LineSegment line = document.GetLineSegment(i);
                string text = document.GetText(line);
                int column = 0;

                if (text.Contains("{"))
                {
                    column = text.LastIndexOf("{") + 1;
                    FoldMarker openFoldMarker = new FoldMarker(document, i, column, 0, 0);
                    openFoldMarkers.Add(openFoldMarker);
                }
                else
                {
                    if (text.Contains("}"))
                    {
                        if (openFoldMarkers.Count > 0)
                        {
                            FoldMarker openFoldMarker = openFoldMarkers[openFoldMarkers.Count - 1];

                            if (i > openFoldMarker.StartLine)
                            {
                                column = text.LastIndexOf("}");
                                folds.Add(new FoldMarker(document, openFoldMarker.StartLine, openFoldMarker.StartColumn, i, column));
                                openFoldMarkers.Remove(openFoldMarker);
                                openFoldMarker = null;
                            }
                            else
                            {
                                openFoldMarkers.Remove(openFoldMarker);
                                openFoldMarker = null;
                            }
                        }
                    }
                }
            }

            return folds;
        }
    }
}

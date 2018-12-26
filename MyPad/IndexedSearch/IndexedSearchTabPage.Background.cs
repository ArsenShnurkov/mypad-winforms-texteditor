using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;
using System.Linq;

namespace MyPad
{
    public partial class IndexedSearchTabPage : TabPage
    {
        string searchQuery;
        DoWorkEventArgs e;

        void backgroundWorker1_DoWork (object sender, DoWorkEventArgs e)
        {
            this.e = e;
            SearchRequest req = e.Argument as SearchRequest;
            this.searchQuery = req.query;
            if (string.IsNullOrWhiteSpace (this.searchQuery)) {
                return;
            }

            string [] words = req.query.Split (new string [] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var files = new Dictionary<string, int> ();
            int includedWords = 0;
            foreach (var w in words) {
                bool exclude = false;
                string wildWord = w;
                if (w.StartsWith ("!", StringComparison.InvariantCulture)) {
                    exclude = true;
                    wildWord = w.Substring (1);
                } else {
                    includedWords++;
                }
                if (Globals.allWords.ContainsKey (wildWord)) {
                    foreach (var fileInfo in Globals.allWords [wildWord].Files) {
                        if (this.backgroundWorker1.CancellationPending) {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            e.Cancel = true;
                            this.backgroundWorker1.ReportProgress (0);
                            return;
                        }
                        if (files.ContainsKey (fileInfo.FullName)) {
                            int count = files [fileInfo.FullName];
                            if (count != -1) {
                                files [fileInfo.FullName] = count + 1;
                            }
                        } else {
                            files.Add (fileInfo.FullName, 1);
                        }
                        if (exclude) {
                            files [fileInfo.FullName] = -1;
                        }
                    }
                }
            }
            var filteredList = new List<FileInfo> ();
            foreach (var kvp in files) {
                if (kvp.Value == includedWords) {
                    //if (kvp.Value != -1) {
                    var fi = new FileInfo (kvp.Key);
                    filteredList.Add (fi);
                }
            }

            foreach (var fn in filteredList.OrderByDescending (fi => fi.LastAccessTimeUtc)) {
                try {
                    ProcessFile (fn, words);
                } catch (Exception ex) {
                    Trace.WriteLine (ex.ToString ());
                }
            }
        }

        void ProcessFile (FileInfo fi, string [] words)
        {
            string [] fileLines = System.IO.File.ReadAllLines (fi.FullName);
            for (int li = 0; li < fileLines.Length; ++li) {
                bool found = false;
                foreach (var word in words) {
                    if (fileLines [li].IndexOf (word, StringComparison.InvariantCulture) >= 0) {
                        found = true;
                    }
                }
                if (found) {
                    SearchResult res = new SearchResult ();
                    res.filePathName = fi.FullName;
                    res.lineNumber = li;
                    res.lineContent = fileLines [li];
                    this.backgroundWorker1.ReportProgress (0, res);
                }
            }
        }
    }
}


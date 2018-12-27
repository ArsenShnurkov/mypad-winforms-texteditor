using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyPad
{
    public class FileSearcherBackgroundWorker : BackgroundWorker
    {
        string searchQuery;
        DoWorkEventArgs e;

        public FileSearcherBackgroundWorker ()
        {
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork (DoWorkEventArgs e)
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
                        if (this.CancellationPending) {
                            // Set the e.Cancel flag so that the WorkerCompleted event
                            // knows that the process was cancelled.
                            e.Cancel = true;
                            this.ReportProgress (0);
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

            int total = filteredList.Count;
            int current = 0;
            foreach (var fn in filteredList.OrderByDescending (fi => fi.LastAccessTimeUtc)) {
                if (this.CancellationPending == true) {
                    e.Cancel = true;
                    Trace.WriteLine ("Нить остановлена");
                    break;
                }
                try {
                    int percentage = (100 * current++) / total;
                    ProcessFile (fn, words, percentage);
                } catch (Exception ex) {
                    Trace.WriteLine (ex.ToString ());
                }
            }

        }
        void ProcessFile (FileInfo fi, string [] words, int percentage)
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
                    this.ReportProgress (percentage, res);
                    Thread.Sleep (20);
                }
            }
        }
    }
}

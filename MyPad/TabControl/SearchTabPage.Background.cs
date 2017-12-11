using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;

namespace MyPad
{
    class SearchRequest
    {
        public string SearchDirectory;
        public string query;
    }

    class SearchResult
    {
        public string filePathName;
        public int lineNumber;
        public string lineContent;
    }

    public partial class SearchTabPage : TabPage
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

            ProcessDirectory (new DirectoryInfo (req.SearchDirectory));
        }

        void ProcessDirectory (DirectoryInfo di)
        {
            FileInfo [] fileInfos = di.GetFiles ("*.htm");
            var files = new SortedList<string, FileInfo> ();
            for (int f1 = 0; f1 < fileInfos.Length; ++f1) {
                FileInfo sf = fileInfos [f1];
                files.Add (sf.Name, sf);
            }
            foreach (string fileName in files.Keys) {
                if (this.backgroundWorker1.CancellationPending) {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    this.backgroundWorker1.ReportProgress (0);
                    return;
                }
                ProcessFile (files [fileName]);

            }

            DirectoryInfo [] subdirs = di.GetDirectories ();
            var dirs = new SortedList<string, DirectoryInfo> ();
            for (int d1 = 0; d1 < subdirs.Length; ++d1) {
                DirectoryInfo sd = subdirs [d1];
                dirs.Add (sd.Name, sd);
            }
            foreach (string shortName in dirs.Keys) {
                if (this.backgroundWorker1.CancellationPending) {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    this.backgroundWorker1.ReportProgress (0);
                    return;
                }
                ProcessDirectory (dirs [shortName]);
            }
        }

        void ProcessFile (FileInfo fi)
        {
            Thread.Sleep (10);
            string [] fileLines = System.IO.File.ReadAllLines (fi.FullName);
            for (int li = 0; li < fileLines.Length; ++li) {
                if (fileLines [li].IndexOf (this.searchQuery, StringComparison.InvariantCulture) >= 0) {
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


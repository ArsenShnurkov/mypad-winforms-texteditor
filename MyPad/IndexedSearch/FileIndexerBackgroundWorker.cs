using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MyPad
{
    public class FileIndexerBackgroundWorker : BackgroundWorker
    {
        public FileIndexerBackgroundWorker ()
        {
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
        }

        protected override void OnDoWork (DoWorkEventArgs e)
        {
            var args = (string [])e.Argument;
            var path = args [0];
            var ext = args [1];
            int i = 0;
            // Print the full path of all .wmv files that are somewhere in the C:\Windows directory or its subdirectories
            foreach (var file in TraverseDirectory (path, f => f.Extension == ext)) {
                if (this.CancellationPending == true) {
                    e.Cancel = true;
                    Trace.WriteLine ("Нить остановлена");
                    break;
                }
                // Perform a time consuming operation and report progress.
                Trace.WriteLine (file.FullName);
                i++;
                this.ReportProgress (0, i);
            }
        }

        static IEnumerable<FileInfo> TraverseDirectory (string rootPath, Func<FileInfo, bool> Pattern)
        {
            var directoryStack = new Stack<DirectoryInfo> ();
            directoryStack.Push (new DirectoryInfo (rootPath));
            while (directoryStack.Count > 0) {
                var dir = directoryStack.Pop ();
                try {
                    foreach (var i in dir.GetDirectories ())
                        directoryStack.Push (i);
                } catch (UnauthorizedAccessException) {
                    continue; // We don't have access to this directory, so skip it
                }
                foreach (var f in dir.GetFiles ().Where (Pattern)) // "Pattern" is a function
                    yield return f;
            }
        }
    }
}


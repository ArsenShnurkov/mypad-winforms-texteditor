using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MyPad
{
    public class FileIndexerBackgroundWorker : BackgroundWorker
    {
        protected int filecountEstimation;

        public FileIndexerBackgroundWorker (int estimation)
        {
            this.WorkerReportsProgress = true;
            this.WorkerSupportsCancellation = true;
            filecountEstimation = estimation;
        }

        protected override void OnDoWork (DoWorkEventArgs e)
        {
            var args = (string [])e.Argument;
            var path = args [0];
            var ext = args [1];
            Globals.InitIndex ();
            int i = 0;
            foreach (var file in TraverseDirectory (path, f => f.Extension == ext)) {
                if (this.CancellationPending == true) {
                    e.Cancel = true;
                    Trace.WriteLine ("Нить остановлена");
                    break;
                }
                // Perform a time consuming operation and report progress.
                try {
                    SplitFileIntoWords (file.FullName);
                } catch (Exception ex) {
                    Trace.WriteLine (file.FullName);
                    Trace.WriteLine (ex.ToString ());
                }
                i++;
                if (filecountEstimation == 0) {
                    filecountEstimation = 100;
                }
                int percent = (100 * i) / filecountEstimation;
                this.ReportProgress (percent, i);
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

        void SplitFileIntoWords (string fullName)
        {
            var dof = new DescriptionOfFile (fullName);
            dof.Index = Globals.allFiles.Files.Count;
            Globals.allFiles.Files.AddLast (dof);

            var words = new Dictionary<string, int> ();
            string content = File.ReadAllText (fullName);
            var matches = Regex.Matches (content, @"([a-z_]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            foreach (var match in matches) {
                string word = IndexOfWords.GetNormalizedString (match.ToString ());
                if (words.ContainsKey (word)) {
                    words [word]++;
                } else {
                    words.Add (word, 1);
                }
            }
            var matchesRus = Regex.Matches (content, @"([а-яёА-ЯЁ_а́еёио́у́ы́э́ю́я́]+)", RegexOptions.Multiline);
            foreach (var match in matchesRus) {
                string word = IndexOfWords.GetNormalizedString (match.ToString ());
                if (words.ContainsKey (word)) {
                    words [word]++;
                } else {
                    words.Add (word, 1);
                }
            }
            var matchesNum = Regex.Matches (content, @"([0-9]+)", RegexOptions.Multiline);
            foreach (var match in matchesNum) {
                string word = IndexOfWords.GetNormalizedString (match.ToString ());
                if (words.ContainsKey (word)) {
                    words [word]++;
                } else {
                    words.Add (word, 1);
                }
            }
            var matchesWeird = Regex.Matches (content, @"([C][#])|([C][+][+])|([F][#])|([F][*])|([A][*])|([C][\-][\-])", RegexOptions.Multiline);
            foreach (var match in matchesWeird) {
                string word = IndexOfWords.GetNormalizedString (match.ToString ());
                if (words.ContainsKey (word)) {
                    words [word]++;
                } else {
                    words.Add (word, 1);
                }
            }
            foreach (var uniqueWord in words.Keys) {
                var wrappedWord = Globals.allWords.NormalizeAndAdd (uniqueWord);
                wrappedWord.Occurences.Files.AddLast (dof);
            }
        }
    }
}

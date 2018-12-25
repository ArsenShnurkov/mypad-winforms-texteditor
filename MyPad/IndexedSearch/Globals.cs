using System;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPad
{
    static partial class Globals
    {
        public static CollectionOfFileDescriptions allFiles = new CollectionOfFileDescriptions ();
        public static IndexOfWords allWords = new IndexOfWords ();

        public static void SaveIndexToCSV (string dirname)
        {
            string fileWithFilenames = Path.Combine (dirname, "files.csv");
            using (var stm = new FileStream (fileWithFilenames, FileMode.OpenOrCreate, FileAccess.Write)) {
                using (var tw = new StreamWriter (stm)) {
                    foreach (var fileWrapper in Globals.allFiles.Files) {
                        tw.WriteLine ("{0}, '{1}'", fileWrapper.Index, fileWrapper.FullName);
                    }
                }
            }
            string fileWithWords = Path.Combine (dirname, "words.csv");
            using (var stm = new FileStream (fileWithWords, FileMode.OpenOrCreate, FileAccess.Write)) {
                using (var tw = new StreamWriter (stm)) {
                    foreach (var wordWrapper in Globals.allWords) {
                        tw.Write ("{0}", wordWrapper.UnifiedRepresentationOfWord);
                        foreach (var fileWrapper in wordWrapper.Occurences.Files) {
                            tw.Write (", ");
                            tw.Write ("{0}", fileWrapper.Index);
                        }
                        tw.WriteLine ();
                    }
                }
            }
        }
        class DummyDisposable : System.IDisposable
        {
            public void Dispose ()
            {
            }
        }
        public static void LoadIndexFromCSV (string dirname)
        {
            allFiles = new CollectionOfFileDescriptions ();
            allWords = new IndexOfWords ();
            string fileWithFilenames = Path.Combine (dirname, "files.csv");
            using (new DummyDisposable ()) {
                var lines = File.ReadLines (fileWithFilenames);
                foreach (var line in lines) {
                    // extract index
                    int idx = line.IndexOf (",", StringComparison.InvariantCulture);
                    string indexString = line.Substring (0, idx);
                    int intIndex = int.Parse (indexString);
                    // extract filename
                    int str1 = line.IndexOf ("'", StringComparison.InvariantCulture);
                    int str2 = line.LastIndexOf ("'", StringComparison.InvariantCulture);
                    string filename = line.Substring (str1 + 1, str2 - str1 - 1);
                    // store them
                    var fileInfo = new DescriptionOfFile (filename);
                    fileInfo.Index = intIndex;
                    allFiles.Files.AddLast (fileInfo);
                }
            }
            var fileArray = allFiles.Files.ToArray ();
            string fileWithWords = Path.Combine (dirname, "words.csv");
            using (var fileStream = new FileStream (fileWithWords, FileMode.Open, FileAccess.Read)) {
                using (var streamReader = new StreamReader (fileStream, Encoding.UTF8, true, 65536)) {
                    String line;
                    while ((line = streamReader.ReadLine ()) != null) {
                        // extract word
                        int idx = line.IndexOf (",", StringComparison.InvariantCulture);
                        string word = line.Substring (0, idx);
                        var wordInfo = Globals.allWords.NormalizeAndAdd (word);
                        idx = idx + 2;
                        while (idx < line.Length) {
                            int fileNumber = 0;
                            for (; idx < line.Length; ++idx) {
                                char c = line [idx];
                                if (c < '0' || c > '9') {
                                    idx = idx + 2;
                                    break;
                                }
                                fileNumber = fileNumber * 10;
                                fileNumber += c - '0';
                            }
                            wordInfo.Occurences.Files.AddLast (fileArray [fileNumber]);
                        }
                    }
                }
            }
        }
    }
}
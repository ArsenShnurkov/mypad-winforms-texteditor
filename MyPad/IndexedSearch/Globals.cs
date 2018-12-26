using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MyPad
{
    static partial class Globals
    {
        public static CollectionOfFileDescriptions allFiles = new CollectionOfFileDescriptions ();
        public static IndexOfWords allWords = new IndexOfWords ();

        public static void InitIndex ()
        {
            Globals.allWords = new IndexOfWords ();
            Globals.allFiles = new CollectionOfFileDescriptions ();
        }

        public static void SaveIndexToDirectory (string dirname)
        {
            SaveFilenames (Path.Combine (dirname, "filenames.txt"));
            SaveWords (Path.Combine (dirname, "words.txt"));
            SaveIndex (Path.Combine (dirname, "index.bdi"));
        }

        public static void LoadIndexFromDirectory (string dirname)
        {
            InitIndex ();

            // Loading filenames
            var loadFilenamesWatch = new Stopwatch ();
            loadFilenamesWatch.Start ();
            LoadFilenames (Path.Combine (dirname, "filenames.txt"));
            loadFilenamesWatch.Stop ();
            Trace.WriteLine (string.Format ("Filenames load time: {0}", loadFilenamesWatch.Elapsed));
            Trace.WriteLine (string.Format ("per item: {0}", loadFilenamesWatch.Elapsed.Ticks / allFiles.Files.Count));

            // Loading words
            var loadWordsWatch = new Stopwatch ();
            loadWordsWatch.Start ();
            LoadWords (Path.Combine (dirname, "words.txt"));
            loadWordsWatch.Stop ();
            Trace.WriteLine (string.Format ("Words load time: {0}", loadWordsWatch.Elapsed));
            Trace.WriteLine (string.Format ("per item: {0}", loadWordsWatch.Elapsed.Ticks / allWords.Count));

            // Loading index
            var loadIndexWatch = new Stopwatch ();
            loadIndexWatch.Start ();
            LoadIndex (Path.Combine (dirname, "index.bdi"));
            loadIndexWatch.Stop ();
            Trace.WriteLine (string.Format ("Index load time: {0}", loadIndexWatch.Elapsed));
        }
        const FileMode openMode = FileMode.OpenOrCreate;
        public static void SaveFilenames (string filename)
        {
            using (var stm = new FileStream (filename, openMode, FileAccess.Write)) {
                stm.Seek (0, SeekOrigin.Begin);
                using (var tw = new StreamWriter (stm)) {
                    foreach (var fileWrapper in Globals.allFiles.Files) {
                        tw.WriteLine (fileWrapper.FullName);
                    }
                    tw.Flush ();
                    stm.SetLength (stm.Position);
                }
            }
        }
        public static void LoadFilenames (string filename)
        {
            int intIndex = 0;
            foreach (var line in File.ReadLines (filename)) {
                var fileInfo = new DescriptionOfFile (line);
                fileInfo.Index = intIndex++;
                allFiles.Files.AddLast (fileInfo);
            }
        }
        public static void SaveWords (string filename)
        {
            using (var stm = new FileStream (filename, openMode, FileAccess.Write)) {
                stm.Seek (0, SeekOrigin.Begin);
                using (var tw = new StreamWriter (stm)) {
                    foreach (var fileWrapper in Globals.allWords) {
                        tw.WriteLine (fileWrapper.UnifiedRepresentationOfWord);
                    }
                    tw.Flush ();
                    stm.SetLength (stm.Position);
                }
            }
        }
        public static void LoadWords (string filename)
        {
            foreach (var line in File.ReadLines (filename)) {
                allWords.AddWithNoChecks (line);
            }
        }
        public static void SaveIndex (string filename)
        {
            using (var stm = new FileStream (filename, openMode, FileAccess.Write)) {
                stm.Seek (0, SeekOrigin.Begin);
                using (var bw = new BinaryWriter (stm)) {
                    // there 3 fields are required to determine bit lenghts
                    bw.Write ((Int32)allWords.Count);
                    int maxFileCountPerWord = 0;
                    foreach (var wordInfo in allWords) {
                        int c = wordInfo.Occurences.Files.Count;
                        if (c > maxFileCountPerWord) {
                            maxFileCountPerWord = c;
                        }
                    }
                    bw.Write ((Int16)maxFileCountPerWord);
                    bw.Write ((Int16)allFiles.Files.Count);
                    // the index itself
                    foreach (var wordInfo in allWords) {
                        //bw.Write (wordInfo.Index);
                        int c = wordInfo.Occurences.Files.Count;
                        bw.Write ((Int16)c);
                        foreach (var file in wordInfo.Occurences.Files) {
                            bw.Write ((Int16)file.Index);
                        }
                    }
                    bw.Flush ();
                    stm.SetLength (stm.Position);
                }
            }
        }
        public static void LoadIndex (string filename)
        {
            try {
                using (var stm = new FileStream (filename, FileMode.Open, FileAccess.Read)) {
                    using (var br = new BinaryReader (stm)) {
                        int wordsCount = br.ReadInt32 ();
                        int maxFileCountPerWord = br.ReadInt16 ();
                        int filesCount = br.ReadInt16 ();
                        for (int wordIndex = 0; wordIndex < wordsCount; wordIndex++) {
                            var wordInfo = allWords [wordIndex];
                            int filesPerWordCount = br.ReadInt16 ();
                            for (int fileIndex = 0; fileIndex < filesPerWordCount; fileIndex++) {
                                int fileInWordIndex = br.ReadInt16 ();
                                if (fileInWordIndex < 0 || fileInWordIndex >= allFiles.Files.Count) {
                                    Debugger.Break ();
                                }
                                var fileInfo = allFiles [fileInWordIndex];
                                wordInfo.Occurences.Files.AddLast (fileInfo);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                Trace.WriteLine (ex.ToString ());
            }
        }
    }
}

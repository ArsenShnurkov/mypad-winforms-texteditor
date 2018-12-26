using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MyPad
{
    public class DescriptionOfFile
    {
        protected string fullname;
        public string FullName {
            get {
                return this.fullname;
            }
        }
        public DescriptionOfFile (string filename)
        {
            this.fullname = filename;
        }
        public int Index;
    }

    public class CollectionOfFileDescriptions
    {
        protected LinkedList<DescriptionOfFile> files = new LinkedList<DescriptionOfFile> ();
        public LinkedList<DescriptionOfFile> Files {
            get {
                return files;
            }
        }
        protected List<DescriptionOfFile> filesRaw = new List<DescriptionOfFile> ();
        public DescriptionOfFile this [int index] {
            get {
                if (filesRaw.Count != files.Count) {
                    filesRaw = files.ToList ();
                }
                return filesRaw [index];
            }
        }
    }

    public class Word
    {
        protected string unifiedRepresentationOfWord;
        public string UnifiedRepresentationOfWord {
            get {
                return this.unifiedRepresentationOfWord;
            }
        }
        protected CollectionOfFileDescriptions occurences = new CollectionOfFileDescriptions ();
        public CollectionOfFileDescriptions Occurences {
            get {
                return this.occurences;
            }
        }
        public Word (string content)
        {
            unifiedRepresentationOfWord = content;
        }
        //public int Index;
    }

    public class IndexOfWords : IEnumerable<Word>
    {
        protected Dictionary<string, Word> words = new Dictionary<string, Word> ();
        public IEnumerable<Word> GetWords ()
        {
            return words.Values;
        }
        public IEnumerator<Word> GetEnumerator ()
        {
            return GetWords ().GetEnumerator ();
        }

        IEnumerator IEnumerable.GetEnumerator ()
        {
            return GetEnumerator ();
        }

        public Word NormalizeAndAdd (string wildWord)
        {
            var normalizedString = GetNormalizedString (wildWord);
            if (words.ContainsKey (normalizedString)) {
                return words [normalizedString];
            } else {
                var wordInformation = new Word (normalizedString);
                //wordInformation.Index = words.Count;
                words.Add (normalizedString, wordInformation);
                return wordInformation;
            }
        }

        internal void AddWithNoChecks (string line)
        {
            var wordInformation = new Word (line);
            words.Add (line, wordInformation);
        }

        public CollectionOfFileDescriptions this [string wildWord] {
            get {
                var normalizedString = GetNormalizedString (wildWord);
                if (words.ContainsKey (normalizedString) == false) {
                    return null;
                }
                var wordInformation = words [normalizedString];
                return wordInformation.Occurences;
            }
        }
        protected List<Word> wordsRaw = new List<Word> ();
        public Word this [int index] {
            get {
                if (wordsRaw.Count != words.Count) {
                    wordsRaw = words.Values.ToList ();
                }
                return wordsRaw [index];
            }
        }
        public static string GetNormalizedString (string wildWord)
        {
            return wildWord.ToLower (CultureInfo.CurrentCulture);
        }

        internal bool ContainsKey (string wildWord)
        {
            var normalizedString = GetNormalizedString (wildWord);
            return this.words.ContainsKey (normalizedString);
        }

        public int Count {
            get {
                return this.words.Count;
            }
        }

    }
}

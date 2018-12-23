using System;
using System.Collections.Generic;
using System.Globalization;

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
    }

    public class CollectionOfFileDescriptions
    {
        protected LinkedList<DescriptionOfFile> files = new LinkedList<DescriptionOfFile> ();
        public LinkedList<DescriptionOfFile> Files {
            get {
                return files;
            }
        }
        public DescriptionOfFile this [int index] {
            get {
                LinkedListNode<DescriptionOfFile> node = files.First;
                for (int i = 0; i < index; ++i) {
                    node = node.Next;
                }
                return node.Value;
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
    }

    public class IndexOfWords
    {
        protected Dictionary<string, Word> words = new Dictionary<string, Word> ();
        public Word NormalizeAndAdd (string wildWord)
        {
            var normalizedString = GetNormalizedString (wildWord);
            var wordInformation = new Word (normalizedString);
            words.Add (normalizedString, wordInformation);
            return wordInformation;
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
        string GetNormalizedString (string wildWord)
        {
            return wildWord.ToLower (CultureInfo.CurrentCulture);
        }
    }
}

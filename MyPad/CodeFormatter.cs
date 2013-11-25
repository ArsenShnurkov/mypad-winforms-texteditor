using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPad
{
    public enum CodeFormatOptions
    {
        BracketsOnNewline,
        ElseOnNewLine
    };

    public class CodeFormatter
    {
        public static string Format(string text, CodeFormatOptions options)
        {
            StringBuilder formattedText = new StringBuilder();

            string ch = "";
            string nextCh = "";
            string prevCh = "";
            int tabSize = 0;

            for (int i = 0; i < text.Length; i++)
            {
                ch = text[i].ToString();
                nextCh = "";
                prevCh = "";

                if (i > 0)
                    prevCh = text[i - 1].ToString();
                if ((i + 1) < text.Length)
                    nextCh = text[i + 1].ToString();

                if (ch == "{")
                {
                    if ((options & CodeFormatOptions.BracketsOnNewline) == CodeFormatOptions.BracketsOnNewline)
                    {
                        formattedText.Append(string.Format("{0}{1}{{0}", Environment.NewLine, RepeatString("\t", tabSize)));
                    }
                    else
                    {
                        formattedText.Append(string.Format(" {{0}", Environment.NewLine));
                    }
                    tabSize++;
                }
                else if (ch == "}")
                {
                    tabSize--;
                    formattedText.Append(string.Format("{0}}{1}", RepeatString("\t", tabSize), Environment.NewLine));
                }
                else if (ch == "\t")
                {
                    //We will add our own tabs, so we will just be skipping this character.
                }
                else
                {
                    formattedText.Append(ch);
                }
            }

            return formattedText.ToString();
        }

        public static string RepeatString(string str, int times)
        {
            string text = "";

            for (int i = 0; i < times; i++)
            {
                text += str;
            }
            return text;
        }

        public static int FindPrevBracket(string text, char openBracket, char closeBracket)
        {
            int open = 1;

            for (int i = text.Length - 1; i > 0; i--)
            {
                if (text[i] == closeBracket)
                    open++;
                else if (text[i] == openBracket)
                    open--;

                if (open == 0)
                    return i;
            }

            return 0;
        }

        public static int FindNextBracket(string text, char openBracket, char closeBracket)
        {
            int open = 1;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == openBracket)
                    open++;
                if (text[i] == closeBracket)
                    open--;

                if (open == 0)
                    return i;
            }

            return 0;
        }
    }
}

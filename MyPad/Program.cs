using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MyPad
{
    static class Program
    {
        public static readonly string DefaultIndexFileName = "index.htm";
        public static TextClipboard TextClipboard = new TextClipboard();
        static CommunicationFactory cf = new CommunicationFactory();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string file = string.Empty;

            if (args.Length > 0)
            {
                file = args[0].Trim();
                if (File.Exists(file) == false)
                {
                    file = string.Empty;
                }
            }

            string encoding = Encoding.Default.EncodingName;
            if (args.Length > 1)
            {
                string e = args[1].Trim();
                var encObj = Encoding.GetEncoding(e);
                if (encObj != null)
                {
                    encoding = encObj.EncodingName;
                }
            }

            if (file != null && file != string.Empty)
            {
                if (cf.PassCommand(file, encoding))
                {
                    return;
                }
            }

            cf.Start();
            try
            {
                Application.Run(new MainForm(file));
            }
            finally
            {
                cf.Stop();
            }
        }

        public static MainForm GetMainForm()
        {
            var form = (MainForm)Application.OpenForms[0];
            return form;
        }

        public static string GetDefaultTemplateText(string caption, string sourceFilename, string targetFilename)
        {
            //string relativeUri = EditorTabPage.GetRelativeUriString (sourceFilename, targetFilename);
            string backlink = EditorTabPage.GetRelativeUriString (targetFilename, sourceFilename);

            var links = new StringBuilder();
            links.AppendFormat("<a href=\"{0}\">{1}</a>", backlink, GetTextTitleFromFile(sourceFilename));
            string defaultDocumentName = Program.DefaultIndexFileName;
            if (defaultDocumentName.CompareTo (backlink) != 0 && defaultDocumentName.CompareTo(targetFilename) != 0)
            {
                links.Append (",");
                links.Append (Environment.NewLine);
                links.AppendFormat("<a href=\"{0}\">{1}</a>", defaultDocumentName, GetTextTitleFromFile(defaultDocumentName));
            }

            string title = GetTextTitleFromFile (sourceFilename);
            var par = new StringBuilder();
            par.AppendFormat("title={0}", Uri.EscapeDataString(title));
            par.Append("&");
            par.AppendFormat("header={0}", Uri.EscapeDataString(title));
            par.Append("&");

            var escapedString = Uri.EscapeDataString (links.ToString ());
            par.AppendFormat("links={0}", escapedString);
            // A potentially dangerous Request.QueryString value was detected from the client

            string text = TemplateEngine2.ProcessRequest("Text1.aspx", par.ToString());
            return text;
        }

        public static string GetTextTitleFromFile(string sourceFilename)
        {
            var mf = GetMainForm ();
            if (mf.Exists(sourceFilename) == false)
            {
                return sourceFilename; // default value for new file
            }

            string content = String.Empty;
            if (mf.AlreadyOpen (sourceFilename))
            {
                content = mf.FindTabByPath(sourceFilename).Editor.Text;
            }
            else
            {
                using (StreamReader sr = new StreamReader(sourceFilename))
                {
                    content = sr.ReadToEnd();
                }
            }

            string title = sourceFilename; // default value
            try
            {
                title = ExtractTitle(content); 
            }
            catch (Exception ex)
            {
                Trace.WriteLine (ex.ToString ());
            }
            return title;
        }

        public static string ExtractTitle(string content)
        {
            string ws = @"\w*";
            string regexQ = string.Format ("{0}\\<{0}title{0}\\>([^<]*)\\<{0}/{0}title{0}\\>", ws);
            Regex regex = new Regex(regexQ);
            Match match = regex.Match(content);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }
    }
}

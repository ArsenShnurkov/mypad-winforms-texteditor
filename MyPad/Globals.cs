using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Configuration;
using System.Threading;

namespace MyPad
{
    static partial class Globals
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
#if DEBUG
            var nameCurrentCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
            Console.WriteLine($"Name (current) = {nameCurrentCulture}");
            var name = Thread.CurrentThread.CurrentCulture.Name;
            Console.WriteLine($"Name (thread) = {name}");
            var lcid = Thread.CurrentThread.CurrentCulture.LCID;
            Console.WriteLine($"LCID = {lcid}");
            /*var cultureName = Thread.CurrentThread.CurrentCulture.DisplayName;
            Console.WriteLine($"CultureName = {cultureName}");
            var englishName = Thread.CurrentThread.CurrentCulture.EnglishName;
            Console.WriteLine($"EnglishName = {englishName}");*/
#endif
            RegisterGlobalExceptionHandlers ();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string file = string.Empty;

            if (args.Length > 0)
            {
                file = args[0].Trim();
                if (File.Exists(file) == false)
                {
                    var u = new Uri (file);
                    if (u.IsFile && File.Exists (u.AbsolutePath))
                    {
                        file = u.AbsolutePath;
                    } else
                    {
                        file = string.Empty;
                    }
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

            if (!string.IsNullOrWhiteSpace(file))
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

        /// <summary>
        /// Gets the assembly product.
        /// </summary>
        /// <remarks>http://stackoverflow.com/questions/7320078/read-assemblytitle-attribute-in-asp-net</remarks>
        /// <value>The assembly product.</value>
        public static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyProductAttribute)attributes[0];
                    if (titleAttribute.Product.Length > 0)
                        return titleAttribute.Product;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        // http://stackoverflow.com/questions/10941657/creating-files-recursively-creating-directories
        public static void EnsureDirectoryExists(string targetFilename)
        {
            var dir = Path.GetDirectoryName (targetFilename);
            if (false == Directory.Exists (dir))
            {
                Directory.CreateDirectory (dir);
            }
        }

        // http://stackoverflow.com/questions/453161/best-practice-to-save-application-settings-in-a-windows-forms-application
        public static void CreateDefaultConfig(string filename)
        {
            // ConfigurationManager
            var cfg = Get(filename);
            cfg.AppSettings.Settings.Add ("AtomFeedLocation", "/var/calculate/remote/distfiles/egit-src/blog/notifications.atom");
            cfg.Save ();
        }

        public static Configuration Get(string fileName)
        {
            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = fileName };
            var cfg = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return cfg;
        }

        public static Configuration LoadConfiguration()
        {
            Configuration cfg;
            // вычисление пути до файла конфига в пользовательской директории
            StringBuilder location = new StringBuilder("${SpecialFolder.LocalApplicationData}/${AssemblyProduct}/${AssemblyTitle}.config");
            // roaming
            location.Replace ("${SpecialFolder.ApplicationData}", System.Environment.GetFolderPath (System.Environment.SpecialFolder.ApplicationData));
            // local
            location.Replace ("${SpecialFolder.LocalApplicationData}", System.Environment.GetFolderPath (System.Environment.SpecialFolder.LocalApplicationData));
            location.Replace ("${AssemblyProduct}", Globals.AssemblyProduct);
            location.Replace ("${AssemblyTitle}", Globals.AssemblyTitle);
            var filename = location.ToString ();
            Globals.EnsureDirectoryExists (filename);
            if (false == File.Exists (filename))
            {
                CreateDefaultConfig (filename);
            }
            for (;;)
            {
                try
                {
                    cfg = Get (filename);
                    break;
                } catch (Exception ex)
                {
                    Trace.WriteLine (ex.ToString ());
                    CreateDefaultConfig (filename);
                    continue;
                }
            }
            return cfg;
        }

        public static string GetDefaultTemplateText(string targetFilename, string caption, string sourceFilename)
        {
            var filenameOnly = Path.GetFileName(targetFilename);
            var filepathOnly = Path.GetDirectoryName(targetFilename);
            //string relativeUri = EditorTabPage.GetRelativeUriString (sourceFilename, targetFilename);
            string backlink = EditorTabPage.GetRelativeUriString (targetFilename, sourceFilename);

            var links = new StringBuilder();
            links.AppendFormat("<a href=\"{0}\">{1}</a>", backlink, GetTextTitleFromFile(sourceFilename));
            string defaultDocumentName = Globals.DefaultIndexFileName;
            if ( defaultDocumentName.CompareTo(filenameOnly) != 0 // If we create a page link from index.htm, there is no need to add it twice
                && defaultDocumentName.CompareTo (backlink) != 0 // If we create **/index.htm, there is no need to link to itself
            )
            {
                var indexfullpath = Path.Combine (filepathOnly, defaultDocumentName);
                var indextitle = GetTextTitleFromFile (indexfullpath);
                links.Append (",");
                links.Append (Environment.NewLine);
                links.AppendFormat("<a href=\"{0}\">{1}</a>", defaultDocumentName, indextitle);
            }

            var par = new StringBuilder();
            par.AppendFormat("title={0}", Uri.EscapeDataString(caption));
            par.Append("&");
            par.AppendFormat("header={0}", Uri.EscapeDataString(caption));
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


        internal static bool IsUnix
        {
            get { 
                int p = (int) Environment.OSVersion.Platform;
                return ((p == 4) || (p == 128) || (p == 6));
            }
        }

        public static bool IsLinux
        {
            get {
                bool isLinux = System.IO.Path.DirectorySeparatorChar == '/';
                return isLinux;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace MyPad
{
    static class Program
    {
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
    }
}

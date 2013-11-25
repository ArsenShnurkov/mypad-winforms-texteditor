using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace MyPad
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string file = "";

            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                    file = args[0];
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (file != null && file != string.Empty)
            {
                Application.Run(new MainForm(file));
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}

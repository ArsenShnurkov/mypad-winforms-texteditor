using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace MyPad
{
    public class ApplicationLog
    {
        static string logPath = "";

        public static void Init()
        {
            logPath = Path.Combine(Application.StartupPath, "AppLog.txt");
        }

        public static void LogMessage(string message)
        {
            StreamWriter sw = new StreamWriter(logPath, true);
            sw.WriteLine(message);
            sw.Close();
        }
    }
}

/*
 * Copyright (c) 2009 Cory Borrow
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MyPad
{
    public class ExternalApp
    {
        string[] extensions;
        string name;
        string filename;
        string path;
        string workingDir;
        string arguments;

        public string[] Extensions
        {
            get
            {
                return extensions;
            }
            set
            {
                extensions = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }

        public string AppPath
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }

        public string WorkingDir
        {
            get
            {
                return workingDir;
            }
            set
            {
                workingDir = value;
            }
        }

        public string Arguments
        {
            get
            {
                return arguments;
            }
            set
            {
                arguments = value;
            }
        }

        public ExternalApp()
        {
            this.extensions = new string[] { "" };
            this.name = "";
            this.filename = "";
            this.path = "";
            this.workingDir = "";
            this.arguments = "";
        }

        public ExternalApp(string[] extensions, string name, string fullpath, string workingDir, string arguments)
        {
            this.extensions = extensions;
            this.name = name;
            this.filename = fullpath;
            this.path = Path.GetDirectoryName(fullpath);
            this.workingDir = workingDir;
            this.arguments = arguments;
        }

        public void Execute(string activeFile)
        {
            string dir = Path.GetDirectoryName(activeFile);
            string file = Path.GetFileName(activeFile);

            arguments = arguments.Replace("%d", dir);
            arguments = arguments.Replace("%f", file);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = arguments;
            psi.FileName = filename;
            psi.WorkingDirectory = workingDir;
            psi.CreateNoWindow = true;

            Process.Start(psi);
        }

        /*public string Execute(string activeFile)
        {
            string dir = Path.GetDirectoryName(activeFile);
            string file = Path.GetFileName(activeFile);

            arguments = arguments.Replace("%d", dir);
            arguments = arguments.Replace("%f", file);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.Arguments = arguments;
            psi.FileName = filename;
            psi.WorkingDirectory = workingDir;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;

            Process p = Process.Start(psi);
            string output = p.StandardOutput.ReadToEnd();

            if (p != null)
            {
                p.Close();
            }

            return output;
        }*/
    }
}

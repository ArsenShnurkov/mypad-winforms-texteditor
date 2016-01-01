using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace MyPad
{
    public class CopyTextEventArgs : EventArgs
    {
        string text;

        public string Text { 
            get {
                return text;
            }
        }

        public CopyTextEventArgs(string text)
        {
            this.text = text;
        }
    }

    public delegate void CopyTextEventHandler(object sender, CopyTextEventArgs e);

    public class TextClipboard
    {
        // Code duplication: TextAreaClipboardHandler.cs also has SafeSetClipboard
        [ThreadStatic] static int SafeSetClipboardDataVersion;

        const string LineSelectedType = "MSDEVLineSelect";  // This is the type VS 2003 and 2005 use for flagging a whole line copy

        protected virtual void OnCopyText(CopyTextEventArgs e)
        {
            if (CopyText != null) {
                CopyText(this, e);
            }
        }

        public event CopyTextEventHandler CopyText;

        public bool CopyTextToClipboard(string stringToCopy, bool asLine)
        {
            if (stringToCopy.Length > 0) {
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.UnicodeText, true, stringToCopy);
                if (asLine) {
                    MemoryStream lineSelected = new MemoryStream(1);
                    lineSelected.WriteByte(1);
                    dataObject.SetData(LineSelectedType, false, lineSelected);
                }

                OnCopyText(new CopyTextEventArgs(stringToCopy));

                SafeSetClipboard(dataObject);
                return true;
            } else {
                return false;
            }
        }

        static void SafeSetClipboard(object dataObject)
        {
            // Work around ExternalException bug. (SD2-426)
            // Best reproducable inside Virtual PC.
            int version = unchecked(++SafeSetClipboardDataVersion);
            try {
                Clipboard.SetDataObject(dataObject, true);
            } catch (ExternalException) {
                Timer timer = new Timer();
                timer.Interval = 100;
                timer.Tick += delegate {
                    timer.Stop();
                    timer.Dispose();
                    if (SafeSetClipboardDataVersion == version) {
                        try {
                            Clipboard.SetDataObject(dataObject, true, 10, 50);
                        } catch (ExternalException) { }
                    }
                };
                timer.Start();
            }
        }    
    }
}


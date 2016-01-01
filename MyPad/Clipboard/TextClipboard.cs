using System;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using ICSharpCode.TextEditor;

namespace MyPad
{
    /// <remarks>
    /// uses Clipboard and DataObject classes from
    /// System.Windows.Forms.dll
    /// </summary>
    public class TextClipboard
    {
        public event CopyTextEventHandler CopyText;

        protected virtual void OnCopyText(CopyTextEventArgs e)
        {
            if (CopyText != null) {
                CopyText(this, e);
            }
        }

        ///<remarks>
        /// This is the type VS 2003 and 2005 use for flagging a whole line copy
        /// </remarks>
        const string LineSelectedType = "MSDEVLineSelect";  

        public bool CopyTextToClipboard(string stringToCopy, bool asLine)
        {
            if (string.IsNullOrEmpty(stringToCopy))
            {
                return false;
            }

            DataObject dataObject = new DataObject();

            dataObject.SetData(DataFormats.UnicodeText, true, stringToCopy);

            if (asLine) // additional format
            {
                MemoryStream lineSelected = new MemoryStream(1);
                lineSelected.WriteByte(1);
                dataObject.SetData(LineSelectedType, false, lineSelected);
            }

            OnCopyText(new CopyTextEventArgs(stringToCopy));

            SafeSetClipboard(dataObject);

            return true;
        }

        [ThreadStatic] static int SafeSetClipboardDataVersion;

        /// <remarks>
        /// Code duplication: TextAreaClipboardHandler.cs from ICSharpCode.TextEditor
        /// also has SafeSetClipboard
        /// </remarks>
        static void SafeSetClipboard(object dataObject)
        {
            // Work around ExternalException bug. (SD2-426)
            // Best reproducable inside Virtual PC.
            int version = unchecked(++SafeSetClipboardDataVersion);
            try
            {
                Clipboard.SetDataObject(dataObject, true);
            }
            catch (ExternalException)
            {
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

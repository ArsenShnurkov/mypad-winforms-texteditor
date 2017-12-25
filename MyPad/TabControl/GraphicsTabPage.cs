using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System;

namespace MyPad
{
    public partial class GraphicsTabPage : TabPage, IChangeableTab, ISaveableTab
    {
        public byte [] fileContent;
        public GraphicsTabPage ()
        {
            InitializeComponent ();
        }

        public bool IsSavingNecessary {
            get {
                return true;
            }

            set {
                throw new NotImplementedException ();
            }
        }

        public static Bitmap ByteToImage (byte [] blob)
        {
            Bitmap bm;
            using (MemoryStream mStream = new MemoryStream ()) {
                byte [] pData = blob;
                mStream.Write (pData, 0, Convert.ToInt32 (pData.Length));
                bm = new Bitmap (mStream, false);
            }
            return bm;
        }

        public string GetFileFullPathAndName ()
        {
            return this.ToolTipText;
        }

        public void SaveFile (string newNamePathFilenameExt)
        {
            File.WriteAllBytes (newNamePathFilenameExt, this.fileContent);
            FileInfo fi = new FileInfo (newNamePathFilenameExt);
            this.Text = fi.Name;
            this.ToolTipText = fi.FullName;
        }
    }
}

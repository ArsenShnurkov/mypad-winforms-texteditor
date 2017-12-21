using System;
using System.Windows.Forms;

namespace MyPad
{
    public static class TabControlExtensions
    {
        public static int GetUntitledTabCount (this TabControl tabControl1)
        {
            int count = 0;

            foreach (TabPage tb in tabControl1.TabPages) {
                if (tb is EditorTabPage) {
                    EditorTabPage etb = tb as EditorTabPage;
                    if (etb.HasSomethingOnDisk == false) {
                        count++;
                    }
                }
            }

            return count;
        }
    }

    interface IInitializable
    {
        void Initialize ();
    }

}


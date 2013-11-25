using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyPad
{
    public struct Margins
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    };

    public class Win32
    {
        public static bool ExtendGlass(Form form, Margins margins)
        {
            int enabled = 0;
            DwmIsCompositionEnabled(ref enabled);

            if (enabled == 1)
            {
                if (DwmExtendFrameIntoClientArea(form.Handle, ref margins) == 0)
                    return true;
            }
            return false;
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins lpMargins);

        [DllImport("dwmapi.dll")]
        private static extern int DwmIsCompositionEnabled(ref int lpEnabled);
    }
}

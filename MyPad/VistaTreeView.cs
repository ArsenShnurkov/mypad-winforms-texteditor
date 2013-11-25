using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyPad
{
    public class VistaTreeView : TreeView
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= Win32.TVS_EX_AUTOHSCROLL;

                return cp;
            }
        }

        public VistaTreeView()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            //this.HotTracking = true;
            this.ShowLines = false;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            int dwStyle = Win32.SendMessage(this.Handle, Win32.TVM_GETEXTENDEDSTYLE, 0, 0);
            dwStyle |= Win32.TVS_EX_AUTOHSCROLL;
            //dwStyle |= Win32.TVS_EX_FADEINOUTEXPANDOS;

            Win32.SendMessage(this.Handle, Win32.TVM_SETEXTENDEDSTYLE, 0, dwStyle);
            Win32.SetWindowTheme(this.Handle, "explorer", null);
        }
    }
}

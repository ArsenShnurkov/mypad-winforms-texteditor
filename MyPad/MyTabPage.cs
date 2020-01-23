using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MyPad
{
    public class MyTabPage : Panel
    {
        //public delegate void TextChangedEventHandler (object sender, EventArgs e);
        //public event TextChangedEventHandler TextChanged;

        //Rectangle bounds;
        //string text = "";
        string toolTipText = "";

        /*public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                base.OnTextChanged(EventArgs.Empty);
            }
        }*/

        public string ToolTipText {
            get {
                return toolTipText;
            }
            set {
                toolTipText = value;
            }
        }

        /*public Rectangle Bounds {
            get {
                return bounds;
            }
            internal set {
                bounds = value;
            }
        }*/

        public MyTabPage ()
            : base ()
        {
            this.BackColor = SystemColors.Control;
            base.TextChanged += new EventHandler (OnTextChanged);
        }

        protected virtual void OnTextChanged (object sender, EventArgs e)
        {

        }
    }
}

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MyPad
{
    public partial class GraphicsTabPage : TabPage
    {
        public PictureBox Image;

        private void InitializeComponent ()
        {
            this.Image = new PictureBox ();
            this.SuspendLayout ();
            this.Controls.Add (Image);
            this.AutoScroll = true;
            //...

            this.ResumeLayout ();
            this.PerformLayout ();
        }
    }
}

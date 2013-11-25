using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPad.Plugins
{
    public interface ITextPlugin : IPlugin
    {
        void KeyPressed(object sender, KeyPressEventArgs e);
    }
}

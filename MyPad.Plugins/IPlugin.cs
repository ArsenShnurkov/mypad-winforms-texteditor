using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyPad.Plugins
{
    public interface IPlugin
    {
        string Name
        {
            get;
        }

        string Description
        {
            get;
        }

        string Author
        {
            get;
        }

        string Website
        {
            get;
        }

        string Email
        {
            get;
        }

        void SetActiveWindow(Panel activeForm);
    }
}

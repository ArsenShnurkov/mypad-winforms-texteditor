using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MyPad;
using MyPad.Plugins;

namespace ExampleTextPlugin
{
    public class ExampleTextPlugin : ITextPlugin
    {
        EditorWindow activeEditor;

        public string Name
        {
            get { return "Example text plugin"; }
        }

        public string Description
        {
            get { return "An example of a text plugin"; }
        }

        public string Author
        {
            get { return "Cory Borrow"; }
        }

        public string Website
        {
            get { return "http://cborrow.wordpress.com"; }
        }

        public string Email
        {
            get { return "cborrow03@gmail.com"; }
        }

        public void SetActiveWindow(Form activeForm)
        {
            activeEditor = (EditorWindow)activeForm;
        }

        public void KeyPressed(object sender, KeyPressEventArgs e)
        {
            activeEditor.TextEditorControl.ActiveTextAreaControl.TextArea.InsertChar(e.KeyChar);
        }
    }
}

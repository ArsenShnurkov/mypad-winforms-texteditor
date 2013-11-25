using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;

using MyPad.Plugins;

namespace ExamplePlugin
{
    public class ExamplePlugin : IMenuPlugin
    {
        ToolStripMenuItem menuItem = new ToolStripMenuItem();
        Panel activeWindow;

        public string Name
        {
            get
            {
                return "Insert date";
            }
        }

        public string Description
        {
            get
            {
                return "Inserts the current date into the open document";
            }
        }

        public string Author
        {
            get
            {
                return "Cory Borrow";
            }
        }

        public string Website
        {
            get
            {
                return "http://cborrow.wordpress.com";
            }
        }

        public string Email
        {
            get
            {
                return "cborrow03@gmail.com";
            }
        }

        public ToolStripMenuItem MenuItem
        {
            get
            {
                return menuItem;
            }
        }

        public ExamplePlugin()
        {
            menuItem = new ToolStripMenuItem();
            menuItem.Text = "Insert date";
            menuItem.Click += new EventHandler(MenuItemClick);
        }

        public void SetActiveWindow(Panel activeForm)
        {
            activeWindow = activeForm;
        }

        public void MenuItemClick(object sender, EventArgs e)
        {
            if (activeWindow != null)
            {
                TextEditorControl editor = (TextEditorControl)activeWindow.Controls["textEditorControl"];
                editor.ActiveTextAreaControl.TextArea.InsertString(DateTime.Now.ToString());
                //activeWindow.TextEditorControl.ActiveTextAreaControl.TextArea.InsertString(DateTime.Now.ToString());
            }
        }
    }
}

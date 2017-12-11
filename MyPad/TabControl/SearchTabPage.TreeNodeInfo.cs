using System.Windows.Forms;

namespace MyPad
{
    public partial class SearchTabPage : TabPage
    {

        class TreeNodeInfo
        {
            public string ShortName;
            public string LongName;
            public int LineNumber;
            public TreeNodeInfo (string shortName)
            {
                this.ShortName = shortName;
                LongName = null;
                LineNumber = -1;
            }
            public TreeNodeInfo (string shortName, string longName) : this (shortName)
            {
                this.LongName = longName;
            }
            public TreeNodeInfo (string shortName, string longName, int lineNumber) : this (shortName, longName)
            {
                this.LineNumber = lineNumber;
            }
        }
    }
}

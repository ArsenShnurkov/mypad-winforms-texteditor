using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace MyPad
{
    public partial class FileBrowser : Form
    {
        public delegate void OpenFileEventHandler(string file);
        public event OpenFileEventHandler OpenFile;

        private delegate void AddTreeNodeDel(TreeNode node);

        ImageList imageList;
        Thread initThread;
        Thread loadIconsThread;

        public FileBrowser()
        {
            InitializeComponent();

            imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(16, 16);
            vistaTreeView1.StateImageList = imageList;
            vistaTreeView1.BeforeExpand += new TreeViewCancelEventHandler(treeView1_BeforeExpand);
            vistaTreeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseDoubleClick);

            this.OpenFile += new OpenFileEventHandler(OnOpenFile);

            initThread = new Thread(new ThreadStart(Init));
            initThread.Start();
        }

        public void Init()
        {
            string[] drives = Directory.GetLogicalDrives();

            foreach (string drive in drives)
            {
                if (!drive.StartsWith("A") && !drive.StartsWith("B"))
                {
                    TreeNode node = new TreeNode();
                    node.Text = drive;
                    node.ToolTipText = drive;

                    try
                    {
                        ListPath(node, drive);
                    }
                    catch (Exception ex)
                    {
                        string err = ex.Message;
                    }

                    if (InvokeRequired)
                        this.Invoke(new AddTreeNodeDel(AddTreeNode), new object[] { node });
                    else
                        vistaTreeView1.Nodes.Add(node);
                }
            }
        }

        public void AddTreeNode(TreeNode node)
        {
            vistaTreeView1.Nodes.Add(node);
        }

        public void ListPath(TreeNode parentNode, string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (DirectoryInfo d in di.GetDirectories())
            {
                if ((d.Attributes & FileAttributes.System) != FileAttributes.System && 
                    (d.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    TreeNode node = new TreeNode();
                    node.Text = d.Name;
                    node.ToolTipText = d.FullName;
                    node.Nodes.Add(" ");

                    parentNode.Nodes.Add(node);
                }
            }

            foreach (FileInfo fi in di.GetFiles())
            {
                if ((fi.Attributes & FileAttributes.System) != FileAttributes.System &&
                    (fi.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                {
                    if (IsTextFile(fi.FullName))
                    {
                        TreeNode node = new TreeNode();
                        node.Text = fi.Name;
                        node.ToolTipText = fi.FullName;

                        if (imageList.Images.ContainsKey(fi.Extension))
                            node.StateImageKey = fi.Extension;
                        else
                        {
                            Icon icon = Icon.ExtractAssociatedIcon(fi.FullName);
                            imageList.Images.Add(fi.Extension, icon);
                            node.StateImageKey = fi.Extension;
                        }

                        parentNode.Nodes.Add(node);
                    }
                }
            }

            /*Thread t = new Thread(new ParameterizedThreadStart(LoadIcons));
            t.Start(parentNode);*/
        }

        public void LoadIcons(object nodeObj)
        {
            TreeNode parentNode = (TreeNode)nodeObj;

            foreach (TreeNode node in parentNode.Nodes)
            {
                string file = node.ToolTipText;

                if (File.Exists(file))
                {
                    string ext = Path.GetExtension(file);
                    string name = Path.GetFileName(file);

                    if (ext != ".exe" && ext != ".lnk")
                    {
                        if (imageList.Images.ContainsKey(ext))
                            node.StateImageKey = ext;
                        else
                        {
                            Icon icon = Icon.ExtractAssociatedIcon(file);
                            imageList.Images.Add(ext, icon);
                            node.StateImageKey = ext;
                        }
                    }
                    else
                    {
                        if (imageList.Images.ContainsKey(name))
                            node.StateImageKey = name;
                        else
                        {
                            Icon icon = Icon.ExtractAssociatedIcon(file);
                            imageList.Images.Add(name, icon);
                            node.StateImageKey = name;
                        }
                    }
                }
            }
        }

        public bool IsTextFile(string file)
        {
            string[] textTypes = new string[] { ".txt", ".c", ".cpp", ".h", ".hpp", ".cc", ".cxx", ".html", ".css", ".xhtml", ".htm", ".php", ".php3",
                ".php4", ".php5", ".php6", ".phtml", ".pl", ".py", ".rb", ".rhtml", ".java", ".cs", ".vb", ".vbs", ".js", ".inc" };

            string ext = Path.GetExtension(file);

            if(textTypes.Contains(ext.ToLower()))
                return true;
            return false;
        }

        protected virtual void OnOpenFile(string file)
        {
            
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (initThread != null && initThread.IsAlive)
                initThread.Abort();

            base.OnClosing(e);
        }

        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode node = e.Node;

            if (node.Nodes.Count == 1 && node.Nodes[0].Text == " ")
            {
                node.Nodes.Clear();
                ListPath(node, node.ToolTipText);
            }
        }

        void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            string file = node.ToolTipText;

            if (File.Exists(file))
            {
                OpenFile(file);
            }
        }

        private void FileBrowser_Load(object sender, EventArgs e)
        {
            
        }
    }
}

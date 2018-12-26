namespace MyPad
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            var t = this.GetType ();
            var resources = new System.ComponentModel.ComponentResourceManager (t);
            System.Diagnostics.Trace.WriteLine (resources.BaseName);
            System.Diagnostics.Trace.WriteLine (resources.ResourceSetType.Name);
            var v = resources.GetObject ("toolStripButton1.Image");

            this.menuStrip1 = new System.Windows.Forms.MenuStrip ();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator ();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator ();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator ();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator ();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator ();
            this.enchanceHyperlinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.enchanceImagelinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.convertTextToHtmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeBoldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeSelectionRedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.MakeH6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.insertNbspToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator ();
            this.highlightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator ();
            this.toolStripSeparator13a = new System.Windows.Forms.ToolStripSeparator ();
            this.lineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.moveLineUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.moveLineDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator ();
            this.cutLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.copyLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator ();
            this.commentLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator ();
            this.wrapInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.showSearchTabPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.simpleFindAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.findAndReplaceRegExToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.findAndReplaceRawToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.toolbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.statusbarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip ();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel ();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel ();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel ();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip ();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator ();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator ();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator ();
            this.tabControl1 = new System.Windows.Forms.TabControl ();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog ();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog ();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator ();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator ();
            this.toolStripButtonNewDocument = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonOpenDocument = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonSaveDocument = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonSaveAll = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonCut = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonPaste = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonFind = new System.Windows.Forms.ToolStripButton ();
            this.toolStripButtonInsertImageURL = new System.Windows.Forms.ToolStripButton ();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.reloadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.atomFeedEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem ();
            this.menuStrip1.SuspendLayout ();
            this.statusStrip1.SuspendLayout ();
            this.toolStrip1.SuspendLayout ();
            this.SuspendLayout ();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.simpleFindAndReplaceToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.highlightingToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point (0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size (894, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.saveAllToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.reloadFileToolStripMenuItem,
            this.toolStripSeparator5,
            this.closeToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.recentFilesToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size (37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler (this.saveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size (183, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler (this.closeToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler (this.closeAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size (183, 6);
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size (183, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler (this.quitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator13,
            this.insertNbspToolStripMenuItem,
            this.toolStripSeparator13a,
            this.lineToolStripMenuItem,
            this.selectionToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size (39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size (161, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler (this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size (161, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler (this.selectAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator5a
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator5a";
            this.toolStripSeparator6.Size = new System.Drawing.Size (161, 6);
            // 
            // enchanceHyperlinkToolStripMenuItem
            // 
            this.enchanceHyperlinkToolStripMenuItem.Name = "enchanceHyperlinkToolStripMenuItem";
            this.enchanceHyperlinkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.enchanceHyperlinkToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.enchanceHyperlinkToolStripMenuItem.Text = "Enchance Hyperlink";
            this.enchanceHyperlinkToolStripMenuItem.Click += new System.EventHandler (this.enchanceHyperlinkToolStripMenuItem_Click);
            // 
            // enchanceImagelinkToolStripMenuItem
            // 
            this.enchanceImagelinkToolStripMenuItem.Name = "enchanceImagelinkToolStripMenuItem";
            this.enchanceImagelinkToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.enchanceImagelinkToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.enchanceImagelinkToolStripMenuItem.Text = "Enchance Imagelink";
            this.enchanceImagelinkToolStripMenuItem.Click += new System.EventHandler (this.enchanceImagelinkToolStripMenuItem_Click);
            // 
            // convertTextToHtmlToolStripMenuItem
            // 
            this.convertTextToHtmlToolStripMenuItem.Name = "convertTextToHtmlToolStripMenuItem";
            this.convertTextToHtmlToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.convertTextToHtmlToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.convertTextToHtmlToolStripMenuItem.Text = "Create &&lt;, &&gt; and &&quot;";
            this.convertTextToHtmlToolStripMenuItem.Click += new System.EventHandler (this.convertTextToHtmlToolStripMenuItem_Click);
            // 
            // MakeBoldToolStripMenuItem
            // 
            this.MakeBoldToolStripMenuItem.Name = "MakeBoldToolStripMenuItem";
            this.MakeBoldToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.MakeBoldToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeBoldToolStripMenuItem.Text = "Make Bold";
            this.MakeBoldToolStripMenuItem.Click += new System.EventHandler (this.MakeBoldToolStripMenuItem_Click);
            // 
            // MakeSelectionRedToolStripMenuItem
            // 
            this.MakeSelectionRedToolStripMenuItem.Name = "MakeSelectionRedToolStripMenuItem";
            this.MakeSelectionRedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MakeSelectionRedToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeSelectionRedToolStripMenuItem.Text = "Make Red";
            this.MakeSelectionRedToolStripMenuItem.Click += new System.EventHandler (this.MakeSelectionRedToolStripMenuItem_Click);
            // 
            // MakeBRToolStripMenuItem
            // 
            this.MakeBRToolStripMenuItem.Name = "MakeBRToolStripMenuItem";
            this.MakeBRToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Enter)));
            this.MakeBRToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeBRToolStripMenuItem.Text = "Make BR";
            this.MakeBRToolStripMenuItem.Click += new System.EventHandler (this.MakeBRToolStripMenuItem_Click);
            // 
            // MakeH1ToolStripMenuItem
            // 
            this.MakeH1ToolStripMenuItem.Name = "MakeH1ToolStripMenuItem";
            this.MakeH1ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.MakeH1ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH1ToolStripMenuItem.Text = "Make H1";
            this.MakeH1ToolStripMenuItem.Click += new System.EventHandler (this.MakeH1ToolStripMenuItem_Click);
            // 
            // MakeH2ToolStripMenuItem
            // 
            this.MakeH2ToolStripMenuItem.Name = "MakeH2ToolStripMenuItem";
            this.MakeH2ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.MakeH2ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH2ToolStripMenuItem.Text = "Make H2";
            this.MakeH2ToolStripMenuItem.Click += new System.EventHandler (this.MakeH2ToolStripMenuItem_Click);
            // 
            // MakeH3ToolStripMenuItem
            // 
            this.MakeH3ToolStripMenuItem.Name = "MakeH3ToolStripMenuItem";
            this.MakeH3ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.MakeH3ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH3ToolStripMenuItem.Text = "Make H3";
            this.MakeH3ToolStripMenuItem.Click += new System.EventHandler (this.MakeH3ToolStripMenuItem_Click);
            // 
            // MakeH4ToolStripMenuItem
            // 
            this.MakeH4ToolStripMenuItem.Name = "MakeH4ToolStripMenuItem";
            this.MakeH4ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.MakeH4ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH4ToolStripMenuItem.Text = "Make H4";
            this.MakeH4ToolStripMenuItem.Click += new System.EventHandler (this.MakeH4ToolStripMenuItem_Click);
            // 
            // MakeH5ToolStripMenuItem
            // 
            this.MakeH5ToolStripMenuItem.Name = "MakeH5ToolStripMenuItem";
            this.MakeH5ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.MakeH5ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH5ToolStripMenuItem.Text = "Make H5";
            this.MakeH5ToolStripMenuItem.Click += new System.EventHandler (this.MakeH5ToolStripMenuItem_Click);
            // 
            // MakeH6ToolStripMenuItem
            // 
            this.MakeH6ToolStripMenuItem.Name = "MakeH6ToolStripMenuItem";
            this.MakeH6ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
            this.MakeH6ToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.MakeH6ToolStripMenuItem.Text = "Make H6";
            this.MakeH6ToolStripMenuItem.Click += new System.EventHandler (this.MakeH6ToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size (161, 6);
            // 
            // highlightingToolStripMenuItem
            // 
            this.highlightingToolStripMenuItem.Name = "highlightingToolStripMenuItem";
            this.highlightingToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.highlightingToolStripMenuItem.Text = "Highlighting";
            // 
            // insertNbspToolStripMenuItem
            // 
            this.insertNbspToolStripMenuItem.Name = "insertNbspToolStripMenuItem";
            this.insertNbspToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.insertNbspToolStripMenuItem.Text = "Insert &&nbsp;";
            this.insertNbspToolStripMenuItem.Click += new System.EventHandler (this.insertNbspToolStripMenuItem_Click);
            this.insertNbspToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control)
                | System.Windows.Forms.Keys.Space)));
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size (161, 6);
            // 
            // 
            // toolStripSeparator13a
            // 
            this.toolStripSeparator13a.Name = "toolStripSeparator13a";
            this.toolStripSeparator13a.Size = new System.Drawing.Size (161, 6);
            // 
            // lineToolStripMenuItem
            // 
            this.lineToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.moveLineUpToolStripMenuItem,
            this.moveLineDownToolStripMenuItem,
            this.toolStripSeparator7,
            this.cutLineToolStripMenuItem,
            this.copyLineToolStripMenuItem,
            this.toolStripSeparator9,
            this.commentLineToolStripMenuItem});
            this.lineToolStripMenuItem.Name = "lineToolStripMenuItem";
            this.lineToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.lineToolStripMenuItem.Text = "Line";
            // 
            // moveLineUpToolStripMenuItem
            // 
            this.moveLineUpToolStripMenuItem.Name = "moveLineUpToolStripMenuItem";
            this.moveLineUpToolStripMenuItem.Size = new System.Drawing.Size (163, 22);
            this.moveLineUpToolStripMenuItem.Text = "Move Line Up";
            this.moveLineUpToolStripMenuItem.Click += new System.EventHandler (this.moveLineUpToolStripMenuItem_Click);
            // 
            // moveLineDownToolStripMenuItem
            // 
            this.moveLineDownToolStripMenuItem.Name = "moveLineDownToolStripMenuItem";
            this.moveLineDownToolStripMenuItem.Size = new System.Drawing.Size (163, 22);
            this.moveLineDownToolStripMenuItem.Text = "Move Line Down";
            this.moveLineDownToolStripMenuItem.Click += new System.EventHandler (this.moveLineDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size (160, 6);
            // 
            // cutLineToolStripMenuItem
            // 
            this.cutLineToolStripMenuItem.Name = "cutLineToolStripMenuItem";
            this.cutLineToolStripMenuItem.Size = new System.Drawing.Size (163, 22);
            this.cutLineToolStripMenuItem.Text = "Cut Line";
            this.cutLineToolStripMenuItem.Click += new System.EventHandler (this.cutLineToolStripMenuItem_Click);
            // 
            // copyLineToolStripMenuItem
            // 
            this.copyLineToolStripMenuItem.Name = "copyLineToolStripMenuItem";
            this.copyLineToolStripMenuItem.Size = new System.Drawing.Size (163, 22);
            this.copyLineToolStripMenuItem.Text = "Copy Line";
            this.copyLineToolStripMenuItem.Click += new System.EventHandler (this.copyLineToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size (160, 6);
            // 
            // commentLineToolStripMenuItem
            // 
            this.commentLineToolStripMenuItem.Name = "commentLineToolStripMenuItem";
            this.commentLineToolStripMenuItem.Size = new System.Drawing.Size (163, 22);
            this.commentLineToolStripMenuItem.Text = "Comment Line";
            this.commentLineToolStripMenuItem.Click += new System.EventHandler (this.commentLineToolStripMenuItem_Click);
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.clearSelectionToolStripMenuItem,
            this.toolStripSeparator8,
            this.enchanceHyperlinkToolStripMenuItem,
            this.enchanceImagelinkToolStripMenuItem,
            this.convertTextToHtmlToolStripMenuItem,
            this.MakeBoldToolStripMenuItem,
            this.MakeSelectionRedToolStripMenuItem,
            this.MakeBRToolStripMenuItem,
            this.MakeH1ToolStripMenuItem,
            this.MakeH2ToolStripMenuItem,
            this.MakeH3ToolStripMenuItem,
            this.MakeH4ToolStripMenuItem,
            this.MakeH5ToolStripMenuItem,
            this.MakeH6ToolStripMenuItem,
            this.wrapInToolStripMenuItem
            });
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.selectionToolStripMenuItem.Text = "Selection";
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size (152, 22);
            this.clearSelectionToolStripMenuItem.Text = "Clear Selection";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler (this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size (149, 6);
            // 
            // wrapInToolStripMenuItem
            // 
            this.wrapInToolStripMenuItem.Name = "wrapInToolStripMenuItem";
            this.wrapInToolStripMenuItem.Size = new System.Drawing.Size (152, 22);
            this.wrapInToolStripMenuItem.Text = "Wrap in /* */";
            this.wrapInToolStripMenuItem.Click += new System.EventHandler (this.wrapInToolStripMenuItem_Click);
            // 
            // searchInFilesToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
                this.showSearchTabPageToolStripMenuItem,
                this.atomFeedEditorToolStripMenuItem,
            });
            this.toolsToolStripMenuItem.Name = nameof (this.toolsToolStripMenuItem);
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size (54, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // showSearchTabPageToolStripMenuItem
            // 
            this.showSearchTabPageToolStripMenuItem.Name = "showSearchTabPageToolStripMenuItem";
            this.showSearchTabPageToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F4;
            this.showSearchTabPageToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.showSearchTabPageToolStripMenuItem.Text = "Show search tool";
            this.showSearchTabPageToolStripMenuItem.Click += new System.EventHandler (this.showSearchTabPageToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.simpleFindAndReplaceToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
                this.findToolStripMenuItem,
                this.findNextToolStripMenuItem,
                this.findAndReplaceRegExToolStripMenuItem,
                this.findAndReplaceRawToolStripMenuItem,
            });
            this.simpleFindAndReplaceToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.simpleFindAndReplaceToolStripMenuItem.Size = new System.Drawing.Size (54, 20);
            this.simpleFindAndReplaceToolStripMenuItem.Text = "Find/Replace";
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.findNextToolStripMenuItem.Text = "Find Next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler (this.findNextToolStripMenuItem_Click);
            // 
            // findAndReplaceRegExToolStripMenuItem
            // 
            this.findAndReplaceRegExToolStripMenuItem.Name = "findAndReplaceRegExToolStripMenuItem";
            this.findAndReplaceRegExToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.F)));
            this.findAndReplaceRegExToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.findAndReplaceRegExToolStripMenuItem.Text = "Find and Replace RegEx";
            this.findAndReplaceRegExToolStripMenuItem.Click += new System.EventHandler (this.findAndReplaceRegExToolStripMenuItem_Click);
            // 
            // findAndReplaceRawToolStripMenuItem
            // 
            this.findAndReplaceRawToolStripMenuItem.Name = "findAndReplaceRawToolStripMenuItem";
            this.findAndReplaceRawToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control)
                | System.Windows.Forms.Keys.H)));
            this.findAndReplaceRawToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.findAndReplaceRawToolStripMenuItem.Text = "Find and Replace raw";
            this.findAndReplaceRawToolStripMenuItem.Click += new System.EventHandler (this.findAndReplaceRawToolStripMenuItem_Click);
            // 
            // toolbarToolStripMenuItem
            // 
            this.toolbarToolStripMenuItem.Checked = true;
            this.toolbarToolStripMenuItem.CheckOnClick = true;
            this.toolbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolbarToolStripMenuItem.Name = "toolbarToolStripMenuItem";
            this.toolbarToolStripMenuItem.Size = new System.Drawing.Size (123, 22);
            this.toolbarToolStripMenuItem.Text = "Toolbar";
            this.toolbarToolStripMenuItem.Click += new System.EventHandler (this.toolbarToolStripMenuItem_Click);
            // 
            // statusbarToolStripMenuItem
            // 
            this.statusbarToolStripMenuItem.Checked = true;
            this.statusbarToolStripMenuItem.CheckOnClick = true;
            this.statusbarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusbarToolStripMenuItem.Name = "statusbarToolStripMenuItem";
            this.statusbarToolStripMenuItem.Size = new System.Drawing.Size (123, 22);
            this.statusbarToolStripMenuItem.Text = "Statusbar";
            this.statusbarToolStripMenuItem.Click += new System.EventHandler (this.statusbarToolStripMenuItem_Click);

            this.optionsToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
                this.settingsToolStripMenuItem,
                this.toolStripSeparator6,
                this.toolbarToolStripMenuItem,
                this.statusbarToolStripMenuItem,
                });
            this.optionsToolStripMenuItem.Name = nameof (this.optionsToolStripMenuItem);
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size (48, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.helpToolStripMenuItem1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size (44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size (107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler (this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange (new System.Windows.Forms.ToolStripItem [] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point (0, 543);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size (894, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size (719, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size (80, 17);
            this.toolStripStatusLabel2.Text = "Line: 0";
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.AutoSize = false;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size (80, 17);
            this.toolStripStatusLabel3.Text = "Col: 0";
            this.toolStripStatusLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange (new System.Windows.Forms.ToolStripItem [] {
                                this.toolStripButtonNewDocument,
                                this.toolStripButtonOpenDocument,
                                this.toolStripButtonSaveDocument,
                                this.toolStripButtonSaveAll,
                                this.toolStripSeparator10,
                                this.toolStripButtonUndo,
                                this.toolStripButtonRedo,
                                this.toolStripSeparator11,
                                this.toolStripButtonCut,
                                this.toolStripButtonCopy,
                                this.toolStripButtonPaste,
                                this.toolStripSeparator12,
                                this.toolStripButtonFind,
                                this.toolStripSeparator15,
                                this.toolStripButtonInsertImageURL,
            });
            this.toolStrip1.Location = new System.Drawing.Point (0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size (894, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size (6, 25);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size (6, 25);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size (6, 25);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point (0, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size (894, 494);
            this.tabControl1.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";

            this.toolStripSeparator14.Name = nameof (this.toolStripSeparator14);
            this.toolStripSeparator14.Size = new System.Drawing.Size (151, 6);

            this.toolStripSeparator15.Name = nameof (this.toolStripSeparator15);
            this.toolStripSeparator15.Size = new System.Drawing.Size (151, 6);
            // 
            // toolStripButton1
            // 
            this.toolStripButtonNewDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNewDocument.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton1.Image")));
            this.toolStripButtonNewDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNewDocument.Name = nameof (this.toolStripButtonNewDocument);
            this.toolStripButtonNewDocument.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonNewDocument.Text = "New Document";
            this.toolStripButtonNewDocument.Click += new System.EventHandler (this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButtonOpenDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenDocument.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton2.Image")));
            this.toolStripButtonOpenDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenDocument.Name = nameof (this.toolStripButtonOpenDocument); ;
            this.toolStripButtonOpenDocument.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonOpenDocument.Text = "Open Document";
            this.toolStripButtonOpenDocument.Click += new System.EventHandler (this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButtonSaveDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveDocument.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton3.Image")));
            this.toolStripButtonSaveDocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveDocument.Name = nameof (this.toolStripButtonSaveDocument);
            this.toolStripButtonSaveDocument.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonSaveDocument.Text = "Save Document";
            this.toolStripButtonSaveDocument.Click += new System.EventHandler (this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButtonSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAll.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton4.Image")));
            this.toolStripButtonSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAll.Name = nameof (this.toolStripButtonSaveAll);
            this.toolStripButtonSaveAll.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonSaveAll.Text = "Save All";
            this.toolStripButtonSaveAll.Click += new System.EventHandler (this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton5.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = nameof (this.toolStripButtonUndo);
            this.toolStripButtonUndo.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonUndo.Text = "Undo";
            this.toolStripButtonUndo.Click += new System.EventHandler (this.toolStripButton5_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton6.Image")));
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = nameof (toolStripButtonRedo);
            this.toolStripButtonRedo.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonRedo.Text = "Redo";
            this.toolStripButtonRedo.Click += new System.EventHandler (this.toolStripButton6_Click);
            // 
            // toolStripButton7
            // 
            this.toolStripButtonCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCut.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton7.Image")));
            this.toolStripButtonCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCut.Name = nameof (this.toolStripButtonCut);
            this.toolStripButtonCut.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonCut.Text = "Cut";
            this.toolStripButtonCut.Click += new System.EventHandler (this.toolStripButton7_Click);
            // 
            // toolStripButton8
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton8.Image")));
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = nameof (toolStripButtonCopy);
            this.toolStripButtonCopy.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonCopy.Text = "Copy";
            this.toolStripButtonCopy.Click += new System.EventHandler (this.toolStripButton8_Click);

            this.toolStripButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPaste.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton9.Image")));
            this.toolStripButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPaste.Name = nameof (toolStripButtonPaste);
            this.toolStripButtonPaste.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonPaste.Text = "Paste";
            this.toolStripButtonPaste.Click += new System.EventHandler (this.toolStripButton9_Click);

            this.toolStripButtonFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFind.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButton10.Image")));
            this.toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFind.Name = nameof (this.toolStripButtonFind);
            this.toolStripButtonFind.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonFind.Text = "Find";
            this.toolStripButtonFind.Click += new System.EventHandler (this.toolStripButton10_Click);

            this.toolStripButtonInsertImageURL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonInsertImageURL.Image = ((System.Drawing.Image)(resources.GetObject ("toolStripButtonInsertImageURL.Image")));
            this.toolStripButtonInsertImageURL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonInsertImageURL.Name = nameof (this.toolStripButtonFind);
            this.toolStripButtonInsertImageURL.Size = new System.Drawing.Size (23, 22);
            this.toolStripButtonInsertImageURL.Text = "Image";
            this.toolStripButtonInsertImageURL.Click += new System.EventHandler (this.toolStripButtonInsertImageURL_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.Name = nameof (this.newToolStripMenuItem);
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler (this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler (this.openToolStripMenuItem_Click);
            // 
            // reloadFileToolStripMenuItem
            // 
            this.reloadFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("reloadFileToolStripMenuItem.Image")));
            this.reloadFileToolStripMenuItem.Name = "reloadFileToolStripMenuItem";
            this.reloadFileToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.reloadFileToolStripMenuItem.Text = "Reload File";
            this.reloadFileToolStripMenuItem.Click += new System.EventHandler (this.reloadFileToolStripMenuItem_Click);
            // 
            // saveAllToolStripMenuItem
            // 
            this.saveAllToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("saveAllToolStripMenuItem.Image")));
            this.saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
            this.saveAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.S)));
            this.saveAllToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.saveAllToolStripMenuItem.Text = "Save All";
            this.saveAllToolStripMenuItem.Click += new System.EventHandler (this.saveAllToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size (186, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler (this.saveToolStripMenuItem_Click);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("undoToolStripMenuItem.Image")));
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler (this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("redoToolStripMenuItem.Image")));
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler (this.redoToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler (this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler (this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size (164, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler (this.pasteToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("findToolStripMenuItem.Image")));
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size (236, 22);
            this.findToolStripMenuItem.Text = "Find";
            this.findToolStripMenuItem.Click += new System.EventHandler (this.findToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject ("optionsToolStripMenuItem.Image")));
            this.settingsToolStripMenuItem.Name = nameof (this.settingsToolStripMenuItem);
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size (116, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler (this.settingsToolStripMenuItem_Click);
            // 
            // atomFeedEditorToolStripMenuItem
            // 
            this.atomFeedEditorToolStripMenuItem.Image = AtomFeedIcon.AtomFeedEditorToolStripMenuItem_16x16_Image;
            this.atomFeedEditorToolStripMenuItem.Name = "atomFeedEditorToolStripMenuItem";
            this.atomFeedEditorToolStripMenuItem.Size = new System.Drawing.Size (116, 22);
            this.atomFeedEditorToolStripMenuItem.Text = "ATOM feed editor";
            this.atomFeedEditorToolStripMenuItem.Click += new System.EventHandler (this.atomFeedEditorToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Enabled = false;
            this.helpToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject ("helpToolStripMenuItem1.Image")));
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size (107, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler (this.helpToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size (894, 565);
            this.Controls.Add (this.tabControl1);
            this.Controls.Add (this.toolStrip1);
            this.Controls.Add (this.statusStrip1);
            this.Controls.Add (this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject ("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MyPad";
            this.menuStrip1.ResumeLayout (false);
            this.menuStrip1.PerformLayout ();
            this.statusStrip1.ResumeLayout (false);
            this.statusStrip1.PerformLayout ();
            this.toolStrip1.ResumeLayout (false);
            this.toolStrip1.PerformLayout ();
            this.ResumeLayout (false);
            this.PerformLayout ();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enchanceHyperlinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enchanceImagelinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertTextToHtmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeBoldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeSelectionRedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeH6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSearchTabPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simpleFindAndReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndReplaceRegExToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndReplaceRawToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atomFeedEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem lineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveLineUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveLineDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem cutLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem wrapInToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem commentLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonNewDocument;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenDocument;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveDocument;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton toolStripButtonCut;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonPaste;
        private System.Windows.Forms.ToolStripButton toolStripButtonFind;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripButton toolStripButtonInsertImageURL;
        private System.Windows.Forms.ToolStripMenuItem toolbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusbarToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem highlightingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertNbspToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13a;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    }
}


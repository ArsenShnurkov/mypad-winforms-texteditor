namespace MyPad.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows.Forms;

    partial class OptionsDialog
    {
        protected TabPage tabPage3;
        protected Label labelSearchDirectory;
        protected TextBox textBoxSearchDirectory;
        protected Label labelIndexLocationDirectory;
        protected TextBox textBoxIndexLocationDirectory;
        protected Button buttonReindex;
        protected Label currentFilesCount;

        const string IndexFileName = "default-index.dat";
        const string SearchIndexDirectory = "SearchIndexDirectory";

        FileIndexerBackgroundWorker worker;

        protected void Initialize_SearchSettingsTab ()
        {
            worker = new FileIndexerBackgroundWorker ();
            worker.ProgressChanged += this.backgroundWorker1_OnProgressChanged;
            worker.RunWorkerCompleted += this.backgroundWorker1_OnRunWorkerCompleted;

            this.tabPage3 = new TabPage ();
            this.labelSearchDirectory = new Label ();
            this.textBoxSearchDirectory = new TextBox ();
            this.labelIndexLocationDirectory = new Label ();
            this.textBoxIndexLocationDirectory = new TextBox ();
            this.buttonReindex = new Button ();
            this.currentFilesCount = new Label ();

            int leftHint = 6;
            int widthHint = this.tabPage1.Width - 2 * leftHint;
            int verticalHint = 20;
            int verticalSpacing = 10;
            Control currentControl;
            // 
            // labelSearchDirectory
            // 
            currentControl = this.labelSearchDirectory;
            this.labelSearchDirectory.Text = "Директория поиска:";
            this.labelSearchDirectory.AutoSize = true;
            this.labelSearchDirectory.TabIndex = 1;
            this.labelSearchDirectory.Name = nameof (this.labelSearchDirectory);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);
            verticalHint += currentControl.Height + verticalSpacing;
            // 
            // textBoxSearchDirectory
            // 
            currentControl = this.textBoxSearchDirectory;
            this.textBoxSearchDirectory.TabIndex = 2;
            this.textBoxSearchDirectory.Name = nameof (this.textBoxSearchDirectory);
            this.textBoxSearchDirectory.Size = new System.Drawing.Size (
                    widthHint,
                    currentControl.Height);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);
            this.textBoxSearchDirectory.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            verticalHint += currentControl.Height + verticalSpacing;
            // 
            // labelIndexLocationDirectory
            // 
            currentControl = this.labelIndexLocationDirectory;
            this.labelIndexLocationDirectory.Text = "Директория хранения индекса:";
            this.labelIndexLocationDirectory.AutoSize = true;
            this.labelIndexLocationDirectory.TabIndex = 3;
            this.labelIndexLocationDirectory.Name = nameof (this.labelSearchDirectory);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);

            verticalHint += currentControl.Height + verticalSpacing;
            // 
            // textBoxIndexLocationDirectory
            // 
            currentControl = this.textBoxIndexLocationDirectory;
            this.textBoxIndexLocationDirectory.TabIndex = 1;
            this.textBoxIndexLocationDirectory.Name = nameof (this.textBoxSearchDirectory);
            this.textBoxIndexLocationDirectory.Size = new System.Drawing.Size (
                    widthHint,
                    currentControl.Height);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);
            this.textBoxIndexLocationDirectory.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            verticalHint += currentControl.Height + verticalSpacing;

            currentControl = this.buttonReindex;
            this.buttonReindex.Text = "Пересоздать индекс";
            this.buttonReindex.AutoSize = true;
            this.buttonReindex.Click += new System.EventHandler (this.buttonReindex_Click);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);
            this.buttonReindex.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            verticalHint += currentControl.Height + verticalSpacing;
            // 
            // currentFilesCount
            // 
            currentControl = this.currentFilesCount;
            this.currentFilesCount.Text = "0";
            this.currentFilesCount.AutoSize = true;
            this.currentFilesCount.TabIndex = 3;
            this.currentFilesCount.Name = nameof (this.currentFilesCount);
            currentControl.Location = new System.Drawing.Point (leftHint, verticalHint);

            verticalHint += currentControl.Height + verticalSpacing;
            // 
            // tabPage3
            // 
            tabPage3.Text = "Директория поиска";
            tabPage3.Controls.Add (labelSearchDirectory);
            tabPage3.Controls.Add (textBoxSearchDirectory);
            tabPage3.Controls.Add (labelIndexLocationDirectory);
            tabPage3.Controls.Add (textBoxIndexLocationDirectory);
            tabPage3.Controls.Add (buttonReindex);
            tabPage3.Controls.Add (currentFilesCount);
            tabPage3.Location = new System.Drawing.Point (4, 22);
            tabPage3.Name = nameof (tabPage3);
            tabPage3.Padding = new Padding (3);
            tabPage3.Size = new System.Drawing.Size (505, 297);
            tabPage3.TabIndex = 0;
            tabPage3.UseVisualStyleBackColor = true;
            tabControl1.Controls.Add (tabPage3);
        }

        protected void LoadIndexSettings ()
        {
            var dirname = cfg.AppSettings.Settings [SearchIndexDirectory]?.Value;
            if (Globals.IsLinux) {
                if (string.IsNullOrEmpty (dirname)) {
                    // Get directory from XDG guidelines
                    dirname = System.Environment.GetEnvironmentVariable ("XDG_CACHE_HOME");
                    // Calculate default value
                    if (string.IsNullOrEmpty (dirname)) {
                        dirname = Path.Combine ("~/.cache/", Globals.AssemblyProduct);
                    }
                }
                // create if not exist
                if (dirname.StartsWith ("~", StringComparison.InvariantCulture)) {
                    var homeDir = System.Environment.GetFolderPath (System.Environment.SpecialFolder.UserProfile);
                    dirname = Path.Combine (homeDir, dirname.Substring (2));
                }
                if (Directory.Exists (dirname) == false) {
                    if (Directory.Exists (dirname) == false) {
                        Directory.CreateDirectory (dirname);
                    }

                    PermissionSet permissionSet = new PermissionSet (PermissionState.None);

                    FileIOPermission writePermission = new FileIOPermission (FileIOPermissionAccess.Write, dirname);
                    FileIOPermission discoveryPermission = new FileIOPermission (FileIOPermissionAccess.PathDiscovery, dirname);
                    permissionSet.AddPermission (writePermission);
                    permissionSet.AddPermission (discoveryPermission);
                    if (permissionSet.IsSubsetOf (AppDomain.CurrentDomain.PermissionSet)) {
                        // You have create/write permissions
                    } else {
                        // You don't have permissions
                        throw new InvalidOperationException ("Directory is readonly");
                    }
                    // create an empty file to test access rights
                    //var filename = Path.Combine (dirname, IndexFileName);
                    //File.Create (filename).Dispose ();
                }
            }
            this.textBoxIndexLocationDirectory.Text = dirname;
            if (Globals.allFiles.Files.Count == 0) {
                Globals.LoadIndexFromCSV (dirname);
                this.currentFilesCount.Text = Globals.allFiles.Files.Count.ToString ();
            }
        }
        protected void SaveIndexSettings ()
        {
            var dirname = Path.Combine (this.textBoxIndexLocationDirectory.Text, "");
            cfg.AppSettings.Settings.Add (SearchIndexDirectory, dirname);
            if (Globals.allFiles.Files.Count == 0) {
                Globals.SaveIndexToCSV (dirname);
            }
        }

        private void buttonReindex_Click (object sender, EventArgs e)
        {
            if (worker.IsBusy) {
                // sends a message to the worker thread that work should be cancelled
                // via BackgroundWorker.CancellationPending
                worker.CancelAsync ();
                while (worker.IsBusy) {
                    Application.DoEvents ();
                }
                return;
            }
            var SearchDirectory = Globals.LoadConfiguration ().AppSettings.Settings ["SearchDirectory"]?.Value;
            worker.RunWorkerAsync (new string [] { SearchDirectory, ".htm" });
        }
        void backgroundWorker1_OnProgressChanged (object sender, ProgressChangedEventArgs e)
        {
            this.currentFilesCount.Text = string.Format ("{0}", ((int)e.UserState));
        }

        void backgroundWorker1_OnRunWorkerCompleted (object sender, RunWorkerCompletedEventArgs e)
        {
            try {
                var dirname = cfg.AppSettings.Settings [SearchIndexDirectory]?.Value;
                Globals.SaveIndexToCSV (dirname);
                string text;
                if (e.Cancelled) {
                    text = this.currentFilesCount.Text + " - Cancelled";
                } else {
                    text = this.currentFilesCount.Text + " - Finished";
                }
                this.currentFilesCount.Text = text;
            } catch (Exception ex) {
                this.currentFilesCount.Text = ex.ToString ();
            }
            this.currentFilesCount.AutoSize = true;
        }
    }
}

namespace MyPad.Dialogs
{
    using System.Windows.Forms;
    using System.Drawing;

    partial class InsertImageDialog
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

        private System.Windows.Forms.Label labelURL;
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Label labelFileName;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelALT;
        private System.Windows.Forms.TextBox textBoxALT;
        private System.Windows.Forms.Button buttonOK;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent ()
        {
            this.labelURL = new System.Windows.Forms.Label ();
            this.textBoxURL = new System.Windows.Forms.TextBox ();
            this.labelFileName = new System.Windows.Forms.Label ();
            this.textBoxFileName = new System.Windows.Forms.TextBox ();
            this.labelALT = new System.Windows.Forms.Label ();
            this.textBoxALT = new System.Windows.Forms.TextBox ();
            this.buttonOK = new System.Windows.Forms.Button ();
            this.SuspendLayout ();
            this.Controls.Add (this.labelURL);
            this.Controls.Add (this.textBoxURL);
            this.Controls.Add (this.labelFileName);
            this.Controls.Add (this.textBoxFileName);
            this.Controls.Add (this.labelALT);
            this.Controls.Add (this.textBoxALT);
            this.Controls.Add (this.buttonOK);

            this.labelURL.Text = "Интернет-адрес изображения:";
            this.labelURL.Name = nameof (this.labelURL);
            this.labelURL.Location = new System.Drawing.Point (10, 10);
            this.labelURL.AutoSize = true;
            this.labelURL.TabIndex = 1;

            this.textBoxURL.Location = new System.Drawing.Point (10, this.labelURL.Bottom + 10);
            this.textBoxURL.Name = nameof (this.textBoxURL);
            this.textBoxURL.Size = new System.Drawing.Size (this.ClientSize.Width - 20, 20);
            this.textBoxURL.TabIndex = 2;
            this.textBoxURL.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.textBoxURL.TextChanged += textBoxURL_TextChanged;

            this.labelFileName.Text = "Новое имя файла:";
            this.labelFileName.Name = nameof (this.labelFileName);
            this.labelFileName.Location = new System.Drawing.Point (10, this.textBoxURL.Bottom + 10);
            this.labelFileName.AutoSize = true;
            this.labelFileName.TabIndex = 3;

            this.textBoxFileName.Location = new System.Drawing.Point (10, this.labelFileName.Bottom + 10);
            this.textBoxFileName.Name = nameof (this.textBoxFileName);
            this.textBoxFileName.Size = new System.Drawing.Size (this.ClientSize.Width - 20, 20);
            this.textBoxFileName.TabIndex = 4;
            this.textBoxFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.labelALT.Text = "Описание файла:";
            this.labelALT.Name = nameof (this.labelALT);
            this.labelALT.Location = new System.Drawing.Point (10, this.textBoxFileName.Bottom + 10);
            this.labelALT.AutoSize = true;
            this.labelALT.TabIndex = 5;

            this.textBoxALT.Location = new System.Drawing.Point (10, this.labelALT.Bottom + 10);
            this.textBoxALT.Name = nameof (this.textBoxALT);
            this.textBoxALT.Size = new System.Drawing.Size (this.ClientSize.Width - 20, 20);
            this.textBoxALT.TabIndex = 6;
            this.textBoxALT.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Name = nameof (this.buttonOK);
            this.buttonOK.Text = "Вперёд!";
            this.buttonOK.Size = new System.Drawing.Size (75, 23);
            this.buttonOK.Location = new System.Drawing.Point ((this.ClientSize.Width - this.buttonOK.Width) / 2, this.textBoxALT.Bottom + 10);
            this.buttonOK.TabIndex = 7;
            this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.AcceptButton = this.buttonOK;
            this.buttonOK.Click += new System.EventHandler (this.buttonOK_Click);

            this.ClientSize = new Size (this.ClientSize.Width, this.buttonOK.Bottom + 10);
            this.ResumeLayout (false);
            this.PerformLayout ();
        }

        #endregion
    }
}
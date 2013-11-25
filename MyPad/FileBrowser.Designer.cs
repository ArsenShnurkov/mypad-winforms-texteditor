namespace MyPad
{
    partial class FileBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.vistaTreeView1 = new MyPad.VistaTreeView();
            this.SuspendLayout();
            // 
            // vistaTreeView1
            // 
            this.vistaTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vistaTreeView1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vistaTreeView1.Location = new System.Drawing.Point(0, 0);
            this.vistaTreeView1.Name = "vistaTreeView1";
            this.vistaTreeView1.ShowLines = false;
            this.vistaTreeView1.Size = new System.Drawing.Size(234, 405);
            this.vistaTreeView1.TabIndex = 0;
            // 
            // FileBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 405);
            this.Controls.Add(this.vistaTreeView1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FileBrowser";
            this.Text = "FileBrowser";
            this.Load += new System.EventHandler(this.FileBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private VistaTreeView vistaTreeView1;


    }
}
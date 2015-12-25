namespace MyPad {
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    partial class EntryCreateOrEditDialog : Form {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        Panel panel1;
        Label urlLabel;
        TextBox urlText;
        Label titleLabel;
        TextBox titleText;
        Label msgLabel;
        TextBox msgText;
        Button closeButton;
        Button saveButton;

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
            var resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
            Debug.Assert (resources != null);
            this.SuspendLayout();

            panel1 = new Panel (); // it is neccessary for size computations
            urlLabel = new Label ();
            urlText = new TextBox ();
            titleLabel = new Label ();
            titleText = new TextBox ();
            msgLabel = new Label();
            msgText = new TextBox ();
            closeButton = new Button ();
            saveButton = new Button ();

            panel1.Height = 1000;
            panel1.Width = 1000;
            panel1.Controls.Add (urlLabel);
            panel1.Controls.Add (urlText);
            panel1.Controls.Add (titleLabel);
            panel1.Controls.Add (titleText);
            panel1.Controls.Add (msgLabel);
            panel1.Controls.Add (msgText);
            panel1.Controls.Add (closeButton);
            panel1.Controls.Add (saveButton);

            panel1.Dock = DockStyle.Fill;

            int formSpacing = 6;

            saveButton.Text = "Save";
            saveButton.Height = 20;
            saveButton.Width = 80;
            saveButton.Top = saveButton.Parent.Height - saveButton.Height - formSpacing;
            saveButton.Left = saveButton.Parent.Width - saveButton.Width - formSpacing;
            saveButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            saveButton.DialogResult = DialogResult.OK;
            this.AcceptButton = saveButton;

            closeButton.Text = "Close";
            closeButton.Height = 20;
            closeButton.Width = 80;
            closeButton.Top = closeButton.Parent.Height - closeButton.Height - formSpacing;
            closeButton.Left = closeButton.Parent.Width - closeButton.Width - saveButton.Width - 2 * formSpacing;
            closeButton.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            closeButton.DialogResult = DialogResult.Cancel;
            this.CancelButton = closeButton;

            urlLabel.Text = "Url:";
            urlLabel.Left = formSpacing;
            urlLabel.AutoSize = true;
            urlLabel.Top = formSpacing;
            urlLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            urlText.Left = formSpacing;
            urlText.Top = urlLabel.Bottom + formSpacing;
            urlText.Height = 20;
            urlText.Width = urlText.Parent.Width - 2 * formSpacing;
            urlText.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            titleLabel.Text = "Title:";
            titleLabel.Left = formSpacing;
            titleLabel.AutoSize = true;
            titleLabel.Top = urlText.Bottom + formSpacing;
            titleLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            titleText.Left = formSpacing;
            titleText.Top = titleLabel.Bottom + formSpacing;
            titleText.Height = 20;
            titleText.Width = titleText.Parent.Width - 2 * formSpacing;
            titleText.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

            msgLabel.Text = "Brief annotation";
            msgLabel.Left = formSpacing;
            msgLabel.AutoSize = true;
            msgLabel.Top = titleText.Bottom + formSpacing;
            msgLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

            msgText.Top = msgLabel.Bottom + formSpacing;
            msgText.Left = formSpacing;
            msgText.Multiline = true;
            msgText.Height = closeButton.Top - msgLabel.Bottom - 2 * formSpacing;
            msgText.Width = urlText.Width;
            msgText.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            this.Controls.Add (panel1);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "EntryCreateOrEditDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SingleEntry Dialog";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}

namespace MyPad.Dialogs
{
    using System.Windows.Forms;

    partial class OptionsDialog
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
            this.tabPage1 = new TabPage ();
            this.labelAtomFeedFileLocation = new Label ();
            this.textBoxAtomFeedFileLocation = new TextBox ();

            this.label1 = new Label ();
            this.label2 = new Label ();

            this.panel1 = new Panel ();
            this.button2 = new Button ();
            this.button1 = new Button ();
            this.tabControl1 = new TabControl ();
            this.tabPage2 = new TabPage ();
            this.groupBox2 = new GroupBox ();
            this.comboBox2 = new ComboBox ();
            this.comboBox1 = new ComboBox ();
            this.groupBox1 = new GroupBox ();
            this.checkBox10 = new CheckBox ();
            this.checkBox9 = new CheckBox ();
            this.checkBox8 = new CheckBox ();
            this.checkBox7 = new CheckBox ();
            this.checkBox6 = new CheckBox ();
            this.checkBox5 = new CheckBox ();
            this.checkBox4 = new CheckBox ();
            this.checkBox3 = new CheckBox ();
            this.checkBox2 = new CheckBox ();
            this.checkBox1 = new CheckBox ();
            this.checkBox11 = new CheckBox ();
            this.groupBox3 = new GroupBox ();
            this.checkBox12 = new CheckBox ();
            this.panel1.SuspendLayout ();
            this.tabControl1.SuspendLayout ();
            this.tabPage2.SuspendLayout ();
            this.groupBox2.SuspendLayout ();
            this.groupBox1.SuspendLayout ();
            this.groupBox3.SuspendLayout ();
            this.SuspendLayout ();

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point (6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size (35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point (142, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size (27, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Size";
            // 
            // labelAtomFeedFileLocation
            // 
            this.labelAtomFeedFileLocation.Location = new System.Drawing.Point (6, 20);
            this.labelAtomFeedFileLocation.AutoSize = true;
            this.labelAtomFeedFileLocation.TabIndex = 0;
            this.labelAtomFeedFileLocation.Text = "Location of feed file:";
            this.labelAtomFeedFileLocation.Name = "labelAtomFeedFileLocation";

            // 
            // textBoxAtomFeedFileLocation
            // 
            this.textBoxAtomFeedFileLocation.Location = new System.Drawing.Point (6, 40);
            this.textBoxAtomFeedFileLocation.TabIndex = 1;
            this.textBoxAtomFeedFileLocation.Name = "textBoxAtomFeedFileLocation";
            this.textBoxAtomFeedFileLocation.Size = new System.Drawing.Size (
                this.tabPage1.Width - 2 * this.textBoxAtomFeedFileLocation.Left,
                this.textBoxAtomFeedFileLocation.Height);
            this.textBoxAtomFeedFileLocation.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;


            // 
            // panel1
            // 
            this.panel1.Controls.Add (this.button2);
            this.panel1.Controls.Add (this.button1);
            this.panel1.Dock = DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point (0, 323);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size (513, 38);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.button2.DialogResult = DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point (345, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size (75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler (this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.button1.DialogResult = DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point (426, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size (75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler (this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add (this.tabPage2);
            this.tabControl1.Controls.Add (this.tabPage1);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point (0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size (513, 323);
            this.tabControl1.TabIndex = 1;

            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add (this.groupBox3);
            this.tabPage2.Controls.Add (this.groupBox2);
            this.tabPage2.Controls.Add (this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point (4, 22);
            this.tabPage2.Name = nameof (this.tabPage2);
            this.tabPage2.Padding = new Padding (3);
            this.tabPage2.Size = new System.Drawing.Size (505, 297);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add (this.labelAtomFeedFileLocation);
            this.tabPage1.Controls.Add (this.textBoxAtomFeedFileLocation);
            this.tabPage1.Location = new System.Drawing.Point (4, 22);
            this.tabPage1.Name = nameof (this.tabPage1);
            this.tabPage1.Padding = new Padding (3);
            this.tabPage1.Size = new System.Drawing.Size (505, 297);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ATOM Feed";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add (this.comboBox2);
            this.groupBox2.Controls.Add (this.label2);
            this.groupBox2.Controls.Add (this.comboBox1);
            this.groupBox2.Controls.Add (this.label1);
            this.groupBox2.Location = new System.Drawing.Point (264, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size (233, 82);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Font";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point (145, 40);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size (82, 21);
            this.comboBox2.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point (9, 40);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size (119, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add (this.checkBox9);
            this.groupBox1.Controls.Add (this.checkBox8);
            this.groupBox1.Controls.Add (this.checkBox7);
            this.groupBox1.Controls.Add (this.checkBox6);
            this.groupBox1.Controls.Add (this.checkBox5);
            this.groupBox1.Controls.Add (this.checkBox4);
            this.groupBox1.Controls.Add (this.checkBox3);
            this.groupBox1.Controls.Add (this.checkBox2);
            this.groupBox1.Controls.Add (this.checkBox1);
            this.groupBox1.Location = new System.Drawing.Point (8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size (239, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Look and Feel";
            // 
            // checkBox10
            // 
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point (9, 19);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size (195, 17);
            this.checkBox10.TabIndex = 19;
            this.checkBox10.Text = "Auto Insert Curly Brackets ( { and } )";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.AutoSize = true;
            this.checkBox9.Location = new System.Drawing.Point (6, 203);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size (159, 17);
            this.checkBox9.TabIndex = 18;
            this.checkBox9.Text = "Highlight Matching Brackets";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Location = new System.Drawing.Point (6, 180);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size (121, 17);
            this.checkBox8.TabIndex = 17;
            this.checkBox8.Text = "Show Line Numbers";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point (6, 157);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size (115, 17);
            this.checkBox7.TabIndex = 16;
            this.checkBox7.Text = "Show Invalid Lines";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point (6, 134);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size (89, 17);
            this.checkBox6.TabIndex = 15;
            this.checkBox6.Text = "Show HRuler";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point (6, 111);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size (88, 17);
            this.checkBox5.TabIndex = 14;
            this.checkBox5.Text = "Show VRuler";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point (6, 88);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size (80, 17);
            this.checkBox4.TabIndex = 13;
            this.checkBox4.Text = "Show Tabs";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point (6, 65);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size (92, 17);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Show Spaces";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point (6, 42);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size (113, 17);
            this.checkBox2.TabIndex = 11;
            this.checkBox2.Text = "Show EOL Marker";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point (6, 19);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size (140, 17);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.Text = "Allow caret beyond EOL";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            this.checkBox11.AutoSize = true;
            this.checkBox11.Location = new System.Drawing.Point (9, 42);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size (96, 17);
            this.checkBox11.TabIndex = 2;
            this.checkBox11.Text = "Enable Folding";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add (this.checkBox12);
            this.groupBox3.Controls.Add (this.checkBox11);
            this.groupBox3.Controls.Add (this.checkBox10);
            this.groupBox3.Location = new System.Drawing.Point (264, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size (233, 141);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Editing";
            // 
            // checkBox12
            // 
            this.checkBox12.AutoSize = true;
            this.checkBox12.Location = new System.Drawing.Point (9, 65);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size (141, 17);
            this.checkBox12.TabIndex = 20;
            this.checkBox12.Text = "Convert Tabs to Spaces";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF (6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size (513, 361);
            this.Controls.Add (this.tabControl1);
            this.Controls.Add (this.panel1);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "OptionsDialog";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Options";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;

            this.panel1.ResumeLayout (false);
            this.tabControl1.ResumeLayout (false);
            this.tabPage2.ResumeLayout (false);
            this.groupBox2.ResumeLayout (false);
            this.groupBox2.PerformLayout ();
            this.groupBox1.ResumeLayout (false);
            this.groupBox1.PerformLayout ();
            this.groupBox3.ResumeLayout (false);
            this.groupBox3.PerformLayout ();
            this.ResumeLayout (false);

        }

        #endregion

        private Label label1;
        private Label label2;

        private Label labelAtomFeedFileLocation;
        private TextBox textBoxAtomFeedFileLocation;

        private Panel panel1;

        private Button button1;
        private Button button2;

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;

        private ComboBox comboBox1;
        private ComboBox comboBox2;

        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private CheckBox checkBox5;
        private CheckBox checkBox6;
        private CheckBox checkBox7;
        private CheckBox checkBox8;
        private CheckBox checkBox9;
        private CheckBox checkBox10;
        private CheckBox checkBox11;
        private CheckBox checkBox12;

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}

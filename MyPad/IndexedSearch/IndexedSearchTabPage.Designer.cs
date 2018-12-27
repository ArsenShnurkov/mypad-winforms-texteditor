using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MyPad
{
    public partial class IndexedSearchTabPage : TabPage
    {
        ContainerControl panelContainer;
        TextBox textBoxSearchString;
        Button buttonStartSearch;
        Button buttonStopSearch;
        TabControl tabControlOutput;
        TabPage tabPageSearchResults;
        TabPage tabPageSearchStatistics;
        TabPage tabPageSearchErrors;
        TreeView treeViewResults;
        ProgressBar progressBarSearch;

        private void InitializeComponent ()
        {
            panelContainer = new ContainerControl ();
            textBoxSearchString = new TextBox ();
            buttonStartSearch = new Button ();
            buttonStopSearch = new Button ();
            tabControlOutput = new TabControl ();
            tabPageSearchResults = new TabPage ();
            tabPageSearchStatistics = new TabPage ();
            tabPageSearchErrors = new TabPage ();
            treeViewResults = new TreeView ();
            progressBarSearch = new ProgressBar ();

            this.Controls.Add (panelContainer);

            panelContainer.Dock = DockStyle.Fill;
            panelContainer.Controls.Add (textBoxSearchString);
            panelContainer.Controls.Add (progressBarSearch);
            panelContainer.Controls.Add (buttonStartSearch);
            panelContainer.Controls.Add (buttonStopSearch);
            panelContainer.Controls.Add (tabControlOutput);

            buttonStopSearch.Text = "Стоп";
            buttonStopSearch.AutoSize = true;
            buttonStopSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonStopSearch.Top = 10;
            buttonStopSearch.Left = buttonStopSearch.Parent.Right - buttonStopSearch.Width - 10;
            buttonStopSearch.TabIndex = 3;
            buttonStopSearch.Click += buttonStopSearch_Click;
            buttonStopSearch.Visible = false;

            buttonStartSearch.Text = "Старт";
            buttonStartSearch.AutoSize = true;
            buttonStartSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonStartSearch.Top = 10;
            buttonStartSearch.Left = buttonStartSearch.Parent.Right - buttonStartSearch.Width - 10;
            buttonStartSearch.TabIndex = 2;
            buttonStartSearch.Click += buttonStartSearch_Click;

            textBoxSearchString.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            textBoxSearchString.Top = 10;
            textBoxSearchString.Left = 10;
            textBoxSearchString.Width = buttonStartSearch.Left - 10 * 2;
            textBoxSearchString.Enabled = true;
            textBoxSearchString.ReadOnly = false;
            textBoxSearchString.TabIndex = 1;

            progressBarSearch.Left = textBoxSearchString.Left;
            progressBarSearch.Top = textBoxSearchString.Top;
            progressBarSearch.Width = textBoxSearchString.Width;
            progressBarSearch.Height = textBoxSearchString.Height;
            progressBarSearch.Visible = false;

            tabControlOutput.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            tabControlOutput.Left = 10;
            tabControlOutput.Width = tabControlOutput.Parent.Width - 10 * 2;
            tabControlOutput.Top = textBoxSearchString.Bottom + 10;
            tabControlOutput.Height = tabControlOutput.Parent.Bottom - tabControlOutput.Top - 10;
            tabControlOutput.TabIndex = 3;
            tabControlOutput.TabPages.Add (tabPageSearchResults);
            tabControlOutput.TabPages.Add (tabPageSearchStatistics);
            tabControlOutput.TabPages.Add (tabPageSearchErrors);

            tabPageSearchResults.Text = "Результаты";
            tabPageSearchStatistics.Text = "Статистика";
            tabPageSearchErrors.Text = "Ошибки";
            tabPageSearchResults.Controls.Add (treeViewResults);

            treeViewResults.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            treeViewResults.Top = 10;
            treeViewResults.Left = 10;
            treeViewResults.Width = treeViewResults.Parent.Width - 10 * 2;
            treeViewResults.Height = treeViewResults.Parent.Height - 10 * 2;
            treeViewResults.ShowRootLines = false;
            treeViewResults.NodeMouseDoubleClick += treeViewResults_NodeMouseDoubleClick;

            this.treeViewResults.KeyPress += treeViewResults_KeyPress;

            panelContainer.ActiveControl = textBoxSearchString;
        }
    }
}

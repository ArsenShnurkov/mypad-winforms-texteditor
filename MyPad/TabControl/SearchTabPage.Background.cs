using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace MyPad
{
    class SearchResult
    {
        public FileInfo fi;
        public int line;
    }

    public partial class SearchTabPage : TabPage
    {
        private void backgroundWorker1_DoWork (object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++) {
                Thread.Sleep (100);
                int percentage = i * 100 / 1000;
                // Periodically report progress to the main thread so that it can
                // update the UI.  In most cases you'll just need to send an
                // integer that will update a ProgressBar 
                SearchResult res = new SearchResult ();
                res.fi = new FileInfo (i.ToString ());
                res.line = i;
                this.backgroundWorker1.ReportProgress (percentage, res);
                // Periodically check if a cancellation request is pending.
                // If the user clicks cancel the line
                // m_AsyncWorker.CancelAsync(); if ran above.  This
                // sets the CancellationPending to true.
                // You must check this flag in here and react to it.
                // We react to it by setting e.Cancel to true and leaving
                if (this.backgroundWorker1.CancellationPending) {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    this.backgroundWorker1.ReportProgress (0);
                    return;
                }
            }
        }
    }
}
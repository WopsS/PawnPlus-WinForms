using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public enum ApplicationStatusType
    {
        Ready,
        Busy,
        Compiling,
        Compiled,
        CompiledWithErrors,
        TextLength,
        OpeningProject,
        OpeningFile,
    }

    public class ApplicationStatus
    {
        private BackgroundWorker backgroundWorker;
        private StatusStrip statusStrip;
        private ToolStripLabel toolStripLabel;

        public ApplicationStatus(StatusStrip statusStrip, ToolStripLabel toolStripLabel)
        {
            this.backgroundWorker = new BackgroundWorker();

            this.backgroundWorker.DoWork += new DoWorkEventHandler(backgroundWorker_DoWork);
            this.backgroundWorker.WorkerSupportsCancellation = true;

            this.statusStrip = statusStrip;
            this.toolStripLabel = toolStripLabel;
        }

        /// <summary>
        /// Change application status.
        /// </summary>
        /// <param name="NewStatus">Type for status.</param>
        /// <param name="StatusFlag">If this is true timer can change status to ready, otherwise current status won't be changed.</param>
        ///
        public void setApplicationStatus(ApplicationStatusType NewStatus, bool StatusFlag)
        {
            this.statusStrip.BackColor = getStatusColor(NewStatus);
            this.toolStripLabel.Text = getStatusText(NewStatus);

            if (backgroundWorker.IsBusy == true)
                backgroundWorker.CancelAsync();

            if (StatusFlag == true)
                backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Get text for the new status.
        /// </summary>
        /// <param name="currentStatus">New status type.</param>
        /// <returns>Status name.</returns>
        private string getStatusText(ApplicationStatusType currentStatus)
        {
            if (ApplicationStatusType.Busy == currentStatus)
                return "Busy";
            else if (ApplicationStatusType.Compiling == currentStatus)
                return "Compiling...";
            else if (ApplicationStatusType.Compiled == currentStatus)
                return "Compiled";
            else if (ApplicationStatusType.CompiledWithErrors == currentStatus)
                return "Compiled with errors";
            else if (ApplicationStatusType.TextLength == currentStatus)
                return "Current document is empty";
            else if (ApplicationStatusType.OpeningProject == currentStatus)
                return "Opening project...";
            else if (ApplicationStatusType.OpeningFile == currentStatus)
                return "Opening file...";

            return "Ready";
        }

        /// <summary>
        /// Get color for the new status.
        /// </summary>
        /// <param name="currentStatus">New status type.</param>
        /// <returns>Color for the new status.</returns>
        private Color getStatusColor(ApplicationStatusType currentStatus)
        {
            if (ApplicationStatusType.Compiling == currentStatus || ApplicationStatusType.Busy == currentStatus || ApplicationStatusType.OpeningProject == currentStatus || ApplicationStatusType.OpeningFile == currentStatus)
                return Color.FromArgb(202, 81, 0);
            else if (ApplicationStatusType.Compiled == currentStatus)
                return Color.FromArgb(0, 94, 157);
            else if (ApplicationStatusType.CompiledWithErrors == currentStatus || ApplicationStatusType.TextLength == currentStatus)
                return Color.FromArgb(170, 0, 0);

            return Color.FromArgb(0, 122, 204);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(5000);

            this.setApplicationStatus(ApplicationStatusType.Ready, false);
        }
    }
}

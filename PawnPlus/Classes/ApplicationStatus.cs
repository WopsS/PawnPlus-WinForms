using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    class ApplicationStatus
    {
        public static bool StatusFlag = false;

        public enum Status
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

        /// <summary>
        /// Change application status.
        /// </summary>
        /// <param name="NewStatus">Type for status.</param>
        /// <param name="StatusFlag">If this is true timer can change status to ready, otherwise current status won't be changed.</param>
        public static void setApplicationStatus(Status NewStatus, bool StatusFlag)
        {
            Program.main.StatusBar.BackColor = getStatusColor(NewStatus);
            Program.main.StatusLabel.Text = getStatusText(NewStatus);

            setStatusFlag(StatusFlag);
        }

        private static void setStatusFlag(bool Flag)
        {
            StatusFlag = Flag;
        }

        private static string getStatusText(Status currentStatus)
        {
            if (Status.Busy == currentStatus)
                return "Busy";
            else if (Status.Compiling == currentStatus)
                return "Compiling...";
            else if (Status.Compiled == currentStatus)
                return "Compiled";
            else if (Status.CompiledWithErrors == currentStatus)
                return "Compiled with errors";
            else if (Status.TextLength == currentStatus)
                return "Current document is empty";
            else if (Status.OpeningProject == currentStatus)
                return "Opening project...";
            else if (Status.OpeningFile == currentStatus)
                return "Opening file...";

            return "Ready";
        }

        private static Color getStatusColor(Status currentStatus)
        {
            if (Status.Compiling == currentStatus || Status.Busy == currentStatus || Status.OpeningProject == currentStatus || Status.OpeningFile == currentStatus)
                return Color.FromArgb(202, 81, 0);
            else if (Status.Compiled == currentStatus)
                return Color.FromArgb(0, 94, 157);
            else if (Status.CompiledWithErrors == currentStatus || Status.TextLength == currentStatus)
                return Color.FromArgb(170, 0, 0);

            return Color.FromArgb(0, 122, 204);
        }
    }
}

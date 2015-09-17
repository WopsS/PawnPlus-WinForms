using PawnPlus.Language;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public static class StatusManager
    {
        private static StatusStrip statusBar;
        private static ToolStripLabel columnLabel, lineLabel, statusLabel;

        /// <summary>
        /// Constructor for the static class, it is called manually.
        /// </summary>
        /// <param name="statusBar">Object of the status bar.</param>
        /// <param name="statusLabel">Object of the status label.</param>
        /// <param name="lineLabel">Object of the line label.</param>
        /// <param name="columnLabel">Object of the line label.</param>
        public static void Construct(StatusStrip statusBar, ToolStripLabel statusLabel, ToolStripLabel lineLabel, ToolStripLabel columnLabel)
        {
            if (StatusManager.statusBar != null) // Prevent double construct.
            {
                return;
            }

            StatusManager.statusBar = statusBar;
            StatusManager.columnLabel = columnLabel;
            StatusManager.lineLabel = lineLabel;
            StatusManager.statusLabel = statusLabel;
        }

        /// <summary>
        /// Set line and column label on the status bar.
        /// </summary>
        /// <param name="line">Number of the line.</param>
        /// <param name="column">Number of the column.</param>
        public static void SetLineColumn(int line, int column)
        {
            lineLabel.Text = string.Format(LanguageManager.GetText(LanguageEnum.MenuLine), line);
            columnLabel.Text = string.Format(LanguageManager.GetText(LanguageEnum.MenuColumn), column);
        }

        public static void SetStatus()
        {
            // TODO: Create function to set the status.
        }
    }
}

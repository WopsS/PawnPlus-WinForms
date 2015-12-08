using PawnPlus.Language;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public enum StatusType
    {
        Error,
        Finish,
        Info,
        Warning
    }

    public enum StatusReset
    {
        None,
        OneSecond,
        ThreeSeconds,
        FiveSeconds
    }

    public static class StatusManager
    {
        private static StatusStrip statusBar;
        private static Timer readyTimer = new Timer();
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

            readyTimer.Tick += ReadyTimer_Tick;
        }

        private static void ReadyTimer_Tick(object sender, System.EventArgs e)
        {
            Set(StatusType.Info, LanguageEnum.StatusReady, StatusReset.None);
            readyTimer.Stop();
        }

        /// <summary>
        /// Set status of the application.
        /// </summary>
        /// <param name="type">Type of the status.</param>
        /// <param name="languageIndex">Language index of the text.</param>
        /// <param name="resetTime">Reset time until status will be 'Ready'.</param>
        public static void Set(StatusType type, LanguageEnum languageIndex, StatusReset resetTime)
        {
            Color color = Color.Empty;

            switch (type)
            {
                case StatusType.Error:
                    {
                        color = Color.FromArgb(170, 0, 0);
                        break;
                    }
                case StatusType.Finish:
                    {
                        color = Color.FromArgb(0, 94, 157);
                        break;
                    }
                case StatusType.Info:
                    {
                        color = Color.FromArgb(0, 122, 204);
                        break;
                    }
                case StatusType.Warning:
                    {
                        color = Color.FromArgb(202, 81, 0);
                        break;
                    }
            }

            statusBar.BackColor = color;
            statusLabel.Text = LanguageManager.GetText(languageIndex);

            if (resetTime != StatusReset.None)
            {
                int interval = 0;

                switch (resetTime)
                {
                    case StatusReset.OneSecond:
                        {
                            interval = 1000;
                            break;
                        }
                    case StatusReset.ThreeSeconds:
                        {
                            interval = 3000;
                            break;
                        }
                    case StatusReset.FiveSeconds:
                        {
                            interval = 5000;
                            break;
                        }
                }

                readyTimer.Stop();
                readyTimer.Interval = interval;
                readyTimer.Start();
            }
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
    }
}

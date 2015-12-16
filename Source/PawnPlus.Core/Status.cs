using PawnPlus.Core.Events;
using PawnPlus.Core.Forms;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public enum StatusType : byte
    {
        None,
        Error,
        Finish,
        Info,
        Warning
    }

    public enum StatusReset : byte
    {
        None,
        OneSecond,
        ThreeSeconds,
        FiveSeconds
    }

    public static class Status
    {
        private static Main mainForm = (Main)Application.OpenForms["Main"];

        private static StatusType oldType { get; set; } = StatusType.None;

        private static Timer readyTimer = new Timer();

        static Status()
        {
            EventStorage.AddListener<Editor, CaretPositionChangedArgs>(EventKey.CaretPositionChanged, event_CaretPositionChanged);
            readyTimer.Tick += ReadyTimer_Tick;
        }

        private static void event_CaretPositionChanged(Editor editor, CaretPositionChangedArgs e)
        {
            SetLineColumn(e.Line, e.Column);
        }

        private static void ReadyTimer_Tick(object sender, System.EventArgs e)
        {
            Set(StatusType.Info, StatusReset.None, Localization.Status_Ready);
            readyTimer.Stop();
        }

        /// <summary>
        /// Set status of the application.
        /// </summary>
        /// <param name="type">Type of the status.</param>
        /// <param name="text">Text to be set.</param>
        /// <param name="resetTime">Reset time until status will be 'Ready'.</param>
        public static void Set(StatusType type, StatusReset resetTime, string text, params string[] parameters)
        {
            if (readyTimer.Enabled == true)
            {
                readyTimer.Stop();
            }

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

            string oldText = mainForm.statusLabel.Text;
            oldType = type;

            mainForm.statusBar.BackColor = color;
            mainForm.statusLabel.Text = string.Format(text, parameters);

            EventStorage.Fire(EventKey.StatusChanged, null, new StatusChangedEventArgs(oldText, oldType, mainForm.statusLabel.Text, type));

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
            mainForm.lineLabel.Text = string.Format(Localization.Status_Line, line);
            mainForm.columnLabel.Text = string.Format(Localization.Status_Column, column);
        }
    }
}

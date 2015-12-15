using System;

namespace PawnPlus.Core.Events
{
    public delegate void DownloadHandlerDelegate(object sender, DownloadHandlerEventArgs e);

    public class DownloadHandlerEventArgs : EventArgs
    {
        public virtual string DownloadedText { get; }

        public virtual int ProgressPercentage { get; }

        public DownloadHandlerEventArgs(string downloadedText, int progressPercentage)
        {
            this.DownloadedText = downloadedText;
            this.ProgressPercentage = progressPercentage;
        }
    }
}

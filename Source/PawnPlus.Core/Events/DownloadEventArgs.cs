using System;

namespace PawnPlus.Core.Events
{
    public class DownloadEventArgs : EventArgs
    {
        public virtual string DownloadedText { get; }

        public virtual int ProgressPercentage { get; }

        public DownloadEventArgs(string downloadedText, int progressPercentage)
        {
            this.DownloadedText = downloadedText;
            this.ProgressPercentage = progressPercentage;
        }
    }
}

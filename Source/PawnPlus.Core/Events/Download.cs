using System;

namespace PawnPlus.Core.Events
{
    /*
    * Format:
    *
    *   [access modifier] void [Name](object sender, DownloadHandlerEventArgs e)
    *   {
    *       // Do something.
    *   }
    */

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

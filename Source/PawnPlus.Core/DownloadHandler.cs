using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace PawnPlus.Core
{
    public class DownloadHandlerEventArgs
    {
        public string downloadedText { get; private set; }
        public int progressPercentage { get; private set; }

        public DownloadHandlerEventArgs(string DownloadedText, int ProgressPercentage)
        {
            this.downloadedText = DownloadedText;
            this.progressPercentage = ProgressPercentage;
        }
    }

    public class DownloadHandler
    {
        public delegate void DownloadHandlerDelegate(object sender, DownloadHandlerEventArgs e);
        public event DownloadHandlerDelegate DownloadProgressChanged = delegate { }, DownloadProgressComplete = delegate { };

        private WebClient webClient = new WebClient();
        private Queue<Tuple<Uri, string>> LinksQueue = new Queue<Tuple<Uri, string>>();
        private string currentSavePath = string.Empty;
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        public DownloadHandler(Tuple<Uri, string>[] Values)
        {
            foreach(Tuple<Uri, string> Value in Values)
            {
                this.LinksQueue.Enqueue(Value);
            }

            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);
        }

        public DownloadHandler(Uri URL, string SavePath)
        {
            this.LinksQueue.Enqueue(new Tuple<Uri, string>(URL, SavePath));

            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);
        }

        ~DownloadHandler()
        {
            this.webClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
            this.webClient.DownloadFileCompleted -= new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);

            this.webClient.Dispose();
        }

        public void Start()
        {
            if (this.LinksQueue.Count == 0)
            {
                return;
            }

            this.manualResetEvent.Reset();
            Tuple<Uri, string> CurrentLink = this.LinksQueue.Dequeue();

            this.currentSavePath = CurrentLink.Item2;
            this.webClient.DownloadFileAsync(CurrentLink.Item1, CurrentLink.Item2);
            this.manualResetEvent.WaitOne();
        }

        public void Cancel()
        {
            this.webClient.CancelAsync();
        }

        private void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            string downloadedText = string.Format("{0} of {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));

            this.DownloadProgressChanged(this, new DownloadHandlerEventArgs(downloadedText, e.ProgressPercentage));
        }

        private void DownloadProgressCompleteEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            this.manualResetEvent.Set();

            if (e.Cancelled == true)
            {
                File.Delete(this.currentSavePath);
                this.DownloadProgressComplete(this, new DownloadHandlerEventArgs("Canceled", -1));

                return;
            }

            this.DownloadProgressComplete(this, new DownloadHandlerEventArgs("Completed", 100));
        }
    }
}

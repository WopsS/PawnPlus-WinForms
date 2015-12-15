using PawnPlus.Core.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;

namespace PawnPlus.Core
{
    public class DownloadHandler : IDisposable
    {
        private WebClient webClient = new WebClient();
        private Queue<Tuple<Uri, string>> linksQueue = new Queue<Tuple<Uri, string>>();
        private string currentSavePath = string.Empty;
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="values">Values to be downloaded.</param>
        public DownloadHandler(Tuple<Uri, string>[] values)
        {
            foreach (Tuple<Uri, string> value in values)
            {
                this.linksQueue.Enqueue(value);
            }

            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="url">URL of the file.</param>
        /// <param name="savePath">Path where to save it.</param>
        public DownloadHandler(Uri url, string savePath)
        {
            this.linksQueue.Enqueue(new Tuple<Uri, string>(url, savePath));

            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);
        }

        /// <summary>
        /// Class destructor.
        /// </summary>
        ~DownloadHandler()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.webClient.DownloadProgressChanged -= new DownloadProgressChangedEventHandler(this.DownloadProgressChangedEventHandler);
                this.webClient.DownloadFileCompleted -= new AsyncCompletedEventHandler(this.DownloadProgressCompleteEventHandler);

                this.manualResetEvent.Dispose();
                this.webClient.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Download the first file and remove it from the queue.
        /// </summary>
        public void Start()
        {
            if (this.linksQueue.Count == 0)
            {
                return;
            }

            this.manualResetEvent.Reset();
            Tuple<Uri, string> CurrentLink = this.linksQueue.Dequeue();

            this.currentSavePath = CurrentLink.Item2;
            this.webClient.DownloadFileAsync(CurrentLink.Item1, CurrentLink.Item2);
            this.manualResetEvent.WaitOne();
        }

        /// <summary>
        /// Cancel file downloading.
        /// </summary>
        public void Cancel()
        {
            this.webClient.CancelAsync();
        }

        private void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            string downloadedText = string.Format("{0} of {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
            EventStorage.Fire(EventKey.DownloadProgressChanged, this, new DownloadHandlerEventArgs(downloadedText, e.ProgressPercentage));
        }

        private void DownloadProgressCompleteEventHandler(object sender, AsyncCompletedEventArgs e)
        {
            this.manualResetEvent.Set();

            if (e.Cancelled == true)
            {
                File.Delete(this.currentSavePath);
                EventStorage.Fire(EventKey.DownloadProgressComplete, this, new DownloadHandlerEventArgs("Canceled", -1));

                return;
            }

            EventStorage.Fire(EventKey.DownloadProgressComplete, this, new DownloadHandlerEventArgs("Completed", 100));
        }
    }
}

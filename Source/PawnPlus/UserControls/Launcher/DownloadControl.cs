using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace PawnPlus
{
    public partial class DownloadControl : UserControl
    {
        public WebClient webClient = new WebClient();

        public string FileName = String.Empty;

        public DownloadControl()
        {
            InitializeComponent();
        }

        public void DownloadFile(string URL, string FileName, bool isUpdate)
        {
            if (isUpdate == true)
                this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DonwloadComplete);

            this.FileName = FileName;

            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DonwloadProcess);
            this.webClient.DownloadFileAsync(new Uri(URL), FileName);

        }

        public void DonwloadProcess(object sender, DownloadProgressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.DownloadedMBLabel.Text = String.Format("{0} of {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (e.TotalBytesToReceive / 1024d / 1024d).ToString("0.00"));
                this.DonwloadProcessBar.Value = e.ProgressPercentage;
                this.DownloadPercentLabel.Text = e.ProgressPercentage.ToString() + "%";
            }));
        }

        private void DonwloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates", this.FileName));

                this.Invoke(new MethodInvoker(delegate
                {
                    Application.Exit();
                }));
            }
            catch (Exception) { }
        }
    }
}

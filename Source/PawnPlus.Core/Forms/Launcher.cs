using Newtonsoft.Json;
using PawnPlus.Core.Events;
using PawnPlus.Core.Exceptions;
using PawnPlus.Core.Extensibility;
using PawnPlus.Core.UserControls;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class Launcher : Form
    {
        /// <summary>
        /// It is <c>true</c> if it is closed by our code, <c>false</c> otherwise.
        /// </summary>
        public bool IsSafe { get; private set; }

        private DownloadControl downloadControl;

        private StatusControl statusControl;

        public Launcher()
        {
            InitializeComponent();

            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            this.statusControl = new StatusControl();

            this.controlsPanel.Controls.Add(this.statusControl);
            this.backgroundWorker.RunWorkerAsync();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the application if the form isn't closed manually.
            if (this.IsSafe == false)
            {
                Application.Exit();
            }
        }

        private void principalPanel_Paint(object sender, PaintEventArgs e)
        {
            int Thickness = 1, HalfThickness = Thickness / 2;

            using (Pen p = new Pen(Color.Black, Thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(HalfThickness, HalfThickness, principalPanel.ClientSize.Width - Thickness, principalPanel.ClientSize.Height - Thickness));
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.backgroundWorker.ReportProgress(1, Localization.Text_VersionChecking);
            Thread.Sleep(100);

            if (PawnPlus.Properties.Settings.Default.UpgradeRequired == true)
            {
                this.backgroundWorker.ReportProgress(5, Localization.Text_VersionSettings);

                PawnPlus.Properties.Settings.Default.Upgrade();
                PawnPlus.Properties.Settings.Default.UpgradeRequired = false;
                PawnPlus.Properties.Settings.Default.Save();
            }

            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    this.backgroundWorker.ReportProgress(10, Localization.Text_CheckingFiles);
                    Thread.Sleep(100);

                    string JSONString = string.Empty;

                    using (WebClient webClient = new WebClient())
                    {
                        JSONString = webClient.DownloadString("https://raw.githubusercontent.com/WopsS/PawnPlus/master/Information.json");
                    }

                    InformationJSON applicationJSON = JsonConvert.DeserializeObject<InformationJSON>(JSONString);

                    if (Directory.EnumerateFiles(ApplicationData.PawnDirectory).Any() == false || Directory.EnumerateFiles(ApplicationData.IncludesDirectory).Any() == false)
                    {
                        FileInfo fileInfo = null;

                        string TemporaryFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                        string SAMPZIPPath = Path.Combine(TemporaryFolder, applicationJSON.SAMPZIPName), CompilerZIPPath = Path.Combine(TemporaryFolder, applicationJSON.CompilerZIPName);

                        Directory.CreateDirectory(TemporaryFolder);

                        DownloadHandler downloadHandler = new DownloadHandler(new Tuple<Uri, string>[]
                        {
                            Tuple.Create(new Uri(applicationJSON.SAMPFilesLink + "/" + applicationJSON.SAMPZIPName), SAMPZIPPath),
                            Tuple.Create(new Uri(applicationJSON.CompilerLink + "/" + applicationJSON.CompilerZIPName), CompilerZIPPath),
                        });

                        EventStorage.AddListener<object, DownloadHandlerEventArgs>(EventKey.DownloadProgressChanged, this.DownloadHandler_DownloadProgressChanged);
                        EventStorage.AddListener<object, DownloadHandlerEventArgs>(EventKey.DownloadProgressComplete, this.DownloadHandler_DownloadProgressComplete);

                        this.backgroundWorker.ReportProgress(15, Localization.Text_ServerFilesDownloading);
                        Thread.Sleep(500);

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.downloadControl = new DownloadControl();

                            this.Height = 190;
                            this.controlsPanel.Controls.Clear();
                            this.controlsPanel.Controls.Add(this.downloadControl);
                        }));

                        downloadHandler.Start(); // Download SA-MP files.
                        fileInfo = new FileInfo(SAMPZIPPath);

                        if (File.Exists(SAMPZIPPath) == true && fileInfo.Length > 0)
                        {
                            this.backgroundWorker.ReportProgress(15, Localization.Text_ServerFilesUnpacking);
                            Thread.Sleep(1000);

                            ZipArchive Archive = ZipArchive.Open(SAMPZIPPath);

                            foreach (ZipArchiveEntry archiveEntry in Archive.Entries)
                            {
                                if (archiveEntry.IsDirectory == false)
                                {
                                    archiveEntry.WriteToDirectory(TemporaryFolder, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                                }
                            }

                            Archive.Dispose();

                            this.backgroundWorker.ReportProgress(15, Localization.Text_ServerFilesCopying);
                            Thread.Sleep(1000);

                            // Let's copy server includes.
                            string[] Files = Directory.GetFiles(Path.Combine(TemporaryFolder, "pawno", "include"));

                            foreach (string CurrentFile in Files)
                            {
                                File.Copy(CurrentFile, Path.Combine(ApplicationData.IncludesDirectory, Path.GetFileName(CurrentFile)), true);
                            }

                            this.backgroundWorker.ReportProgress(15, Localization.Text_ServerFilesCopied);
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            this.backgroundWorker.ReportProgress(15, Localization.Text_ServerFilesError);
                            Thread.Sleep(2000);
                        }

                        this.backgroundWorker.ReportProgress(15, Localization.Text_CompilerFilesDownloading);
                        Thread.Sleep(500);

                        this.Invoke(new MethodInvoker(delegate
                        {
                            this.downloadControl = new DownloadControl();

                            this.Height = 190;
                            this.controlsPanel.Controls.Clear();
                            this.controlsPanel.Controls.Add(this.downloadControl);
                        }));

                        downloadHandler.Start(); // Download ZEEX compiler files.
                        fileInfo = new FileInfo(CompilerZIPPath);

                        if (File.Exists(CompilerZIPPath) == true && fileInfo.Length > 0)
                        {
                            this.backgroundWorker.ReportProgress(15, Localization.Text_CompilerFilesUnpacking);
                            Thread.Sleep(1000);

                            ZipArchive Archive = ZipArchive.Open(CompilerZIPPath);

                            foreach (ZipArchiveEntry archiveEntry in Archive.Entries)
                            {
                                if (archiveEntry.IsDirectory == false)
                                {
                                    archiveEntry.WriteToDirectory(TemporaryFolder, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                                }
                            }

                            Archive.Dispose();

                            this.backgroundWorker.ReportProgress(15, Localization.Text_CompilerFilesCopying);
                            Thread.Sleep(1000);

                            // Let's copy PAWN files.
                            File.Copy(Path.Combine(TemporaryFolder, CompilerZIPPath.Remove(CompilerZIPPath.Length - 4), "bin", "pawnc.dll"), Path.Combine(ApplicationData.PawnDirectory, Path.GetFileName("pawnc.dll")), true);
                            File.Copy(Path.Combine(TemporaryFolder, CompilerZIPPath.Remove(CompilerZIPPath.Length - 4), "bin", "pawncc.exe"), Path.Combine(ApplicationData.PawnDirectory, Path.GetFileName("pawncc.exe")), true);

                            this.backgroundWorker.ReportProgress(15, Localization.Text_CompilerFilesCopied);
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            this.backgroundWorker.ReportProgress(15, Localization.Text_CompilerFilesError);
                            Thread.Sleep(2000);
                        }

                        // Let's delete the temporary folder, we don't need it from now.
                        Directory.Delete(TemporaryFolder, true);

                        EventStorage.RemoveListener<object, DownloadHandlerEventArgs>(EventKey.DownloadProgressChanged, this.DownloadHandler_DownloadProgressChanged);
                        EventStorage.RemoveListener<object, DownloadHandlerEventArgs>(EventKey.DownloadProgressComplete, this.DownloadHandler_DownloadProgressComplete);
                    }

                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandledException(ex);
                }
            }

            this.backgroundWorker.ReportProgress(20, Localization.Text_LoadingPlugins);
            Thread.Sleep(100);

            PluginManager.Load();

            this.backgroundWorker.ReportProgress(99, Localization.Text_Starting);
            Thread.Sleep(100);

            this.backgroundWorker.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null && e.UserState.ToString().Length > 0)
            {
                this.statusControl.statusLabel.Text = e.UserState.ToString();
                Logger.Write(LogType.Debug, this.statusControl.statusLabel.Text);
            }

            if (e.ProgressPercentage == 100)
            {
                this.IsSafe = true;
                this.Close();
            }
        }

        private void DownloadHandler_DownloadProgressChanged(object sender, DownloadHandlerEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.downloadControl.SetProcessValue(e.ProgressPercentage);
                this.downloadControl.SetPercentage(e.ProgressPercentage);
                this.downloadControl.SetDownloadedMegaBytes(e.DownloadedText);
            }));
        }

        private void DownloadHandler_DownloadProgressComplete(object sender, DownloadHandlerEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.Height = 140;
                this.controlsPanel.Controls.Clear();
                this.controlsPanel.Controls.Add(this.statusControl);
            }));
        }
    }
}

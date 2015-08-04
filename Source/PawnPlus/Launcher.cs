using Newtonsoft.Json;
using PawnPlus.Core;
using PawnPlus.Core.UserControls.Launcher;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus
{
    public partial class Launcher : Form
    {
        private StatusControl statusControl;
        private DownloadControl downloadControl;

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
            if(Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
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
            this.backgroundWorker.ReportProgress(1, "Checking for old version...");
            Thread.Sleep(100);

            if (Properties.Settings.Default.UpgradeRequired == true)
            {
                this.backgroundWorker.ReportProgress(5, "Obtain settings from the old version...");

                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.UpgradeRequired = false;
                Properties.Settings.Default.Save();
            }

            this.backgroundWorker.ReportProgress(10, "Checking files...");
            Thread.Sleep(100);

            string JSONString = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                JSONString = webClient.DownloadString("https://raw.githubusercontent.com/WopsS/PawnPlus/master/Information.json");
            }

            ApplicationJSON applicationJSON = JsonConvert.DeserializeObject<ApplicationJSON>(JSONString);

            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn")) == false || Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", "include")) == false)
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", "include"));

                string TemporaryFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName()), OurPAWNFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn");
                string SAMPZIPPath = Path.Combine(TemporaryFolder, applicationJSON.SAMPZIPName), CompilerZIPPath = Path.Combine(TemporaryFolder, applicationJSON.CompilerZIPName);
                Directory.CreateDirectory(TemporaryFolder);

                DownloadHandler downloadHandler = new DownloadHandler(new Tuple<Uri, string>[]
                {
                    Tuple.Create(new Uri(applicationJSON.SAMPFilesLink + "/" + applicationJSON.SAMPZIPName), SAMPZIPPath),
                    Tuple.Create(new Uri(applicationJSON.CompilerLink + "/" + applicationJSON.CompilerZIPName), CompilerZIPPath),
                });

                downloadHandler.DownloadProgressChanged += DownloadHandler_DownloadProgressChanged;
                downloadHandler.DownloadProgressComplete += DownloadHandler_DownloadProgressComplete;

                this.backgroundWorker.ReportProgress(15, "Downloading server files...");
                Thread.Sleep(500);

                this.Invoke(new MethodInvoker(delegate
                {
                    this.downloadControl = new DownloadControl();

                    this.Height = 190;
                    this.controlsPanel.Controls.Clear();
                    this.controlsPanel.Controls.Add(this.downloadControl);
                }));

                downloadHandler.Start(); // Download SA-MP files.

                if (File.Exists(SAMPZIPPath) == true)
                {
                    this.backgroundWorker.ReportProgress(15, "Download completed. Unpacking server files...");
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

                    this.backgroundWorker.ReportProgress(15, "Unpacking server files completed. Proceed with copying files...");
                    Thread.Sleep(1000);

                    // Let's copy server includes.
                    string[] Files = Directory.GetFiles(Path.Combine(TemporaryFolder, "pawno", "include"));

                    foreach (string CurrentFile in Files)
                    {
                        File.Copy(CurrentFile, Path.Combine(OurPAWNFolder, "include", Path.GetFileName(CurrentFile)), true);
                    }

                    this.backgroundWorker.ReportProgress(15, "Server files copied.");
                    Thread.Sleep(1000);
                }
                else
                {
                    this.backgroundWorker.ReportProgress(15, "Server files couldn't be downloaded. Proceed with compiler files...");
                    Thread.Sleep(2000);
                }

                this.backgroundWorker.ReportProgress(15, "Downloading compiler files...");
                Thread.Sleep(500);

                this.Invoke(new MethodInvoker(delegate
                {
                    this.downloadControl = new DownloadControl();

                    this.Height = 190;
                    this.controlsPanel.Controls.Clear();
                    this.controlsPanel.Controls.Add(this.downloadControl);
                }));

                downloadHandler.Start(); // Download ZEEX compiler files.

                if (File.Exists(CompilerZIPPath) == true)
                {
                    this.backgroundWorker.ReportProgress(15, "Download completed. Unpacking compiler files...");
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

                    this.backgroundWorker.ReportProgress(15, "Unpacking compiler files completed. Proceed with copying files...");
                    Thread.Sleep(1000);

                    // Let's copy PAWN files.
                    File.Copy(Path.Combine(TemporaryFolder, CompilerZIPPath.Remove(CompilerZIPPath.Length - 4), "bin", "pawnc.dll"), Path.Combine(OurPAWNFolder, Path.GetFileName("pawnc.dll")), true);
                    File.Copy(Path.Combine(TemporaryFolder, CompilerZIPPath.Remove(CompilerZIPPath.Length - 4), "bin", "pawncc.exe"), Path.Combine(OurPAWNFolder, Path.GetFileName("pawncc.exe")), true);

                    this.backgroundWorker.ReportProgress(15, "Compiler files copied.");
                    Thread.Sleep(1000);
                }
                else
                {
                    this.backgroundWorker.ReportProgress(15, "Compiler files couldn't be downloaded. Proceed with program startup...");
                    Thread.Sleep(2000);
                }

                // Let's delete the temporary folder, we don't need it from now.
                Directory.Delete(TemporaryFolder, true);

                downloadHandler.DownloadProgressChanged -= DownloadHandler_DownloadProgressChanged;
                downloadHandler.DownloadProgressComplete -= DownloadHandler_DownloadProgressComplete;
            }

            Thread.Sleep(50);

            this.backgroundWorker.ReportProgress(100, "Starting up...");
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.statusControl.statusLabel.Text = e.UserState.ToString();

            if(e.ProgressPercentage == 100)
            {
                Main mainForm = new Main();
                mainForm.Show();

                this.Close();
            }
        }

        private void DownloadHandler_DownloadProgressChanged(object sender, DownloadHandlerEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                this.downloadControl.SetProcessValue(e.progressPercentage);
                this.downloadControl.SetPercentage(e.progressPercentage);
                this.downloadControl.SetDownloadedMegaBytes(e.downloadedText);
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

using Microsoft.Win32;
using PawnPlus.Core;
using PawnPlus.Core.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PawnPlus
{
    public partial class Launcher : Form
    {
        private string ProjectPath = null;

        private StatusControl statuscontrol = new StatusControl();
        private DownloadControl downloadcontrol = new DownloadControl();

        public Launcher(string CurrentPath = null)
        {
            InitializeComponent();

            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;

            if (CurrentPath != null && Path.GetExtension(CurrentPath) == ".pawnplusproject")
                this.ProjectPath = CurrentPath;
            else if (CurrentPath != null)
                Program.main.OpenFile(CurrentPath);
        }

        private void Launcher_Load(object sender, EventArgs e)
        {
            this.ControlsPanel.Controls.Add(statuscontrol);

            this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusPanel_Paint);
            Program.main.ProjectInformation.Add("Path", this.ProjectPath);

            BackgroundWorker.RunWorkerAsync();
        }

        private void Launcher_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();

            if (Program.main.CloseApplication == false)
                e.Cancel = true;
        }

        private void StatusPanel_Paint(object sender, PaintEventArgs e)
        {
            int Thickness = 1;
            int HalfThickness = Thickness / 2;

            using (Pen p = new Pen(Color.Black, Thickness))
            {
                e.Graphics.DrawRectangle(p, new Rectangle(HalfThickness, HalfThickness, Panel.ClientSize.Width - Thickness, Panel.ClientSize.Height - Thickness));
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker.ReportProgress(1, "Checking for old version...");
            if (Properties.Settings.Default.UpgradeRequired == true)
                this.getOldSettings();
            Thread.Sleep(50);

            BackgroundWorker.ReportProgress(10, "Checking files...");
            this.CheckingFiles();
            Thread.Sleep(100);

            BackgroundWorker.ReportProgress(15, "Checking project...");
            if (this.ProjectPath != null)
                CheckingProject();
            Thread.Sleep(100);

            BackgroundWorker.ReportProgress(80, "Checking for updates...");
            this.CheckingForUpdates(false);
            Thread.Sleep(100);

            BackgroundWorker.ReportProgress(90, "Preparing...");
            this.PreparingProgram();
            Thread.Sleep(100);

            BackgroundWorker.ReportProgress(100, "Complete");
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.statuscontrol.StatusLabel.Text = e.UserState.ToString();

            if (e.UserState.ToString() == "Complete")
            {
                this.Hide();
                Program.main.Show();
                Program.main.BringToFront();
            }
        }

        private void UpdateBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.CheckingForUpdates(true);
        }

        private void getOldSettings()
        {
            BackgroundWorker.ReportProgress(5, "Obtain settings from the old version...");

            Properties.Settings.Default.Upgrade();
            Properties.Settings.Default.UpgradeRequired = false;
            Properties.Settings.Default.Save();
        }

        private void CheckingFiles()
        {
            Program.main.LayoutFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Layout.xml");

            if (Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", "include")) == false)
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", "include"));

            if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates")) == true)
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates"), true);
        }

        private void CheckingProject()
        {
            BackgroundWorker.ReportProgress(20, "Obtain informations about the project...");

            Program.main.LoadProject(this.ProjectPath);
        }

        public void CheckingForUpdates(bool isUserAction)
        {
            try
            {
                string NewVersion = new WebClient().DownloadString("http://www.pawnplus.eu/version.php").Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                if (this.Version.CompareTo(NewVersion) < 0)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.Height = 190;
                        this.ControlsPanel.Controls.Clear();
                        this.ControlsPanel.Controls.Add(downloadcontrol);
                        this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusPanel_Paint);
                    }));

                    if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates")))
                        Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates"));

                    // Github download link
                    //this.downloadcontrol.DownloadFile(String.Format("{0}/{1}/{2}", "https://github.com/WopsS/PawnPlus/releases/download", NewVersion, String.Format("PawnPlus-Setup-{0}.exe", NewVersion)), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates", "PawnPlus.exe"), true);

                    this.downloadcontrol.DownloadFile(String.Format("{0}/{1}", "http://www.pawnplus.eu/public/installers", String.Format("PawnPlus-Setup-{0}.exe", NewVersion)), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Updates", String.Format("PawnPlus-Setup-{0}.exe", NewVersion)), true);

                    // if isUserAction is false, then it function will report progress to the first background worker.
                    if (isUserAction == false)
                        BackgroundWorker.ReportProgress(85, "Downloading update...");

                    while (this.downloadcontrol.webClient.IsBusy == true) { }
                }
                else if (this.Version.CompareTo(NewVersion) == 0 && isUserAction == true)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.ControlsPanel.Controls.Clear();
                        this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusPanel_Paint);

                         this.Hide();
                    }));

                    MessageBox.Show("No update is available.");
                }
            }
            catch (Exception e) 
            { 
                MessageBox.Show(e.ToString()); 
            }
        }

        private void PreparingProgram()
        {
            Program.main.MethodsList = MethodsProvider.InitializeMethods();
            Program.main.VersionLabel.Text = String.Format("Version {0} beta", this.Version);

            Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.Ready, false);
        }

        public string Version
        {
            get
            {
                FileVersionInfo Program = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
                return string.Format("{0}.{1}.{2}", Program.ProductMajorPart, Program.ProductMinorPart, Program.ProductBuildPart);
            }
        }
    }
}

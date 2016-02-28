using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace PawnPlus.Core.Views
{
    /// <summary>
    /// Interaction logic for Launcher.xaml
    /// </summary>
    public partial class Splash : Window
    {
        private BackgroundWorker backgroundWorker = new BackgroundWorker();

        public Splash()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.backgroundWorker.WorkerReportsProgress = true;

            this.backgroundWorker.DoWork += backgroundWorker_DoWork;
            this.backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;

            this.backgroundWorker.RunWorkerAsync();

            this.progressBar.Visibility = Visibility.Collapsed;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.backgroundWorker.ReportProgress(1, "Starting up..");
            Thread.Sleep(100);

            this.backgroundWorker.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 100)
            {
                this.Close();
            }
            else
            {
                this.statusLabel.Content = e.UserState.ToString();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}

using System.Windows.Forms;

namespace PawnPlus.Core.UserControls
{
    public partial class DownloadControl : UserControl
    {
        public DownloadControl()
        {
            InitializeComponent();
        }

        public void SetProcessValue(int Value)
        {
            this.downloadProcessBar.Value = Value;
        }

        public void SetPercentage(int Percentage)
        {
            this.downloadPercentageLabel.Text = Percentage.ToString() + "%";
        }

        public void SetDownloadedMegaBytes(string Text)
        {
            this.downloadedMBLabel.Text = Text;
        }
    }
}

using System.Windows;

namespace PawnPlus.Core.Views
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.lineLabel.Visibility = Visibility.Hidden;
            this.columnLabel.Visibility = Visibility.Hidden;
        }
    }
}

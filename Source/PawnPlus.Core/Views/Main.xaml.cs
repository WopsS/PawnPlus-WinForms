using System.Windows;
using PawnPlus.Core.Classes;

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

            States.UIStates &= ~PawnFlags.Saved;
        }

        #region Menu
        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            if ((States.UIStates & PawnFlags.Saved) == PawnFlags.Saved)
            {
                MessageBoxResult result = MessageBox.Show("Close PawnPlus without saving ?", "Close PawnPlus", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    // Open Savedialog
                }
            }
            else
            {
                this.Close();
            }
        }

        private void Menu_File_Click_File(object sender, RoutedEventArgs e)
        {
            States.UIStates |= PawnFlags.Saved;
        }
        #endregion
    }
}

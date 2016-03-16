using System.Windows;
using PawnPlus.Core.Classes;
using System.Windows.Input;

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

        private void PawnPlus_Loaded(object sender, RoutedEventArgs e)
        {
            this.lineLabel.Visibility = Visibility.Hidden;
            this.columnLabel.Visibility = Visibility.Hidden;

            States.UIStates &= ~PawnFlags.Closeing;
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
                    States.UIStates |= PawnFlags.Closeing;
                    this.Close();
                }
                else
                {
                    // Open Savedialog
                }
            }
            else
            {
                States.UIStates |= PawnFlags.Closeing;
                this.Close();
            }
        }

        private void Menu_File_Click_File(object sender, RoutedEventArgs e)
        {
            States.UIStates |= PawnFlags.Saved;
        }

        private void PawnPlus_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((States.UIStates & PawnFlags.Closeing) != PawnFlags.Closeing)
            {
                if ((States.UIStates & PawnFlags.Saved) == PawnFlags.Saved)
                {
                    MessageBoxResult result = MessageBox.Show("Close PawnPlus without saving ?", "Close PawnPlus", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        // Open Savedialog
                    }
                }
                else
                {
                    return;
                }
            }
        }

        #endregion

        private void PawnPlus_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) // Is Alt key pressed
            {
                if (Keyboard.IsKeyDown(Key.S))
                {
                    States.UIStates |= PawnFlags.Saved;
                    MessageBox.Show("lel");
                }
            }
        }
        
    }
}

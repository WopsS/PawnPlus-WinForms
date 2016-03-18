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
        private States State;

        public Main()
        {
            InitializeComponent();
        }

        private void PawnPlus_Loaded(object sender, RoutedEventArgs e)
        {
            this.lineLabel.Visibility = Visibility.Hidden;
            this.columnLabel.Visibility = Visibility.Hidden;

            State = new States();
            State.Init_UIStates();
        }

        #region Menu

        private void Menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            if ((States.UIStates & PawnFlags.Saved) != PawnFlags.Saved && (States.UIStates & PawnFlags.FileOpen) == PawnFlags.FileOpen)
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

        private void PawnPlus_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if ((States.UIStates & PawnFlags.Closeing) != PawnFlags.Closeing)
            {
                if ((States.UIStates & PawnFlags.Saved) != PawnFlags.Saved && (States.UIStates & PawnFlags.FileOpen) == PawnFlags.FileOpen)
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
            if ((States.UIStates & PawnFlags.FileOpen) == PawnFlags.FileOpen)
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) // Is Controlkey pressed
                {
                    if (Keyboard.IsKeyDown(Key.S))
                    {
                        States.UIStates |= PawnFlags.Saved;
                    }
                }
            }
        }

        private void Menu_File_Click_Project(object sender, RoutedEventArgs e)
        {
            State.FileActive();
        }

        private void Menu_File_Click_File(object sender, RoutedEventArgs e)
        {
            State.FileActive();
        }

        private void Menu_Open_Click_Project(object sender, RoutedEventArgs e)
        {
            State.FileActive();
        }

        private void Menu_Open_Click_File(object sender, RoutedEventArgs e)
        {
            State.FileActive();
        }
    }
}

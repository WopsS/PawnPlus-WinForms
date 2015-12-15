using PawnPlus.Core.Forms;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus.Core.Exceptions
{
    public static class ExceptionHandler
    {
        public static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Write(LogType.Error, "UI Exception occured: {0}", Environment.NewLine + e.Exception.ToString());

            DialogResult dialogResult = DialogResult.OK;

            try
            {
                dialogResult = ShowException("UI Exception", e.Exception.Message, e.Exception.ToString(), true);
            }
            catch
            {
                try
                {
                    MessageBox.Show("Fatal Windows Forms Error", "Fatal Windows Forms Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                }
                finally
                {
                    Environment.Exit(1);
                }
            }

            if (dialogResult == DialogResult.Abort)
            {
                Environment.Exit(1);
            }
        }

        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Write(LogType.Error, "Unhandled Exception occured: {0}", Environment.NewLine + e.ExceptionObject.ToString());

            try
            {
                Exception exception = (Exception)e.ExceptionObject;
                ShowException("Unhandled Exception", exception.Message, exception.ToString(), false);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }

        private static DialogResult ShowException(string title, string message, string stackTrace, bool continueButton)
        {
            return (new ExceptionForm(title, message, stackTrace, continueButton)).ShowDialog();
        }
    }
}

using PawnPlus.Core.Forms;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus.Core.Exceptions
{
    public enum ExceptionType
    {
        Handled,
        Unhandled
    }

    public static class ExceptionHandler
    {
        public static void HandledException(Exception exception)
        {
            Logger.Write(LogType.Error, Localization.Text_HandledExceptionOccurred, Environment.NewLine + exception.ToString());
            ShowException(ExceptionType.Handled, Localization.Name_HandledException, exception.Message, exception.ToString(), false);
        }

        public static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Logger.Write(LogType.Error, Localization.Text_UIException, Environment.NewLine + e.Exception.ToString());

            DialogResult dialogResult = DialogResult.OK;

            try
            {
                dialogResult = ShowException(ExceptionType.Unhandled, Localization.Name_UIException, e.Exception.Message, e.Exception.ToString(), true);
            }
            catch
            {
                try
                {
                    MessageBox.Show(Localization.Text_FatalWindowsForms, Localization.Text_FatalWindowsForms, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
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
            Logger.Write(LogType.Error, Localization.Text_UnhandledExceptionOccured, Environment.NewLine + e.ExceptionObject.ToString());

            try
            {
                Exception exception = (Exception)e.ExceptionObject;
                ShowException(ExceptionType.Unhandled, Localization.Name_UnhandledException, exception.Message, exception.ToString(), false);
            }
            catch (Exception)
            {
                Environment.Exit(1);
            }
        }

        private static DialogResult ShowException(ExceptionType type, string title, string message, string stackTrace, bool continueButton)
        {
            return (new ExceptionForm(type, title, message, stackTrace, continueButton)).ShowDialog();
        }
    }
}

using PawnPlus.Core;
using PawnPlus.Core.Exceptions;
using PawnPlus.Core.Forms;
using PawnPlus.Properties;
using System;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace PawnPlus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler.UIThreadException);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler.UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Localization.Culture = new CultureInfo(Settings.Default.Language);

            Launcher launcher = new Launcher();
            launcher.ShowDialog();

            if (launcher.IsSafe == true)
            {
                launcher.Dispose();
                Application.Run(new Main());
            }
        }
    }
}

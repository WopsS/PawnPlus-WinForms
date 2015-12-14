using PawnPlus.Properties;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace PawnPlus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Language.Manager.Set(new CultureInfo(Settings.Default.Language));
            }
            catch (Exception)
            {
                Language.Manager.Set(new CultureInfo("en-US"));

                // TODO: Write the exception to log file.
            }

            Launcher launcher = new Launcher();
            Application.Run(launcher);

            if (launcher.IsSafe == true)
            {
                launcher.Dispose();
                Application.Run(new Main());
            }
        }
    }
}

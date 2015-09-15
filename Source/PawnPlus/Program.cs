using PawnPlus.Language;
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
                LanguageManager.SetLanguage(new CultureInfo(Settings.Default.Language));
            }
            catch (Exception)
            {
                LanguageManager.SetLanguage(new CultureInfo("en-US"));
            }

            Launcher launcher = new Launcher();
            Application.Run(launcher);

            if (launcher.ClosedSafe() == true)
            {
                launcher.Dispose();
                Application.Run(new Main());
            }
        }
    }
}

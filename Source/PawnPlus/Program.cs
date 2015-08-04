using System;
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

            Launcher launcher = new Launcher();
            launcher.Show();

            Application.Run();
        }
    }
}

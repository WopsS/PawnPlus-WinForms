using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PawnPlus
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static Launcher launcher;
        public static Main main;
        public static ProjectExplorer projectexplorer;
        public static Output output;
        public static CreateProject createproject;
        public static CreateFile createfile;
        public static Options options;
        public static About about;

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            launcher = new Launcher();
            main = new Main();
            projectexplorer = new ProjectExplorer();
            output = new Output();

            if (args.Count() > 0)
                Application.Run(new Launcher(args[0]));
            else
                Application.Run(new Launcher());
        }
    }
}

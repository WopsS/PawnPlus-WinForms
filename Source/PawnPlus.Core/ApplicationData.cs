using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public static class ApplicationData
    {
        /// <summary>
        /// Path for application in "%appdata%" directory.
        /// </summary>
        public static string AppData { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus");

        /// <summary>
        /// Entry program.
        /// </summary>
        public static Assembly Entry { get; } = Assembly.GetEntryAssembly();

        /// <summary>
        /// Path for PAWN's include directory.
        /// </summary>
        public static string IncludesDirectory { get; } = Path.Combine(AppData, "Pawn", "include");

        /// <summary>
        /// Path for application in "My Documents" directory.
        /// </summary>
        public static string MyDocumentsDirectory { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PawnPlus");

        /// <summary>
        /// Path for PAWN directory.
        /// </summary>
        public static string PawnDirectory { get; } = Path.Combine(AppData, "Pawn");

        /// <summary>
        /// Path to plugins directory.
        /// </summary>
        public static string PluginsPath { get; } = Path.Combine(Application.StartupPath, "plugins");

        /// <summary>
        /// Version of the program.
        /// </summary>
        public static string Version { get; } = string.Format("{0}.{1}.{2}-beta1", Entry.GetName().Version.Major, Entry.GetName().Version.Minor, Entry.GetName().Version.Build);

        static ApplicationData()
        {
            if (Directory.Exists(AppData) == false)
            {
                Directory.CreateDirectory(AppData);
            }

            if (Directory.Exists(IncludesDirectory) == false)
            {
                Directory.CreateDirectory(IncludesDirectory);
            }

            if (Directory.Exists(MyDocumentsDirectory) == false)
            {
                Directory.CreateDirectory(MyDocumentsDirectory);
            }

            if (Directory.Exists(PluginsPath) == false)
            {
                Directory.CreateDirectory(PluginsPath);
            }
        }
    }
}

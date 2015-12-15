using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PawnPlus.Core
{
    public static class ApplicationData
    {
        /// <summary>
        /// Path for application in "%appdata%" directory.
        /// </summary>
        public static string AppData { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus");

        /// <summary>
        /// Path for application in "My Documents" directory.
        /// </summary>
        public static string MyDocumentsPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PawnPlus");

        /// <summary>
        /// Path to plugins directory.
        /// </summary>
        public static string PluginsPath { get; } = Path.Combine(Environment.CurrentDirectory, "plugins");

        static ApplicationData()
        {
            if (Directory.Exists(AppData) == false)
            {
                Directory.CreateDirectory(AppData);
            }

            if (Directory.Exists(MyDocumentsPath) == false)
            {
                Directory.CreateDirectory(MyDocumentsPath);
            }

            if (Directory.Exists(PluginsPath) == false)
            {
                Directory.CreateDirectory(PluginsPath);
            }
        }
    }
}

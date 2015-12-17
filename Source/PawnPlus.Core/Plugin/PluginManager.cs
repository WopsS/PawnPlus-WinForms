using PawnPlus.Core.Events;
using PawnPlus.Core.Exceptions;
using PawnPlus.Core.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public static class PluginManager
    {
        /// <summary>
        /// Number of loaded plugins.
        /// </summary>
        public static int Count { get { return Plugins.Count; } }

        /// <summary>
        /// Event raised when a plugin is loaded.
        /// </summary>
        public static event EventHandler<PluginEventArgs> Loaded;

        /// <summary>
        /// List of all loded plugins.
        /// </summary>
        public static List<Plugin> Plugins { get; } = new List<Plugin>();

        internal static void Load()
        {
            foreach (string pluginPath in Directory.GetFiles(ApplicationData.PluginsPath, "*.dll"))
            {
                try
                {
                    Plugin plugin = new Plugin(pluginPath);

                    if (plugin.IsValid == true)
                    {
                        Plugins.Add(plugin);

                        string pluginLoaded = string.Format(Localization.Text_PluginLoaded, plugin.Name);

                        ((Launcher)Application.OpenForms["Launcher"]).backgroundWorker.ReportProgress(0, pluginLoaded);
                        Loaded(null, new PluginEventArgs(plugin.Assembly, plugin.Author, plugin.Description, plugin.Name));
                    }
                    else
                    {
                        throw new InvalidPluginException(Path.GetFileName(pluginPath));
                    }
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandledException(ex);
                }
            }
        }

        internal static void Unload()
        {
            Plugins.Clear();
        }
    }
}

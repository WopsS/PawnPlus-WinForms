using System;
using System.Reflection;

namespace PawnPlus.Core.Events
{
   /*
   * Format:
   *
   *   [access modifier] void [Name](object sender, PluginLoadedEventArgs e)
   *   {
   *       // Do something.
   *   }
   */

    public class PluginLoadedEventArgs : EventArgs
    {
        public string Author { get; }

        public string Description { get; }

        public string Name { get; }

        public Assembly Plugin { get; }

        public PluginLoadedEventArgs(Assembly plugin, string author, string description, string name)
        {
            this.Author = author;
            this.Description = description;
            this.Name = name;
            this.Plugin = plugin;
        }
    }
}

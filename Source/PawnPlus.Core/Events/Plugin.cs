using System;
using System.Reflection;

namespace PawnPlus.Core.Events
{
    public delegate void PluginLoaded(Assembly plugin, PluginLoadedEventArgs e);

    public class PluginLoadedEventArgs : EventArgs
    {
        public string Author { get; }

        public string Description { get; }

        public string Name { get; }

        public PluginLoadedEventArgs(string author, string description, string name)
        {
            this.Author = author;
            this.Description = description;
            this.Name = name;
        }
    }
}

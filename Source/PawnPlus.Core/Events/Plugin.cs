using System;
using System.Reflection;

namespace PawnPlus.Core.Events
{
    public class PluginEventArgs : EventArgs
    {
        public string Author { get; }

        public string Description { get; }

        public string Name { get; }

        public Assembly Plugin { get; }

        public PluginEventArgs(Assembly plugin, string author, string description, string name)
        {
            this.Author = author;
            this.Description = description;
            this.Name = name;
            this.Plugin = plugin;
        }
    }
}

using System;
using System.Reflection;

namespace PawnPlus.Core.Events
{
    public class PluginEventArgs : EventArgs
    {
        public virtual string Author { get; }

        public virtual string Description { get; }

        public virtual string Name { get; }

        public virtual Assembly Plugin { get; }

        public PluginEventArgs(Assembly plugin, string author, string description, string name)
        {
            this.Author = author;
            this.Description = description;
            this.Name = name;
            this.Plugin = plugin;
        }
    }
}

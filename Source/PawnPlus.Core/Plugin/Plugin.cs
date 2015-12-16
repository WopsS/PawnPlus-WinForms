using System;
using System.Reflection;

namespace PawnPlus.Core
{
    public class Plugin
    {
        /// <summary>
        /// Plugin's assembly.
        /// </summary>
        public virtual Assembly Assembly { get; }

        /// <summary>
        /// Name of the plugin's author.
        /// </summary>
        public virtual string Author { get; }

        /// <summary>
        /// Description of the plugin.
        /// </summary>
        public virtual string Description { get; }

        /// <summary>
        /// Plugin's display name.
        /// </summary>
        public virtual string Name { get; }

        /// <summary>
        /// If the plugin is for our program this will be true, false otherwise.
        /// </summary>
        internal virtual bool IsValid { get; } = false;

        private IPlugin instance = null;

        public Plugin(string path)
        {
            Type pluginType = typeof(IPlugin);
            this.Assembly = Assembly.LoadFrom(path);

            if (this.Assembly != null)
            {
                foreach (Type type in this.Assembly.GetTypes())
                {
                    if (type.IsInterface == false && type.IsAbstract == false)
                    {
                        if (type.GetInterface(pluginType.FullName) != null)
                        {
                            this.instance = (IPlugin)Activator.CreateInstance(type, new object[] { });

                            this.Author = this.instance.Author;
                            this.Description = this.instance.Description;
                            this.Name = this.instance.Name;

                            // Mark the plugin as valid for our program.
                            this.IsValid = true;
                        }
                    }
                }
            }
        }
    }
}

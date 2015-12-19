using System;

namespace PawnPlus.Core.Events
{
    public class ProjectEventArgs : EventArgs
    {
        public virtual bool Handled { get; set; }

        public virtual string Name { get; }

        public virtual string Path { get; }

        public ProjectEventArgs(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}

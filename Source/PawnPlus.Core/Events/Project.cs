using System;

namespace PawnPlus.Core.Events
{
    public delegate void ProjectLoaded(Project sender, ProjectEventArgs e);

    public delegate void ProjectClosed(Project sender, ProjectEventArgs e);

    public class ProjectEventArgs : EventArgs
    {
        public virtual string Name { get; }

        public virtual string Path { get; }

        public ProjectEventArgs(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }
    }
}

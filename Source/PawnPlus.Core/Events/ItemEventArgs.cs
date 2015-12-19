using System;

namespace PawnPlus.Core.Events
{
    public class ItemEventArgs : EventArgs
    {
        public virtual string Path { get; }

        public ItemEventArgs(string path)
        {
            this.Path = path;
        }
    }
}

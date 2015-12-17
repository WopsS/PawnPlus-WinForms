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

    public class ItemRenamedEventArgs : EventArgs
    {
        public virtual string NewValue { get; }

        public virtual string OldValue { get; }

        public ItemRenamedEventArgs(string oldValue, string newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}

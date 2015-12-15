using System;

namespace PawnPlus.Core.Events
{
    public delegate void ItemAdded(object sender, ItemEventArgs e);

    public delegate void ItemDeleted(object sender, ItemEventArgs e);

    public class ItemEventArgs : EventArgs
    {
        public virtual string Path { get; }

        public ItemEventArgs(string path)
        {
            this.Path = path;
        }
    }

    public delegate void ItemRenamed(object sender, ItemRenamedEventArgs e);

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

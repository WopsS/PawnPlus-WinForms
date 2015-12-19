using System;

namespace PawnPlus.Core.Events
{
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

using System;

namespace PawnPlus.Core.Events
{
    /*
    * Format for "ItemAdded" and "ItemDeleted":
    *
    *   [access modifier] void [Name](object sender, ItemEventArgs e)
    *   {
    *       // Do something.
    *   }
    */

    public class ItemEventArgs : EventArgs
    {
        public virtual string Path { get; }

        public ItemEventArgs(string path)
        {
            this.Path = path;
        }
    }

    /*
    * Format for "ItemRenamed":
    *
    *   [access modifier] void [Name](object sender, ItemRenamedEventArgs e)
    *   {
    *       // Do something.
    *   }
    */

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

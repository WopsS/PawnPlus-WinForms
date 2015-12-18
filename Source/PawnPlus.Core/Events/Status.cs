using System;

namespace PawnPlus.Core.Events
{
    public class StatusChangedEventArgs : EventArgs
    {
        public virtual string OldText { get; }

        public virtual StatusType OldType { get; }

        public virtual string Text { get; }

        public virtual StatusType Type { get; }

        public StatusChangedEventArgs(string oldText, StatusType oldType, string text, StatusType type)
        {
            this.OldText = oldText;
            this.OldType = oldType;
            this.Text = text;
            this.Type = type;
        }
    }
}

using System;

namespace PawnPlus.Core.Events
{
    public class CaretPositionChangedArgs : EventArgs
    {
        public virtual int Column { get; }

        public virtual int Line { get; }

        public CaretPositionChangedArgs(int line, int column)
        {
            this.Column = column;
            this.Line = line;
        }
    }
}

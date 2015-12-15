using PawnPlus.Core.Forms;
using System;

namespace PawnPlus.Core.Events
{
    public delegate void CaretPositionChanged(Editor editor, CaretPositionChangedArgs e);

    public class CaretPositionChangedArgs : EventArgs
    {
        public int Column { get; }

        public int Line { get; }

        public CaretPositionChangedArgs(int line, int column)
        {
            this.Column = column;
            this.Line = line;
        }
    }
}

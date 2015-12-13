using PawnPlus.CodeEditor;
using System;

namespace PawnPlus.Core.Events.Caret
{
    public class PositionChangedArgs : EventArgs
    {
        public int Column { get; private set; }
        public int Line { get; private set; }

        public PositionChangedArgs(int line, int column)
        {
            this.Column = column;
            this.Line = line;
        }
    }

    public delegate void PositionChanged(Editor editor, PositionChangedArgs e);
}

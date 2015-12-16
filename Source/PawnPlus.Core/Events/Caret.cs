using System;

namespace PawnPlus.Core.Events
{
    /*
    * Format:
    *
    *   [access modifier] void [Name](object sender, CaretPositionChangedArgs e)
    *   {
    *       // Do something.
    *   }
    */

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

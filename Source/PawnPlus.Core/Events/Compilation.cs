using PawnPlus.Core.Forms;
using System;

namespace PawnPlus.Core.Events
{
    /*
    * Format:
    *
    *   [access modifier] void [Name](object sender, CompilationEventArgs e)
    *   {
    *       // Do something.
    *   }
    */

    public class CompilationEventArgs : EventArgs
    {
        public Editor Editor { get; }

        public string FileName { get; }

        public CompilationEventArgs(Editor editor, string fileName)
        {
            this.Editor = editor;
            this.FileName = fileName;
        }

        public CompilationEventArgs(string fileName)
        {
            this.FileName = fileName;
        }
    }
}

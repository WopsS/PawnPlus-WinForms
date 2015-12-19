using PawnPlus.Core.Forms;
using System;

namespace PawnPlus.Core.Events
{
    public class CompilationEventArgs : EventArgs
    {
        public virtual Editor Editor { get; }

        public virtual string FileName { get; }

        public virtual bool Handled { get; set; }

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

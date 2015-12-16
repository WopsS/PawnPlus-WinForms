using System.Collections.Generic;

namespace PawnPlus.Core
{
    public class CompilationError
    {
        public virtual string FileName { get; set; }

        public virtual string FilePath { get; set; }

        public virtual int Line { get; set; }

        public virtual string Message { get; set; }

        public override string ToString()
        {
            return string.Format("{0}({1}): {2}", FileName, Line, Message);
        }
    }

    public class CompilationResult
    {
        public virtual bool Successful { get; set; }

        public readonly List<CompilationError> Errors = new List<CompilationError>();

        public virtual string Output { get; set; }
    }
}

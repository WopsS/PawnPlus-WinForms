using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PawnPlus.Core
{
    public class CompileError
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

    public class CompileResult
    {
        public virtual bool Successful { get; set; }

        public readonly List<CompileError> Errors = new List<CompileError>();

        public virtual string Output { get; set; }
    }
}

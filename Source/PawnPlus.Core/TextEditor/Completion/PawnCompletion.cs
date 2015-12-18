using ICSharpCode.AvalonEdit.CodeCompletion;
using System.Collections.Generic;
using System.Linq;

namespace PawnPlus.Core.TextEditor.Completion
{
    public class PawnCompletion
    {
        public static List<ICompletionData> DefaultCompletion = new List<ICompletionData>();

        static PawnCompletion()
        {
            // TODO: Add completion methods here.
        }

        public PawnCompletion()
        {

        }

        public IList<ICompletionData> GetCompletions()
        {
            List<ICompletionData> result = DefaultCompletion.ToList();

            return result;
        }
    }
}

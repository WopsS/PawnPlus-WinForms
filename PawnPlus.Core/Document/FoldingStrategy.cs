using System.Collections.Generic;
using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Folding;

namespace PawnPlus.Core.Document
{
    public class CodeFoldingStrategy : IFoldingStrategy
    {
        public List<Fold> GenerateFolds(IDocument document, string fileName, object parseInformation)
        {
            List<Fold> folds = new List<Fold>();

            for (int offset = 0; offset < document.TextLength; ++offset)
            {
                char c = document.GetCharAt(offset);

                if (c == '{')
                {
                    int offsetOfClosingBracket = TextHelper.FindClosingBracket(document, offset + 1, '{', '}');

                    if (offsetOfClosingBracket > 0)
                    {
                        int length = offsetOfClosingBracket - offset + 1;
                        folds.Add(new Fold(document, offset, length, "{...}", false));
                    }
                }
            }

            return folds;
        }
    }
}

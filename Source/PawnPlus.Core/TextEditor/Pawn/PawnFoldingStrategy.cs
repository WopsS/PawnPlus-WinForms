using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System.Collections.Generic;

namespace PawnPlus.Core.TextEditor
{
    public class PawnFolding
    {
        public int FirstErrorOffset { get; set; }

        public List<NewFolding> Foldings { get; } = new List<NewFolding>();
    }

    public class PawnFoldingStrategy
    {
        private BracketSearcher bracketSearcher = new BracketSearcher();

        private PawnFolding foldings = new PawnFolding();

        private Stack<int> startOffsets = new Stack<int>();

        private int currentOffset = 0;

        public PawnFoldingStrategy()
        {   
        }

        public void UpdateFoldings(FoldingManager foldingManager, TextDocument textDocument)
        {
            CreateNewFoldings(textDocument);
            foldingManager.UpdateFoldings(this.foldings.Foldings, this.foldings.FirstErrorOffset);
        }

        private void CreateCommentFold(TextDocument textDocument)
        {
            this.startOffsets.Push(this.currentOffset - 1);

            for (; this.currentOffset < textDocument.TextLength; this.currentOffset++)
            {
                char character = textDocument.GetCharAt(this.currentOffset);

                if (character == '*' && textDocument.GetCharAt(++this.currentOffset) == '/')
                {
                    this.currentOffset++;
                    foldings.Foldings.Add(new NewFolding(startOffsets.Pop(), this.currentOffset) { Name = "/* ..." });

                    break;
                }
            }
        }

        private void CreateMethodFold(TextDocument textDocument)
        {
            // TODO: Find a way to fold just functions brackets.

            this.startOffsets.Push(this.currentOffset);

            this.bracketSearcher.OpeningBrackets = "{";
            this.bracketSearcher.ClosingBrackets = "}";

            BracketSearchResult result = this.bracketSearcher.SearchBracket(textDocument, this.currentOffset + 1);

            if (result != null)
            {
                int startOffset = startOffsets.Pop();

                // Skip empty spaces.
                for(int i = startOffset - 1; i > 0; i--)
                {
                    char character = textDocument.GetCharAt(i);

                    if (char.IsWhiteSpace(character) == false && character != '\n' && character != '\r')
                    {
                        startOffset = i + 1;
                        break;
                    }
                }

                this.foldings.Foldings.Add(new NewFolding(startOffset, result.ClosingBracketOffset + 1) { Name = "..." });
            }
        }

        private void CreateNewFoldings(TextDocument textDocument)
        {
            for (this.currentOffset = 0; this.currentOffset < textDocument.TextLength; this.currentOffset++)
            {
                char character = textDocument.GetCharAt(this.currentOffset);

                if (character == '{')
                {
                    this.CreateMethodFold(textDocument);
                }
                else if (character == '/' && ++this.currentOffset < textDocument.TextLength && textDocument.GetCharAt(this.currentOffset) == '*')
                {
                    this.CreateCommentFold(textDocument);
                }
            }

            this.foldings.Foldings.Sort((first, second) => first.StartOffset.CompareTo(second.StartOffset));
        }
    }
}

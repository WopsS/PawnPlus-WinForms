using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Indentation;
using ICSharpCode.AvalonEdit.Indentation.CSharp;
using System;

namespace PawnPlus.Core.TextEditor
{
    public class IndentationStrategy : DefaultIndentationStrategy
    {
        public string IndentationString { get; set; } = "\t";

        public IndentationStrategy()
        {
        }

        public IndentationStrategy(TextEditorOptions options)
        {
            this.IndentationString = options.IndentationString;
        }

        public void Indent(IDocumentAccessor document, bool keepEmptyLines)
        {
            if (document == null)
            {
                throw new ArgumentNullException("document");
            }
        }

        public override void IndentLine(TextDocument textDocument, DocumentLine line)
        {
            TextDocumentAccessor documentAccessor = new TextDocumentAccessor(textDocument, line.LineNumber, line.LineNumber);
            Indent(documentAccessor, false);

            if (documentAccessor.Text.Length == 0)
            {
                base.IndentLine(textDocument, line);
            }
        }

        public override void IndentLines(TextDocument document, int beginLine, int endLine)
        {
            Indent(new TextDocumentAccessor(document, beginLine, endLine), true);
        }
    }
}

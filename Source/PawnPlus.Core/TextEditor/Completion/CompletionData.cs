using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Windows.Media;

namespace PawnPlus.Core.TextEditor.Completion
{
    public class CompletionData
    {
        public ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }

        public object Content
        {
            get { return this.Text; }
        }

        public object Description
        {
            get { return "Description for " + this.Text; }
        }

        public CompletionData(string text)
        {
            this.Text = text;
        }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}

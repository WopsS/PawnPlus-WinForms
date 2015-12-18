using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PawnPlus.Core.TextEditor.Completion
{

    public class CompletionData : ICompletionData
    {
        public ImageSource Image
        {
            get
            {
                return null;
            }
        }

        public string Text { get; private set; }

        public object Content
        {
            get { return this.Text; }
        }

        public object Description { get; private set; }

        public double Priority
        {
            get
            {
                return 1.0;
            }
        }

        public CompletionData(string text, string description)
        {
            this.Text = text;
            this.Description = description;
        }


        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}

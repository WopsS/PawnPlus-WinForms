// Copyright (c) 2014 AlphaSierraPapa for the SharpDevelop Team
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this
// software and associated documentation files (the "Software"), to deal in the Software
// without restriction, including without limitation the rights to use, copy, modify, merge,
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
// to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using System;
using System.Windows.Media;

namespace PawnPlus.Core.Extensibility
{
    public class SelectionColorizer : ColorizingTransformer
    {
        public static readonly Brush DefaultBackground = new SolidColorBrush(Color.FromArgb(0, 51, 153, 255));
        public static readonly Brush DefaultForeground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

        private TextArea textArea;

        public SelectionColorizer(TextArea textArea)
        {
            if (textArea == null)
            {
                throw new ArgumentNullException("textArea");
            }

            this.textArea = textArea;
        }

        protected override void Colorize(ITextRunConstructionContext context)
        {
            int lineStartOffset = context.VisualLine.FirstDocumentLine.Offset;
            int lineEndOffset = context.VisualLine.LastDocumentLine.Offset + context.VisualLine.LastDocumentLine.TotalLength;

            foreach (SelectionSegment segment in textArea.Selection.Segments)
            {
                int segmentStart = segment.StartOffset;
                int segmentEnd = segment.EndOffset;

                if (segmentEnd <= lineStartOffset || segmentStart >= lineEndOffset)
                {
                    continue;
                }

                int startColumn;

                if (segmentStart < lineStartOffset)
                {
                    startColumn = 0;
                }
                else
                {
                    startColumn = context.VisualLine.ValidateVisualColumn(segment.StartOffset, segment.StartVisualColumn, this.textArea.Selection.EnableVirtualSpace);
                }

                int endColumn;

                if (segmentEnd > lineEndOffset)
                {
                    endColumn = this.textArea.Selection.EnableVirtualSpace ? int.MaxValue : context.VisualLine.VisualLengthWithEndOfLineMarker;
                }
                else
                {
                    endColumn = context.VisualLine.ValidateVisualColumn(segment.EndOffset, segment.EndVisualColumn, this.textArea.Selection.EnableVirtualSpace);
                }

                ChangeVisualElements(startColumn, endColumn, element =>
                {
                    element.TextRunProperties.SetForegroundBrush(DefaultForeground);
                    element.TextRunProperties.SetBackgroundBrush(DefaultBackground);
                });
            }
        }
    }
}

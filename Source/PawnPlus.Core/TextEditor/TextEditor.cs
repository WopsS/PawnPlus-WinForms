using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using PawnPlus.Core.Events;
using PawnPlus.Core.TextEditor.Completion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace PawnPlus.Core.TextEditor
{
    public class CodeTextEditor : ICSharpCode.AvalonEdit.TextEditor
    {
        /// <summary>
        /// Event raised when the caret position is changed.
        /// </summary>
        public static event EventHandler<CaretPositionChangedArgs> CaretPositionChanged;

        public BracketHighlightRenderer BracketHighlightRenderer { get; set; }

        public PawnCompletion Completion { get; set; } = new PawnCompletion();

        public FoldingManager FoldingManager { get; set; }

        public PawnFoldingStrategy FoldingStrategy { get; set; } = new PawnFoldingStrategy();

        protected BracketSearcher bracketSearcher = new BracketSearcher();

        protected CompletionWindow completionWindow;

        protected InsightWindow insightWindow;

        private string extension = string.Empty;

        public CodeTextEditor()
        {
            // TODO: Create indentation strategy.
            // TODO: Finish completion.

            this.ShowLineNumbers = true;
            this.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            this.FontSize = 12;
            this.Options.ConvertTabsToSpaces = true;

            this.TextArea.Caret.PositionChanged += OnCaretPositionChanged;
            //this.TextArea.TextEntering += OnTextEntering;
            //this.TextArea.TextEntered += OnTextEntered;

            this.TextArea.SelectionCornerRadius = 0;
            this.TextArea.TextView.LineTransformers.Add(new SelectionColorizer(this.TextArea));

            this.BracketHighlightRenderer = new BracketHighlightRenderer(this.TextArea.TextView);
            this.FoldingManager = FoldingManager.Install(this.TextArea);

            // Add CTRL+SPACE at command bindings.
            //RoutedCommand ctrlSpace = new RoutedCommand();
            //ctrlSpace.InputGestures.Add(new KeyGesture(Key.Space, ModifierKeys.Control));

            //this.CommandBindings.Add(new CommandBinding(ctrlSpace, this.OnCTRLSpaceCommand));
        }

        private void OnCaretPositionChanged(object sender, EventArgs e)
        {
            if (this.BracketHighlightRenderer != null)
            {
                BracketSearchResult bracketSearchResult = this.bracketSearcher.SearchBracket(this.Document, this.TextArea.Caret.Offset);
                this.BracketHighlightRenderer.SetHighlight(bracketSearchResult);
            }

            CaretPositionChanged(this, new CaretPositionChangedArgs(this.TextArea.Caret.Line, this.TextArea.Caret.Column));
        }

        private void OnTextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (char.IsLetterOrDigit(e.Text[0]) == false)
                {
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
        }

        private void OnTextEntered(object sender, TextCompositionEventArgs e)
        {
            if (this.extension == ".pwn" || this.extension == ".inc")
            {
                this.ShowCompletion(e.Text, false);
            }
        }

        public bool IsCaretInString()
        {
            DocumentLine line = this.Document.GetLineByOffset(this.CaretOffset);

            List<int> offsets = new List<int>();

            for (int i = line.Offset; i < line.EndOffset; i++)
            {
                char character = this.Document.GetCharAt(i);

                bool greater = i - 1 >= line.Offset;

                if (char.IsWhiteSpace(character) == false)
                {
                    switch (character)
                    {
                        case '"':
                        {
                            // Skip "\"".
                            if (greater == true && this.Document.GetCharAt(i - 1) == '\\')
                            {
                                continue;
                            }

                            offsets.Add(i);
                            break;
                        }
                        case '\'':
                        {
                            // Skip "\'".
                            if (greater == true && this.Document.GetCharAt(i - 1) == '\\')
                            {
                                continue;
                            }

                            offsets.Add(i);
                            break;
                        }
                    }
                }
            }

            // Let's check if our carret is in string.
            while (offsets.Count > 1)
            {
                int startOffset = offsets[0];
                int endOffset = offsets[1];

                offsets.RemoveRange(0, 2);

                if (startOffset < this.CaretOffset && endOffset >= this.CaretOffset)
                {
                    return true;
                }
            }

            // If we have just an offset there is a string without ending mark.
            return offsets.Count == 1;
        }

        /// <summary>
        /// Loads the text from the stream, auto-detecting the encoding.
        /// </summary>
        /// <param name="fileName">Path to the file.</param>
        public new void Load(string fileName)
        {
            this.extension = Path.GetExtension(fileName);
            this.Document.FileName = fileName;

            base.Load(fileName);
            this.FoldingStrategy.UpdateFoldings(this.FoldingManager, this.Document);
        }

        /// <summary>
        /// Saves the text to the stream.
        /// </summary>
        /// <param name="fileName">Path to the file.</param>
        public new void Save(string fileName)
        {
            // A work-around for Cyrillic.
            byte[] bytes = new byte[this.Text.Length * sizeof(char)];
            Buffer.BlockCopy(this.Text.ToCharArray(), 0, bytes, 0, bytes.Length);

            using (StreamReader reader = new StreamReader(new MemoryStream(bytes), System.Text.Encoding.Default))
            {
                this.Encoding = reader.CurrentEncoding;
            }

            this.extension = Path.GetExtension(fileName);
            this.Document.FileName = fileName;

            base.Save(fileName);
        }

        private void ShowCompletion(string text, bool controlSpace)
        {
            if (string.IsNullOrEmpty(Document.FileName) || this.IsCaretInString() == true)
            {
                return;
            }

            if (this.completionWindow == null)
            {
                this.completionWindow = new CompletionWindow(this.TextArea);
                this.completionWindow.CloseWhenCaretAtBeginning = controlSpace;
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;

                foreach (ICompletionData completion in this.Completion.GetCompletions().OrderBy(item => item.Text))
                {
                    data.Add(completion);
                }

                this.completionWindow.Show();
                this.completionWindow.Closed += (sender, e) => completionWindow = null;
            }

            this.completionWindow.StartOffset--;
        }

        private void OnCTRLSpaceCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.extension == ".pwn" || this.extension == ".inc")
            {
                this.ShowCompletion(null, true);
            }
        }
    }
}

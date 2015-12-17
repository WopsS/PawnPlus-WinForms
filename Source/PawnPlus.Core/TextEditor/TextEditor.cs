using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using PawnPlus.Core.Events;
using System;
using System.IO;
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

        public FoldingManager FoldingManager { get; set; }

        public PawnFoldingStrategy FoldingStrategy { get; set; } = new PawnFoldingStrategy();

        protected BracketSearcher bracketSearcher = new BracketSearcher();

        public CodeTextEditor()
        {
            // TODO: Create indentation strategy.

            this.ShowLineNumbers = true;
            this.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            this.FontSize = 12;
            this.Options.ConvertTabsToSpaces = true;

            this.TextArea.Caret.PositionChanged += OnCaretPositionChanged;

            this.TextArea.SelectionCornerRadius = 0;
            this.TextArea.TextView.LineTransformers.Add(new SelectionColorizer(this.TextArea));

            this.BracketHighlightRenderer = new BracketHighlightRenderer(this.TextArea.TextView);
            this.FoldingManager = FoldingManager.Install(this.TextArea);
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
        
        /// <summary>
        /// Loads the text from the stream, auto-detecting the encoding.
        /// </summary>
        /// <param name="fileName">Path to the file.</param>
        public new void Load(string fileName)
        {
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

            base.Save(fileName);
        }
    }
}

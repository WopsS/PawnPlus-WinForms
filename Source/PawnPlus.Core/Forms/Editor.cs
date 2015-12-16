using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using PawnPlus.Core.AddIns;
using PawnPlus.Core.Events;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core.Forms
{
    public partial class Editor : DockContent
    {
        public TextEditor TextEditor { get; private set; }

        /// <summary>
        /// Path to the file which is edited.
        /// Empty by default.
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            set
            {
                this.Text = Path.GetFileName(value);
                this.filePath = value;
            }
        }

        /// <summary>
        /// Get the 'modified' flag.
        /// </summary>
        public bool IsModified { get { return TextEditor.IsModified; } }

        public bool HasProject { get; set; }

        private BracketHighlightRenderer bracketHighlightRenderer;

        private BracketSearcher bracketSearcher = new BracketSearcher();

        private ElementHost elementHost = new ElementHost();

        private string filePath = string.Empty;

        public Editor()
        {
            InitializeComponent();

            this.TextEditor = new TextEditor();
        }

        private void CodeEditor_Load(object sender, EventArgs e)
        {
            this.elementHost.Dock = DockStyle.Fill;
            this.elementHost.Child = this.TextEditor;

            this.Controls.Add(this.elementHost);

            this.TextEditor.ShowLineNumbers = true;
            this.TextEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            this.TextEditor.FontSize = 12;
            this.TextEditor.Options.ConvertTabsToSpaces = true;

            this.TextEditor.Document.UpdateFinished += codeEditor_UpdateFinished;
            this.TextEditor.TextArea.Caret.PositionChanged += codeEditor_CaretPositionChanged;
            this.TextEditor.TextArea.SelectionChanged += codeEditor_SelectionChanged;

            // TODO: Create indentation strategy.

            string extenstion = Path.GetExtension(filePath);

            if (extenstion == ".pwn" || extenstion == ".inc")
            {
                Stream stream = null;

                try
                {
                    // Let's load the syntax hightlight.
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.PAWNSyntax.xml");

                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        stream = null;
                        this.TextEditor.SyntaxHighlighting = HighlightingLoader.Load(xmlReader, HighlightingManager.Instance);
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }

                this.bracketHighlightRenderer = new BracketHighlightRenderer(this.TextEditor.TextArea.TextView);

                FoldingManager foldingManager = FoldingManager.Install(TextEditor.TextArea);
                PawnFoldingStrategy foldingStrategy = new PawnFoldingStrategy();
                foldingStrategy.UpdateFoldings(foldingManager, TextEditor.Document);

                this.TextEditor.TextArea.IndentationStrategy = new IndentationStrategy(this.TextEditor.Options);
               
            }

            this.TextEditor.TextArea.SelectionCornerRadius = 0;
            this.TextEditor.TextArea.TextView.LineTransformers.Add(new SelectionColorizer(this.TextEditor.TextArea));

            EventStorage.AddListener<object, EventArgs>(EventKey.TextCopying, this.event_TextCopying);
            EventStorage.AddListener<object, EventArgs>(EventKey.TextCutting, this.event_TextCutting);

            ((IScrollInfo)this.TextEditor.TextArea).ScrollOwner.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            ((IScrollInfo)this.TextEditor.TextArea).ScrollOwner.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Workspace.CloseFile(this.FilePath);

            this.TextEditor.Document.UpdateFinished -= codeEditor_UpdateFinished;
            this.elementHost.Dispose();

            EventStorage.RemoveListener<object, EventArgs>(EventKey.TextCopying, this.event_TextCopying);
            EventStorage.RemoveListener<object, EventArgs>(EventKey.TextCutting, this.event_TextCutting);
        }

        private void codeEditor_CaretPositionChanged(object sender, EventArgs e)
        {
            if (this.bracketHighlightRenderer != null)
            {
                BracketSearchResult bracketSearchResult = this.bracketSearcher.SearchBracket(this.TextEditor.Document, this.TextEditor.TextArea.Caret.Offset);
                this.bracketHighlightRenderer.SetHighlight(bracketSearchResult);
            }

            EventStorage.Fire(EventKey.CaretPositionChanged, this, new CaretPositionChangedArgs(this.TextEditor.TextArea.Caret.Line, this.TextEditor.TextArea.Caret.Column));
        }

        private void codeEditor_SelectionChanged(object sender, EventArgs e)
        {
            this.TextEditor.ScrollTo(this.TextEditor.TextArea.Caret.Line, this.TextEditor.TextArea.Selection.EndPosition.Column);
            Workspace.CurrentEditor.TextEditor.TextArea.Caret.Column = this.TextEditor.TextArea.Selection.EndPosition.Column;
            Workspace.CurrentEditor.TextEditor.TextArea.Caret.BringCaretToView();
        }

        private void codeEditor_UpdateFinished(object sender, EventArgs e)
        {
            if (this.IsModified == false && this.Text[this.Text.Length - 1] == '*')
            {
                this.Text = this.Text.Remove(this.Text.Length - 1);
            }
            else if (this.IsModified == true && this.Text[this.Text.Length - 1] != '*')
            {
                this.Text += '*';
            }
        }

        private void event_TextCopying(object sender, EventArgs e)
        {
            if (this.IsActivated == true)
            {
                this.TextEditor.Copy();
            }
        }

        private void event_TextCutting(object sender, EventArgs e)
        {
            if (this.IsActivated == true)
            {
                this.TextEditor.Cut();
            }
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public void Open(string fileName)
        {
            this.FilePath = fileName;
            this.TextEditor.Load(fileName);

            IntPtr hIcon;

            if (fileName.Contains(".inc"))
            {
                hIcon = Properties.Resources.IncludeIcon.GetHicon();
            }
            else
            {
                hIcon = Properties.Resources.FileIcon.GetHicon();
            }

            this.Icon = Icon.FromHandle(hIcon);
        }

        /// <summary>
        /// Save file.
        /// </summary>
        /// <param name="fileName">Path where file should be saved.</param>
        public void Save(string fileName)
        {
            // A work-around for Cyrillic.
            byte[] bytes = new byte[this.TextEditor.Text.Length * sizeof(char)];
            Buffer.BlockCopy(this.TextEditor.Text.ToCharArray(), 0, bytes, 0, bytes.Length);

            using (StreamReader reader = new StreamReader(new MemoryStream(bytes), Encoding.Default))
            {
                this.TextEditor.Encoding = reader.CurrentEncoding;
            }

            this.FilePath = fileName;
            this.Text = Path.GetFileName(fileName);

            this.TextEditor.Save(this.FilePath);
        }

        /// <summary>
        /// Save file.
        /// </summary>
        public void Save()
        {
            this.Save(this.FilePath);
        }
    }
}

using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using PawnPlus.Core;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.CodeEditor
{
    public partial class Editor : DockContent
    {
        public readonly TextEditor codeEditor;

        /// <summary>
        /// Path to the file which is edited.
        /// Empty by default.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// Get the 'modified' flag.
        /// </summary>
        public bool IsModified { get { return codeEditor.IsModified; } }

        public bool IsProjectFile { get; set; }

        private ElementHost elementHost = new ElementHost();

        public Editor()
        {
            InitializeComponent();

            this.codeEditor = new TextEditor();
        }

        private void CodeEditor_Load(object sender, EventArgs e)
        {
            this.elementHost.Dock = DockStyle.Fill;
            this.elementHost.Child = this.codeEditor;

            this.Controls.Add(this.elementHost);

            this.codeEditor.ShowLineNumbers = true;
            this.codeEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            this.codeEditor.FontSize = 12;

            this.codeEditor.TextArea.Caret.PositionChanged += codeEditor_Caret_PositionChanged;
            this.codeEditor.Document.UpdateFinished += codeEditor_UpdateFinished;

            // TODO: Create folding and indentation strategy.

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.CodeEditor.Resources.PAWNSyntax.xml")) // Let's load the syntax hightlight.
            {
                using (XmlReader xmlReader = XmlReader.Create(stream))
                {
                    this.codeEditor.SyntaxHighlighting = HighlightingLoader.Load(xmlReader, HighlightingManager.Instance);
                }
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            CEManager.Close(this.FilePath);
            this.codeEditor.TextArea.Caret.PositionChanged -= codeEditor_Caret_PositionChanged;
            this.codeEditor.Document.UpdateFinished -= codeEditor_UpdateFinished;

            this.elementHost.Dispose();
        }

        private void codeEditor_Caret_PositionChanged(object sender, EventArgs e)
        {
            if (CEManager.ActiveDocument == this) // Just to be sure about the position of the caret, maybe it will be changed somehow (but I don't think so).
            {
                StatusManager.SetLineColumn(this.codeEditor.TextArea.Caret.Line, this.codeEditor.TextArea.Caret.Column);
            }
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

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public void Open(string fileName)
        {
            this.FilePath = fileName;
            this.codeEditor.Load(fileName);
            this.Text = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Save file.
        /// </summary>
        /// <param name="fileName">Path where file should be saved.</param>
        public void Save(string fileName)
        {
            this.FilePath = fileName;
            this.Text = Path.GetFileName(fileName);

            this.codeEditor.Save(this.FilePath);
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

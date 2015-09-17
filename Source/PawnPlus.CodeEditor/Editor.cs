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

        public Editor()
        {
            InitializeComponent();

            this.codeEditor = new TextEditor();
        }

        private void CodeEditor_Load(object sender, EventArgs e)
        {
            ElementHost elementHost = new ElementHost();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = this.codeEditor;

            this.Controls.Add(elementHost);

            this.codeEditor.ShowLineNumbers = true;
            this.codeEditor.FontFamily = new System.Windows.Media.FontFamily("Consolas");
            this.codeEditor.FontSize = 12;

            this.codeEditor.TextArea.Caret.PositionChanged += codeEditor_Caret_PositionChanged;

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
            CEManager.CloseFile(this.FilePath);
            this.codeEditor.TextArea.Caret.PositionChanged -= codeEditor_Caret_PositionChanged;
        }


        private void codeEditor_Caret_PositionChanged(object sender, EventArgs e)
        {
            if(CEManager.ActiveDocument == this) // Just to be sure about the position of the caret, maybe it will be changed somehow (but I don't think so).
            {
                StatusManager.SetLineColumn(this.codeEditor.TextArea.Caret.Line, this.codeEditor.TextArea.Caret.Column);
            }
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        public void Open(string filePath)
        {
            this.FilePath = filePath;
            this.codeEditor.Load(filePath);
            this.Text = Path.GetFileName(filePath);
        }
    }
}

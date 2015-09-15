using ICSharpCode.AvalonEdit;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
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
        private string filePath = string.Empty;

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
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            CEManager.CloseFile(this.filePath);
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="FilePath">Path to the file.</param>
        public void Open(string FilePath)
        {
            this.filePath = FilePath;
            this.codeEditor.Load(FilePath);
            this.Text = Path.GetFileName(FilePath);
        }
    }
}

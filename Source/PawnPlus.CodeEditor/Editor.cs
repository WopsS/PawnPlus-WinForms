using ICSharpCode.AvalonEdit;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.CodeEditor
{
    public partial class Editor : DockContent
    {
        public readonly TextEditor codeEditor;

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
    }
}

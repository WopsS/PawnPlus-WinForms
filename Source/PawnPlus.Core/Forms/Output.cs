using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core.Forms
{
    public partial class Output : DockContent
    {
        public Output()
        {
            InitializeComponent();
        }

        private void Output_Load(object sender, EventArgs e)
        {
            // Translate controls.
            this.Text = Localization.Name_Output;
        }

        private void outBox_DoubleClick(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}+{END}");
            SendKeys.Flush();

            int lineIndex = this.outBox.GetLineFromCharIndex(this.outBox.SelectionStart);

            if (lineIndex < Workspace.LastCompilationResult.Errors.Count)
            {
                CompileError error = Workspace.LastCompilationResult.Errors[lineIndex];

                if (File.Exists(error.FilePath) == true)
                {
                    Workspace.OpenFile(error.FilePath, true);
                }

                Workspace.SetActiveEditor(error.FilePath, true);

                // Focus on the error line.
                int line = error.Line;

                Workspace.CurrentEditor.codeEditor.ScrollToLine(line);
                Workspace.CurrentEditor.codeEditor.TextArea.Caret.Line = line;
                Workspace.CurrentEditor.codeEditor.TextArea.Caret.BringCaretToView();
                Workspace.CurrentEditor.codeEditor.TextArea.Caret.Show();
            }
        }

        public void ClearText()
        {
            this.outBox.Clear();
        }

        public void SetText(string text, bool append)
        {
            if (append == false)
            {
                this.ClearText();
            }

            this.outBox.AppendText(text + Environment.NewLine);
        }
    }
}

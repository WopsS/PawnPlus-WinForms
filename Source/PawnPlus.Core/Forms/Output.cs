using PawnPlus.Core.Events;
using System;
using System.IO;
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

            Workspace.DocumentChanged += this.event_ActiveDocumentChanged;
            Compilation.Completed += this.event_CompilationCompleted;
            Compilation.Started += this.event_CompilationStarted;
        }

        private void Output_FormClosing(object sender, FormClosingEventArgs e)
        {
            Workspace.DocumentChanged -= this.event_ActiveDocumentChanged;
            Compilation.Completed -= this.event_CompilationCompleted;
            Compilation.Started -= this.event_CompilationStarted;
        }

        private void event_ActiveDocumentChanged(object arg1, EventArgs arg2)
        {
            this.ClearText();
        }

        private void event_CompilationCompleted(object sender, CompilationEventArgs e)
        {
            if (Workspace.Compilation.Result.Successful == true)
            {
                Status.Set(StatusType.Finish, StatusReset.FiveSeconds, Localization.Status_Compiled);
            }
            else
            {
                foreach (CompilationError error in Workspace.Compilation.Result.Errors)
                {
                    this.SetText(error.ToString(), true);
                }

                Status.Set(StatusType.Error, StatusReset.FiveSeconds, Localization.Status_CompiledError);

                // Add empty line because we have errors and we want a empty line to show the output.
                this.SetText(Environment.NewLine, true);
            }

            this.SetText(Workspace.Compilation.Result.Output, true);
        }

        private void event_CompilationStarted(object sender, CompilationEventArgs e)
        {
            this.ClearText();
        }

        private void outBox_DoubleClick(object sender, EventArgs e)
        {
            SendKeys.Send("{HOME}+{END}");
            SendKeys.Flush();

            int lineIndex = this.outBox.GetLineFromCharIndex(this.outBox.SelectionStart);

            if (lineIndex < Workspace.Compilation.Result.Errors.Count)
            {
                CompilationError error = Workspace.Compilation.Result.Errors[lineIndex];

                if (File.Exists(error.FilePath) == true)
                {
                    Workspace.OpenFile(error.FilePath, true);
                }

                Workspace.SetActiveEditor(error.FilePath, true);

                // Focus on the error line.
                int line = error.Line;

                Workspace.CurrentEditor.TextEditor.ScrollToLine(line);
                Workspace.CurrentEditor.TextEditor.TextArea.Caret.Line = line;
                Workspace.CurrentEditor.TextEditor.TextArea.Caret.BringCaretToView();
                Workspace.CurrentEditor.TextEditor.TextArea.Caret.Show();
            }
        }

        /// <summary>
        /// Copies the current selection to Clipboard.
        /// The action will be performed if the current window is actived.
        /// </summary>
        public void Copy()
        {
            if (this.IsActivated == true)
            {
                this.outBox.Copy();
            }
        }

        /// <summary>
        /// Removes the current selection and copies it to the clipboard.
        /// The action will be performed if the current window is actived.
        /// </summary>
        public void Cut()
        {
            if (this.IsActivated == true)
            {
                this.outBox.Cut();
            }
        }

        private void ClearText()
        {
            this.outBox.Clear();
        }

        private void SetText(string text, bool append)
        {
            if (append == false)
            {
                this.ClearText();
            }

            this.outBox.AppendText(text + Environment.NewLine);
        }
    }
}

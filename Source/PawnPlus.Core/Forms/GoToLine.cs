using System;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class GoToLine : Form
    {
        public GoToLine()
        {
            InitializeComponent();
        }

        private void GoToLine_Load(object sender, EventArgs e)
        {
            this.TranslateUI();

            // Set maximum numeric value for 'numericUpDown'.
            this.numericUpDown.Maximum = Workspace.CurrentEditor.TextEditor.LineCount;

            // Set current line for 'numericUpDown'.
            this.numericUpDown.Value = Workspace.CurrentEditor.TextEditor.TextArea.Caret.Line;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int line = Convert.ToInt32(this.numericUpDown.Value);

            Workspace.CurrentEditor.TextEditor.ScrollToLine(line);
            Workspace.CurrentEditor.TextEditor.TextArea.Caret.Line = line;
            Workspace.CurrentEditor.TextEditor.TextArea.Caret.BringCaretToView();
            Workspace.CurrentEditor.TextEditor.TextArea.Caret.Show();

            this.Close();
        }

        private void TranslateUI()
        {
            this.Text = Localization.Name_GoToLine;
            this.lineNumberLabel.Text = string.Format(Localization.Text_GoTo, Workspace.CurrentEditor.TextEditor.LineCount);
            this.cancelButton.Text = Localization.Text_Cancel;
            this.okButton.Text = Localization.Text_OK;
        }
    }
}

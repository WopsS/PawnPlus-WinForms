using PawnPlus.CodeEditor;
using PawnPlus.Language;
using System;
using System.Windows.Forms;

namespace PawnPlus
{
    public partial class GoToLine : Form
    {
        public GoToLine()
        {
            InitializeComponent();
        }

        private void GoToLine_Load(object sender, EventArgs e)
        {
            // Translate controls.
            this.Text = Translation.Name_GoToLine;
            this.lineNumberLabel.Text = string.Format(Translation.Text_GoTo, CEManager.ActiveDocument.codeEditor.LineCount);
            this.cancelButton.Text = Translation.Text_Cancel;
            this.okButton.Text = Translation.Text_OK;

            // Set maximum numeric value for 'numericUpDown'.
            this.numericUpDown.Maximum = CEManager.ActiveDocument.codeEditor.LineCount;

            // Set current line for 'numericUpDown'.
            this.numericUpDown.Value = CEManager.ActiveDocument.codeEditor.TextArea.Caret.Line;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            int line = Convert.ToInt32(this.numericUpDown.Value);

            CEManager.ActiveDocument.codeEditor.ScrollToLine(line);
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.Line = line;
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.BringCaretToView();
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.Show();

            this.Close();
        }
    }
}

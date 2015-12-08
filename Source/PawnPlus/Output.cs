using PawnPlus.CodeEditor;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class Output : DockContent
    {
        public Output()
        {
            InitializeComponent();
        }

        private void outBox_DoubleClick(object sender, EventArgs e)
        {
            RichTextBox richTextBox = ((RichTextBox)sender);

            SendKeys.Send("{HOME}+{END}");
            SendKeys.Flush();

            string SelectedText = this.outBox.SelectedText;

            Match match = Regex.Match(SelectedText, @"(.+)\((.+)\)\s:");

            if (File.Exists(match.Groups[1].ToString()) == true)
            {
                CEManager.Open(match.Groups[1].ToString(), true);
            }

            CEManager.SetActiveDocument(match.Groups[1].ToString(), true);

            // Focus on the error line.

            int line = Convert.ToInt32(match.Groups[2].ToString());

            CEManager.ActiveDocument.codeEditor.ScrollToLine(line);
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.Line = line;
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.BringCaretToView();
            CEManager.ActiveDocument.codeEditor.TextArea.Caret.Show();
        }

        public void ClearText()
        {
            this.outBox.Clear();
        }

        public void SetText(string text, bool append)
        {
            if(append == false)
            {
                this.ClearText();
            }

            this.outBox.AppendText(text + Environment.NewLine);
        }
    }
}

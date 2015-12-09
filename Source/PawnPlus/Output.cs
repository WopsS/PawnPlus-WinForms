using PawnPlus.CodeEditor;
using PawnPlus.Project;
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

            Match match = Regex.Match(SelectedText, @"(.+)\\(.+)\((.+)\)\s:");

            string filePath = match.Groups[2].ToString();

            // Check if the file is an include.
            if (Path.GetExtension(filePath) == ".inc")
            {
                if (ProjectManager.IsOpen == true && File.Exists(Path.Combine(ProjectManager.Path, "includes", filePath)) == true) // Check if the "includes" folder from poject constains this include.
                {
                    filePath = Path.Combine(ProjectManager.Path, "includes", filePath);
                }
                else // Check PAWN "includes" folder.
                {
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Pawn", "include", filePath);

                    if(File.Exists(filePath) == false)
                    {
                        return;
                    }
                }
            }

            if (File.Exists(filePath) == true)
            {
                CEManager.Open(filePath, true);
            }

            CEManager.SetActiveDocument(filePath, true);

            // Focus on the error line.

            int line = Convert.ToInt32(match.Groups[3].ToString());

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

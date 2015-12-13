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

            if (string.IsNullOrEmpty(SelectedText) == true || string.IsNullOrWhiteSpace(SelectedText) == true)
            {
                return;
            }

            Match match = Regex.Match(SelectedText, @"(.+)\((.+)\)\s:");

            string filePath = match.Groups[1].ToString();

            // Check if the file is an include.
            if (Path.GetExtension(filePath) == ".inc")
            {
                filePath = match.Groups[1].ToString();

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
            else
            {
                filePath = match.Groups[1].ToString();

                // Check if we have a file in project with that name.
                if (ProjectManager.IsOpen == true)
                {
                    if (File.Exists(Path.Combine(ProjectManager.Path, "filterscripts", filePath)) == true)
                    {
                        filePath = Path.Combine(ProjectManager.Path, "filterscripts", filePath);
                    }
                    else if (File.Exists(Path.Combine(ProjectManager.Path, "gamemodes", filePath)) == true)
                    {
                        filePath = Path.Combine(ProjectManager.Path, "gamemodes", filePath);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (File.Exists(filePath) == false) // If we don't have it, let's search through our open documents.
                {
                    bool found = false;

                    foreach (Editor editor in CEManager.ToList().Values)
                    {
                        if(Path.GetFileName(editor.FilePath) == filePath)
                        {
                            filePath = editor.FilePath;
                            found = true;

                            break;
                        }
                    }

                    // Still not found? Then let's stop the execution of the function to prevent an exception.
                    if(found == false)
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

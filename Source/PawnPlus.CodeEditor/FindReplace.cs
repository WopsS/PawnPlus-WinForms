using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using PawnPlus.Language;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.CodeEditor
{
    public partial class FindReplace : Form
    {
        private bool findPageSize = false;

        public FindReplace()
        {
            InitializeComponent();
        }

        private void FindReplace_Load(object sender, EventArgs e)
        {
            // Translate controls.
            this.Text = Translation.Name_FindReplace;
            this.tabControl.TabPages[0].Name = Translation.Text_Find;
            this.tabControl.TabPages[1].Name = Translation.Text_Replace;
            this.findWhatLabel.Text = Translation.Text_FindWhat;
            this.findNextButton.Text = Translation.Text_FindNext;
            this.findPreviousButton.Text = Translation.Text_FindPrevious;
            this.replaceWhatLabel.Text = Translation.Text_FindWhat;
            this.replaceWithLabel.Text = Translation.Text_ReplaceWith;
            this.replaceButton.Text = Translation.Text_Replace;
            this.replaceAllButton.Text = Translation.Text_ReplaceAll;
            this.lookInLabel.Text = Translation.Text_LookIn;
            this.caseSentitiveCheckBox.Text = Translation.Text_CaseSensitive;
            this.wholeWordCheckBox.Text = Translation.Text_WholeWord;

            this.lookInComboBox.Items.Add(Translation.Text_CurrentDocument);
            this.lookInComboBox.Items.Add(Translation.Text_AllDocuments);
            this.lookInComboBox.SelectedIndex = 0;

            // Disable 'Look in' until it will be implemented.
            this.lookInComboBox.Enabled = false;

            // Call the event manually to set the correct size.
            this.tabControl_SelectedIndexChanged(this, new EventArgs());

            // TODO: Write option for 'Look in'.
        }

        private void FindReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the current tab is tab for finding.
            if (this.tabControl.SelectedIndex == 0)
            {
                if (findPageSize == false)
                {
                    this.Height -= 40;
                    this.tabControl.Height -= 40;
                    findPageSize = true;
                }
            }
            else
            {
                if (findPageSize == true)
                {
                    this.Height += 40;
                    this.tabControl.Height += 40;

                    findPageSize = false;
                }
            }

            this.optionPanel.Parent = this.tabControl.TabPages[this.tabControl.SelectedIndex];
        }

        private void findNextButton_Click(object sender, EventArgs e)
        {
            this.FindNext(this.findWhatText.Text, true);
        }

        private void findPreviousButton_Click(object sender, EventArgs e)
        {
            this.FindPrevious(this.findWhatText.Text, true);
        }

        private void replaceAllButton_Click(object sender, EventArgs e)
        {
            TextEditor editor = CEManager.ActiveDocument.codeEditor;

            while (this.FindNext(this.replaceWhatText.Text, false) == true)
            {
                editor.Document.Replace(editor.Text.IndexOf(this.replaceWhatText.Text, editor.SelectionStart, editor.SelectionLength), this.replaceWhatText.Text.Length, this.replaceWithText.Text);
            }
        }

        private void replaceButton_Click(object sender, EventArgs e)
        {
            if (this.FindNext(this.replaceWhatText.Text, false) == true)
            {
                TextEditor editor = CEManager.ActiveDocument.codeEditor;
                editor.Document.Replace(editor.Text.IndexOf(this.replaceWhatText.Text, editor.SelectionStart, editor.SelectionLength), this.replaceWhatText.Text.Length, this.replaceWithText.Text);
            }
        }

        public bool FindNext(string findText, bool scrollToCaret)
        {
            TextEditor editor = CEManager.ActiveDocument.codeEditor;
            string editorText = editor.Text;

            if(string.IsNullOrEmpty(findText) == true)
            {
                findText = this.findWhatText.Text;
            }

            if (this.caseSentitiveCheckBox.Checked == false)
            {
                editorText = editorText.ToLower();
                findText = findText.ToLower();
            }

            if (string.IsNullOrEmpty(findText) == true || string.IsNullOrEmpty(editorText) == true)
            {
                return false;
            }

            int offset = editor.CaretOffset + 1;
            int index = editorText.IndexOf(findText, offset > editor.Text.Length ? 0 : offset);

            if (index == -1)
            {
                index = editorText.IndexOf(findText, 0);
            }

            if (index > -1)
            {
                return MoveCaret(findText, index, scrollToCaret);
            }

            return false;
        }

        public bool FindPrevious(string findText, bool scrollToCaret)
        {
            TextEditor editor = CEManager.ActiveDocument.codeEditor;
            string editorText = editor.Text;

            if (string.IsNullOrEmpty(findText) == true)
            {
                findText = this.findWhatText.Text;
            }

            if (this.caseSentitiveCheckBox.Checked == false)
            {
                editorText = editorText.ToLower();
                findText = findText.ToLower();
            }

            if (string.IsNullOrEmpty(findText) == true || string.IsNullOrEmpty(editorText) == true)
            {
                return false;
            }

            int offset = editor.CaretOffset - 2;
            int index = editorText.LastIndexOf(findText, offset < 0 ? editor.Text.Length : offset);

            if (index == -1)
            {
                index = editorText.LastIndexOf(findText, editor.Text.Length);
            }

            if (index > -1)
            {
                return MoveCaret(findText, index, scrollToCaret);
            }

            return false;
        }

        /// <summary>
        /// Move caret to offset.
        /// </summary>
        /// <param name="findText">Text which was finded.</param>
        /// <param name="offset">Offset where the text is.</param>
        /// <param name="scrollToCaret">If true editor will scroll to caret.</param>
        /// <returns>Returns true if the caret was moved, false otherwise.</returns>
        /// <remarks>This function will return false if the finded text need to be 'whole word' and it isn't.</remarks>
        private bool MoveCaret(string findText, int offset, bool scrollToCaret)
        {
            TextEditor editor = CEManager.ActiveDocument.codeEditor;

            editor.Select(offset, findText.Length);

            // Check if we need the whole word, if we need it let's compare with the selection.
            if (this.wholeWordCheckBox.Checked == true && string.Compare(editor.SelectedText, findText) != 0)
            {
                editor.TextArea.ClearSelection();
                return false;
            }

            if (scrollToCaret == true)
            {
                DocumentLine documentLine = editor.TextArea.Document.GetLineByOffset(offset);
                editor.ScrollTo(documentLine.LineNumber, 0);
            }

            return true;
        }

        public void ShowFind(IWin32Window owner, string selectedText)
        {
            this.tabControl.SelectTab(0);
            this.findWhatText.Text = selectedText;

            // Check if the windows isn't visible to prevent exception.
            if (this.Visible == false)
            {
                this.Show(owner);
            }
        }

        public void ShowReplace(IWin32Window owner, string selectedText)
        {
            this.tabControl.SelectTab(1);
            this.replaceWhatText.Text = selectedText;

            // Check if the windows isn't visible to prevent exception.
            if (this.Visible == false)
            {
                this.Show(owner);
            }
        }
    }
}

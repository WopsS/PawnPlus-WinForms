using DigitalRune.Windows.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    public partial class GoToLine : Form
    {
        public GoToLine()
        {
            InitializeComponent();

        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(GoToLineText.Text) > Program.main.CodeEditors[Program.main.CodeEditorPath].CodeBox.Document.TotalNumberOfLines || Convert.ToInt32(GoToLineText.Text) < 1 || GoToLineText.Text.Length < 0)
                LineError.SetError(GoToLineText, "Inserted line is invalid");
            else if (GoToLineText.Text.Length > 0)
            {
                Program.main.CodeEditors[Program.main.CodeEditorPath].CodeBox.ActiveTextAreaControl.Caret.Position = new TextLocation(10000, Convert.ToInt32(GoToLineText.Text) - 1); // 10,000 for last column of row.
                Program.main.CodeEditors[Program.main.CodeEditorPath].CodeBox.Focus();
                this.Close();
            }
        }

        private void GoToLineText_TextChanged(object sender, EventArgs e)
        {
            long LongOut;

            if (!long.TryParse(GoToLineText.Text, out LongOut))
                GoToLineText.Clear();
        }

        private void GoToLineText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OkButton.Select();
                SendKeys.Send("{ENTER}");
            }
        }
    }
}

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
    public partial class FindReplace : Form
    {
        Point InitialPosition;
        Point MovedPosition;
        bool FormClicked = false;

        public FindReplace()
        {
            InitializeComponent();
            this.TransparencyKey = Color.AliceBlue;
            this.BackColor = Color.AliceBlue;
        }

        #region Move this form
        private void Find_MouseDown(object sender, MouseEventArgs e)
        {
            FormClicked = true;
            InitialPosition = Cursor.Position;
            MovedPosition = this.Location;
        }

        private void Find_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormClicked == true)
            {
                Point NewPoint = Point.Subtract(Cursor.Position, new Size(InitialPosition));
                this.Location = Point.Add(MovedPosition, new Size(NewPoint));
            }
        }

        private void Find_MouseUp(object sender, MouseEventArgs e)
        {
            FormClicked = false;
        }

        private void TabControl_MouseDown(object sender, MouseEventArgs e)
        {
            FormClicked = true;
            InitialPosition = Cursor.Position;
            MovedPosition = this.Location;
        }

        private void TabControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormClicked == true)
            {
                Point NewPoint = Point.Subtract(Cursor.Position, new Size(InitialPosition));
                this.Location = Point.Add(MovedPosition, new Size(NewPoint));
            }
        }

        private void TabControl_MouseUp(object sender, MouseEventArgs e)
        {
            FormClicked = false;
        }

        private void FindTab_MouseDown(object sender, MouseEventArgs e)
        {
            FormClicked = true;
            InitialPosition = Cursor.Position;
            MovedPosition = this.Location;
        }

        private void FindTab_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormClicked == true)
            {
                Point NewPoint = Point.Subtract(Cursor.Position, new Size(InitialPosition));
                this.Location = Point.Add(MovedPosition, new Size(NewPoint));
            }
        }

        private void FindTab_MouseUp(object sender, MouseEventArgs e)
        {
            FormClicked = false;
        }

        private void ReplaceTab_MouseDown(object sender, MouseEventArgs e)
        {
            FormClicked = true;
            InitialPosition = Cursor.Position;
            MovedPosition = this.Location;
        }

        private void ReplaceTab_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormClicked == true)
            {
                Point NewPoint = Point.Subtract(Cursor.Position, new Size(InitialPosition));
                this.Location = Point.Add(MovedPosition, new Size(NewPoint));
            }
        }

        private void ReplaceTab_MouseUp(object sender, MouseEventArgs e)
        {
            FormClicked = false;
        }
        #endregion

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void close2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void FindTab_Enter(object sender, EventArgs e)
        {
            this.Height = this.Height - 70;
        }

        private void ReplaceTab_Enter(object sender, EventArgs e)
        {
            this.Height = this.Height + 70;
        }

        private void findnext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FindWhat.Text))
                Program.main.CodeEditors[Program.main.CodeEditorPath].FindNextMatch(FindWhat.Text, true, casesensitive.Checked, matchwholeword.Checked);
        }

        private void ReplacePrevious_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FindWhat2.Text))
                Program.main.CodeEditors[Program.main.CodeEditorPath].Replace(FindWhat2.Text, ReplaceWithText.Text, false, true, casesensitive2.Checked, matchwholeword2.Checked);
        }

        private void ReplaceNext_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FindWhat2.Text))
                Program.main.CodeEditors[Program.main.CodeEditorPath].Replace(FindWhat2.Text, ReplaceWithText.Text, false, false, casesensitive2.Checked, matchwholeword2.Checked);
        }

        private void ReplaceAll_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FindWhat2.Text))
                Program.main.CodeEditors[Program.main.CodeEditorPath].Replace(FindWhat2.Text, ReplaceWithText.Text, true, false, casesensitive2.Checked, matchwholeword2.Checked);
        }

        private void FindTab_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                Program.main.CodeEditors[Program.main.CodeEditorPath].FindNextMatch(FindWhat.Text, true, casesensitive.Checked, matchwholeword.Checked);
        }

        private void FindWhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                findnext.Select();
                SendKeys.Send("{ENTER}");
            }
        }
    }
}

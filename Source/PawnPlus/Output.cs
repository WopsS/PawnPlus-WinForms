using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void Output_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.main.CloseApplication == false)
                e.Cancel = true;

            this.IsHidden = true;
            Program.main.outputToolStripMenuItem.Checked = false;
        }

        public void setOutputText(string Text, bool canClear)
        {
            if(canClear == true)
                this.OutputBox.Clear();

            if (Text.Length == 0)
                return;

            this.OutputBox.AppendText(Text);
        }

        private void OutputBox_DoubleClick(object sender, EventArgs e)
        {
            RichTextBox richTextBox = ((RichTextBox)sender);

            SendKeys.Send("{HOME}+{END}");
            SendKeys.Flush();

            string SelectedText = this.OutputBox.SelectedText;

            Match match = Regex.Match(SelectedText, @"(.+)\((.+)\)\s:");

            if (Program.main.CodeEditors.ContainsKey(match.Groups[1].ToString()) == true)
            {
                Program.main.CodeEditors[match.Groups[1].ToString()].Activate();
                Program.main.CodeEditors[match.Groups[1].ToString()].Select();
                Program.main.CodeEditors[match.Groups[1].ToString()].Focus();
            }
            else
            {
                try
                {
                    Program.main.OpenFile(match.Groups[1].ToString());
                }
                catch(Exception)
                { }
            }

            // TODO: Change to selected line error.

            //if (Program.main.CodeEditors.ContainsKey(match.Groups[1].ToString()) == true)
            //    Program.main.CodeEditors[match.Groups[1].ToString()].CodeBox.ActiveTextAreaControl.Caret.Position = new TextLocation(10000, Convert.ToInt32(match.Groups[2].ToString()) - 1);
        }
    }
}

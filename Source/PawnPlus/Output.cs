using PawnPlus.Core;
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

        public void SetOutputText(string Text, bool canClear)
        {
            if(canClear == true)
                this.OutputBox.Clear();

            if (Text.Length == 0)
                return;

            this.OutputBox.AppendText(Text);
            this.OutputBox.ScrollToCaret();
        }

        private void OutputBox_DoubleClick(object sender, EventArgs e)
        {
            RichTextBox richTextBox = ((RichTextBox)sender);

            SendKeys.Send("{HOME}+{END}");
            SendKeys.Flush();

            string SelectedText = this.OutputBox.SelectedText;

            Match match = Regex.Match(SelectedText, @"(.+)\((.+)\)\s:");

            if (ApplicationInformations.Informations.CodeEditors.ContainsKey(match.Groups[1].ToString()) == true)
            {
                ApplicationInformations.Informations.CodeEditors[match.Groups[1].ToString()].Activate();
                ApplicationInformations.Informations.CodeEditors[match.Groups[1].ToString()].Select();
                ApplicationInformations.Informations.CodeEditors[match.Groups[1].ToString()].Focus();
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

            if (ApplicationInformations.Informations.CodeEditors.ContainsKey(match.Groups[1].ToString()) == true)
            {
                try
                {
                    ApplicationInformations.Informations.CodeEditors[match.Groups[1].ToString()].ChangeCaretLine(Convert.ToInt32(match.Groups[2].ToString()) - 1);
                }
                catch (Exception)
                { }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
                this.OutputText.Clear();

            if (Text.Length == 0)
                return;

            this.OutputText.AppendText(Text);
        }
    }
}

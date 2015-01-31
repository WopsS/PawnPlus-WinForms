using PawnPlus.Core.Document;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class CodeEditor : DockContent
    {
        public string FilePath = null;

        public CodeEditor()
        {
            InitializeComponent();
        }

        private void CodeBox_Load(object sender, EventArgs e)
        {

        }

        private void CodeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (InitialContent != this.CodeBox.Document.TextContent && Program.main.CloseApplication == false)
            //{
            //    DialogResult dialogResult = new DialogResult();

            //    dialogResult = MessageBox.Show(this, "Do you want to save your changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        Program.main.SaveFile(this.Text);
            //    }
            //    else if (dialogResult == DialogResult.Cancel)
            //    {
            //        e.Cancel = true;

            //        return;
            //    }
            //}

            Program.main.CodeEditors.Remove(this.FilePath);
        }

        private void OnCaretPositionChanged(object sender, EventArgs eventArgs)
        {
           
            //Program.main.ChangeLineColumn(caret.Line, caret.Column);
        }

        private void DocumentTextChanged(object sender, EventArgs eventArgs)
        {
            this.checkInitialContent();
        }

        /// <summary>
        /// Check if the current text is same as original.
        /// </summary>
        public void checkInitialContent()
        {
            //if (this.InitialContent == this.CodeBox.Document.TextContent)
            //    this.DockHandler.TabText = this.DockHandler.TabText.Replace("*", String.Empty);
            //else
            //    this.DockHandler.TabText = this.DockHandler.TabText.Replace("*", String.Empty) + "*";
        }
    }
}

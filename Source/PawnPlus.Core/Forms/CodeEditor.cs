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

namespace PawnPlus.Core.Forms
{
    public partial class CodeEditor : DockContent
    {
        public CurrentFileType Informations { get; set; }

        public CodeEditor()
        {
            InitializeComponent();

            Informations = new CurrentFileType();

            this.CodeBox.ConfigurationManager.Language = "cpp";
            this.CodeBox.Lexing.Lexer = ScintillaNET.Lexer.Cpp;
            this.CodeBox.Lexing.LexerName = "cpp";
            this.CodeBox.Margins[2].Width = 12;
        }

        private void CodeEditor_Load(object sender, EventArgs e)
        {
            this.Informations.Lines = 0;
            ReadText();
        }

        private void CodeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO: Save file.

            ApplicationInformations.Informations.CodeEditors.Remove(this.Informations.Path);
        }

        private void CodeBox_TextChanged(object sender, EventArgs e)
        {
            if (this.Informations.Lines != this.CodeBox.Lines.Count)
            {
                this.CodeBox.Margins[0].Width = TextRenderer.MeasureText(this.CodeBox.Lines.Count.ToString(), this.CodeBox.Font).Width + 10;
                this.Informations.Lines = this.CodeBox.Lines.Count;
            }
        }

        /// <summary>
        /// Read text from a file.
        /// </summary>
        public void ReadText()
        {
            this.CodeBox.Text = Encoding.GetEncoding(1252).GetString(File.ReadAllBytes(this.Text));
            this.CodeBox.UndoRedo.EmptyUndoBuffer();
            this.CodeBox.Modified = false;

            this.CheckInitialContent();
        }

        /// <summary>
        /// Check if the current text is same as original.
        /// </summary>
        public void CheckInitialContent()
        {
            if (this.CodeBox.Text.Length == 0)
                return;

            //FileInfo fileInfo = new FileInfo(this.Text);

            //if(ApplicationInformations.Informations.CodeEditors[this.Text].Informations.fileInfo.LastWriteTime != fileInfo.LastWriteTime)
            //{
                
            //}
        }

        /// <summary>
        /// Change caret line.
        /// </summary>
        /// <param name="Line">New position.</param>
        public void ChangeCaretLine(int Line)
        {
            this.CodeBox.Caret.LineNumber = Line;
        }
    }
}

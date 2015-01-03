using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Completion;
using DigitalRune.Windows.TextEditor.Document;
using DigitalRune.Windows.TextEditor.Formatting;
using DigitalRune.Windows.TextEditor.Highlighting;
using DigitalRune.Windows.TextEditor.Insight;
using DigitalRune.Windows.TextEditor.Utilities;
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
        public string InitialContent = null, FilePath = null;

        public CodeEditor()
        {
            InitializeComponent();

            // Event for position change.
            this.CodeBox.ActiveTextAreaControl.Caret.PositionChanged += this.OnCaretPositionChanged;
            // Add * if text is different from original
            this.CodeBox.Document.DocumentChanged += this.DocumentTextChanged;

            HighlightingManager.Manager.AddSyntaxModeFileProvider(new CustomResourceSyntaxModeProvider());
        }

        private void CodeBox_Load(object sender, EventArgs e)
        {
            this.CodeBox.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("PAWN");
            this.CodeBox.Document.FormattingStrategy = new CSharpFormattingStrategy();

            this.CodeBox.Document.FoldingManager.FoldingStrategy = new CodeFoldingStrategy();
        }

        private void CodeEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (InitialContent != this.CodeBox.Document.TextContent && Program.main.CloseApplication == false)
            {
                DialogResult dialogResult = new DialogResult();

                dialogResult = MessageBox.Show(this, "Do you want to save your changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                    Program.main.SaveFile(this.Text);
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;

                    return;
                }
            }

            Program.main.CodeEditors.Remove(this.FilePath);
        }

        private void OnCaretPositionChanged(object sender, EventArgs eventArgs)
        {
            Caret caret = (Caret)sender;

            Program.main.ChangeLineColumn(caret.Line, caret.Column);
        }

        private void DocumentTextChanged(object sender, EventArgs eventArgs)
        {
            this.checkInitialContent();

            if (this.UpdateFoldings.IsBusy == false)
                this.UpdateFoldings.RunWorkerAsync();
        }

        private void CodeBox_CompletionRequest(object sender, CompletionEventArgs e)
        {
            // Disabled until all SA-MP function is added.

            if (TextHelper.FindStringStart(this.CodeBox.Document, this.CodeBox.ActiveTextAreaControl.Caret.Offset) != -1 && TextHelper.FindStringStart(this.CodeBox.Document, this.CodeBox.ActiveTextAreaControl.Caret.Offset) != -1)
                return;

            if (e.Key == '\0')
                this.CodeBox.ShowCompletionWindow(new CodeCompletionDataProvider(), e.Key, false);
            else if (char.IsLetter(e.Key))
                this.CodeBox.ShowCompletionWindow(new CodeCompletionDataProvider(), e.Key, true);

            this.checkInitialContent();
        }

        private void CodeBox_InsightRequest(object sender, InsightEventArgs e)
        {
                this.CodeBox.ShowInsightWindow(new MethodInsight());
        }

        private void UpdateFoldings_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);
            this.CodeBox.Document.FoldingManager.UpdateFolds();
        }
        
        public void FindPrevious(string Text, bool IsFind, bool CaseSensitive, bool MatchWord)
        {
            if (string.IsNullOrEmpty(Text))
                return;

            CodeBox.Select();

            IDocument CodeBoxFind = CodeBox.Document;

            if (CaseSensitive == true)
            {
                int offset = CodeBoxFind.TextContent.LastIndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                if (offset >= 0)
                {
                    TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                    TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                    string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                    if (String.Compare(RequestText.Trim(), Text.Trim()) == 0 && IsFind == true)
                    {
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                        CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                    }
                    else if (String.Compare(RequestText.Trim(), Text.Trim()) == 0 && IsFind == false)
                    {
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                        CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                    }
                }
            }
            else if (MatchWord == true)
            {
                bool OnlyWords = false;

                Regex Check = new Regex("^[a-zA-Z ]+$");

                if (IsFind == true)
                {
                    if (Check.IsMatch(Text))
                        OnlyWords = true;
                    else
                        OnlyWords = false;

                    if (OnlyWords == true)
                    {
                        int offset = CodeBoxFind.TextContent.LastIndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                        if (offset >= 0)
                        {
                            TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                            TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                            CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                            string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

                            if (String.Compare(RequestText.Trim(), Text.Trim()) == 0)
                                CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                        }
                    }
                }
                else
                {
                    if (Check.IsMatch(Text))
                        OnlyWords = true;
                    else
                        OnlyWords = false;

                    if (OnlyWords == true)
                    {
                        int offset = CodeBoxFind.TextContent.LastIndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                        if (offset >= 0)
                        {
                            TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                            TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                            CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                            string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

                            if (String.Compare(RequestText.Trim(), Text.Trim()) == 0)
                                CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                        }
                    }
                }
            }
            else
            {
                int offset = CodeBoxFind.TextContent.ToLower().LastIndexOf(Text.ToLower().Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                if (offset >= 0)
                {
                    TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                    TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                    CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                }
            }
        }

        public void FindNextMatch(string Text, bool IsFind, bool CaseSensitive, bool MatchWord)
        {
            if (string.IsNullOrEmpty(Text))
                return;

            CodeBox.Select();

            IDocument CodeBoxFind = CodeBox.Document;

            if (CaseSensitive == true)
            {
                int offset = CodeBoxFind.TextContent.IndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                if (offset >= 0)
                {
                    TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                    TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                    string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                    if (String.Compare(RequestText.Trim(), Text.Trim()) == 0 && IsFind == true)
                    {
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                        CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                    }
                    else if (String.Compare(RequestText.Trim(), Text.Trim()) == 0 && IsFind == false)
                    {
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                        CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                    }
                }
            }
            else if (MatchWord == true)
            {
                bool OnlyWords = false;

                Regex Check = new Regex("^[a-zA-Z ]+$");

                if (IsFind == true)
                {
                    if (Check.IsMatch(Text))
                        OnlyWords = true;
                    else
                        OnlyWords = false;

                    if (OnlyWords == true)
                    {
                        int offset = CodeBoxFind.TextContent.IndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                        if (offset >= 0)
                        {
                            TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                            TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                            CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                            string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

                            if (String.Compare(RequestText.Trim(), Text.Trim()) == 0)
                                CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                        }
                    }
                }
                else
                {
                    if (Check.IsMatch(Text))
                        OnlyWords = true;
                    else
                        OnlyWords = false;

                    if (OnlyWords == true)
                    {
                        int offset = CodeBoxFind.TextContent.IndexOf(Text.Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                        CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                        if (offset >= 0)
                        {
                            TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                            TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                            CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);

                            string RequestText = CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;

                            if (String.Compare(RequestText.Trim(), Text.Trim()) == 0)
                                CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                        }
                    }
                }
            }
            else
            {
                int offset = CodeBoxFind.TextContent.ToLower().IndexOf(Text.ToLower().Trim(), CodeBox.ActiveTextAreaControl.TextArea.Caret.Offset);
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                if (offset >= 0)
                {
                    TextLocation startLocation = CodeBoxFind.OffsetToPosition(offset);
                    TextLocation endLocation = CodeBoxFind.OffsetToPosition(offset + Text.Trim().Length);
                    CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startLocation, endLocation);
                    CodeBox.ActiveTextAreaControl.Caret.Position = endLocation;
                }
            }
        }

        public void Replace(string Text, string ReplaceText, bool replaceAll, bool ReplacePrevious, bool CaseSensitive, bool MatchWord)
        {
            if (ReplacePrevious == false)
            {
                if (string.IsNullOrEmpty(Text))
                    return;

                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                int offset;

                if (!replaceAll)
                {
                    if (!CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                        FindNextMatch(Text, false, CaseSensitive, MatchWord);

                    try
                    {
                        offset = CodeBox.Document.PositionToOffset(CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.Selections[0].StartPosition);

                        if (offset > 0)
                            CodeBox.Document.Replace(offset, Text.Trim().Length, ReplaceText.Trim());
                    }
                    catch { }
                }
                else
                {
                    FindNextMatch(Text, false, CaseSensitive, MatchWord);

                    while (CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                    {
                        offset = CodeBox.Document.PositionToOffset(CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.Selections[0].StartPosition);

                        if (offset > 0)
                            CodeBox.Document.Replace(offset, Text.Trim().Length, ReplaceText.Trim());

                        FindNextMatch(Text, false, CaseSensitive, MatchWord);
                    }
                }
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
            }

            if (ReplacePrevious == true)
            {
                if (string.IsNullOrEmpty(Text))
                    return;

                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();

                int offset;

                if (!CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.HasSomethingSelected)
                    FindPrevious(Text, false, CaseSensitive, MatchWord);

                try
                {
                    offset = CodeBox.Document.PositionToOffset(CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.Selections[0].StartPosition);

                    if (offset > 0)
                        CodeBox.Document.Replace(offset, Text.Trim().Length, ReplaceText.Trim());
                }
                catch { }
                CodeBox.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
            }
        }

        /// <summary>
        /// Check if the current text is same as original.
        /// </summary>
        public void checkInitialContent()
        {
            if (this.InitialContent == this.CodeBox.Document.TextContent)
                this.DockHandler.TabText = this.DockHandler.TabText.Replace("*", String.Empty);
            else
                this.DockHandler.TabText = this.DockHandler.TabText.Replace("*", String.Empty) + "*";
        }
    }
}

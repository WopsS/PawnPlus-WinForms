using PawnPlus.Core.Events;
using PawnPlus.Core.Forms;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PawnPlus.Core
{
    public enum CompilationStatus : byte
    {
        Canceled,
        Compiling,
        EditorNull,
        EmptyText,
        InvalidFile,
        NotRunning,
        Started
    }

    public class Compilation
    {
        public string FileName { get; private set; }

        public CompilationResult Result { get; private set; } = new CompilationResult();

        private BackgroundWorker backgroundWorker = new BackgroundWorker();

        private Main mainForm = (Main)Application.OpenForms["Main"];

        public Compilation()
        {
            backgroundWorker.WorkerSupportsCancellation = true;

            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string amxPath = Path.Combine(Path.GetDirectoryName(this.FileName), string.Format("{0}.amx", Path.GetFileNameWithoutExtension(this.FileName)));
            string arguments = string.Format("\"{0}\" -o\"{1}\" -;+ -(+ {2}", this.FileName, amxPath, PawnPlus.Properties.Settings.Default.Compiler_Arguments.ToString());

            // If project is opened let's add path to project's includes.
            if (Project.IsOpen == true)
            {
                arguments = string.Format("{0} -i\"{1}\"", arguments, Path.Combine(Workspace.Project.IncludesDirectory));
            }

            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(ApplicationData.AppData, "Pawn", "pawncc.exe");
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            while (process.HasExited == false)
            {
                if (this.backgroundWorker.CancellationPending == true)
                {
                    EventStorage.Fire(EventKey.CompilationCanceled, this, new CompilationEventArgs(this.FileName));

                    e.Cancel = true;
                    return;
                }
            }

            string[] errors = process.StandardError.ReadToEnd().Split(Environment.NewLine.ToCharArray());

            this.Result = new CompilationResult();

            foreach (string errorMessage in errors)
            {
                if (string.IsNullOrEmpty(errorMessage) == true || string.IsNullOrWhiteSpace(errorMessage) || errorMessage == "Compilation aborted.")
                {
                    continue;
                }

                Match match = Regex.Match(errorMessage, @"(.+)\((.+)\)\s:(.+)");

                string filePath = match.Groups[1].ToString();
                string fileName = filePath.Replace(string.Format("{0}\\", ApplicationData.IncludesDirectory), string.Empty);

                if (Project.IsOpen == true)
                {
                    fileName = fileName.Replace(string.Format("{0}\\", Workspace.Project.BaseDirectory), string.Empty);
                }

                this.Result.Errors.Add(new CompilationError() { FileName = fileName, FilePath = filePath, Line = Convert.ToInt32(match.Groups[2].ToString()), Message = match.Groups[3].ToString() });
            }

            this.Result.Successful = this.Result.Errors.Count == 0;
            this.Result.Output = process.StandardOutput.ReadToEnd();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Enable 'Save*' menu items.
            this.mainForm.saveToolStripMenuItem.Enabled = true;
            this.mainForm.savesAsToolStripMenuItem.Enabled = true;
            this.mainForm.saveAllToolStripMenuItem.Enabled = true;

            EventStorage.Fire(EventKey.CompilationCompleted, this, new CompilationEventArgs(this.FileName));
        }

        public CompilationStatus Cancel()
        {
            if (this.backgroundWorker.IsBusy == false)
            {
                return CompilationStatus.NotRunning;
            }

            EventStorage.Fire(EventKey.CompilationCanceling, this, new CompilationEventArgs(this.FileName));

            this.backgroundWorker.CancelAsync();
            return CompilationStatus.Canceled;
        }

        public CompilationStatus Start(string fileName)
        {
            // Get instance of the editor by file name.
            Editor editor = Workspace.GetEditorByKey(fileName);

            EventStorage.Fire(EventKey.CompilationStarting, this, new CompilationEventArgs(editor, fileName));

            this.FileName = fileName;

            if (editor == null)
            {
                return CompilationStatus.EditorNull;
            }
            else if (this.backgroundWorker.IsBusy == true)
            {
                return CompilationStatus.Compiling;
            }
            else if (Path.GetExtension(FileName) != ".pwn")
            {
                return CompilationStatus.InvalidFile;
            }
            else if (string.IsNullOrEmpty(editor.TextEditor.Text) == true || string.IsNullOrWhiteSpace(editor.TextEditor.Text) == true)
            {
                Status.Set(StatusType.Error, StatusReset.FiveSeconds, Localization.Status_EmptyText);
                return CompilationStatus.EmptyText;
            }

            // Disable 'Save*' menu items.
            this.mainForm.saveToolStripMenuItem.Enabled = false;
            this.mainForm.savesAsToolStripMenuItem.Enabled = false;
            this.mainForm.saveAllToolStripMenuItem.Enabled = false;

            // Saving all files opened.
            Status.Set(StatusType.Warning, StatusReset.None, Localization.Status_SavingFiles);

            foreach (Editor currentEditor in Workspace.GetEditors().Values)
            {
                if (currentEditor.IsModified == true)
                {
                    currentEditor.Save();
                }
            }

            Status.Set(StatusType.Warning, StatusReset.None, Localization.Status_Compiling);
            this.backgroundWorker.RunWorkerAsync();

            EventStorage.Fire(EventKey.CompilationStarted, this, new CompilationEventArgs(editor, fileName));
            return CompilationStatus.Started;
        }
    }
}

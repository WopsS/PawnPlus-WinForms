using PawnPlus.Core.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core
{
    public static class Workspace
    {
        /// <summary>
        /// Instance of the current editor.
        /// </summary>
        public static Editor CurrentEditor { get; set; }

        /// <summary>
        /// Result about last compilation.
        /// </summary>
        public static CompileResult LastCompilationResult { get; set; }

        /// <summary>
        /// Instance of the current project, null if project is closed.
        /// </summary>
        public static Project Project { get; set; }

        private static Dictionary<string, Editor> editors = new Dictionary<string, Editor>();
        private static Main mainForm = (Main)Application.OpenForms["Main"];

        /// <summary>
        /// Close a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be closed.</param>
        public static void CloseFile(string filePath)
        {
            if (editors.ContainsKey(filePath) == false) // Is there a file with this path? No, let's stop execution.
            {
                return;
            }

            editors.Remove(filePath);
        }

        /// <summary>
        /// Close all files.
        /// </summary>
        /// <param name="projectFiles">If this is <c>true</c> it will close all files from the Workspace.Project.</param>
        /// <returns>Returns <c>true</c> if cancel button is not pressed, <c>false</c> otherwise.</returns>
        public static bool CloseAllFiles(bool projectFiles)
        {
            bool saveAll = false;

            foreach (Editor editor in editors.Values.ToList())
            {
                if (((projectFiles == true && editor.HasProject == true) || (projectFiles == false && editor.HasProject == false)))
                {
                    if (editor.IsModified == true)
                    {
                        if (saveAll == false)
                        {
                            DialogResult dialogResult = new DialogResult();

                            dialogResult = MessageBox.Show("Do you want to save all changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                            if (dialogResult == DialogResult.Yes)
                            {
                                saveAll = true;
                            }
                            else if (dialogResult == DialogResult.Cancel)
                            {
                                return true;
                            }
                        }

                        if (saveAll == true)
                        {
                            editor.Save();
                        }
                    }

                    editor.Close();
                }
            }

            return false;
        }

        /// <summary>
        /// Get a list of all opened editors.
        /// </summary>
        /// <returns>Returns a list of editors.</returns>
        public static Dictionary<string, Editor> GetEditors()
        {
            return editors;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be opened.</param>
        /// <param name="isInProject">If this is <c>true</c> it will mark file as a project file.</param>
        /// <returns>Returns an instance object of the <see cref="Editor"/> class or null if the file already exist.</returns>
        public static Editor OpenFile(string filePath, bool isInProject)
        {
            if (editors.ContainsKey(filePath) == true) // Is file already opened? Yes, let's stop execution.
            {
                return null;
            }

            Editor editor = new Editor();

            editor.Open(filePath);
            editor.HasProject = Project.Files.Contains(filePath);

            editors.Add(filePath, editor);
            editor.Show(mainForm.dockPanel, DockState.Document);

            return editor;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be opened.</param>
        /// <returns>Returns an instance object of the <see cref="Editor"/> class or null if the file already exist.</returns>
        public static Editor OpenFile(string filePath)
        {
            return OpenFile(filePath, false);
        }

        /// <summary>
        /// Set active document.
        /// </summary>
        /// <param name="filePath">Path of the document to be set as an active document.</param>
        /// <param name="focus">If true it will focus the document as active window.</param>
        public static void SetActiveEditor(string filePath, bool focus)
        {
            if (editors.ContainsKey(filePath) == true) // Set active document if the editor exist in list.
            {
                if (focus == true)
                {
                    editors[filePath].Activate();
                    editors[filePath].Select();
                    editors[filePath].Focus();
                }
            }
        }
    }
}

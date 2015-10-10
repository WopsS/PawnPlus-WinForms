using System.Collections.Generic;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.CodeEditor
{
    public static class CEManager
    {
        /// <summary>
        /// Instance of an active document.
        /// </summary>
        public static Editor ActiveDocument { get; private set; }

        private static Dictionary<string, Editor> editorList = new Dictionary<string, Editor>();
        private static DockPanel dockPanel;

        /// <summary>
        /// Constructor for the static class, it is called manually.
        /// </summary>
        /// <param name="dockPanel">Object of the dock panel.</param>
        public static void Construct(DockPanel dockPanel)
        {
            if (CEManager.dockPanel != null) // Prevent double construct.
            {
                return;
            }

            CEManager.dockPanel = dockPanel;
        }

        /// <summary>
        /// Close a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be closed.</param>
        public static void Close(string filePath)
        {
            if (editorList.ContainsKey(filePath) == false) // Is there a file with this path? No, let's stop execution.
            {
                return;
            }

            editorList.Remove(filePath);
        }

        /// <summary>
        /// Close all files.
        /// </summary>
        /// <param name="projectFiles">If this is <c>true</c> it will close all files from the project.</param>
        /// <returns>Returns <c>true</c> if cancel button is not pressed, <c>false</c> otherwise.</returns>
        public static bool CloseAll(bool projectFiles)
        {
            bool saveAll = false;

            foreach (Editor editor in editorList.Values)
            {
                if (((projectFiles == true && editor.IsProjectFile == true) || (projectFiles == false && editor.IsProjectFile == false)) && editor.IsModified == true)
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
            }

            return false;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be opened.</param>
        /// <param name="isInProject">If this is <c>true</c> it will mark file as a project file.</param>
        /// <returns>Returns an instance object of the <see cref="Editor"/> class or null if the file already exist.</returns>
        public static Editor Open(string filePath, bool isInProject)
        {
            if (editorList.ContainsKey(filePath) == true) // Is file already opened? Yes, let's stop execution.
            {
                return null;
            }

            Editor editor = new Editor();
            editor.Open(filePath);
            editor.IsProjectFile = isInProject;

            editorList.Add(filePath, editor);
            editor.Show(dockPanel, DockState.Document);

            return editor;
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be opened.</param>
        /// <returns>Returns an instance object of the <see cref="Editor"/> class or null if the file already exist.</returns>
        public static Editor Open(string filePath)
        {
            return Open(filePath, false);
        }

        /// <summary>
        /// Get a list of all opened editors.
        /// </summary>
        /// <returns>Returns a list of editors.</returns>
        public static Dictionary<string, Editor> ToList()
        {
            return editorList;
        }

        /// <summary>
        /// Set active document.
        /// </summary>
        /// <param name="editorForm">Returns instance of the <see cref="Editor"/> class which need to be set as an active document.</param>
        public static void SetActiveDocument(Editor editorForm)
        {
            if (editorList.ContainsValue(editorForm) == true) // Set active document if the editor exist in list.
            {
                ActiveDocument = editorForm;
            }
        }

        /// <summary>
        /// Set active document.
        /// </summary>
        /// <param name="filePath">Path of the document to be set as an active document.</param>
        /// <param name="focus">If true it will focus the document as active window.</param>
        public static void SetActiveDocument(string filePath, bool focus)
        {
            if (editorList.ContainsKey(filePath) == true) // Set active document if the editor exist in list.
            {
                SetActiveDocument(editorList[filePath]);

                if (focus == true)
                {
                    editorList[filePath].Activate();
                    editorList[filePath].Select();
                    editorList[filePath].Focus();
                }
            }
        }
    }
}

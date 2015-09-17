using System.Collections.Generic;
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
        public static void CloseFile(string filePath)
        {
            if (editorList.ContainsKey(filePath) == false) // Is there a file with this path? No, let's stop execution.
            {
                return;
            }

            editorList[filePath].Dispose();
            editorList.Remove(filePath);
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="filePath">Path of the file which need to be opened.</param>
        /// <returns>Returns an instance object of the <see cref="Editor"/> class or null if the file already exist.</returns>
        public static Editor OpenFile(string filePath)
        {
            if (editorList.ContainsKey(filePath) == true) // Is file already opened? Yes, let's stop execution.
            {
                return null;
            }

            Editor editor = new Editor();
            editor.Open(filePath);

            editorList.Add(filePath, editor);
            editor.Show(dockPanel, DockState.Document);

            return editor;
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

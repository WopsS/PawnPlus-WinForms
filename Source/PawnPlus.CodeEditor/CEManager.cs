using System.Collections.Generic;
using System.Windows;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.CodeEditor
{
    public static class CEManager
    {
        public static Editor activeDocument { get; private set; }
        private static Dictionary<string, Editor> editorList = new Dictionary<string, Editor>();
        private static DockPanel dockPanel;

        public static void Construct(DockPanel DockPanel)
        {
            dockPanel = DockPanel;
        }

        public static void CloseFile(string FilePath)
        {
            editorList[FilePath].Dispose();
            editorList.Remove(FilePath);
        }

        public static Editor OpenFile(string FilePath)
        {
            Editor editor = new Editor();
            editor.Open(FilePath);

            editorList.Add(FilePath, editor);
            editor.Show(dockPanel, DockState.Document);

            return editor;
        }

        public static void SetActiveDocument(Editor EditorForm)
        {
            if (editorList.ContainsValue(EditorForm) == true) // Set active document if the editor exist in list.
            {
                activeDocument = EditorForm;
            }
        }
    }
}

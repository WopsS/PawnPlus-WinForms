using PawnPlus.CodeEditor;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Project
{
    internal static class ProjectManager
    {
        public static readonly string Extension = ".pawnplusproject";
        public static string Name { get; private set; }
        public static string Path { get; private set; }

        private static TreeView treeView;

        public static void Construct(TreeView treeView)
        {
            ProjectManager.treeView = treeView;
        }

        public static void Close()
        {
            treeView.Nodes.Clear();

            // TODO: Close all files and ask for save if needed.
        }

        /// <summary>
        /// Open a project.
        /// </summary>
        /// <param name="projectPath">Path to the project file.</param>
        /// <returns>Returns true if the project was opened with success, false otherwise.</returns>
        public static bool Open(string projectPath)
        {
            if (projectPath == null || System.IO.Path.GetExtension(projectPath) != Extension)
                return false;

            bool isActive = false;
            string filePath = string.Empty, activeFile = string.Empty;

            using (XmlTextReader projectReader = new XmlTextReader(projectPath))
            {
                while (projectReader.Read())
                {
                    switch (projectReader.Name.ToString())
                    {
                        case "Name":
                            {
                                Name = projectReader.ReadString();
                                break;
                            }
                        case "File":
                            {
                                try
                                {
                                    isActive = Convert.ToBoolean(Convert.ToInt32(projectReader.GetAttribute("Active"))); // Get the attribute first.
                                    filePath = projectReader.ReadString();

                                    if (isActive == true)
                                    {
                                        activeFile = filePath;
                                    }

                                    CEManager.OpenFile(filePath);
                                }
                                catch(Exception)
                                {
                                    // File dosen't exist.
                                }

                                break;
                            }
                    }
                }
            }

            if(activeFile != string.Empty)
            {
                CEManager.SetActiveDocument(activeFile, true);
            }

            Path = System.IO.Path.GetDirectoryName(projectPath); // Set the project path.

            // TODO: Load project files in "project explorer" and set items from menu bar to active.
            LoadDirectory(Path);

            return true;
        }

        /// <summary>
        /// Load a directory and create a project tree.
        /// </summary>
        /// <param name="path">Path of the project.</param>
        private static void LoadDirectory(string path)
        {
            if (treeView == null)
            {
                return;
            }

            treeView.Nodes.Clear();

            treeView.Nodes.Add(CreateNode(new DirectoryInfo(path)));
            treeView.Nodes[0].Expand();
        }

        /// <summary>
        /// Create a node of a directory.
        /// </summary>
        /// <param name="directoryInfo"><see cref="DirectoryInfo"/> of the directory.</param>
        /// <returns>Returns <see cref="TreeNode"/> of the project.</returns>
        private static TreeNode CreateNode(DirectoryInfo directoryInfo)
        {
            TreeNode treeNode;

            if (directoryInfo.Name == System.IO.Path.GetFileName(Path))
            {
                treeNode = new TreeNode(Name, 0, 0);
            }
            else
            {
                treeNode = new TreeNode(directoryInfo.Name, 1, 1);
            }

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                if ((directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) // Check if current folder is hidden.
                {
                    treeNode.Nodes.Add(CreateNode(directory));
                }
            }

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Extension == ".exe" || fileInfo.Extension == ".amx" || fileInfo.Extension == ".sql" || fileInfo.Extension == ".dll" || fileInfo.Extension == ".rec" || fileInfo.Extension == ".so" || 
                    fileInfo.Extension == Extension || fileInfo.Name == "server-readme.txt" || fileInfo.Name == "samp-license.txt")
                {
                    continue;
                }

                if (fileInfo.Name.Contains(".inc"))
                {
                    treeNode.Nodes.Add(fileInfo.FullName, fileInfo.Name, 4, 4);
                }
                else
                {
                    treeNode.Nodes.Add(fileInfo.FullName, fileInfo.Name, 3, 3);
                }
            }

            return treeNode;
        }
    }
}

using PawnPlus.CodeEditor;
using PawnPlus.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PawnPlus.Project
{
    public struct CompiledInformation
    {
        public string Errors { get; set; }

        public bool HasErrors { get { return Errors.Length > 0; } }

        public string Output { get; set; }
    }

    public static class ProjectManager
    {
        public static readonly string Extension = ".pawnplusproject";
        public static bool IsOpen { get; private set; }
        public static string Name { get; private set; }
        public static string Path { get; private set; }
        public static CompiledInformation LastCompilation = new CompiledInformation();

        private static TreeView treeView;
        private static string xmlPath = string.Empty;

        /// <summary>
        /// Add project item to explorer.
        /// </summary>
        /// <param name="type">Type of the item.</param>
        /// <param name="path">Path of the item in project.</param>
        public static void Add(TreeNodeType type, string path)
        {
            // Check if the path is from project.
            if(path.Substring(0, Path.Length) != Path)
            {
                return;
            }

            short imageIndex = 1;

            string name = System.IO.Path.GetFileName(path);
            string extension = System.IO.Path.GetExtension(path);

            // Check extension to know image index.
            if (extension == ".inc")
            {
                imageIndex = 4;
            }
            else if (extension == ".pwn")
            {
                imageIndex = 3;
            }

            // Create TreeView path of the folder.
            string directory = System.IO.Path.Combine(Name, System.IO.Path.GetDirectoryName(path.Remove(0, Path.Length + 1)));

            TreeNode parentNode = TreeNodeHelper.GetNodeByPath(treeView.Nodes, directory);

            if (parentNode != null)
            {
                // Add the node to TreeView.
                TreeNode childNode = parentNode.Nodes.Add(path, name, imageIndex, imageIndex);
                childNode.Tag = type;

                // Now let's sort the TreeView.
                treeView.Sort();

                if (parentNode.IsExpanded == false)
                {
                    parentNode.Expand();
                }
            }
            else
            {
                // TODO: Write the a message to log file.
            }
        }

        /// <summary>
        /// Constructor for the static class, it is called manually.
        /// </summary>
        /// <param name="treeView">Object for file list.</param>
        public static void Construct(TreeView treeView)
        {
            // Prevent double construct.
            if (ProjectManager.treeView != null)
            {
                return;
            }

            ProjectManager.treeView = treeView;
        }

        /// <summary>
        /// Close all project files.
        /// </summary>
        /// <returns>Returns <c>true</c> if cancel button is not pressed, <c>false</c> otherwise.</returns>
        public static bool Close()
        {
            bool cancelPressed = CEManager.CloseAll(true);

            if (cancelPressed == false)
            {
                try
                {
                    treeView.Nodes.Clear();

                    XmlDocument xmlFile = new XmlDocument();
                    xmlFile.LoadXml(File.ReadAllText(xmlPath));

                    XmlNode xmlDocument = xmlFile.DocumentElement;

                    // Let's delete the old 'File' nodes.
                    XmlNodeList xmlNodes = xmlFile.SelectNodes("//File");
                    
                    foreach(XmlNode xmlNode in xmlNodes)
                    {
                        xmlDocument.RemoveChild(xmlNode);
                    }

                    // Let's create new 'File' nodes.
                    XmlNode xmlElement;

                    // Create a list of editors, we will delete from it later.
                    List<Editor> editors = new List<Editor>();

                    // Push to list our project files.
                    foreach (Editor editor in CEManager.Get().Values)
                    {
                        if (editor.IsProjectFile == true)
                        {
                            editors.Add(editor);
                        }
                    }

                    // Save files and close them.
                    foreach (Editor editor in editors)
                    {
                        xmlElement = xmlFile.CreateElement("File");
                        xmlElement.InnerText = editor.FilePath;

                        if (editor == CEManager.ActiveDocument)
                        {
                            XmlNode xmlAttribute = xmlFile.CreateNode(XmlNodeType.Attribute, "Active", string.Empty);
                            xmlAttribute.Value = "1";

                            xmlElement.Attributes.SetNamedItem(xmlAttribute);
                        }

                        xmlDocument.AppendChild(xmlElement);

                        editor.Close();
                    }

                    // All done, let's save the XML.
                    xmlFile.Save(xmlPath);
                }
                catch (Exception)
                {
                    // TODO: Write the exception to log file.
                }

                IsOpen = false;
            }

            return cancelPressed;
        }

        /// <summary>
        /// Open a project.
        /// </summary>
        /// <param name="projectPath">Path to the project file.</param>
        /// <returns>Returns true if the project was opened with success, false otherwise.</returns>
        public static bool Open(string projectPath)
        {
            if (projectPath == null || System.IO.Path.GetExtension(projectPath) != Extension)
            {
                return false;
            }

            if (IsOpen == true)
            {
                Close();
            }

            xmlPath = projectPath;

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

                                    CEManager.Open(filePath, true);
                                }
                                catch(Exception)
                                {
                                    // File dosen't exist.
                                    // TODO: Write the exception to log file.
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

            LoadDirectory(Path);

            IsOpen = true;

            return true;
        }

        /// <summary>
        /// Load a directory and create a project tree.
        /// </summary>
        /// <param name="path">Path of the project.</param>
        public static void LoadDirectory(string path)
        {
            if (treeView == null)
            {
                return;
            }

            treeView.BeginUpdate();

            treeView.Nodes.Clear();

            TreeNode treeNode = CreateNode(new DirectoryInfo(path));
            treeNode.Tag = TreeNodeType.Root;

            treeView.Nodes.Add(treeNode);
            treeView.Nodes[0].Expand();

            treeView.EndUpdate();
        }

        /// <summary>
        /// Reload the project directory.
        /// </summary>
        public static void ReloadDirectory()
        {
            LoadDirectory(Path);
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

            TreeNode childNode = null;

            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                // Check if current folder is hidden or it's name is "plugins".
                if ((directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden && directory.Name != "plugins")
                {
                    childNode = CreateNode(directory);
                    childNode.Tag = TreeNodeType.Directory;

                    treeNode.Nodes.Add(childNode);
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
                    childNode = treeNode.Nodes.Add(fileInfo.FullName, fileInfo.Name, 4, 4);
                }
                else
                {
                    childNode = treeNode.Nodes.Add(fileInfo.FullName, fileInfo.Name, 3, 3);
                }

                childNode.Tag = TreeNodeType.File;
            }

            return treeNode;
        }
    }
}
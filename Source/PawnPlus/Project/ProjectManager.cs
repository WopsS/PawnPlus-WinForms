using PawnPlus.CodeEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PawnPlus.Project
{
    internal class CompiledInformation
    {
        public string Errors { get; set; }

        public bool HasErrors { get { return Errors.Length > 0; } }

        public string Output { get; set; }
    }

    internal static class ProjectManager
    {
        public static readonly string Extension = ".pawnplusproject";
        public static bool IsOpen { get; private set; }
        public static string Name { get; private set; }
        public static string Path { get; private set; }
        public static CompiledInformation LastCompiled = new CompiledInformation();

        private static TreeView treeView;
        private static string xmlPath = string.Empty;
        private static Main mainForm = (Main)Application.OpenForms[0];

        public static void Construct(TreeView treeView)
        {
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

                    foreach (Editor editor in CEManager.ToList().Values)
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

            mainForm.SetMenuStatus(true, true, true);
            mainForm.SetFormName("PawnPlus - " + Name);

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
using PawnPlus.Core.Events;
using PawnPlus.Core.Exceptions;
using PawnPlus.Core.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace PawnPlus.Core
{
    public class Project
    {
        public static string Extension
        {
            get { return ".pawnplusproject"; }
        }

        public static bool IsOpen
        {
            get { return Workspace.Project != null; }
        }

        /// <summary>
        /// Event raised when the project is closed.
        /// </summary>
        public static event EventHandler<ProjectEventArgs> Closed;

        /// <summary>
        /// Event raised when the project is opened.
        /// </summary>
        public static event EventHandler<ProjectEventArgs> Loaded;

        public virtual string BaseDirectory
        {
            get { return Path.GetDirectoryName(this.FileName); }
        }

        public virtual string FileName { get; set; }

        public virtual string IncludesDirectory
        {
            get { return Path.Combine(this.BaseDirectory, "includes"); }
        }

        public virtual string Name
        {
            get { return Path.GetFileNameWithoutExtension(this.FileName); }
        }

        public readonly List<string> Directories = new List<string>();

        public readonly List<string> Files = new List<string>();

        public static Project Open(string fileName)
        {
            if (File.Exists(fileName) == false)
            {
                ExceptionHandler.HandledException(new FileNotFoundException(string.Format(Localization.Exception_ProjectFileNotFound, fileName), fileName));
                return null;
            }

            Project result = new Project(fileName);
            Workspace.Project = result;

            using (XmlTextReader xmlReader = new XmlTextReader(fileName))
            {
                string activeFile = string.Empty;

                while (xmlReader.Read())
                {
                    switch (xmlReader.Name.ToString())
                    {
                        case "File":
                        {
                            bool wasActive = Convert.ToBoolean(xmlReader.GetAttribute("Active")); // Get the attribute first.
                            string filePath = xmlReader.ReadString();

                            if (File.Exists(filePath) == true)
                            {
                                Workspace.OpenFile(filePath, true);

                                if (wasActive == true)
                                {
                                    activeFile = filePath;
                                }
                            }
                            else
                            {
                                Logger.Write(new FileNotFoundException(string.Format(Localization.Exception_FileNotFound, filePath), filePath));
                            }

                            break;
                        }
                    }
                }

                if (string.IsNullOrEmpty(activeFile) == false)
                {
                    Workspace.SetActiveEditor(activeFile, true);
                }
            }

            if (Loaded != null)
            {
                Loaded(null, new ProjectEventArgs(result.Name, result.BaseDirectory));
            }

            return result;
        }

        public Project(string fileName)
        {
            this.FileName = fileName;

            this.PreloadDirectory(new DirectoryInfo(this.BaseDirectory));

            Explorer.ItemAdded += this.event_ItemAdded;
            Explorer.ItemDeleted += this.event_ItemDeleted;
            Explorer.ItemRenamed += this.event_ItemRenamed;
        }

        ~Project()
        {
            Explorer.ItemAdded -= this.event_ItemAdded;
            Explorer.ItemDeleted -= this.event_ItemDeleted;
            Explorer.ItemRenamed -= this.event_ItemRenamed;
        }

        private void event_ItemAdded(object sender, ItemEventArgs e)
        {
            if (this.Directories.Contains(e.Path) == false && this.Files.Contains(e.Path) == false)
            {
                // If extension is null then it is a directory.
                if (string.IsNullOrEmpty(Path.GetExtension(e.Path)) == true)
                {
                    this.Directories.Add(e.Path);
                }
                else
                {
                    this.Files.Add(e.Path);
                }
            }
        }

        private void event_ItemDeleted(object sender, ItemEventArgs e)
        {
            if (this.Directories.Contains(e.Path) == true)
            {
                // Let's update directories path.
                foreach (string directoryPath in this.Directories.ToList())
                {
                    if (directoryPath.Length > e.Path.Length && directoryPath.Substring(0, e.Path.Length) == e.Path)
                    {
                        this.Directories.Remove(directoryPath);
                    }
                }

                // Let's update files path.
                foreach (string fileName in this.Files.ToList())
                {
                    if (fileName.Length > e.Path.Length && fileName.Substring(0, e.Path.Length) == e.Path)
                    {
                        this.Files.Remove(fileName);
                    }
                }

                this.Directories.Remove(e.Path);
            }
            else if (this.Files.Contains(e.Path) == true)
            {
                this.Files.Remove(e.Path);
            }
        }

        private void event_ItemRenamed(object sender, ItemRenamedEventArgs e)
        {
            if (this.Directories.Contains(e.OldValue) == true)
            {
                // Let's update directories path.
                foreach (string directoryPath in this.Directories.ToList())
                {
                    if (directoryPath.Length > e.OldValue.Length && directoryPath.Substring(0, e.OldValue.Length) == e.OldValue)
                    {
                        this.Directories.Remove(directoryPath);
                        this.Directories.Add(Path.Combine(e.NewValue, directoryPath.Remove(0, e.OldValue.Length + 1)));
                    }
                }

                // Let's update files path.
                foreach (string fileName in this.Files.ToList())
                {
                    if (fileName.Length > e.OldValue.Length && fileName.Substring(0, e.OldValue.Length) == e.OldValue)
                    {
                        this.Files.Remove(fileName);
                        this.Files.Add(Path.Combine(e.NewValue, fileName.Remove(0, e.OldValue.Length + 1)));
                    }
                }

                this.Directories.Remove(e.OldValue);
                this.Directories.Add(e.NewValue);
            }
            else if (this.Files.Contains(e.OldValue) == true)
            {
                this.Files.Remove(e.OldValue);
                this.Files.Add(e.NewValue);
            }
        }

        public bool Close()
        {
            bool result = false;

            try
            {
                XmlDocument xmlFile = new XmlDocument();
                xmlFile.LoadXml(File.ReadAllText(this.FileName));

                XmlNode xmlDocument = xmlFile.DocumentElement;

                // Let's delete the old 'File' nodes.
                XmlNodeList xmlNodes = xmlFile.SelectNodes("//File");

                foreach (XmlNode xmlNode in xmlNodes)
                {
                    xmlDocument.RemoveChild(xmlNode);
                }

                // Let's create new 'File' nodes.
                XmlNode xmlElement;

                // Create a list of editors, we will delete from it later.
                List<Editor> editors = new List<Editor>();

                // Push to list our project files.
                foreach (Editor editor in Workspace.GetEditors().Values)
                {
                    if (editor.HasProject == true)
                    {
                        editors.Add(editor);
                    }
                }

                // Save files and close them.
                foreach (Editor editor in editors.ToList())
                {
                    xmlElement = xmlFile.CreateElement("File");
                    xmlElement.InnerText = editor.FilePath;

                    if (editor == Workspace.CurrentEditor)
                    {
                        XmlNode xmlAttribute = xmlFile.CreateNode(XmlNodeType.Attribute, "Active", string.Empty);
                        xmlAttribute.Value = "true";

                        xmlElement.Attributes.SetNamedItem(xmlAttribute);
                    }

                    xmlDocument.AppendChild(xmlElement);
                }

                // All done, let's save the XML.
                xmlFile.Save(this.FileName);

                Workspace.Project = null;
                result = Workspace.CloseAllFiles(true);

                if (Closed != null)
                {
                    Closed(this, new ProjectEventArgs(this.Name, this.BaseDirectory));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandledException(ex);
            }

            return result;
        }

        private void PreloadDirectory(DirectoryInfo directoryInfo)
        {
            foreach (DirectoryInfo subDirectoryInfo in directoryInfo.GetDirectories())
            {
                if ((subDirectoryInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden && subDirectoryInfo.Name != "plugins")
                {
                    this.Directories.Add(subDirectoryInfo.FullName);
                    this.PreloadDirectory(subDirectoryInfo);
                }
            }

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                // Skip few files.
                if (fileInfo.Extension == ".exe" || fileInfo.Extension == ".amx" || fileInfo.Extension == ".sql" || fileInfo.Extension == ".dll" || fileInfo.Extension == ".rec" || fileInfo.Extension == ".so" ||
                    fileInfo.Extension == Extension || fileInfo.Name == "server-readme.txt" || fileInfo.Name == "samp-license.txt")
                {
                    continue;
                }

                this.Files.Add(fileInfo.FullName);
            }
        }
    }
}

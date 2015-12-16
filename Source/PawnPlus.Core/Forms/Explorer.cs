using PawnPlus.Core.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core.Forms
{
    public partial class Explorer : DockContent
    {
        private ContextMenu directoryMenu;
        private ContextMenu fileMenu;
        private ContextMenu rootMenu;

        public Explorer()
        {
            InitializeComponent();
        }

        private void ProjectExplorer_Load(object sender, EventArgs e)
        {
            // Translate controls.
            this.Text = Localization.Name_ProjectExplorer;

            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            imageList.Images.Add(Properties.Resources.application_32xLG);
            imageList.Images.Add(Properties.Resources.folder_Closed_32xLG);
            imageList.Images.Add(Properties.Resources.folder_Open_32xLG);
            imageList.Images.Add(Properties.Resources.FileGroup_10135_32x);
            imageList.Images.Add(Properties.Resources.gear_32xLG);

            this.projectFiles.ImageList = imageList;
            this.projectFiles.LabelEdit = true;
            this.projectFiles.TreeViewNodeSorter = new TreeNodeSorter();

            this.projectFiles.AfterLabelEdit += projectFiles_AfterLabelEdit;

            List<MenuItem> menuItems = new List<MenuItem>();

            // Add items for file context menu.
            menuItems.Add(new MenuItem(Localization.Text_Delete, this.contextMenuDelete_Click));
            menuItems.Add(new MenuItem(Localization.Text_Rename, this.contextMenuRename_Click));
            menuItems.Add(new MenuItem("-"));
            menuItems.Add(new MenuItem(Localization.Text_ShowInExplorer, this.contextMenuShowInExplorer_Click));
            this.fileMenu = new ContextMenu(menuItems.ToArray());

            menuItems.Clear();

            // Add items for folder context menu.
            menuItems.Add(new MenuItem(Localization.Text_Add));

            // Create sub-items for 'Add' menu.
            menuItems[0].MenuItems.Add(new MenuItem(string.Format("{0} {1}", Localization.Text_New, Localization.Text_File), this.contextMenuCreateFile_Click));
            menuItems[0].MenuItems.Add(new MenuItem(string.Format("{0} {1}", Localization.Text_New, Localization.Text_Folder), this.contextMenuCreateFolder_Click));

            menuItems.Add(new MenuItem(Localization.Text_Delete, this.contextMenuDelete_Click));
            menuItems.Add(new MenuItem(Localization.Text_Rename, this.contextMenuRename_Click));
            menuItems.Add(new MenuItem("-"));
            menuItems.Add(new MenuItem(Localization.Text_ShowInExplorer, this.contextMenuShowInExplorer_Click));
            this.directoryMenu = new ContextMenu(menuItems.ToArray());

            menuItems.Clear();

            // Add items for root project context menu.
            menuItems.Add(new MenuItem(Localization.Text_Add));

            // Create sub-items for 'Add' menu.
            menuItems[0].MenuItems.Add(new MenuItem(string.Format("{0} {1}", Localization.Text_New, Localization.Text_File), this.contextMenuCreateFile_Click));
            menuItems[0].MenuItems.Add(new MenuItem(string.Format("{0} {1}", Localization.Text_New, Localization.Text_Folder), this.contextMenuCreateFolder_Click));

            menuItems.Add(new MenuItem("-"));
            menuItems.Add(new MenuItem(Localization.Text_ShowInExplorer, this.contextMenuShowInExplorer_Click));
            this.rootMenu = new ContextMenu(menuItems.ToArray());

            // Add events listener.
            EventStorage.AddListener<Project, ProjectEventArgs>(EventKey.ProjectClosed, event_ProjectClosed);
            EventStorage.AddListener<Project, ProjectEventArgs>(EventKey.ProjectOpened, event_ProjectLoaded);
        }

        private void projectFiles_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }

        private void projectFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void projectFiles_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null || e.Node == this.projectFiles.Nodes[0])
            {
                e.CancelEdit = true;
                return;
            }

            if (e.Label.Length > 0)
            {
                if (e.Label.IndexOfAny(new char[] { '@', ',', '!' }) == -1)
                {
                    string projectPath = Path.GetDirectoryName(this.GetSelectedNodeInternalPath());
                    string oldPath = Path.Combine(Workspace.Project.BaseDirectory, projectPath, e.Node.Text);
                    string newPath = Path.Combine(Workspace.Project.BaseDirectory, projectPath, e.Label);

                    if ((TreeNodeType)e.Node.Tag == TreeNodeType.Directory)
                    {
                        TreeNodeHelper.ChangeKeys(e.Node.Nodes, oldPath, newPath);

                        // Change directory's name.
                        Directory.Move(oldPath, newPath);
                    }
                    else
                    {
                        TreeNodeHelper.ChangeKey(e.Node, newPath);

                        // Change file's name.
                        File.Move(oldPath, newPath);
                    }

                    EventStorage.Fire(EventKey.ItemRenamed, this, new ItemRenamedEventArgs(oldPath, newPath));

                    // Change editor path.
                    foreach (Editor editor in Workspace.GetEditors().Values.ToList())
                    {
                        // Check if file path is greater or equal with modified path.
                        if (editor.FilePath.Length >= oldPath.Length && editor.FilePath.Substring(0, oldPath.Length) == oldPath)
                        {
                            string editorPath = editor.FilePath;

                            // Remove and add the editor.
                            editor.Close();
                            Workspace.OpenFile(Path.Combine(newPath, editorPath.Remove(0, editorPath.Length <= oldPath.Length ? oldPath.Length : oldPath.Length + 1)));
                        }
                    }

                    e.Node.EndEdit(false);
                }
                else
                {
                    e.CancelEdit = true;
                    // TODO: Show message for invalid characters.
                    e.Node.BeginEdit();
                }
            }
            else
            {
                e.CancelEdit = true;
                // TODO: Show message for empty name.
                e.Node.BeginEdit();
            }
        }

        private void projectFiles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.projectFiles.SelectedNode.Level == 0)
            {
                this.projectFiles.SelectedNode.NodeFont = new Font(this.projectFiles.Font, FontStyle.Bold);
                this.projectFiles.SelectedNode.Text = this.projectFiles.SelectedNode.Text;
            }
        }

        private void projectFiles_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(this.projectFiles.SelectedNode.Name) == true)
            {
                Workspace.OpenFile(this.projectFiles.SelectedNode.Name, true);
            }

            Workspace.SetActiveEditor(this.projectFiles.SelectedNode.Name, true);
        }

        private void projectFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.projectFiles.Nodes.Count > 0 && e.Button == MouseButtons.Right)
            {
                TreeNode treeNode = this.projectFiles.GetNodeAt(e.Location);

                if (treeNode != null)
                {
                    TreeNode oldTreeNode = this.projectFiles.SelectedNode;
                    this.projectFiles.SelectedNode = treeNode;

                    switch ((TreeNodeType)treeNode.Tag)
                    {
                        case TreeNodeType.Directory:
                        {
                            this.directoryMenu.Show(this.projectFiles, e.Location);
                            break;
                        }
                        case TreeNodeType.File:
                        {
                            this.fileMenu.Show(this.projectFiles, e.Location);
                            break;
                        }
                        case TreeNodeType.Root:
                        {
                            this.rootMenu.Show(this.projectFiles, e.Location);
                            break;
                        }
                    }
                }
            }
        }

        private void contextMenuCreateFile_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Workspace.Project.BaseDirectory, this.GetSelectedNodeInternalPath());

            NewForm newForm = new NewForm(NewFormType.File, path);
            newForm.ShowDialog(this);
        }

        private void contextMenuCreateFolder_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Workspace.Project.BaseDirectory, this.GetSelectedNodeInternalPath(), "New Folder");

            int count = 0;

            // While we have a directory with this name increment "count" to add a suffix to name.
            while (Directory.Exists(path))
            {
                count++;
                path = string.Format("{0} ({1})", path.Replace(string.Format(" ({0})", count - 1), ""), count);
            }

            DirectoryInfo directoryInfo = Directory.CreateDirectory(path);

            ((Explorer)Application.OpenForms["ProjectExplorer"]).Add(TreeNodeType.Directory, path, true);
        }

        private void contextMenuDelete_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Workspace.Project.BaseDirectory, this.GetSelectedNodeInternalPath());

            DialogResult dialogResult = MessageBox.Show(string.Format("'{0}' will be deleted permanently.", Path.GetFileName(path)), Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.OK)
            {
                FileAttributes fileAttributes = File.GetAttributes(path);

                if (fileAttributes.HasFlag(FileAttributes.Directory) == true)
                {
                    // Check if our directory exists.
                    if (Directory.Exists(path) == true)
                    {
                        Directory.Delete(path, true);
                    }
                }
                else
                {
                    // Check if our file exists.
                    if (File.Exists(path) == true)
                    {
                        File.Delete(path);
                    }
                }

                // Close editors.
                foreach (Editor editor in Workspace.GetEditors().Values.ToList())
                {
                    // Check if file path is greater or equal with deleted path.
                    if (editor.FilePath.Length >= path.Length && editor.FilePath.Substring(0, path.Length) == path)
                    {
                        editor.Close();
                    }
                }

                EventStorage.Fire(EventKey.ItemDeleted, this, new ItemEventArgs(path));
                this.projectFiles.SelectedNode.Remove();
            }
        }

        private void contextMenuRename_Click(object sender, EventArgs e)
        {
            if (this.projectFiles.SelectedNode.IsEditing == false)
            {
                this.projectFiles.SelectedNode.BeginEdit();
            }
        }

        private void contextMenuShowInExplorer_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Workspace.Project.BaseDirectory, this.GetSelectedNodeInternalPath());

            if ((TreeNodeType)this.projectFiles.SelectedNode.Tag == TreeNodeType.File)
            {
                // Check if our file exists.
                if (File.Exists(path) == true)
                {
                    Process.Start("explorer.exe", string.Format("/select,\"{0}\"", path));
                }
            }
            else
            {
                // Check if our directory exists.
                if (Directory.Exists(path) == true)
                {
                    Process.Start("explorer.exe", path);
                }
            }
        }

        private void event_ProjectClosed(Project sender, ProjectEventArgs e)
        {
            this.projectFiles.Nodes.Clear();
        }

        private void event_ProjectLoaded(Project sender, ProjectEventArgs e)
        {
            this.projectFiles.BeginUpdate();
            this.projectFiles.Nodes.Clear();

            TreeNode treeNode = new TreeNode(Workspace.Project.Name, 0, 0);
            treeNode.Tag = TreeNodeType.Root;

            this.projectFiles.Nodes.Add(treeNode);

            foreach (string directoryPath in Workspace.Project.Directories)
            {
                this.Add(TreeNodeType.Directory, directoryPath, false);
            }

            foreach (string filePath in Workspace.Project.Files)
            {
                this.Add(TreeNodeType.File, filePath, false);
            }

            this.projectFiles.Nodes[0].Expand();
            this.projectFiles.EndUpdate();
        }

        /// <summary>
        /// Add project item to explorer.
        /// </summary>
        /// <param name="type">Type of the item.</param>
        /// <param name="path">Path of the item in Workspace.Project.</param>
        public void Add(TreeNodeType type, string path, bool expand)
        {
            // Check if the path is from Workspace.Project.
            if (path.Substring(0, Workspace.Project.BaseDirectory.Length) != Workspace.Project.BaseDirectory)
            {
                return;
            }

            // TODO: Check if file exists in project.

            short imageIndex = 1;

            string name = Path.GetFileName(path);
            string extension = Path.GetExtension(path);

            // Check extension to know image index.
            if (extension == ".inc")
            {
                imageIndex = 4;
            }
            else if (string.IsNullOrEmpty(extension) == false)
            {
                imageIndex = 3;
            }

            // Create TreeView path of the folder.
            string directory = Path.Combine(Workspace.Project.Name, Path.GetDirectoryName(path.Remove(0, Workspace.Project.BaseDirectory.Length + 1)));

            TreeNode parentNode = TreeNodeHelper.GetNodeByPath(this.projectFiles.Nodes, directory);

            if (parentNode != null)
            {
                // Add the node to TreeView.
                TreeNode childNode = parentNode.Nodes.Add(path, name, imageIndex, imageIndex);
                childNode.Tag = type;

                // Now let's sort the TreeView.
                this.projectFiles.Sort();

                if (expand == true && parentNode.IsExpanded == false)
                {
                    parentNode.Expand();
                }

                EventStorage.Fire(EventKey.ItemAdded, this, new ItemEventArgs(path));
            }
            else
            {
                // TODO: Write the a message to log file.
            }
        }

        /// <summary>
        /// Remove project name from TreeView path for selected node.
        /// </summary>
        /// <returns>Returns path without project name.</returns>
        private string GetSelectedNodeInternalPath()
        {
            return this.projectFiles.SelectedNode.FullPath.Remove(0, this.projectFiles.SelectedNode.FullPath.Length > Workspace.Project.Name.Length ? Workspace.Project.Name.Length + 1 : Workspace.Project.Name.Length);
        }
    }
}

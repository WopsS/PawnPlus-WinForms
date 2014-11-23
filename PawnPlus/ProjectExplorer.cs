using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class ProjectExplorer : DockContent
    {
        private ContextMenu contextMenu = new ContextMenu();

        public ProjectExplorer()
        {
            InitializeComponent();
        }

        private void ProjectExplorer_Load(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;

            imageList.Images.Add(Properties.Resources.application_32xLG);
            imageList.Images.Add(Properties.Resources.folder_Closed_32xLG);
            imageList.Images.Add(Properties.Resources.folder_Open_32xLG);
            imageList.Images.Add(Properties.Resources.FileGroup_10135_32x);
            imageList.Images.Add(Properties.Resources.gear_32xLG);

            this.FileTree.ImageList = imageList;

            MenuItem menuItem = new MenuItem("Test");
            //menuItem.Click += new EventHandler(tESTAction);
            contextMenu.MenuItems.Add(menuItem);
        }

        private void ProjectExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Program.main.CloseApplication == false)
                e.Cancel = true;

            this.IsHidden = true;
            Program.main.projectExplorerToolStripMenuItem.Checked = false;
        }

        public void LoadDirectory(TreeView treeView, string Path)
        {
            treeView.Nodes.Clear();

            treeView.Nodes.Add(CreateNode(new DirectoryInfo(Path)));
            treeView.Nodes[0].Expand();
        }

        public TreeNode CreateNode(DirectoryInfo directoryInfo)
        {
            TreeNode ParentDirectoryNode;

            if (directoryInfo.Name == Path.GetFileName(Program.main.ProjectInformation["Path"]))
                ParentDirectoryNode = new TreeNode(Program.main.ProjectInformation["Name"], 0, 0);
            else
                ParentDirectoryNode = new TreeNode(directoryInfo.Name, 1, 1);

            foreach (DirectoryInfo Directory in directoryInfo.GetDirectories())
                if ((Directory.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden) // Check if current folder is hidden.
                    ParentDirectoryNode.Nodes.Add(CreateNode(Directory));

            foreach (FileInfo File in directoryInfo.GetFiles())
            {
                if (File.Extension == ".exe" || File.Extension == ".amx" || File.Extension == ".sql" || File.Extension == ".dll" || File.Extension == ".rec" || File.Extension == ".pawnplusproject" || File.Name == "server-readme.txt" || File.Name == "samp-license.txt")
                    continue;

                if (File.Name.Contains(".inc"))
                    ParentDirectoryNode.Nodes.Add(File.FullName, File.Name, 4, 4);
                else
                    ParentDirectoryNode.Nodes.Add(File.FullName, File.Name, 3, 3);
            }

            return ParentDirectoryNode;
        }

        private void FileTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (FileTree.SelectedNode.Level == 0)
            {
                FileTree.SelectedNode.NodeFont = new Font(FileTree.Font, FontStyle.Bold);
                FileTree.SelectedNode.Text = FileTree.SelectedNode.Text;
            }
        }

        private void FileTree_DoubleClick(object sender, EventArgs e)
        {
            if (File.Exists(this.FileTree.SelectedNode.Name) == true && Program.main.CodeEditors.ContainsKey(this.FileTree.SelectedNode.Name) == false)
                Program.main.OpenFile(this.FileTree.SelectedNode.Name);
            else if (Program.main.CodeEditors.ContainsKey(this.FileTree.SelectedNode.Name) && Program.main.dockPanel.Documents.Contains(Program.main.CodeEditors[this.FileTree.SelectedNode.Name]) == true)
            {
                Program.main.CodeEditors[this.FileTree.SelectedNode.Name].Activate();
                Program.main.CodeEditors[this.FileTree.SelectedNode.Name].Select();
                Program.main.CodeEditors[this.FileTree.SelectedNode.Name].Focus();
            }
        }

        private void FileTree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void FileTree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level != 0)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
            }
        }

        private void FileTree_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.FileTree.Nodes.Count > 0 && e.Button == MouseButtons.Right)
                this.contextMenu.Show(this.FileTree, e.Location);
        }
    }
}

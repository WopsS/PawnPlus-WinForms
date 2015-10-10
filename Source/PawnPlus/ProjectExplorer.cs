using PawnPlus.CodeEditor;
using PawnPlus.Project;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class ProjectExplorer : DockContent
    {
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

            this.projectFiles.ImageList = imageList;

            ProjectManager.Construct(this.projectFiles);
        }

        private void projectFiles_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.Node.ImageIndex = 2;
                e.Node.SelectedImageIndex = 2;
            }
        }

        private void projectFiles_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level > 0)
            {
                e.Node.ImageIndex = 1;
                e.Node.SelectedImageIndex = 1;
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
                CEManager.Open(this.projectFiles.SelectedNode.Name, true);
            }

            CEManager.SetActiveDocument(this.projectFiles.SelectedNode.Name, true);
        }
    }
}

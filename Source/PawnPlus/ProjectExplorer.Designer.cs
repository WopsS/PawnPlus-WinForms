namespace PawnPlus
{
    partial class ProjectExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FileTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // FileTree
            // 
            this.FileTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTree.Indent = 19;
            this.FileTree.LineColor = System.Drawing.Color.White;
            this.FileTree.Location = new System.Drawing.Point(0, 0);
            this.FileTree.Name = "FileTree";
            this.FileTree.ShowRootLines = false;
            this.FileTree.Size = new System.Drawing.Size(286, 384);
            this.FileTree.TabIndex = 0;
            this.FileTree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterCollapse);
            this.FileTree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterExpand);
            this.FileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FileTree_AfterSelect);
            this.FileTree.DoubleClick += new System.EventHandler(this.FileTree_DoubleClick);
            this.FileTree.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FileTree_MouseUp);
            // 
            // ProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 384);
            this.ControlBox = false;
            this.Controls.Add(this.FileTree);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProjectExplorer";
            this.Text = "Project Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProjectExplorer_FormClosing);
            this.Load += new System.EventHandler(this.ProjectExplorer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TreeView FileTree;

    }
}
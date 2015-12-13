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
            this.projectFiles = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // projectFiles
            // 
            this.projectFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectFiles.Location = new System.Drawing.Point(0, 0);
            this.projectFiles.Name = "projectFiles";
            this.projectFiles.Size = new System.Drawing.Size(386, 424);
            this.projectFiles.TabIndex = 0;
            this.projectFiles.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.projectFiles_AfterCollapse);
            this.projectFiles.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.projectFiles_AfterExpand);
            this.projectFiles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.projectFiles_AfterSelect);
            this.projectFiles.DoubleClick += new System.EventHandler(this.projectFiles_DoubleClick);
            this.projectFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.projectFiles_MouseUp);
            // 
            // ProjectExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 424);
            this.Controls.Add(this.projectFiles);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProjectExplorer";
            this.Text = "Project explorer";
            this.Load += new System.EventHandler(this.ProjectExplorer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView projectFiles;
    }
}
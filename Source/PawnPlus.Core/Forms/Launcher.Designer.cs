namespace PawnPlus.Core.Forms
{
    partial class Launcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.principalPanel = new System.Windows.Forms.Panel();
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.photoLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.principalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // principalPanel
            // 
            this.principalPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.principalPanel.Controls.Add(this.controlsPanel);
            this.principalPanel.Controls.Add(this.photoLayoutPanel);
            this.principalPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.principalPanel.Location = new System.Drawing.Point(0, 0);
            this.principalPanel.Name = "principalPanel";
            this.principalPanel.Size = new System.Drawing.Size(512, 140);
            this.principalPanel.TabIndex = 1;
            this.principalPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.principalPanel_Paint);
            // 
            // controlsPanel
            // 
            this.controlsPanel.BackColor = System.Drawing.Color.Transparent;
            this.controlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlsPanel.Location = new System.Drawing.Point(0, 112);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(512, 28);
            this.controlsPanel.TabIndex = 1;
            // 
            // photoLayoutPanel
            // 
            this.photoLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.photoLayoutPanel.BackgroundImage = global::PawnPlus.Core.Properties.Resources.PawnPlus;
            this.photoLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.photoLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.photoLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.photoLayoutPanel.Name = "photoLayoutPanel";
            this.photoLayoutPanel.Size = new System.Drawing.Size(512, 112);
            this.photoLayoutPanel.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(512, 140);
            this.Controls.Add(this.principalPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PawnPlus - Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Launcher_FormClosing);
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.principalPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel principalPanel;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.FlowLayoutPanel photoLayoutPanel;
        internal System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}
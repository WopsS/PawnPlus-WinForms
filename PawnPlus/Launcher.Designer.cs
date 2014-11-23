namespace PawnPlus
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Launcher));
            this.Panel = new System.Windows.Forms.Panel();
            this.ControlsPanel = new System.Windows.Forms.Panel();
            this.PhotoLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.BackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.UpdateBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel
            // 
            this.Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Panel.Controls.Add(this.ControlsPanel);
            this.Panel.Controls.Add(this.PhotoLayoutPanel);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel.Location = new System.Drawing.Point(0, 0);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(512, 140);
            this.Panel.TabIndex = 1;
            this.Panel.Paint += new System.Windows.Forms.PaintEventHandler(this.StatusPanel_Paint);
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlsPanel.Location = new System.Drawing.Point(0, 112);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Size = new System.Drawing.Size(512, 28);
            this.ControlsPanel.TabIndex = 1;
            // 
            // PhotoLayoutPanel
            // 
            this.PhotoLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.PhotoLayoutPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PhotoLayoutPanel.BackgroundImage")));
            this.PhotoLayoutPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PhotoLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PhotoLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.PhotoLayoutPanel.Name = "PhotoLayoutPanel";
            this.PhotoLayoutPanel.Size = new System.Drawing.Size(512, 112);
            this.PhotoLayoutPanel.TabIndex = 0;
            // 
            // Timer
            // 
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // BackgroundWorker
            // 
            this.BackgroundWorker.WorkerReportsProgress = true;
            this.BackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.BackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            // 
            // UpdateBackgroundWorker
            // 
            this.UpdateBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateBackgroundWorker_DoWork);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(512, 140);
            this.Controls.Add(this.Panel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Launcher";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PawnPlus - Launcher";
            this.Load += new System.EventHandler(this.Launcher_Load);
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.Timer Timer;
        private System.ComponentModel.BackgroundWorker BackgroundWorker;
        private System.Windows.Forms.FlowLayoutPanel PhotoLayoutPanel;
        private System.Windows.Forms.Panel ControlsPanel;
        public System.ComponentModel.BackgroundWorker UpdateBackgroundWorker;
    }
}
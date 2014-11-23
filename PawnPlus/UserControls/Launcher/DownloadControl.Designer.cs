namespace PawnPlus
{
    partial class DownloadControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DownloadedMBLabel = new System.Windows.Forms.Label();
            this.DownloadPercentLabel = new System.Windows.Forms.Label();
            this.DonwloadProcessBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // DownloadedMBLabel
            // 
            this.DownloadedMBLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadedMBLabel.AutoSize = true;
            this.DownloadedMBLabel.ForeColor = System.Drawing.Color.White;
            this.DownloadedMBLabel.Location = new System.Drawing.Point(216, 55);
            this.DownloadedMBLabel.Name = "DownloadedMBLabel";
            this.DownloadedMBLabel.Size = new System.Drawing.Size(83, 13);
            this.DownloadedMBLabel.TabIndex = 7;
            this.DownloadedMBLabel.Text = "0,00 of 0,00 MB";
            // 
            // DownloadPercentLabel
            // 
            this.DownloadPercentLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DownloadPercentLabel.AutoSize = true;
            this.DownloadPercentLabel.ForeColor = System.Drawing.Color.White;
            this.DownloadPercentLabel.Location = new System.Drawing.Point(12, 55);
            this.DownloadPercentLabel.Name = "DownloadPercentLabel";
            this.DownloadPercentLabel.Size = new System.Drawing.Size(21, 13);
            this.DownloadPercentLabel.TabIndex = 6;
            this.DownloadPercentLabel.Text = "0%";
            // 
            // DonwloadProcessBar
            // 
            this.DonwloadProcessBar.Location = new System.Drawing.Point(15, 10);
            this.DonwloadProcessBar.Name = "DonwloadProcessBar";
            this.DonwloadProcessBar.Size = new System.Drawing.Size(485, 32);
            this.DonwloadProcessBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.DonwloadProcessBar.TabIndex = 5;
            // 
            // DownloadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.DownloadedMBLabel);
            this.Controls.Add(this.DownloadPercentLabel);
            this.Controls.Add(this.DonwloadProcessBar);
            this.Name = "DownloadControl";
            this.Size = new System.Drawing.Size(512, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label DownloadedMBLabel;
        public System.Windows.Forms.Label DownloadPercentLabel;
        public System.Windows.Forms.ProgressBar DonwloadProcessBar;


    }
}

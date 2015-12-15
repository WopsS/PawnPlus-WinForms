namespace PawnPlus.Core.UserControls
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
            this.downloadedMBLabel = new System.Windows.Forms.Label();
            this.downloadPercentageLabel = new System.Windows.Forms.Label();
            this.downloadProcessBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // downloadedMBLabel
            // 
            this.downloadedMBLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadedMBLabel.AutoSize = true;
            this.downloadedMBLabel.ForeColor = System.Drawing.Color.White;
            this.downloadedMBLabel.Location = new System.Drawing.Point(216, 55);
            this.downloadedMBLabel.Name = "downloadedMBLabel";
            this.downloadedMBLabel.Size = new System.Drawing.Size(83, 13);
            this.downloadedMBLabel.TabIndex = 7;
            this.downloadedMBLabel.Text = "0,00 of 0,00 MB";
            // 
            // downloadPercentageLabel
            // 
            this.downloadPercentageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadPercentageLabel.AutoSize = true;
            this.downloadPercentageLabel.ForeColor = System.Drawing.Color.White;
            this.downloadPercentageLabel.Location = new System.Drawing.Point(12, 55);
            this.downloadPercentageLabel.Name = "downloadPercentageLabel";
            this.downloadPercentageLabel.Size = new System.Drawing.Size(21, 13);
            this.downloadPercentageLabel.TabIndex = 6;
            this.downloadPercentageLabel.Text = "0%";
            // 
            // downloadProcessBar
            // 
            this.downloadProcessBar.Location = new System.Drawing.Point(15, 10);
            this.downloadProcessBar.Name = "downloadProcessBar";
            this.downloadProcessBar.Size = new System.Drawing.Size(485, 32);
            this.downloadProcessBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.downloadProcessBar.TabIndex = 5;
            // 
            // DownloadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.downloadedMBLabel);
            this.Controls.Add(this.downloadPercentageLabel);
            this.Controls.Add(this.downloadProcessBar);
            this.Name = "DownloadControl";
            this.Size = new System.Drawing.Size(512, 78);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label downloadedMBLabel;
        public System.Windows.Forms.Label downloadPercentageLabel;
        public System.Windows.Forms.ProgressBar downloadProcessBar;
    }
}

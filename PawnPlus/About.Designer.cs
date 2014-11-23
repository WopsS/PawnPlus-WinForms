namespace PawnPlus
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.Version = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BuildTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.IssuesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.PawnPlusIssuesLinkLabel = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Version
            // 
            this.Version.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Version.AutoSize = true;
            this.Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Version.Location = new System.Drawing.Point(12, 315);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(87, 13);
            this.Version.TabIndex = 13;
            this.Version.Text = "PawnPlus v0.0.0";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.BuildTimeLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(556, 141);
            this.panel1.TabIndex = 9;
            // 
            // BuildTimeLabel
            // 
            this.BuildTimeLabel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BuildTimeLabel.AutoSize = true;
            this.BuildTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuildTimeLabel.Location = new System.Drawing.Point(363, 9);
            this.BuildTimeLabel.Name = "BuildTimeLabel";
            this.BuildTimeLabel.Size = new System.Drawing.Size(82, 13);
            this.BuildTimeLabel.TabIndex = 20;
            this.BuildTimeLabel.Text = "Build time: 0.0.0";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 228);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Credits:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 244);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(233, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Gireada, Sonny and Adrian - Closed beta testers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(299, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "PawnPlus is a Pawn++ IDE, with more features than PAWNO.";
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(469, 305);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "If you find a bug please report it";
            // 
            // IssuesLinkLabel
            // 
            this.IssuesLinkLabel.AutoSize = true;
            this.IssuesLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.IssuesLinkLabel.Location = new System.Drawing.Point(164, 196);
            this.IssuesLinkLabel.Name = "IssuesLinkLabel";
            this.IssuesLinkLabel.Size = new System.Drawing.Size(28, 13);
            this.IssuesLinkLabel.TabIndex = 22;
            this.IssuesLinkLabel.TabStop = true;
            this.IssuesLinkLabel.Text = "here";
            this.IssuesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.IssuesLinkLabel_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "or";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 257);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Visual Studio - Image Library (some images)";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "icons8.com - Application icon";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = ".";
            // 
            // PawnPlusIssuesLinkLabel
            // 
            this.PawnPlusIssuesLinkLabel.AutoSize = true;
            this.PawnPlusIssuesLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.PawnPlusIssuesLinkLabel.Location = new System.Drawing.Point(200, 196);
            this.PawnPlusIssuesLinkLabel.Name = "PawnPlusIssuesLinkLabel";
            this.PawnPlusIssuesLinkLabel.Size = new System.Drawing.Size(28, 13);
            this.PawnPlusIssuesLinkLabel.TabIndex = 27;
            this.PawnPlusIssuesLinkLabel.TabStop = true;
            this.PawnPlusIssuesLinkLabel.Text = "here";
            this.PawnPlusIssuesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PawnPlusIssuesLinkLabel_LinkClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 340);
            this.ControlBox = false;
            this.Controls.Add(this.label8);
            this.Controls.Add(this.PawnPlusIssuesLinkLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.IssuesLinkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Version);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "About";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Version;
        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label BuildTimeLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel IssuesLinkLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.LinkLabel PawnPlusIssuesLinkLabel;
    }
}
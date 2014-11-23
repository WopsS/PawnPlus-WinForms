namespace PawnPlus
{
    partial class CreateFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateFile));
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.NameText = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.NameLabel = new System.Windows.Forms.Label();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TypeComboBox
            // 
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Items.AddRange(new object[] {
            "GameMode",
            "Filterscript",
            "Include"});
            this.TypeComboBox.Location = new System.Drawing.Point(82, 43);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(148, 21);
            this.TypeComboBox.TabIndex = 1;
            // 
            // NameText
            // 
            this.NameText.Location = new System.Drawing.Point(82, 17);
            this.NameText.Name = "NameText";
            this.NameText.Size = new System.Drawing.Size(148, 20);
            this.NameText.TabIndex = 1;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Location = new System.Drawing.Point(45, 81);
            this.OkButton.MaximumSize = new System.Drawing.Size(75, 32);
            this.OkButton.MinimumSize = new System.Drawing.Size(75, 32);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 32);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(126, 81);
            this.cancelButton.MaximumSize = new System.Drawing.Size(75, 32);
            this.cancelButton.MinimumSize = new System.Drawing.Size(75, 32);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 32);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 20);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Name:";
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(12, 46);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(34, 13);
            this.TypeLabel.TabIndex = 5;
            this.TypeLabel.Text = "Type:";
            // 
            // CreateFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 125);
            this.ControlBox = false;
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.NameText);
            this.Controls.Add(this.TypeComboBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(258, 163);
            this.MinimumSize = new System.Drawing.Size(258, 163);
            this.Name = "CreateFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create File";
            this.Load += new System.EventHandler(this.CreateFile_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.TextBox NameText;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label TypeLabel;
    }
}
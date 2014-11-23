namespace PawnPlus
{
    partial class GoToLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoToLine));
            this.CurrentLineText = new System.Windows.Forms.TextBox();
            this.CurrentLine = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.GoToLineText = new System.Windows.Forms.TextBox();
            this.GoToLineLabel = new System.Windows.Forms.Label();
            this.MaximumLineText = new System.Windows.Forms.TextBox();
            this.MaximumLine = new System.Windows.Forms.Label();
            this.LineError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LineError)).BeginInit();
            this.SuspendLayout();
            // 
            // CurrentLineText
            // 
            this.CurrentLineText.Enabled = false;
            this.CurrentLineText.Location = new System.Drawing.Point(120, 6);
            this.CurrentLineText.Name = "CurrentLineText";
            this.CurrentLineText.ReadOnly = true;
            this.CurrentLineText.Size = new System.Drawing.Size(75, 20);
            this.CurrentLineText.TabIndex = 9;
            // 
            // CurrentLine
            // 
            this.CurrentLine.AutoSize = true;
            this.CurrentLine.Location = new System.Drawing.Point(9, 11);
            this.CurrentLine.Name = "CurrentLine";
            this.CurrentLine.Size = new System.Drawing.Size(60, 13);
            this.CurrentLine.TabIndex = 8;
            this.CurrentLine.Text = "Current line";
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelButton.Location = new System.Drawing.Point(120, 83);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // OkButton
            // 
            this.OkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OkButton.Location = new System.Drawing.Point(39, 83);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 14;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // GoToLineText
            // 
            this.GoToLineText.Location = new System.Drawing.Point(120, 56);
            this.GoToLineText.Name = "GoToLineText";
            this.GoToLineText.Size = new System.Drawing.Size(75, 20);
            this.GoToLineText.TabIndex = 13;
            this.GoToLineText.TextChanged += new System.EventHandler(this.GoToLineText_TextChanged);
            this.GoToLineText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GoToLineText_KeyDown);
            // 
            // GoToLineLabel
            // 
            this.GoToLineLabel.AutoSize = true;
            this.GoToLineLabel.Location = new System.Drawing.Point(9, 59);
            this.GoToLineLabel.Name = "GoToLineLabel";
            this.GoToLineLabel.Size = new System.Drawing.Size(90, 13);
            this.GoToLineLabel.TabIndex = 12;
            this.GoToLineLabel.Text = "Go to line number";
            // 
            // MaximumLineText
            // 
            this.MaximumLineText.Enabled = false;
            this.MaximumLineText.Location = new System.Drawing.Point(120, 31);
            this.MaximumLineText.Name = "MaximumLineText";
            this.MaximumLineText.ReadOnly = true;
            this.MaximumLineText.Size = new System.Drawing.Size(75, 20);
            this.MaximumLineText.TabIndex = 11;
            // 
            // MaximumLine
            // 
            this.MaximumLine.AutoSize = true;
            this.MaximumLine.Location = new System.Drawing.Point(9, 35);
            this.MaximumLine.Name = "MaximumLine";
            this.MaximumLine.Size = new System.Drawing.Size(78, 13);
            this.MaximumLine.TabIndex = 10;
            this.MaximumLine.Text = "Maxmimum line";
            // 
            // LineError
            // 
            this.LineError.ContainerControl = this;
            this.LineError.Icon = ((System.Drawing.Icon)(resources.GetObject("LineError.Icon")));
            this.LineError.RightToLeft = true;
            // 
            // GoToLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 113);
            this.Controls.Add(this.CurrentLineText);
            this.Controls.Add(this.CurrentLine);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.GoToLineText);
            this.Controls.Add(this.GoToLineLabel);
            this.Controls.Add(this.MaximumLineText);
            this.Controls.Add(this.MaximumLine);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(220, 151);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(220, 151);
            this.Name = "GoToLine";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Go to line";
            ((System.ComponentModel.ISupportInitialize)(this.LineError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CurrentLine;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label GoToLineLabel;
        private System.Windows.Forms.Label MaximumLine;
        public System.Windows.Forms.TextBox CurrentLineText;
        public System.Windows.Forms.TextBox GoToLineText;
        public System.Windows.Forms.TextBox MaximumLineText;
        private System.Windows.Forms.ErrorProvider LineError;

    }
}
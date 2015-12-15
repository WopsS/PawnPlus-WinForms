namespace PawnPlus.Core.Forms
{
    partial class ExceptionForm
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
            this.label = new System.Windows.Forms.Label();
            this.stackTrace = new System.Windows.Forms.RichTextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.quitButton = new System.Windows.Forms.Button();
            this.exceptionMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(52, 20);
            this.label.MaximumSize = new System.Drawing.Size(425, 0);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(390, 39);
            this.label.TabIndex = 0;
            this.label.Text = "Unhandled exception has occurred in application.\r\nIf you click continue the appli" +
    "cation will ignore this issue and attempt to continue. \r\nIf you click quit, the " +
    "application will close immediately.";
            // 
            // stackTrace
            // 
            this.stackTrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.stackTrace.DetectUrls = false;
            this.stackTrace.Location = new System.Drawing.Point(12, 94);
            this.stackTrace.Name = "stackTrace";
            this.stackTrace.ReadOnly = true;
            this.stackTrace.Size = new System.Drawing.Size(465, 135);
            this.stackTrace.TabIndex = 1;
            this.stackTrace.Text = "";
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continueButton.Location = new System.Drawing.Point(402, 235);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 2;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            // 
            // quitButton
            // 
            this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quitButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.quitButton.Location = new System.Drawing.Point(321, 235);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 3;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            // 
            // exceptionMessage
            // 
            this.exceptionMessage.AutoSize = true;
            this.exceptionMessage.Location = new System.Drawing.Point(52, 71);
            this.exceptionMessage.MaximumSize = new System.Drawing.Size(425, 0);
            this.exceptionMessage.Name = "exceptionMessage";
            this.exceptionMessage.Size = new System.Drawing.Size(50, 13);
            this.exceptionMessage.TabIndex = 4;
            this.exceptionMessage.Text = "Message";
            // 
            // ExceptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 270);
            this.Controls.Add(this.exceptionMessage);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.stackTrace);
            this.Controls.Add(this.label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ExceptionForm";
            this.Load += new System.EventHandler(this.ExceptionForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ExceptionForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.RichTextBox stackTrace;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.Label exceptionMessage;
    }
}
namespace PawnPlus
{
    partial class CodeEditor
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
            this.UpdateFoldings = new System.ComponentModel.BackgroundWorker();
            this.CodeBox = new DigitalRune.Windows.TextEditor.TextEditorControl();
            this.SuspendLayout();
            // 
            // UpdateFoldings
            // 
            this.UpdateFoldings.DoWork += new System.ComponentModel.DoWorkEventHandler(this.UpdateFoldings_DoWork);
            // 
            // CodeBox
            // 
            this.CodeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CodeBox.Location = new System.Drawing.Point(0, 0);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.ShowVRuler = false;
            this.CodeBox.Size = new System.Drawing.Size(268, 492);
            this.CodeBox.TabIndex = 0;
            this.CodeBox.CompletionRequest += new System.EventHandler<DigitalRune.Windows.TextEditor.Completion.CompletionEventArgs>(this.CodeBox_CompletionRequest);
            this.CodeBox.InsightRequest += new System.EventHandler<DigitalRune.Windows.TextEditor.Insight.InsightEventArgs>(this.CodeBox_InsightRequest);
            this.CodeBox.Load += new System.EventHandler(this.CodeBox_Load);
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 492);
            this.Controls.Add(this.CodeBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "CodeEditor";
            this.Text = "CodeEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CodeEditor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public DigitalRune.Windows.TextEditor.TextEditorControl CodeBox;
        private System.ComponentModel.BackgroundWorker UpdateFoldings;

    }
}
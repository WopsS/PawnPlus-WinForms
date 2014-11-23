namespace PawnPlus
{
    partial class Output
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
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // OutputText
            // 
            this.OutputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OutputText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputText.Location = new System.Drawing.Point(0, 0);
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(661, 407);
            this.OutputText.TabIndex = 0;
            this.OutputText.Text = "";
            // 
            // Output
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 407);
            this.Controls.Add(this.OutputText);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Output";
            this.Text = "Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Output_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox OutputText;



    }
}
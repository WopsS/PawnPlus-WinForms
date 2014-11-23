namespace PawnPlus
{
    partial class FindReplace
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.FindTab = new System.Windows.Forms.TabPage();
            this.close = new System.Windows.Forms.Button();
            this.findnext = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.matchwholeword = new System.Windows.Forms.CheckBox();
            this.casesensitive = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FindWhat = new System.Windows.Forms.ComboBox();
            this.ReplaceTab = new System.Windows.Forms.TabPage();
            this.ReplacePrevious = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.matchwholeword2 = new System.Windows.Forms.CheckBox();
            this.casesensitive2 = new System.Windows.Forms.CheckBox();
            this.close2 = new System.Windows.Forms.Button();
            this.ReplaceWithText = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplaceAll = new System.Windows.Forms.Button();
            this.ReplaceNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.FindWhat2 = new System.Windows.Forms.ComboBox();
            this.TabControl.SuspendLayout();
            this.FindTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.ReplaceTab.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.FindTab);
            this.TabControl.Controls.Add(this.ReplaceTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(203, 250);
            this.TabControl.TabIndex = 0;
            this.TabControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TabControl_MouseDown);
            this.TabControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TabControl_MouseMove);
            this.TabControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TabControl_MouseUp);
            // 
            // FindTab
            // 
            this.FindTab.Controls.Add(this.close);
            this.FindTab.Controls.Add(this.findnext);
            this.FindTab.Controls.Add(this.groupBox1);
            this.FindTab.Controls.Add(this.label1);
            this.FindTab.Controls.Add(this.FindWhat);
            this.FindTab.Location = new System.Drawing.Point(4, 22);
            this.FindTab.Name = "FindTab";
            this.FindTab.Padding = new System.Windows.Forms.Padding(3);
            this.FindTab.Size = new System.Drawing.Size(195, 224);
            this.FindTab.TabIndex = 0;
            this.FindTab.Text = "Find";
            this.FindTab.UseVisualStyleBackColor = true;
            this.FindTab.Enter += new System.EventHandler(this.FindTab_Enter);
            this.FindTab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FindTab_MouseDown);
            this.FindTab.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FindTab_MouseMove);
            this.FindTab.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FindTab_MouseUp);
            this.FindTab.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FindTab_PreviewKeyDown);
            // 
            // close
            // 
            this.close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close.Location = new System.Drawing.Point(114, 125);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(75, 23);
            this.close.TabIndex = 9;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            this.close.Click += new System.EventHandler(this.close_Click);
            // 
            // findnext
            // 
            this.findnext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.findnext.Location = new System.Drawing.Point(6, 125);
            this.findnext.Name = "findnext";
            this.findnext.Size = new System.Drawing.Size(75, 23);
            this.findnext.TabIndex = 8;
            this.findnext.Text = "Find next";
            this.findnext.UseVisualStyleBackColor = true;
            this.findnext.Click += new System.EventHandler(this.findnext_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.matchwholeword);
            this.groupBox1.Controls.Add(this.casesensitive);
            this.groupBox1.Location = new System.Drawing.Point(6, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(183, 67);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // matchwholeword
            // 
            this.matchwholeword.AutoSize = true;
            this.matchwholeword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.matchwholeword.Location = new System.Drawing.Point(6, 42);
            this.matchwholeword.Name = "matchwholeword";
            this.matchwholeword.Size = new System.Drawing.Size(110, 17);
            this.matchwholeword.TabIndex = 2;
            this.matchwholeword.Text = "Match whole word";
            this.matchwholeword.UseVisualStyleBackColor = true;
            // 
            // casesensitive
            // 
            this.casesensitive.AutoSize = true;
            this.casesensitive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.casesensitive.Location = new System.Drawing.Point(6, 19);
            this.casesensitive.Name = "casesensitive";
            this.casesensitive.Size = new System.Drawing.Size(93, 17);
            this.casesensitive.TabIndex = 0;
            this.casesensitive.Text = "Case Sensitive";
            this.casesensitive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Find text:";
            // 
            // FindWhat
            // 
            this.FindWhat.AllowDrop = true;
            this.FindWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FindWhat.FormattingEnabled = true;
            this.FindWhat.Location = new System.Drawing.Point(6, 25);
            this.FindWhat.Name = "FindWhat";
            this.FindWhat.Size = new System.Drawing.Size(183, 21);
            this.FindWhat.TabIndex = 5;
            this.FindWhat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindWhat_KeyDown);
            // 
            // ReplaceTab
            // 
            this.ReplaceTab.Controls.Add(this.ReplacePrevious);
            this.ReplaceTab.Controls.Add(this.groupBox2);
            this.ReplaceTab.Controls.Add(this.close2);
            this.ReplaceTab.Controls.Add(this.ReplaceWithText);
            this.ReplaceTab.Controls.Add(this.label3);
            this.ReplaceTab.Controls.Add(this.ReplaceAll);
            this.ReplaceTab.Controls.Add(this.ReplaceNext);
            this.ReplaceTab.Controls.Add(this.label2);
            this.ReplaceTab.Controls.Add(this.FindWhat2);
            this.ReplaceTab.Location = new System.Drawing.Point(4, 22);
            this.ReplaceTab.Name = "ReplaceTab";
            this.ReplaceTab.Padding = new System.Windows.Forms.Padding(3);
            this.ReplaceTab.Size = new System.Drawing.Size(195, 224);
            this.ReplaceTab.TabIndex = 1;
            this.ReplaceTab.Text = "Replace";
            this.ReplaceTab.UseVisualStyleBackColor = true;
            this.ReplaceTab.Enter += new System.EventHandler(this.ReplaceTab_Enter);
            this.ReplaceTab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ReplaceTab_MouseDown);
            this.ReplaceTab.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ReplaceTab_MouseMove);
            this.ReplaceTab.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ReplaceTab_MouseUp);
            // 
            // ReplacePrevious
            // 
            this.ReplacePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplacePrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ReplacePrevious.Location = new System.Drawing.Point(7, 164);
            this.ReplacePrevious.Name = "ReplacePrevious";
            this.ReplacePrevious.Size = new System.Drawing.Size(82, 23);
            this.ReplacePrevious.TabIndex = 19;
            this.ReplacePrevious.Text = "R. previous";
            this.ReplacePrevious.UseVisualStyleBackColor = true;
            this.ReplacePrevious.Click += new System.EventHandler(this.ReplacePrevious_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.matchwholeword2);
            this.groupBox2.Controls.Add(this.casesensitive2);
            this.groupBox2.Location = new System.Drawing.Point(6, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 67);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // matchwholeword2
            // 
            this.matchwholeword2.AutoSize = true;
            this.matchwholeword2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.matchwholeword2.Location = new System.Drawing.Point(6, 42);
            this.matchwholeword2.Name = "matchwholeword2";
            this.matchwholeword2.Size = new System.Drawing.Size(110, 17);
            this.matchwholeword2.TabIndex = 2;
            this.matchwholeword2.Text = "Match whole word";
            this.matchwholeword2.UseVisualStyleBackColor = true;
            // 
            // casesensitive2
            // 
            this.casesensitive2.AutoSize = true;
            this.casesensitive2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.casesensitive2.Location = new System.Drawing.Point(6, 19);
            this.casesensitive2.Name = "casesensitive2";
            this.casesensitive2.Size = new System.Drawing.Size(93, 17);
            this.casesensitive2.TabIndex = 0;
            this.casesensitive2.Text = "Case Sensitive";
            this.casesensitive2.UseVisualStyleBackColor = true;
            // 
            // close2
            // 
            this.close2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close2.Location = new System.Drawing.Point(108, 193);
            this.close2.Name = "close2";
            this.close2.Size = new System.Drawing.Size(82, 23);
            this.close2.TabIndex = 17;
            this.close2.Text = "Close";
            this.close2.UseVisualStyleBackColor = true;
            this.close2.Click += new System.EventHandler(this.close2_Click);
            // 
            // ReplaceWithText
            // 
            this.ReplaceWithText.AllowDrop = true;
            this.ReplaceWithText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplaceWithText.FormattingEnabled = true;
            this.ReplaceWithText.Location = new System.Drawing.Point(7, 65);
            this.ReplaceWithText.Name = "ReplaceWithText";
            this.ReplaceWithText.Size = new System.Drawing.Size(183, 21);
            this.ReplaceWithText.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Replace with:";
            // 
            // ReplaceAll
            // 
            this.ReplaceAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceAll.Location = new System.Drawing.Point(7, 193);
            this.ReplaceAll.Name = "ReplaceAll";
            this.ReplaceAll.Size = new System.Drawing.Size(82, 23);
            this.ReplaceAll.TabIndex = 14;
            this.ReplaceAll.Text = "Replace all";
            this.ReplaceAll.UseVisualStyleBackColor = true;
            this.ReplaceAll.Click += new System.EventHandler(this.ReplaceAll_Click);
            // 
            // ReplaceNext
            // 
            this.ReplaceNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReplaceNext.Location = new System.Drawing.Point(108, 164);
            this.ReplaceNext.Name = "ReplaceNext";
            this.ReplaceNext.Size = new System.Drawing.Size(82, 23);
            this.ReplaceNext.TabIndex = 13;
            this.ReplaceNext.Text = "Replace next";
            this.ReplaceNext.UseVisualStyleBackColor = true;
            this.ReplaceNext.Click += new System.EventHandler(this.ReplaceNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Find text:";
            // 
            // FindWhat2
            // 
            this.FindWhat2.AllowDrop = true;
            this.FindWhat2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FindWhat2.FormattingEnabled = true;
            this.FindWhat2.Location = new System.Drawing.Point(7, 25);
            this.FindWhat2.Name = "FindWhat2";
            this.FindWhat2.Size = new System.Drawing.Size(183, 21);
            this.FindWhat2.TabIndex = 10;
            // 
            // FindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 250);
            this.Controls.Add(this.TabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FindReplace";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Find_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Find_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Find_MouseUp);
            this.TabControl.ResumeLayout(false);
            this.FindTab.ResumeLayout(false);
            this.FindTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ReplaceTab.ResumeLayout(false);
            this.ReplaceTab.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox FindWhat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button close;
        private System.Windows.Forms.Button findnext;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ComboBox ReplaceWithText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ReplaceAll;
        private System.Windows.Forms.Button ReplaceNext;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox FindWhat2;
        private System.Windows.Forms.Button close2;
        public System.Windows.Forms.TabControl TabControl;
        public System.Windows.Forms.TabPage FindTab;
        public System.Windows.Forms.TabPage ReplaceTab;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox matchwholeword2;
        public System.Windows.Forms.CheckBox casesensitive2;
        public System.Windows.Forms.CheckBox matchwholeword;
        public System.Windows.Forms.CheckBox casesensitive;
        private System.Windows.Forms.Button ReplacePrevious;

    }
}
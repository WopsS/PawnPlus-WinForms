namespace PawnPlus.CodeEditor
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.findPage = new System.Windows.Forms.TabPage();
            this.optionPanel = new System.Windows.Forms.Panel();
            this.wholeWordCheckBox = new System.Windows.Forms.CheckBox();
            this.caseSentitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.lookInLabel = new System.Windows.Forms.Label();
            this.lookInComboBox = new System.Windows.Forms.ComboBox();
            this.findPreviousButton = new System.Windows.Forms.Button();
            this.findNextButton = new System.Windows.Forms.Button();
            this.findWhatText = new System.Windows.Forms.TextBox();
            this.findWhatLabel = new System.Windows.Forms.Label();
            this.replacePage = new System.Windows.Forms.TabPage();
            this.replaceAllButton = new System.Windows.Forms.Button();
            this.replaceButton = new System.Windows.Forms.Button();
            this.replaceWithText = new System.Windows.Forms.TextBox();
            this.replaceWithLabel = new System.Windows.Forms.Label();
            this.replaceWhatText = new System.Windows.Forms.TextBox();
            this.replaceWhatLabel = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.findPage.SuspendLayout();
            this.optionPanel.SuspendLayout();
            this.replacePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.findPage);
            this.tabControl.Controls.Add(this.replacePage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(257, 224);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // findPage
            // 
            this.findPage.BackColor = System.Drawing.SystemColors.Window;
            this.findPage.Controls.Add(this.optionPanel);
            this.findPage.Controls.Add(this.findPreviousButton);
            this.findPage.Controls.Add(this.findNextButton);
            this.findPage.Controls.Add(this.findWhatText);
            this.findPage.Controls.Add(this.findWhatLabel);
            this.findPage.Location = new System.Drawing.Point(4, 22);
            this.findPage.Name = "findPage";
            this.findPage.Padding = new System.Windows.Forms.Padding(3);
            this.findPage.Size = new System.Drawing.Size(249, 198);
            this.findPage.TabIndex = 0;
            this.findPage.Text = "Find";
            // 
            // optionPanel
            // 
            this.optionPanel.BackColor = System.Drawing.Color.Transparent;
            this.optionPanel.Controls.Add(this.wholeWordCheckBox);
            this.optionPanel.Controls.Add(this.caseSentitiveCheckBox);
            this.optionPanel.Controls.Add(this.lookInLabel);
            this.optionPanel.Controls.Add(this.lookInComboBox);
            this.optionPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.optionPanel.Location = new System.Drawing.Point(3, 110);
            this.optionPanel.Name = "optionPanel";
            this.optionPanel.Padding = new System.Windows.Forms.Padding(3);
            this.optionPanel.Size = new System.Drawing.Size(243, 85);
            this.optionPanel.TabIndex = 6;
            // 
            // wholeWordCheckBox
            // 
            this.wholeWordCheckBox.AutoSize = true;
            this.wholeWordCheckBox.Location = new System.Drawing.Point(154, 58);
            this.wholeWordCheckBox.Name = "wholeWordCheckBox";
            this.wholeWordCheckBox.Size = new System.Drawing.Size(86, 17);
            this.wholeWordCheckBox.TabIndex = 7;
            this.wholeWordCheckBox.Text = "Whole Word";
            this.wholeWordCheckBox.UseVisualStyleBackColor = true;
            // 
            // caseSentitiveCheckBox
            // 
            this.caseSentitiveCheckBox.AutoSize = true;
            this.caseSentitiveCheckBox.Location = new System.Drawing.Point(12, 58);
            this.caseSentitiveCheckBox.Name = "caseSentitiveCheckBox";
            this.caseSentitiveCheckBox.Size = new System.Drawing.Size(96, 17);
            this.caseSentitiveCheckBox.TabIndex = 6;
            this.caseSentitiveCheckBox.Text = "Case Sensitive";
            this.caseSentitiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // lookInLabel
            // 
            this.lookInLabel.AutoSize = true;
            this.lookInLabel.Location = new System.Drawing.Point(9, 3);
            this.lookInLabel.Name = "lookInLabel";
            this.lookInLabel.Size = new System.Drawing.Size(45, 13);
            this.lookInLabel.TabIndex = 4;
            this.lookInLabel.Text = "Look in:";
            // 
            // lookInComboBox
            // 
            this.lookInComboBox.DisplayMember = "0";
            this.lookInComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lookInComboBox.FormattingEnabled = true;
            this.lookInComboBox.Location = new System.Drawing.Point(9, 16);
            this.lookInComboBox.Name = "lookInComboBox";
            this.lookInComboBox.Size = new System.Drawing.Size(231, 21);
            this.lookInComboBox.TabIndex = 5;
            // 
            // findPreviousButton
            // 
            this.findPreviousButton.Location = new System.Drawing.Point(87, 45);
            this.findPreviousButton.Name = "findPreviousButton";
            this.findPreviousButton.Size = new System.Drawing.Size(75, 23);
            this.findPreviousButton.TabIndex = 3;
            this.findPreviousButton.Text = "Find Prev.";
            this.findPreviousButton.UseVisualStyleBackColor = true;
            this.findPreviousButton.Click += new System.EventHandler(this.findPreviousButton_Click);
            // 
            // findNextButton
            // 
            this.findNextButton.Location = new System.Drawing.Point(168, 45);
            this.findNextButton.Name = "findNextButton";
            this.findNextButton.Size = new System.Drawing.Size(75, 23);
            this.findNextButton.TabIndex = 2;
            this.findNextButton.Text = "Find Next";
            this.findNextButton.UseVisualStyleBackColor = true;
            this.findNextButton.Click += new System.EventHandler(this.findNextButton_Click);
            // 
            // findWhatText
            // 
            this.findWhatText.Location = new System.Drawing.Point(9, 19);
            this.findWhatText.Name = "findWhatText";
            this.findWhatText.Size = new System.Drawing.Size(234, 20);
            this.findWhatText.TabIndex = 1;
            // 
            // findWhatLabel
            // 
            this.findWhatLabel.AutoSize = true;
            this.findWhatLabel.Location = new System.Drawing.Point(6, 3);
            this.findWhatLabel.Name = "findWhatLabel";
            this.findWhatLabel.Size = new System.Drawing.Size(63, 13);
            this.findWhatLabel.TabIndex = 0;
            this.findWhatLabel.Text = "Text to find:";
            // 
            // replacePage
            // 
            this.replacePage.Controls.Add(this.replaceAllButton);
            this.replacePage.Controls.Add(this.replaceButton);
            this.replacePage.Controls.Add(this.replaceWithText);
            this.replacePage.Controls.Add(this.replaceWithLabel);
            this.replacePage.Controls.Add(this.replaceWhatText);
            this.replacePage.Controls.Add(this.replaceWhatLabel);
            this.replacePage.Location = new System.Drawing.Point(4, 22);
            this.replacePage.Margin = new System.Windows.Forms.Padding(0);
            this.replacePage.Name = "replacePage";
            this.replacePage.Padding = new System.Windows.Forms.Padding(3);
            this.replacePage.Size = new System.Drawing.Size(249, 198);
            this.replacePage.TabIndex = 1;
            this.replacePage.Text = "Replace";
            this.replacePage.UseVisualStyleBackColor = true;
            // 
            // replaceAllButton
            // 
            this.replaceAllButton.Location = new System.Drawing.Point(87, 84);
            this.replaceAllButton.Name = "replaceAllButton";
            this.replaceAllButton.Size = new System.Drawing.Size(75, 23);
            this.replaceAllButton.TabIndex = 7;
            this.replaceAllButton.Text = "Replace All";
            this.replaceAllButton.UseVisualStyleBackColor = true;
            this.replaceAllButton.Click += new System.EventHandler(this.replaceAllButton_Click);
            // 
            // replaceButton
            // 
            this.replaceButton.Location = new System.Drawing.Point(168, 84);
            this.replaceButton.Name = "replaceButton";
            this.replaceButton.Size = new System.Drawing.Size(75, 23);
            this.replaceButton.TabIndex = 6;
            this.replaceButton.Text = "Replace";
            this.replaceButton.UseVisualStyleBackColor = true;
            this.replaceButton.Click += new System.EventHandler(this.replaceButton_Click);
            // 
            // replaceWithText
            // 
            this.replaceWithText.Location = new System.Drawing.Point(9, 58);
            this.replaceWithText.Name = "replaceWithText";
            this.replaceWithText.Size = new System.Drawing.Size(234, 20);
            this.replaceWithText.TabIndex = 5;
            // 
            // replaceWithLabel
            // 
            this.replaceWithLabel.AutoSize = true;
            this.replaceWithLabel.Location = new System.Drawing.Point(6, 42);
            this.replaceWithLabel.Name = "replaceWithLabel";
            this.replaceWithLabel.Size = new System.Drawing.Size(81, 13);
            this.replaceWithLabel.TabIndex = 4;
            this.replaceWithLabel.Text = "Text to replace:";
            // 
            // replaceWhatText
            // 
            this.replaceWhatText.Location = new System.Drawing.Point(9, 19);
            this.replaceWhatText.Name = "replaceWhatText";
            this.replaceWhatText.Size = new System.Drawing.Size(234, 20);
            this.replaceWhatText.TabIndex = 3;
            // 
            // replaceWhatLabel
            // 
            this.replaceWhatLabel.AutoSize = true;
            this.replaceWhatLabel.Location = new System.Drawing.Point(6, 3);
            this.replaceWhatLabel.Name = "replaceWhatLabel";
            this.replaceWhatLabel.Size = new System.Drawing.Size(63, 13);
            this.replaceWhatLabel.TabIndex = 2;
            this.replaceWhatLabel.Text = "Text to find:";
            // 
            // FindReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 224);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindReplace";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find and Replace";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindReplace_FormClosing);
            this.Load += new System.EventHandler(this.FindReplace_Load);
            this.tabControl.ResumeLayout(false);
            this.findPage.ResumeLayout(false);
            this.findPage.PerformLayout();
            this.optionPanel.ResumeLayout(false);
            this.optionPanel.PerformLayout();
            this.replacePage.ResumeLayout(false);
            this.replacePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage findPage;
        private System.Windows.Forms.TabPage replacePage;
        private System.Windows.Forms.Button findPreviousButton;
        private System.Windows.Forms.Button findNextButton;
        private System.Windows.Forms.TextBox findWhatText;
        private System.Windows.Forms.Label findWhatLabel;
        private System.Windows.Forms.ComboBox lookInComboBox;
        private System.Windows.Forms.Label lookInLabel;
        private System.Windows.Forms.Panel optionPanel;
        private System.Windows.Forms.CheckBox caseSentitiveCheckBox;
        private System.Windows.Forms.Button replaceAllButton;
        private System.Windows.Forms.Button replaceButton;
        private System.Windows.Forms.TextBox replaceWithText;
        private System.Windows.Forms.Label replaceWithLabel;
        private System.Windows.Forms.TextBox replaceWhatText;
        private System.Windows.Forms.Label replaceWhatLabel;
        private System.Windows.Forms.CheckBox wholeWordCheckBox;
    }
}
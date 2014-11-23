using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.Version.Text = String.Format("PawnPlus v{0}", Program.launcher.Version);

            DateTime dateTime = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
            this.BuildTimeLabel.Text = String.Format("Build time: {0} {1} {2} - {3}", dateTime.ToString("dd"), UppercaseFirst(dateTime.ToString("MMM")), dateTime.ToString("yyyy"), dateTime.ToString("HH:mm:ss"));

            this.Focus();
        }

        private void IssuesLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/WopsS/PawnPlus/issues");
        }

        private void PawnPlusIssuesLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://bugs.pawnplus.eu/");
        }

        private void Icons8Label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://icons8.com/");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string UppercaseFirst(string Text)
        {
            if (String.IsNullOrEmpty(Text))
                return string.Empty;

            return char.ToUpper(Text[0]) + Text.Substring(1);
        }
    }
}

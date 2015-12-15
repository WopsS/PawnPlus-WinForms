using System;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm(string title, string message, string stackTrace, bool continueButton)
        {
            InitializeComponent();

            this.Text = title;
            this.exceptionMessage.Text = message;
            this.stackTrace.Text = stackTrace;

            if (continueButton == false)
            {
                this.continueButton.Visible = false;
                this.quitButton.Location = this.continueButton.Location;
            }
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            int newHeight = this.Height;

            if (this.label.Height != 39)
            {
                newHeight += this.label.Height - 39;
            }

            if (this.exceptionMessage.Height != 13)
            {
                newHeight += this.exceptionMessage.Height - 13;
            }

            this.Height = newHeight;
        }

        private void ExceptionForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);
        }
    }
}

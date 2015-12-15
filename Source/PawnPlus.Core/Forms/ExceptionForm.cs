using PawnPlus.Core.Exceptions;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class ExceptionForm : Form
    {
        public ExceptionForm(ExceptionType type, string title, string message, string stackTrace, bool continueButton)
        {
            InitializeComponent();

            this.Text = title;
            this.exceptionMessage.Text = message;
            this.stackTrace.Text = stackTrace;

            if (type == ExceptionType.Unhandled)
            {
                if (continueButton == false)
                {
                    this.continueButton.Visible = false;
                    this.quitButton.Location = this.continueButton.Location;
                }

                this.label.Text = Localization.Text_UnhandledException;
            }
            else if (type == ExceptionType.Handled)
            {
                this.quitButton.Visible = false;
                this.label.Text = Localization.Text_HandledException;
            }
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {
            int newHeight = this.Height;

            if (this.label.Height != 39)
            {
                newHeight += this.label.Height - Math.Min(this.label.Height, 39);
            }

            if (this.exceptionMessage.Height != 13)
            {
                newHeight += this.exceptionMessage.Height - Math.Min(this.exceptionMessage.Height, 13);
            }

            this.Height = newHeight;
        }

        private void ExceptionForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(SystemIcons.Error, 16, 16);
        }
    }
}

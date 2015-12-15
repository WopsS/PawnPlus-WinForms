using System;
using System.Drawing;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class InputBox : Form
    {
        public InputBox(string name, string labelText, string textBoxValue)
        {
            InitializeComponent();

            this.Text = name;
            this.label.Text = labelText;
            this.textBox.Text = textBoxValue;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            this.TranslateUI();

            this.ClientSize = new Size(Math.Max(300, label.Right + 10), this.ClientSize.Height);

            this.AcceptButton = buttonOK;
            this.CancelButton = buttonCancel;
        }

        public DialogResult Show(ref string value)
        {
            DialogResult dialogResult = this.ShowDialog();
            value = this.textBox.Text;

            return dialogResult; 
        }

        private void TranslateUI()
        {
            this.buttonOK.Text = Localization.Text_OK;
            this.buttonCancel.Text = Localization.Text_Cancel;
        }
    }
}

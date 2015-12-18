using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using PawnPlus.Core.TextEditor;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core.Forms
{
    public partial class Editor : DockContent
    {
        public CodeTextEditor TextEditor { get; set; } = new CodeTextEditor();

        /// <summary>
        /// Path to the file which is edited.
        /// Empty by default.
        /// </summary>
        public string FileName
        {
            get
            {
                return this.TextEditor.Document.FileName;
            }

            set
            {
                this.Text = Path.GetFileName(value);
                this.TextEditor.Load(value);

                this.TextEditor.Document.FileName = value;
            }
        }

        /// <summary>
        /// Get the 'modified' flag.
        /// </summary>
        public bool IsModified { get { return this.TextEditor.IsModified; } }

        public bool HasProject { get; set; }

        protected ElementHost elementHost = new ElementHost();

        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            this.elementHost.Dock = DockStyle.Fill;
            this.elementHost.Child = this.TextEditor;

            this.Controls.Add(this.elementHost);

            this.TextEditor.Document.UpdateFinished += textEditor_UpdateFinished;

            ((IScrollInfo)this.TextEditor.TextArea).ScrollOwner.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            ((IScrollInfo)this.TextEditor.TextArea).ScrollOwner.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;

            string extenstion = Path.GetExtension(this.FileName);

            if (extenstion == ".pwn" || extenstion == ".inc")
            {
                Stream stream = null;

                try
                {
                    // Let's load the syntax hightlight.
                    stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.PAWNSyntax.xml");

                    using (XmlReader xmlReader = XmlReader.Create(stream))
                    {
                        stream = null;
                        this.TextEditor.SyntaxHighlighting = HighlightingLoader.Load(xmlReader, HighlightingManager.Instance);
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Dispose();
                    }
                }

                // this.TextEditor.TextArea.IndentationStrategy = new IndentationStrategy(this.TextEditor.Options);
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            Workspace.CloseFile(this.FileName);

            this.TextEditor.Document.UpdateFinished -= textEditor_UpdateFinished;
            this.elementHost.Dispose();
        }

        private void textEditor_UpdateFinished(object sender, EventArgs e)
        {
            if (this.IsModified == false && this.Text[this.Text.Length - 1] == '*')
            {
                this.Text = this.Text.Remove(this.Text.Length - 1);
            }
            else if (this.IsModified == true && this.Text[this.Text.Length - 1] != '*')
            {
                this.Text += '*';
            }
        }

        /// <summary>
        /// Copies the current selection to the clipboard. 
        /// The action will be performed if the current window is actived.
        /// </summary>
        public void Copy()
        {
            if (this.IsActivated == true)
            {
                this.TextEditor.Copy();
            }
        }

        /// <summary>
        /// Removes the current selection and copies it to the clipboard.
        /// The action will be performed if the current window is actived.
        /// </summary>
        public void Cut()
        {
            if (this.IsActivated == true)
            {
                this.TextEditor.Cut();
            }
        }

        /// <summary>
        /// Open a file.
        /// </summary>
        /// <param name="fileName">Path to the file.</param>
        public void Open(string fileName)
        {
            this.FileName = fileName;

            IntPtr hIcon;

            if (fileName.Contains(".inc"))
            {
                hIcon = Properties.Resources.IncludeIcon.GetHicon();
            }
            else
            {
                hIcon = Properties.Resources.FileIcon.GetHicon();
            }

            this.Icon = Icon.FromHandle(hIcon);
        }

        /// <summary>
        /// Saves the text to file.
        /// </summary>
        /// <param name="fileName">Path to the file.</param>
        public void Save(string fileName)
        {
            this.Text = Path.GetFileName(fileName);
            this.TextEditor.Save(fileName);
        }

        /// <summary>
        /// Saves the text to file.
        /// </summary>
        public void Save()
        {
            this.Save(this.FileName);
        }
    }
}

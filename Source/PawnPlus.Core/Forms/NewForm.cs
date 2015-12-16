using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PawnPlus.Core.Forms
{
    public partial class NewForm : Form
    {
        string fileFolder = string.Empty;
        NewFormType type;

        public NewForm(NewFormType type)
        {
            InitializeComponent();
            this.type = type;
        }

        public NewForm(NewFormType type, string path)
        {
            InitializeComponent();

            this.type = type;
            this.fileFolder = path;
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            this.TranslateUI();

            // Set correct size.
            if (this.type == NewFormType.File)
            {
                this.Height -= 30;
                this.Width -= 60;

                // Hide label, text box for path and browse button.
                this.pathLabel.Visible = false;
                this.pathBox.Visible = false;
                this.browseButton.Visible = false;

                // Set Y position at Y - 25 for type controls.
                this.typeLabel.Top -= 25;
                this.typeComboBox.Top -= 25;

                this.typeComboBox.Items.Add(Localization.Text_Filterscript);
                this.typeComboBox.Items.Add(Localization.Text_Gamemode);
                this.typeComboBox.Items.Add(Localization.Text_Include);
                this.typeComboBox.SelectedIndex = 0;
            }
            else
            {
                this.Height -= 30;

                // Hide label and combobox for type.
                this.typeLabel.Visible = false;
                this.typeComboBox.Visible = false;
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            if (this.folderBrowser.ShowDialog() == DialogResult.OK)
            {
                this.pathBox.Text = this.folderBrowser.SelectedPath;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            this.errorProvider.Clear();

            if (this.nameBox.Text.Length == 0)
            {
                this.errorProvider.SetError(this.nameBox, Localization.Error_EmptyName);
                return;
            }

            if (this.type == NewFormType.File)
            {
                CreateFile();
            }
            else
            {
                if (this.pathBox.Text.Length == 0)
                {
                    this.errorProvider.SetError(this.pathBox, Localization.Error_PathEmpty);
                    return;
                }

                CreateProject();
            }

            this.Close();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateFile()
        {
            Stream resourceStream = null;

            try
            {
                string extension = ".pwn";

                switch (this.typeComboBox.SelectedIndex)
                {
                    case 0: // It is a filterscript.
                    {
                        resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.ProjectFiles.Filterscript.pwn");

                        if (string.IsNullOrEmpty(this.fileFolder) == true)
                        {
                            this.fileFolder = "filterscripts";
                        }

                        break;
                    }
                    case 1: // It is a gamemode.
                    {
                        resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.ProjectFiles.Gamemode.pwn");

                        if (string.IsNullOrEmpty(this.fileFolder) == true)
                        {
                            this.fileFolder = "gamemodes";
                        }

                        break;
                    }
                    case 2: // It is a include.
                    {
                        resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.ProjectFiles.Include.inc");
                        extension = ".inc";

                        if (string.IsNullOrEmpty(this.fileFolder) == true)
                        {
                            this.fileFolder = "includes";
                        }

                        break;
                    }
                }

                using (StreamReader streamReader = new StreamReader(resourceStream))
                {
                    string stream = streamReader.ReadToEnd();
                    string folderPath = Path.Combine(Workspace.Project.BaseDirectory, this.fileFolder);
                    string filePath = Path.Combine(folderPath, string.Format("{0}{1}", this.nameBox.Text, extension));

                    if (Directory.Exists(folderPath) == false)
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    resourceStream = null;

                    File.WriteAllText(filePath, extension == ".inc" ? string.Format(stream, this.nameBox.Text.ToLower()) : stream);
                    ((Explorer)Application.OpenForms["ProjectExplorer"]).Add(TreeNodeType.File, filePath, true);
                }
            }
            finally
            {
                if (resourceStream != null)
                {
                    resourceStream.Dispose();
                }
            }
        }

        private void CreateProject()
        {
            Stream resourceStream = null;

            try
            {
                resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.ProjectFiles.Project.txt");

                if (Directory.Exists(this.folderBrowser.SelectedPath) == false)
                {
                    Directory.CreateDirectory(this.folderBrowser.SelectedPath);
                }

                string projectPath = Path.Combine(this.folderBrowser.SelectedPath, string.Format("{0}{1}", this.nameBox.Text, Project.Extension));

                using (StreamReader streamReader = new StreamReader(resourceStream))
                {
                    File.WriteAllText(projectPath, string.Format(streamReader.ReadToEnd(), this.nameBox.Text));
                }

                // Create default folders for Workspace.Project.
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "filterscripts"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "gamemodes"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "npcmodes"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "plugins"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "scriptfiles"));

                Project.Open(projectPath);
            }
            finally
            {
                if (resourceStream != null)
                {
                    resourceStream.Dispose();
                }
            }
        }

        private void TranslateUI()
        {
            this.Text += this.type == NewFormType.File ? Localization.Text_File.ToLower() : Localization.Text_Project.ToLower();
            this.nameLabel.Text = Localization.Text_Name;
            this.pathLabel.Text = Localization.Text_Path;
            this.typeLabel.Text = Localization.Text_Type;
            this.browseButton.Text = Localization.Text_Browse;
            this.closeButton.Text = Localization.Text_Close;
            this.createButton.Text = Localization.Text_Create;
        }
    }

    public enum NewFormType
    {
        File,
        Project
    }
}
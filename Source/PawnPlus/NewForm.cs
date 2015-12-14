using PawnPlus.Language;
using PawnPlus.Project;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PawnPlus
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
            // Translate controls.
            this.Text += this.type == NewFormType.File ? Translation.Text_File.ToLower() : Translation.Text_Project.ToLower();
            this.nameLabel.Text = Translation.Text_Name;
            this.pathLabel.Text = Translation.Text_Path;
            this.typeLabel.Text = Translation.Text_Type;
            this.browseButton.Text = Translation.Text_Browse;
            this.closeButton.Text = Translation.Text_Close;
            this.createButton.Text = Translation.Text_Create;

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

                this.typeComboBox.Items.Add(Translation.Text_Filterscript);
                this.typeComboBox.Items.Add(Translation.Text_Gamemode);
                this.typeComboBox.Items.Add(Translation.Text_Include);
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
                this.errorProvider.SetError(this.nameBox, Translation.Error_EmptyName);
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
                    this.errorProvider.SetError(this.pathBox, Translation.Error_PathEmpty);
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
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Filterscript.pwn");

                            if (string.IsNullOrEmpty(this.fileFolder) == true)
                            {
                                this.fileFolder = "filterscripts";
                            }

                            break;
                        }
                    case 1: // It is a gamemode.
                        {
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Gamemode.pwn");

                            if (string.IsNullOrEmpty(this.fileFolder) == true)
                            {
                                this.fileFolder = "gamemodes";
                            }

                            break;
                        }
                    case 2: // It is a include.
                        {
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Include.inc");
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
                    string folderPath = Path.Combine(ProjectManager.Path, this.fileFolder);
                    string filePath = Path.Combine(folderPath, string.Format("{0}{1}", this.nameBox.Text, extension));

                    if (Directory.Exists(folderPath) == false)
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    resourceStream = null;

                    File.WriteAllText(filePath, extension == ".inc" ? string.Format(stream, this.nameBox.Text.ToLower()) : stream);
                    ProjectManager.Add(Core.TreeNodeType.File, filePath);
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
                resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Project.txt");

                if (Directory.Exists(this.folderBrowser.SelectedPath) == false)
                {
                    Directory.CreateDirectory(this.folderBrowser.SelectedPath);
                }

                string projectPath = Path.Combine(this.folderBrowser.SelectedPath, string.Format("{0}{1}", this.nameBox.Text, ProjectManager.Extension));

                using (StreamReader streamReader = new StreamReader(resourceStream))
                {
                    File.WriteAllText(projectPath, string.Format(streamReader.ReadToEnd(), this.nameBox.Text));
                }

                // Create default folders for project.
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "filterscripts"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "gamemodes"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "npcmodes"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "plugins"));
                Directory.CreateDirectory(Path.Combine(this.folderBrowser.SelectedPath, "scriptfiles"));

                ProjectManager.Open(projectPath);
            }
            finally
            {
                if (resourceStream != null)
                {
                    resourceStream.Dispose();
                }
            }
        }
    }
}

public enum NewFormType
{
    File,
    Project
}
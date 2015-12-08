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
        NewFormType type;

        public NewForm(NewFormType type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void NewForm_Load(object sender, EventArgs e)
        {
            // Translate controls.
            this.Text += this.type == NewFormType.File ? LanguageManager.GetText(LanguageEnum.NewFormProjectFile) : LanguageManager.GetText(LanguageEnum.NewFormProject);
            this.nameLabel.Text = LanguageManager.GetText(LanguageEnum.NewFormName);
            this.pathLabel.Text = LanguageManager.GetText(LanguageEnum.NewFormPath);
            this.typeLabel.Text = LanguageManager.GetText(LanguageEnum.NewFormType);
            this.browseButton.Text = LanguageManager.GetText(LanguageEnum.NewFormBrowse);
            this.closeButton.Text = LanguageManager.GetText(LanguageEnum.NewFormClose);
            this.createButton.Text = LanguageManager.GetText(LanguageEnum.NewFormCreate);

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

                this.typeComboBox.Items.Add(LanguageManager.GetText(LanguageEnum.Filterscript));
                this.typeComboBox.Items.Add(LanguageManager.GetText(LanguageEnum.Gamemode));
                this.typeComboBox.Items.Add(LanguageManager.GetText(LanguageEnum.Include));
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
            if (this.nameBox.Text.Length == 0)
            {
                this.errorProvider.SetError(this.nameBox, LanguageManager.GetText(LanguageEnum.NewFormEmptyName));
                return;
            }

            if (this.type == NewFormType.File)
            {
                CreateFile();
                ProjectManager.LoadDirectory(ProjectManager.Path);
            }
            else
            {
                if (this.pathBox.Text.Length == 0)
                {
                    this.errorProvider.SetError(this.pathBox, LanguageManager.GetText(LanguageEnum.NewFormEmptyPath));
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
                string Extension = ".pwn";
                string Folder = string.Empty;

                switch (this.typeComboBox.SelectedIndex)
                {
                    case 0: // It is a filterscript.
                        {
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Filterscript.pwn");
                            Folder = "filterscripts";

                            break;
                        }
                    case 1: // It is a gamemode.
                        {
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Gamemode.pwn");
                            Folder = "gamemodes";

                            break;
                        }
                    case 2: // It is a include.
                        {
                            resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.ProjectFiles.Include.inc");
                            Extension = ".inc";
                            Folder = "includes";

                            break;
                        }
                }

                using (StreamReader streamReader = new StreamReader(resourceStream))
                {
                    string Stream = streamReader.ReadToEnd();
                    string FolderPath = Path.Combine(ProjectManager.Path, Folder);

                    if (Directory.Exists(FolderPath) == false)
                    {
                        Directory.CreateDirectory(FolderPath);
                    }

                    resourceStream = null;

                    File.WriteAllText(Path.Combine(FolderPath, string.Format("{0}{1}", this.nameBox.Text, Extension)), Extension == ".inc" ? string.Format(Stream, this.nameBox.Text.ToLower()) : Stream);
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
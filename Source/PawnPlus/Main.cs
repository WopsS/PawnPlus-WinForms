using ICSharpCode.AvalonEdit;
using PawnPlus.CodeEditor;
using PawnPlus.Core;
using PawnPlus.Language;
using PawnPlus.Project;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class Main : Form
    {
        private ProjectExplorer projectExplorer = new ProjectExplorer();
        private Output outputForm = new Output();

        private DeserializeDockContent dockContentLayout;
        private FindReplace findReplace = new FindReplace();
        private string layoutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Layout.xml");

        public Main()
        {
            InitializeComponent();

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.dockContentLayout = new DeserializeDockContent(this.GetLayout);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(this.layoutPath) == true)
            {
                this.dockPanel.LoadFromXml(this.layoutPath, dockContentLayout);
            }
            else
            {
                Stream xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.DefaultLayout.xml");
                this.dockPanel.LoadFromXml(xmlStream, dockContentLayout);
                xmlStream.Close();
            }

            CEManager.Construct(this.dockPanel);
            StatusManager.Construct(this.statusBar, this.statusLabel, this.lineLabel, this.columnLabel);

            this.SetLanguageText();
            StatusManager.Set(StatusType.Info, Translation.Status_Ready, StatusReset.None);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Let's check if project is open and let's close files from project. After that let's close all files.
            if ((ProjectManager.IsOpen == true && ProjectManager.Close() == true) || (ProjectManager.IsOpen == false && CEManager.CloseAll(false) == true))
            {
                e.Cancel = true;
                return;
            }

            if (Directory.Exists(Path.GetDirectoryName(this.layoutPath)) == false) // Check if path exist, if not create it.
            {
                Directory.CreateDirectory(Path.GetDirectoryName(this.layoutPath));
            }

            if (File.Exists(this.layoutPath) == true) // Check if the "Layout.xml" already exist, if it exist let's delete it.
            {
                File.Delete(this.layoutPath);
            }

            dockPanel.SaveAsXml(this.layoutPath);

            Application.ExitThread(); // Close the entire application.
        }

        #region Main functions

        private Rectangle[] Regions = new Rectangle[10];

        private Point FormStartResize, FormResizePoints, FormMousePosition;

        private bool FormMoving, FormRezising, FormRezisingLeft, FormRezisingRight, FormRezisingTop, FormRezisingBottom, FormRezisingTopRight, FormRezisingTopLeft, FormRezisingBottomRight, FormRezisingBottomLeft;

        const int WS_MINIMIZEBOX = 0x20000, CS_DBLCLKS = 0x8;

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (Regions[1].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTopLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[2].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTop = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[3].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTopRight = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[4].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[6].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingRight = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[7].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingBottomLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[8].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingBottom = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Regions[9].Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingBottomRight = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }

                        break;
                    }
            }
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            FormResizePoints = PointToScreen(new Point(e.X, e.Y));
            Regions[0] = this.Bounds;

            if (Capture == true)
            {
                if (FormRezising == true)
                {
                    if (FormRezisingTopLeft)
                        this.Bounds = new Rectangle(Regions[0].X + FormResizePoints.X - FormStartResize.X, Regions[0].Y + FormResizePoints.Y - FormStartResize.Y, Regions[0].Width - FormResizePoints.X + FormStartResize.X, Regions[0].Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingTop)
                        this.Bounds = new Rectangle(Regions[0].X, Regions[0].Y + FormResizePoints.Y - FormStartResize.Y, Regions[0].Width, Regions[0].Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingTopRight)
                        this.Bounds = new Rectangle(Regions[0].X, Regions[0].Y + FormResizePoints.Y - FormStartResize.Y, Regions[0].Width + FormResizePoints.X - FormStartResize.X, Regions[0].Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingLeft)
                        this.Bounds = new Rectangle(Regions[0].X + FormResizePoints.X - FormStartResize.X, Regions[0].Y, Regions[0].Width - FormResizePoints.X + FormStartResize.X, Regions[0].Height);
                    else if (FormRezisingRight)
                        this.Bounds = new Rectangle(Regions[0].X, Regions[0].Y, Regions[0].Width + FormResizePoints.X - FormStartResize.X, Regions[0].Height);
                    else if (FormRezisingBottomLeft)
                        this.Bounds = new Rectangle(Regions[0].X + FormResizePoints.X - FormStartResize.X, Regions[0].Y, Regions[0].Width - FormResizePoints.X + FormStartResize.X, Regions[0].Height + FormResizePoints.Y - FormStartResize.Y);
                    else if (FormRezisingBottom)
                        this.Bounds = new Rectangle(Regions[0].X, Regions[0].Y, Regions[0].Width, Regions[0].Height + FormResizePoints.Y - FormStartResize.Y);
                    else if (FormRezisingBottomRight)
                        this.Bounds = new Rectangle(Regions[0].X, Regions[0].Y, Regions[0].Width + FormResizePoints.X - FormStartResize.X, Regions[0].Height + FormResizePoints.Y - FormStartResize.Y);

                    FormStartResize = FormResizePoints;
                    Refresh();
                }
            }
            else
            {
                FormMousePosition = new Point(e.X, e.Y);

                if (Regions[1].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNWSE;
                else if (Regions[2].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNS;
                else if (Regions[3].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNESW;
                else if (Regions[4].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeWE;
                else if (Regions[5].Contains(FormMousePosition))
                    this.Cursor = Cursors.Default;
                else if (Regions[6].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeWE;
                else if (Regions[7].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNESW;
                else if (Regions[8].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNS;
                else if (Regions[9].Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNWSE;
            }
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            FormRezising = false;

            FormRezisingLeft = false;
            FormRezisingRight = false;

            FormRezisingTop = false;
            FormRezisingBottom = false;

            FormRezisingTopRight = false;
            FormRezisingTopLeft = false;

            FormRezisingBottomRight = false;
            FormRezisingBottomLeft = false;

            Refresh();
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Regions[1] = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y), new Size(3, 3));
                Regions[2] = new Rectangle(new Point(ClientRectangle.X + Regions[1].Width, ClientRectangle.Y), new Size(ClientRectangle.Width - (Regions[1].Width * 2), Regions[1].Height));
                Regions[3] = new Rectangle(new Point(ClientRectangle.X + Regions[1].Width + Regions[2].Width, ClientRectangle.Y), new Size(3, 3));

                Regions[4] = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y + Regions[1].Height), new Size(Regions[1].Width, ClientRectangle.Height - (Regions[1].Width * 2)));
                Regions[5] = new Rectangle(new Point(ClientRectangle.X + Regions[4].Width, ClientRectangle.Y + Regions[1].Height), new Size(Regions[2].Width, Regions[4].Height));
                Regions[6] = new Rectangle(new Point(ClientRectangle.X + Regions[4].Width + Regions[5].Width, ClientRectangle.Y + Regions[1].Height), new Size(Regions[3].Width, Regions[4].Height));

                Regions[7] = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y + Regions[1].Height + Regions[4].Height), new Size(3, 3));
                Regions[8] = new Rectangle(new Point(ClientRectangle.X + Regions[7].Width, ClientRectangle.Y + Regions[1].Height + Regions[4].Height), new Size(ClientRectangle.Width - (Regions[7].Width * 2), Regions[7].Height));
                Regions[9] = new Rectangle(new Point(ClientRectangle.X + Regions[7].Width + Regions[8].Width, ClientRectangle.Y + Regions[1].Height + Regions[4].Height), new Size(3, 3));

                Graphics GFX = e.Graphics;

                SolidBrush Blue = new SolidBrush(Color.FromArgb(0, 122, 204));

                GFX.FillRectangle(Blue, Regions[1]);
                GFX.FillRectangle(Blue, Regions[2]);
                GFX.FillRectangle(Blue, Regions[3]);
                GFX.FillRectangle(Blue, Regions[4]);
                GFX.FillRectangle(Blue, Regions[6]);
                GFX.FillRectangle(Blue, Regions[7]);
                GFX.FillRectangle(Blue, Regions[8]);
                GFX.FillRectangle(Blue, Regions[9]);

            }
            else
            {
                Graphics GFX = e.Graphics;

                GFX.FillRectangle(Brushes.Transparent, Regions[1]);
                GFX.FillRectangle(Brushes.Transparent, Regions[2]);
                GFX.FillRectangle(Brushes.Transparent, Regions[3]);
                GFX.FillRectangle(Brushes.Transparent, Regions[4]);
                GFX.FillRectangle(Brushes.Transparent, Regions[6]);
                GFX.FillRectangle(Brushes.Transparent, Regions[7]);
                GFX.FillRectangle(Brushes.Transparent, Regions[8]);
                GFX.FillRectangle(Brushes.Transparent, Regions[9]);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.Height < 250)
            {
                this.Height = 250;
            }
            else if (this.Width < 350)
            {
                this.Width = 350;
            }
        }

        private void principalPanel_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;

                this.Width = Screen.PrimaryScreen.Bounds.Width / 2;
                this.Height = Screen.PrimaryScreen.Bounds.Height / 2;

                int PositionX = Screen.PrimaryScreen.Bounds.Width - this.Width;
                int PositionY = Screen.PrimaryScreen.Bounds.Height - this.Height;

                this.Location = new Point(PositionX / 2, PositionY / 2);

                this.Padding = new Padding(5, 5, 5, 5);

            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                this.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void principalPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                FormMoving = true;

                FormMousePosition = new Point(e.X, e.Y);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void principalPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormMoving == true)
            {
                this.Location = new Point(MousePosition.X - FormMousePosition.X, MousePosition.Y - FormMousePosition.Y);
            }
        }

        private void principalPanel_MouseUp(object sender, MouseEventArgs e)
        {
            FormMoving = false;
            this.Cursor = Cursors.Default;
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;

                this.Width = Screen.PrimaryScreen.Bounds.Width / 2;
                this.Height = Screen.PrimaryScreen.Bounds.Height / 2;

                int PositionX = Screen.PrimaryScreen.Bounds.Width - this.Width;
                int PositionY = Screen.PrimaryScreen.Bounds.Height - this.Height;

                this.Location = new Point(PositionX / 2, PositionY / 2);

                this.Padding = new Padding(5, 5, 5, 5);

            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
                this.Padding = new Padding(0, 0, 0, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override CreateParams CreateParams // With this form can be minimized from the taskbar.
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;

                return cp;
            }
        }

        public DeserializeDockContent DockContentLayout1
        {
            get
            {
                return dockContentLayout;
            }

            set
            {
                this.dockContentLayout = value;
            }
        }

        public DeserializeDockContent DockContentLayout2
        {
            get
            {
                return dockContentLayout;
            }

            set
            {
                this.dockContentLayout = value;
            }
        }

        #endregion

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveDocument != null)
            {
                Editor editor = (Editor)this.dockPanel.ActiveDocument.DockHandler.Form;

                CEManager.SetActiveDocument(editor);
                StatusManager.SetLineColumn(CEManager.ActiveDocument.codeEditor.TextArea.Caret.Line, CEManager.ActiveDocument.codeEditor.TextArea.Caret.Column);

                this.saveToolStripMenuItem.Text = string.Format(Translation.Text_Save, Path.GetFileName(editor.FilePath));
                this.savesAsToolStripMenuItem.Text = string.Format(Translation.Text_SaveAs, Path.GetFileName(editor.FilePath));

                this.SetMenuStatus(true, false);
            }
            else
            {
                CEManager.SetActiveDocument(null);

                this.SetMenuStatus(false, false);
                StatusManager.SetLineColumn(0, 0);

                this.saveToolStripMenuItem.Text = string.Format(Translation.Text_Save, Translation.Text_SelectedItem);
                this.savesAsToolStripMenuItem.Text = string.Format(Translation.Text_SaveAs, Translation.Text_SelectedItem);

                // Clear output box.
                this.outputForm.ClearText();
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm newForm = new NewForm(NewFormType.Project);
            newForm.ShowDialog(this);
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm newForm = new NewForm(NewFormType.File);
            newForm.ShowDialog(this);
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = string.Format("PawnPlus Project|*{0}", ProjectManager.Extension);

            DialogResult dialogResult = this.openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                ProjectManager.Open(this.openFileDialog.FileName);

                this.SetMenuStatus(true, true, true);
                this.SetFormName(Name);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "PAWN File|*.pwn|Include file|*.inc";

            DialogResult dialogResult = this.openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CEManager.Open(this.openFileDialog.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dockPanel.ActiveDocument.DockHandler.Form.Close();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.Close();
            this.SetMenuStatus(false, true);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((Editor)this.dockPanel.ActiveDocument.DockHandler.Form).Save();
        }

        private void savesAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.saveFileDialog.Filter = "PAWN File|*.pwn|Include file|*.inc|All files|*.*";

            DialogResult dialogResult = this.saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                ((Editor)this.dockPanel.ActiveDocument.DockHandler.Form).Save(this.saveFileDialog.FileName);
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Editor editor in CEManager.Get().Values)
            {
                if (editor.IsModified == true)
                {
                    editor.Save();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEManager.ActiveDocument.codeEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEManager.ActiveDocument.codeEditor.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEManager.ActiveDocument.codeEditor.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEManager.ActiveDocument.codeEditor.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CEManager.ActiveDocument.codeEditor.Paste();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findReplace.ShowFind(this, CEManager.ActiveDocument.codeEditor.SelectedText);
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findReplace.FindNext(string.Empty, true);
        }

        private void findPrevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findReplace.FindPrevious(string.Empty, true);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findReplace.ShowReplace(this, CEManager.ActiveDocument.codeEditor.SelectedText);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToLine gotoLine = new GoToLine();
            gotoLine.ShowDialog(this);
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextEditor textEditor = CEManager.ActiveDocument.codeEditor;

            if (Path.GetExtension(CEManager.ActiveDocument.FilePath) != ".pwn" || this.compilerWorker.IsBusy == true)
            {
                return;
            }
            else if (string.IsNullOrEmpty(textEditor.Text) == true || string.IsNullOrWhiteSpace(textEditor.Text) == true)
            {
                StatusManager.Set(StatusType.Error, Translation.Status_EmptyText, StatusReset.FiveSeconds);
                return;
            }

            this.outputForm.ClearText();

            // Saving all files opened.
            StatusManager.Set(StatusType.Warning, Translation.Status_SavingFiles, StatusReset.None);

            foreach (Editor editor in CEManager.Get().Values)
            {
                if (editor.IsModified == true)
                {
                    editor.Save();
                }
            }

            if (ProjectManager.IsOpen == true)
            {
                // Copying all includes files to PAWN include folder.
                StatusManager.Set(StatusType.Warning, Translation.Status_IncludesCopying, StatusReset.None);

                string targetAppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Pawn", "include");
                string projectDirectory = Path.Combine(ProjectManager.Path, "includes");

                // Check if our directories exists.
                if (Directory.Exists(targetAppDataDirectory) == true && Directory.Exists(projectDirectory) == true)
                {
                    foreach (string file in Directory.GetFiles(projectDirectory))
                    {
                        File.Copy(file, Path.Combine(targetAppDataDirectory, Path.GetFileName(file)), true);
                    }
                }
            }

            // Disable 'Save*' menu items.
            this.saveToolStripMenuItem.Enabled = false;
            this.savesAsToolStripMenuItem.Enabled = false;
            this.saveAllToolStripMenuItem.Enabled = false;

            StatusManager.Set(StatusType.Warning, Translation.Status_Compiling, StatusReset.None);
            this.compilerWorker.RunWorkerAsync();
        }

        private void compileOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = Properties.Settings.Default["Compiler_Arguments"].ToString();

            if(WindowHelper.InputBox(Translation.Text_CompilerOptions, string.Format("{0}:", Translation.Text_Options), ref value) == DialogResult.OK)
            {
                Properties.Settings.Default["Compiler_Arguments"] = value;
                Properties.Settings.Default.Save();
            }
        }

        private void compilerWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string amxPath = Path.Combine(Path.GetDirectoryName(CEManager.ActiveDocument.FilePath), string.Format("{0}.amx", Path.GetFileNameWithoutExtension(CEManager.ActiveDocument.FilePath)));

            Process compilingProcess = new Process();

            compilingProcess.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Pawn", "pawncc.exe");
            compilingProcess.StartInfo.Arguments = string.Format("\"{0}\" -o\"{1}\" -;+ -(+ {2}", CEManager.ActiveDocument.FilePath, amxPath, Properties.Settings.Default["Compiler_Arguments"].ToString());
            compilingProcess.StartInfo.UseShellExecute = false;
            compilingProcess.StartInfo.CreateNoWindow = true;
            compilingProcess.StartInfo.RedirectStandardError = true;
            compilingProcess.StartInfo.RedirectStandardOutput = true;
            compilingProcess.Start();

            while (compilingProcess.HasExited == false)
            {
                ProjectManager.LastCompilation.Errors = compilingProcess.StandardError.ReadToEnd();
                ProjectManager.LastCompilation.Output = compilingProcess.StandardOutput.ReadToEnd();
            }

            // Replace path to includes and gamemodes or filterscripts.
            if (string.IsNullOrEmpty(ProjectManager.Path) == false)
            {
                ProjectManager.LastCompilation.Errors = ProjectManager.LastCompilation.Errors.Replace(string.Format("{0}\\", Path.Combine(ProjectManager.Path, "filterscripts")), string.Empty);
                ProjectManager.LastCompilation.Errors = ProjectManager.LastCompilation.Errors.Replace(string.Format("{0}\\", Path.Combine(ProjectManager.Path, "gamemodes")), string.Empty);
            }

            ProjectManager.LastCompilation.Errors = ProjectManager.LastCompilation.Errors.Replace(string.Format("{0}\\", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Pawn", "include")), string.Empty);
        }

        private void compilerWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (ProjectManager.IsOpen == true)
            {
                // Deleting all includes files to PAWN include folder.
                StatusManager.Set(StatusType.Warning, Translation.Status_IncludesDeleting, StatusReset.None);

                string targetAppDataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PawnPlus", "Pawn", "include");
                string projectDirectory = Path.Combine(ProjectManager.Path, "includes");

                // Check if our directories exists.
                if (Directory.Exists(targetAppDataDirectory) == true && Directory.Exists(projectDirectory) == true)
                {
                    foreach (string file in Directory.GetFiles(projectDirectory))
                    {
                        File.Delete(Path.Combine(targetAppDataDirectory, Path.GetFileName(file)));
                    }
                }
            }

            // Enable 'Save*' menu items.
            this.saveToolStripMenuItem.Enabled = true;
            this.savesAsToolStripMenuItem.Enabled = true;
            this.saveAllToolStripMenuItem.Enabled = true;

            if (ProjectManager.LastCompilation.HasErrors == true)
            {
                this.outputForm.SetText(ProjectManager.LastCompilation.Errors, false);
                StatusManager.Set(StatusType.Error, Translation.Status_CompiledError, StatusReset.FiveSeconds);
            }
            else
            {
                StatusManager.Set(StatusType.Finish, Translation.Status_Compiled, StatusReset.FiveSeconds);
            }

            this.outputForm.SetText(ProjectManager.LastCompilation.Output, true);
        }

        /// <summary>
        /// Change enable value for <see cref="MenuStrip"/> items.
        /// </summary>
        /// <param name="isEnabled">If it will be true, then MenuStrip items will be enabled.</param>
        /// <param name="isProject">If it will be true, then some items for project will be enabled.</param>
        public void SetMenuStatus(bool isEnabled, bool isProject, bool justProjectItems = false)
        {
            if (isProject == true)
            {
                this.newFileToolStripMenuItem.Enabled = isEnabled;
                this.closeProjectToolStripMenuItem.Enabled = isEnabled;
            }

            if (justProjectItems == true)
            {
                return;
            }

            this.closeToolStripMenuItem.Enabled = isEnabled;
            this.saveToolStripMenuItem.Enabled = isEnabled;
            this.savesAsToolStripMenuItem.Enabled = isEnabled;
            this.saveAllToolStripMenuItem.Enabled = isEnabled;

            this.undoToolStripMenuItem.Enabled = isEnabled;
            this.redoToolStripMenuItem.Enabled = isEnabled;
            this.cutToolStripMenuItem.Enabled = isEnabled;
            this.copyToolStripMenuItem.Enabled = isEnabled;
            this.pasteToolStripMenuItem.Enabled = isEnabled;
            this.findToolStripMenuItem.Enabled = isEnabled;
            this.findNextToolStripMenuItem.Enabled = isEnabled;
            this.findPrevToolStripMenuItem.Enabled = isEnabled;
            this.replaceToolStripMenuItem.Enabled = isEnabled;
            this.goToToolStripMenuItem.Enabled = isEnabled;

            this.compileToolStripMenuItem.Enabled = isEnabled;
            this.compileOptionsToolStripMenuItem.Enabled = isEnabled;
        }

        /// <summary>
        /// Set form name.
        /// </summary>
        /// <param name="name">New name of the form.</param>
        public void SetFormName(string name)
        {
            this.FormName.Text = string.Format("PawnPlus - {0}", name);
        }

        /// <summary>
        /// Get layout of the program.
        /// </summary>
        /// <param name="layoutString">Path to the layout file.</param>
        /// <returns>Returns <see cref="IDockContent"/>.</returns>
        private IDockContent GetLayout(string layoutString)
        {
            if (layoutString != typeof(Editor).ToString())
            {
                if (layoutString == typeof(ProjectExplorer).ToString())
                {
                    return this.projectExplorer;
                }
                else if (layoutString == typeof(Output).ToString())
                {
                    return this.outputForm;
                }
            }

            return null;
        }

        /// <summary>
        /// Set language for whole application buttons or menu.
        /// </summary>
        private void SetLanguageText()
        {
            // File tool strip menu item.
            this.fileToolStripMenuItem.Text = Translation.Text_File;
            this.newToolStripMenuItem.Text = Translation.Text_New;
            this.newProjectToolStripMenuItem.Text = Translation.Text_Project;
            this.newFileToolStripMenuItem.Text = Translation.Text_File;
            this.openToolStripMenuItem.Text = Translation.Text_Open;
            this.openProjectToolStripMenuItem.Text = Translation.Text_Project;
            this.openFileToolStripMenuItem.Text = Translation.Text_File;
            this.closeToolStripMenuItem.Text = Translation.Text_Close;
            this.closeProjectToolStripMenuItem.Text = Translation.Text_CloseProject;
            this.saveToolStripMenuItem.Text = string.Format(Translation.Text_Save, Translation.Text_SelectedItem);
            this.savesAsToolStripMenuItem.Text = string.Format(Translation.Text_SaveAs, Translation.Text_SelectedItem);
            this.saveAllToolStripMenuItem.Text = Translation.Text_SaveAll;

            // Edit tool strip menu item.
            this.editToolStripMenuItem.Text = Translation.Text_Edit;
            this.undoToolStripMenuItem.Text = Translation.Text_Undo;
            this.redoToolStripMenuItem.Text = Translation.Text_Redo;
            this.cutToolStripMenuItem.Text = Translation.Text_Cut;
            this.copyToolStripMenuItem.Text = Translation.Text_Copy;
            this.pasteToolStripMenuItem.Text = Translation.Text_Paste;
            this.findToolStripMenuItem.Text = Translation.Text_Find;
            this.findNextToolStripMenuItem.Text = Translation.Text_FindNext;
            this.findPrevToolStripMenuItem.Text = Translation.Text_FindPrevious;
            this.replaceToolStripMenuItem.Text = Translation.Text_Replace;
            this.goToToolStripMenuItem.Text = Translation.Text_GoTo;

            // Build tool strip menu item.
            this.buildToolStripMenuItem.Text = Translation.Text_Build;
            this.compileToolStripMenuItem.Text = Translation.Text_Compile;
            this.compileOptionsToolStripMenuItem.Text = Translation.Text_CompilerOptions;

            // Status bar.
            this.statusLabel.Text = Translation.Status_Ready;

            FileVersionInfo Program = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            this.versionLabel.Text = string.Format(Translation.Status_Version, Program.ProductMajorPart, Program.ProductMinorPart, Program.ProductBuildPart);

            this.lineLabel.Text = string.Format(Translation.Status_Line, 0);
            this.columnLabel.Text = string.Format(Translation.Status_Column, 0);
        }
    }
}

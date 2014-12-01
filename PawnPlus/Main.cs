using DigitalRune.Windows.TextEditor;
using DigitalRune.Windows.TextEditor.Actions;
using DigitalRune.Windows.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    // FEATURE: Add auto downloader to download request files from pawnplus.eu.
    public partial class Main : Form
    {
        public bool CloseApplication = false;
        public string LayoutFile = null;

        public Dictionary<string, string> ProjectInformation = new Dictionary<string, string>();
        public Dictionary<string, CodeEditor> CodeEditors = new Dictionary<string, CodeEditor>();
        public Dictionary<string, MethodInformations> MethodsList = new Dictionary<string, MethodInformations>();
        public List<string> ProjectOpenedFiles = new List<string>();

        /// <summary>
        /// Path for the current CodeEditor.
        /// </summary>
        public string CodeEditorPath = null;

        DeserializeDockContent DockContentLayout;

        public Main()
        {
            InitializeComponent();

            DockContentLayout = new DeserializeDockContent(GetLayout);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            #region LoadLayout

            if (File.Exists(this.LayoutFile))
                dockPanel.LoadFromXml(this.LayoutFile, DockContentLayout);
            else
            {
                Stream xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Resources.DefaultLayout.xml");
                dockPanel.LoadFromXml(xmlStream, DockContentLayout);
                xmlStream.Close();
            }

            #endregion

            Program.findreplace.Show(this);
            Program.findreplace.Hide();

            menuStrip.Renderer = new ToolStripProfessionalRenderer(new MenuStripColorTable());

            if (Program.projectexplorer.Visible == true && Program.projectexplorer.IsDisposed == false && Program.projectexplorer.IsHidden == false)
                this.projectExplorerToolStripMenuItem.Checked = true;
            else
                this.projectExplorerToolStripMenuItem.Checked = false;

            if (Program.output.Visible == true && Program.output.IsDisposed == false && Program.output.IsHidden == false)
                this.outputToolStripMenuItem.Checked = true;
            else
                this.outputToolStripMenuItem.Checked = false;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CompilerWorker.IsBusy == true || this.CloseProject() == false)
            {
                e.Cancel = true;

                return;
            }

            this.CloseApplication = true;

            #region Savelayout
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PawnPlus"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PawnPlus");

            string ConfigFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\PawnPlus", "Layout.xml");

            if (File.Exists(ConfigFile))
                File.Delete(ConfigFile);

            dockPanel.SaveAsXml(ConfigFile);
            #endregion
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit(); Commented for a while.
        }

        private IDockContent GetLayout(string LayoutString)
        {
            if (LayoutString != typeof(CodeEditor).ToString())
            {
                if (LayoutString == typeof(ProjectExplorer).ToString())
                    return Program.projectexplorer;
                else if (LayoutString == typeof(Output).ToString())
                    return Program.output;
            }

            return null;
        }

        #region Main Functions

        private Rectangle Region0, Region1, Region2, Region3, Region4, Region5, Region6, Region7, Region8, Region9;

        private Point FormStartResize, FormResizePoints, FormMousePosition;

        private bool FormMoving, FormRezising, FormRezisingLeft, FormRezisingRight, FormRezisingTop, FormRezisingBottom, FormRezisingTopRight, FormRezisingTopLeft, FormRezisingBottomRight, FormRezisingBottomLeft;

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    {
                        if (Region1.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTopLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region2.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTop = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region3.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingTopRight = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region4.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region6.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingRight = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region7.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingBottomLeft = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region8.Contains(FormMousePosition))
                        {
                            FormRezising = true;
                            FormRezisingBottom = true;
                            FormStartResize = PointToScreen(new Point(e.X, e.Y));
                        }
                        else if (Region9.Contains(FormMousePosition))
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
            Region0 = this.Bounds;

            if (Capture == true)
            {
                if (FormRezising == true)
                {
                    if (FormRezisingTopLeft)
                        this.Bounds = new Rectangle(Region0.X + FormResizePoints.X - FormStartResize.X, Region0.Y + FormResizePoints.Y - FormStartResize.Y, Region0.Width - FormResizePoints.X + FormStartResize.X, Region0.Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingTop)
                        this.Bounds = new Rectangle(Region0.X, Region0.Y + FormResizePoints.Y - FormStartResize.Y, Region0.Width, Region0.Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingTopRight)
                        this.Bounds = new Rectangle(Region0.X, Region0.Y + FormResizePoints.Y - FormStartResize.Y, Region0.Width + FormResizePoints.X - FormStartResize.X, Region0.Height - FormResizePoints.Y + FormStartResize.Y);
                    else if (FormRezisingLeft)
                        this.Bounds = new Rectangle(Region0.X + FormResizePoints.X - FormStartResize.X, Region0.Y, Region0.Width - FormResizePoints.X + FormStartResize.X, Region0.Height);
                    else if (FormRezisingRight)
                        this.Bounds = new Rectangle(Region0.X, Region0.Y, Region0.Width + FormResizePoints.X - FormStartResize.X, Region0.Height);
                    else if (FormRezisingBottomLeft)
                        this.Bounds = new Rectangle(Region0.X + FormResizePoints.X - FormStartResize.X, Region0.Y, Region0.Width - FormResizePoints.X + FormStartResize.X, Region0.Height + FormResizePoints.Y - FormStartResize.Y);
                    else if (FormRezisingBottom)
                        this.Bounds = new Rectangle(Region0.X, Region0.Y, Region0.Width, Region0.Height + FormResizePoints.Y - FormStartResize.Y);
                    else if (FormRezisingBottomRight)
                        this.Bounds = new Rectangle(Region0.X, Region0.Y, Region0.Width + FormResizePoints.X - FormStartResize.X, Region0.Height + FormResizePoints.Y - FormStartResize.Y);

                    FormStartResize = FormResizePoints;
                    Refresh();
                }
            }
            else
            {
                FormMousePosition = new Point(e.X, e.Y);

                if (Region1.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNWSE;
                else if (Region2.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNS;
                else if (Region3.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNESW;
                else if (Region4.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeWE;
                else if (Region5.Contains(FormMousePosition))
                    this.Cursor = Cursors.Default;
                else if (Region6.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeWE;
                else if (Region7.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNESW;
                else if (Region8.Contains(FormMousePosition))
                    this.Cursor = Cursors.SizeNS;
                else if (Region9.Contains(FormMousePosition))
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
                Region1 = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y), new Size(3, 3));
                Region2 = new Rectangle(new Point(ClientRectangle.X + Region1.Width, ClientRectangle.Y), new Size(ClientRectangle.Width - (Region1.Width * 2), Region1.Height));
                Region3 = new Rectangle(new Point(ClientRectangle.X + Region1.Width + Region2.Width, ClientRectangle.Y), new Size(3, 3));

                Region4 = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y + Region1.Height), new Size(Region1.Width, ClientRectangle.Height - (Region1.Width * 2)));
                Region5 = new Rectangle(new Point(ClientRectangle.X + Region4.Width, ClientRectangle.Y + Region1.Height), new Size(Region2.Width, Region4.Height));
                Region6 = new Rectangle(new Point(ClientRectangle.X + Region4.Width + Region5.Width, ClientRectangle.Y + Region1.Height), new Size(Region3.Width, Region4.Height));

                Region7 = new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Y + Region1.Height + Region4.Height), new Size(3, 3));
                Region8 = new Rectangle(new Point(ClientRectangle.X + Region7.Width, ClientRectangle.Y + Region1.Height + Region4.Height), new Size(ClientRectangle.Width - (Region7.Width * 2), Region7.Height));
                Region9 = new Rectangle(new Point(ClientRectangle.X + Region7.Width + Region8.Width, ClientRectangle.Y + Region1.Height + Region4.Height), new Size(3, 3));

                Graphics GFX = e.Graphics;

                SolidBrush Blue = new SolidBrush(Color.FromArgb(0, 122, 204));

                GFX.FillRectangle(Blue, Region1);
                GFX.FillRectangle(Blue, Region2);
                GFX.FillRectangle(Blue, Region3);
                GFX.FillRectangle(Blue, Region4);
                GFX.FillRectangle(Blue, Region6);
                GFX.FillRectangle(Blue, Region7);
                GFX.FillRectangle(Blue, Region8);
                GFX.FillRectangle(Blue, Region9);

            }
            else
            {
                Graphics GFX = e.Graphics;

                GFX.FillRectangle(Brushes.Transparent, Region1);
                GFX.FillRectangle(Brushes.Transparent, Region2);
                GFX.FillRectangle(Brushes.Transparent, Region3);
                GFX.FillRectangle(Brushes.Transparent, Region4);
                GFX.FillRectangle(Brushes.Transparent, Region6);
                GFX.FillRectangle(Brushes.Transparent, Region7);
                GFX.FillRectangle(Brushes.Transparent, Region8);
                GFX.FillRectangle(Brushes.Transparent, Region9);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (this.Height < 250)
                this.Height = 250;
            else if (this.Width < 350)
                this.Width = 350;
        }

        private void PrincipalPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                FormMoving = true;

                FormMousePosition = new Point(e.X, e.Y);
                this.Cursor = Cursors.SizeAll;
            }
        }

        private void PrincipalPanel_DoubleClick(object sender, EventArgs e)
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

        private void PrincipalPanel_MouseUp(object sender, MouseEventArgs e)
        {
            FormMoving = false;
            this.Cursor = Cursors.Default;
        }

        private void PrincipalPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (FormMoving == true)
                this.Location = new Point(MousePosition.X - FormMousePosition.X, MousePosition.Y - FormMousePosition.Y);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, EventArgs e)
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

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.createproject = new CreateProject();
            Program.createproject.ShowDialog(this);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.createfile = new CreateFile();
            Program.createfile.ShowDialog(this);
        }

        private void ProjectOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            LoadProject(ProjectOpenFileDialog.FileName);
        }

        private void FileOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            this.OpenFile(FileOpenFileDialog.FileName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CodeEditors[this.CodeEditorPath].Close();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseProject();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveFile(this.CodeEditorPath);
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAllFiles(false);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CodeEditors[this.CodeEditorPath].CodeBox.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CodeEditors[this.CodeEditorPath].CodeBox.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut cut = new Cut();
            cut.Execute(this.CodeEditors[this.CodeEditorPath].CodeBox);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy copy = new Copy();
            copy.Execute(this.CodeEditors[this.CodeEditorPath].CodeBox);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste paste = new Paste();
            paste.Execute(this.CodeEditors[this.CodeEditorPath].CodeBox);
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.findreplace.IsDisposed == true)
                Program.findreplace = new FindReplace();

            if (Program.findreplace.Visible == true)
            {
                Program.findreplace.TabControl.SelectedIndex = 0;
                Program.findreplace.FindWhat.Select();
                Program.findreplace.Activate();
            }
            else
            {
                Program.findreplace.TabControl.SelectedIndex = 0;
                Program.findreplace.FindWhat.Select();
                Program.findreplace.Show(this);
            }
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.findreplace.IsDisposed == true)
                Program.findreplace = new FindReplace();

            this.CodeEditors[this.CodeEditorPath].FindNextMatch(Program.findreplace.FindWhat.Text, true, Program.findreplace.casesensitive.Checked, Program.findreplace.matchwholeword.Checked);
        }

        private void findPrevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.findreplace.IsDisposed == true)
                Program.findreplace = new FindReplace();

            this.CodeEditors[this.CodeEditorPath].FindPrevious(Program.findreplace.FindWhat.Text, true, Program.findreplace.casesensitive.Checked, Program.findreplace.matchwholeword.Checked);
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Program.findreplace.IsDisposed == true)
                Program.findreplace = new FindReplace();

            if (Program.findreplace.Visible == true)
            {
                Program.findreplace.TabControl.SelectedIndex = 1;
                Program.findreplace.FindWhat2.Select();
                Program.findreplace.Activate();
            }
            else
            {
                Program.findreplace.TabControl.SelectedIndex = 1;
                Program.findreplace.FindWhat2.Select();
                Program.findreplace.Show(this);
            }
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToLine gotoline = new GoToLine();

            gotoline.CurrentLineText.Text = (this.CodeEditors[this.CodeEditorPath].CodeBox.ActiveTextAreaControl.TextArea.Caret.Line + 1).ToString();
            gotoline.MaximumLineText.Text = this.CodeEditors[this.CodeEditorPath].CodeBox.Document.TotalNumberOfLines.ToString();
            gotoline.GoToLineText.Text = (this.CodeEditors[this.CodeEditorPath].CodeBox.ActiveTextAreaControl.TextArea.Caret.Line + 1).ToString();
            gotoline.GoToLineText.Select();

            gotoline.ShowDialog(this.CodeEditors[this.CodeEditorPath]);
        }

        private void projectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.OpeningProject, false);

            DialogResult Result = ProjectOpenFileDialog.ShowDialog(this);

            if (Result == DialogResult.Cancel || Result == DialogResult.OK)
                ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.Ready, false);
        }

        private void fileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.OpeningFile, false);

            DialogResult Result = FileOpenFileDialog.ShowDialog(this);

            if (Result == DialogResult.Cancel || Result == DialogResult.OK)
                ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.Ready, false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void projectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.projectExplorerToolStripMenuItem.Checked == true)
            {
                Program.projectexplorer.IsHidden = true;
                this.projectExplorerToolStripMenuItem.Checked = false;
            }
            else
            {
                Program.projectexplorer.IsHidden = false;
                this.projectExplorerToolStripMenuItem.Checked = true;
            }
        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.outputToolStripMenuItem.Checked == true)
            {
                Program.output.IsHidden = true;
                this.outputToolStripMenuItem.Checked = false;
            }
            else
            {
                Program.output.IsHidden = false;
                this.outputToolStripMenuItem.Checked = true;
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.CodeEditorPath == null)
                return;
            else if (this.CodeEditors[this.CodeEditorPath].CodeBox.Document.TextLength == 0)
            {
                ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.TextLength, true);

                return;
            }
            else if (Path.GetExtension(this.CodeEditors[this.CodeEditorPath].FilePath) != ".pwn")
                return;
            else if (CompilerWorker.IsBusy == true)
                return;

            Program.output.setOutputText(String.Empty, true);

            this.SaveAllFiles(false);

            this.saveToolStripMenuItem.Enabled = false;

            ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.Compiling, false);

            CompilerWorker.RunWorkerAsync();

        }

        private void CompilerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Process Compiling = new Process();

            string TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileNameWithoutExtension(this.CodeEditorPath) + ".pwn");
            string AmxPath = Path.Combine(Path.GetDirectoryName(this.CodeEditorPath), Path.GetFileNameWithoutExtension(this.CodeEditorPath));

            StreamWriter PFile = new StreamWriter(TempFile, false, Encoding.Default);
            PFile.Write(this.CodeEditors[this.CodeEditorPath].CodeBox.Document.TextContent);
            PFile.Close();

            Compiling.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", Properties.Settings.Default["CompilerName"].ToString());
            Compiling.StartInfo.Arguments = String.Format("{0} {1} {2} {3}", Path.GetFileNameWithoutExtension(this.CodeEditorPath) + ".pwn", "-o\"" + AmxPath + "\"", "-;+ -(+", Properties.Settings.Default["CompilerArguments"].ToString());
            Compiling.StartInfo.UseShellExecute = false;
            Compiling.StartInfo.CreateNoWindow = true;
            Compiling.StartInfo.RedirectStandardError = true;
            Compiling.StartInfo.RedirectStandardOutput = true;
            Compiling.Start();

            while (!Compiling.HasExited)
            {
                this.ProjectInformation["CompileErrors"] = Compiling.StandardError.ReadToEnd();
                this.ProjectInformation["Output"] = Compiling.StandardOutput.ReadToEnd();
            }

            File.Delete(TempFile);

        }

        private void CompilerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.ProjectInformation["CompileErrors"].Length > 0)
                ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.CompiledWithErrors, true);
            else
                ApplicationStatus.setApplicationStatus(ApplicationStatus.Status.Compiled, true);

            Program.output.setOutputText(ProjectInformation["CompileErrors"].Length == 0 ? "" : ProjectInformation["CompileErrors"] + Environment.NewLine, true);
            Program.output.setOutputText(ProjectInformation["Output"], false);

            this.saveToolStripMenuItem.Enabled = true;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.options = new Options();
            Program.options.ShowDialog(this);
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.launcher.Show();

            Program.launcher.UpdateBackgroundWorker.RunWorkerAsync();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.about = new About();
            Program.about.ShowDialog(this);
        }

        private void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            this.CodeEditorPath = getCodeEditorPath();

            if (CodeEditorPath != null)
            {
                this.closeToolStripMenuItem.Enabled = true;
                this.setMenuStripItemsStatus(true, false);

                this.saveToolStripMenuItem.Text = String.Format("Save {0}", Path.GetFileName(CodeEditorPath));
                this.savesAsToolStripMenuItem.Text = String.Format("Save {0} As...", Path.GetFileName(CodeEditorPath));

                IDocument document = this.CodeEditors[this.CodeEditorPath].CodeBox.Document;
                Caret caret = this.CodeEditors[this.CodeEditorPath].CodeBox.ActiveTextAreaControl.Caret;

                this.ChangeLineColumn(caret.Line, caret.Column);
            }
            else
            {
                this.closeToolStripMenuItem.Enabled = false;
                this.setMenuStripItemsStatus(false, false);

                this.saveToolStripMenuItem.Text = "Save Selected Item";
                this.savesAsToolStripMenuItem.Text = "Save Selected Item As...";

                this.ChangeLineColumn(-1, -1);

                this.CodeEditorPath = null;
            }
        }

        /// <summary>
        /// Change enable value for MenuStrip items.
        /// </summary>
        /// <param name="isEnabled">If it will be true, then MenuStrip items will be enabled.</param>
        /// <param name="isProject">If it will be true, then some items for project will be enabled.</param>
        public void setMenuStripItemsStatus(bool isEnabled, bool isProject, bool justProjectItems = false)
        {
            if (isProject == true)
            {
                this.fileToolStripMenuItem.Enabled = isEnabled;
                this.closeProjectToolStripMenuItem.Enabled = isEnabled;
            }

            if (justProjectItems == true)
                return;

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
        }

        /// <summary>
        /// Get name for the current code editor form.
        /// </summary> 
        public string getCodeEditorPath()
        {
            if (dockPanel.ActiveDocument != null)
            {
                string[] SplitedString = dockPanel.ActiveDocument.ToString().Split(new string[] { "Text: " }, StringSplitOptions.None);

                return SplitedString[1];
            }

            return null;
        }

        /// <summary>
        /// Change line and column text when user move caret.
        /// </summary>
        /// <param name="Line">Current line, if the line will be -1, then line text will be 0.</param>
        /// <param name="Column">Current column, if the column will be -1, then column text will be 0.</param>
        public void ChangeLineColumn(int Line, int Column)
        {
            this.LineLabel.Text = string.Format("Line {0}", Line + 1);
            this.ColumnLabel.Text = string.Format("Column {0}", Column + 1);
        }

        /// <summary>
        /// Load project if user open project file.
        /// </summary> 
        /// <param name="ProjectPath">Path to the file with 'pawnplusproject' extension.</param>
        public void LoadProject(string ProjectPath)
        {
            string CurrentFilePath = null, CurrentAttribute = null;
            string[] FilePath = new string[2];

            this.CloseProject();

            if (ProjectPath != null)
            {
                XmlTextReader ProjectReader = new XmlTextReader(ProjectPath);

                while (ProjectReader.Read())
                {
                    switch (ProjectReader.Name.ToString())
                    {
                        case "Name":
                            this.ProjectInformation.Add("Name", ProjectReader.ReadString());
                            break;

                        case "File":
                            try
                            {
                                CurrentAttribute = ProjectReader.GetAttribute("Active");
                                CurrentFilePath = ProjectReader.ReadString();

                                if (CurrentAttribute == "1")
                                {
                                    FilePath[0] = CurrentFilePath;
                                    FilePath[1] = "true";
                                }

                                
                                this.OpenFile(CurrentFilePath);
                            }
                            catch (Exception)
                            {
                                // File dosen't exist.
                            }

                            break;
                    }
                }

                if (FilePath[1] == "true")
                {
                    this.CodeEditors[FilePath[0]].Activate();
                    this.CodeEditors[FilePath[0]].Select();
                    this.CodeEditors[FilePath[0]].Focus();
                }

                if (this.ProjectInformation.ContainsKey("Path") == false)
                    this.ProjectInformation.Add("Path", Path.GetDirectoryName(ProjectPath));
                else
                    this.ProjectInformation["Path"] = Path.GetDirectoryName(ProjectPath);

                // PawnPlusProject = Path to file with .pawnplusproject extension.
                this.ProjectInformation.Add("PawnPlusProject", ProjectPath);
                this.ProjectInformation.Add("CompileErrors", String.Empty);
                this.ProjectInformation.Add("Output", String.Empty);

                if (this.ProjectInformation["Path"].Length != 0)
                    Program.projectexplorer.LoadDirectory(Program.projectexplorer.FileTree, this.ProjectInformation["Path"]);

                this.setMenuStripItemsStatus(true, true, true);

                this.FormName.Text = "PawnPlus - " + this.ProjectInformation["Name"];
            }
        }

        /// <summary>
        /// Close current project.
        /// </summary>
        /// <returns>true is all files is saved, false otherwise.</returns>
        public bool CloseProject()
        {
            this.setMenuStripItemsStatus(false, true);

            bool isUnsaved = false;

            if (this.ProjectInformation.ContainsKey("PawnPlusProject") == true)
            {
                string ProjectPath = Path.Combine(this.ProjectInformation["Path"], this.ProjectInformation["PawnPlusProject"]);

                XmlDocument ProjectXML = new XmlDocument();
                ProjectXML.LoadXml(File.ReadAllText(ProjectPath));

                XmlNode Project = ProjectXML.DocumentElement;

                XmlElement OpenedFiles = ProjectXML.CreateElement("OpenedFiles");

                if (ProjectXML.SelectSingleNode("//OpenedFiles") == null)
                    Project.AppendChild(OpenedFiles);
                else
                    Project.ReplaceChild(OpenedFiles, ProjectXML.SelectSingleNode("//OpenedFiles"));

                foreach (KeyValuePair<string, CodeEditor> CodeEditor in this.CodeEditors.ToList())
                {
                    XmlNode CurrentFile = ProjectXML.CreateElement("File");
                    CurrentFile.InnerText = CodeEditor.Value.FilePath;

                    if (CodeEditor.Value == this.dockPanel.ActiveDocument)
                    {
                        XmlNode Attribute = ProjectXML.CreateNode(XmlNodeType.Attribute, "Active", String.Empty);
                        Attribute.Value = "1";

                        CurrentFile.Attributes.SetNamedItem(Attribute);
                    }

                    OpenedFiles.AppendChild(CurrentFile);
                }

                ProjectXML.Save(ProjectPath);
            }

            foreach (KeyValuePair<string, CodeEditor> CodeEditor in this.CodeEditors.ToList())
            {
                if (CodeEditor.Value.DockHandler.TabText.Contains("*") == true)
                    isUnsaved = true;
                else
                    CodeEditor.Value.Close();
            }

            if (isUnsaved == true)
            {
                DialogResult dialogResult = new DialogResult();

                dialogResult = MessageBox.Show(this, "Do you want to save all changes?", "Save changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                    SaveAllFiles(true);
                else if (dialogResult == DialogResult.Cancel)
                    return false;

                isUnsaved = false;
            }

            this.CodeEditors = new Dictionary<string, CodeEditor>();
            this.ProjectInformation = new Dictionary<string, string>();
            Program.projectexplorer.FileTree.Nodes.Clear();

            this.FormName.Text = "PawnPlus";

            return true;
        }

        /// <summary>
        /// Open file when user request it.
        /// </summary>
        /// <param name="FilePath">Path to the file.</param>
        public void OpenFile(string FilePath)
        {
            if (this.CodeEditors.ContainsKey(FilePath) == false)
            {
                string FileName = Path.GetFileName(FilePath);

                this.CodeEditors.Add(FilePath, new CodeEditor());
                this.CodeEditors[FilePath].Text = FilePath;
                this.CodeEditors[FilePath].DockHandler.TabText = FileName;
                this.CodeEditors[FilePath].CodeBox.Document.TextContent = File.ReadAllText(FilePath, Encoding.UTF8);
                this.CodeEditors[FilePath].InitialContent = this.CodeEditors[FilePath].CodeBox.Document.TextContent;
                this.CodeEditors[FilePath].FilePath = FilePath;
                this.CodeEditors[FilePath].checkInitialContent();

                if(Path.GetExtension(FilePath) == ".pwn")
                    this.CodeEditors[FilePath].Icon = Properties.Resources.FileGroup_10135_32x_icon;
                else if (Path.GetExtension(FilePath) == ".inc")
                    this.CodeEditors[FilePath].Icon = Properties.Resources.gear_32xLG_icon;
                else
                    this.CodeEditors[FilePath].Icon = Properties.Resources.text_16xLG_icon;

                this.CodeEditors[FilePath].Show(this.dockPanel, DockState.Document);

                // Update folds.
                this.CodeEditors[FilePath].CodeBox.Document.FoldingManager.UpdateFolds(null, null);
            }
        }

        /// <summary>
        /// Save current file.
        /// </summary>
        /// <param name="Path">Path to the file.</param>
        public void SaveFile(string Path)
        {
            File.WriteAllText(Path, this.CodeEditors[Path].CodeBox.Document.TextContent, Encoding.UTF8);
            this.CodeEditors[Path].InitialContent = this.CodeEditors[Path].CodeBox.Document.TextContent;
            this.CodeEditors[Path].checkInitialContent();
        }

        /// <summary>
        /// Save all opened files.
        /// </summary>
        public void SaveAllFiles(bool CloseFiles)
        {
            foreach (KeyValuePair<string, CodeEditor> CodeEditor in this.CodeEditors.ToList())
            {
                if (CodeEditor.Value.DockHandler.TabText.Contains("*") == true)
                    this.SaveFile(CodeEditor.Key);

                if (CloseFiles == true)
                    CodeEditor.Value.Close();
            }
        }
    }
}

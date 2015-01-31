﻿using PawnPlus.Core;
using PawnPlus.Core.Document;
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
    public partial class Main : Form
    {
        public bool CloseApplication = false;
        public string LayoutFile = null;

        public ApplicationStatus applicationStatus;
        private DeserializeDockContent DockContentLayout;

        public Main()
        {
            InitializeComponent();

            DockContentLayout = new DeserializeDockContent(GetLayout);
            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
            this.applicationStatus = new ApplicationStatus(this.StatusBar, this.StatusLabel);
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
            Application.Exit();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {

        }

        private IDockContent GetLayout(string LayoutString)
        {
            if (LayoutString == typeof(ProjectExplorer).ToString())
                return Program.projectexplorer;
            else if (LayoutString == typeof(Output).ToString())
                return Program.output;

            return null;
        }

        #region Main Functions

        private Rectangle Region0, Region1, Region2, Region3, Region4, Region5, Region6, Region7, Region8, Region9;

        private Point FormStartResize, FormResizePoints, FormMousePosition;

        private bool FormMoving, FormRezising, FormRezisingLeft, FormRezisingRight, FormRezisingTop, FormRezisingBottom, FormRezisingTopRight, FormRezisingTopLeft, FormRezisingBottomRight, FormRezisingBottomLeft;
        
        const int WS_MINIMIZEBOX = 0x20000, CS_DBLCLKS = 0x8;

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
            Program.main.OpenFile(FileOpenFileDialog.FileName);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.Close();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CloseProject();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.main.SaveFile(ApplicationInformations.Informations.CurrentFile.Informations.Path);
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAllFiles(false);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.UndoRedo.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.UndoRedo.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.Clipboard.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ApplicationInformations.Informations.CurrentFile.IsActivated == true)
            {
                ApplicationInformations.Informations.CurrentFile.CodeBox.Clipboard.Copy();
            }
            else if (Program.output.IsActivated == true)
            {
                Program.output.OutputBox.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ApplicationInformations.Informations.CurrentFile.IsActivated == true)
            {
                ApplicationInformations.Informations.CurrentFile.CodeBox.Clipboard.Paste();
            }
            else if (Program.output.IsActivated == true)
            {
                Program.output.OutputBox.Paste();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.FindReplace.ShowFind();
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.FindReplace.Window.FindNext();
        }

        private void findPrevToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.FindReplace.Window.FindPrevious();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.FindReplace.ShowReplace();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationInformations.Informations.CurrentFile.CodeBox.GoTo.ShowGoToDialog();
        }

        private void projectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.OpeningProject, false);

            DialogResult Result = ProjectOpenFileDialog.ShowDialog(this);

            if (Result == DialogResult.Cancel || Result == DialogResult.OK)
                Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.Ready, false);
        }

        private void fileToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.OpeningFile, false);

            DialogResult Result = FileOpenFileDialog.ShowDialog(this);

            if (Result == DialogResult.Cancel || Result == DialogResult.OK)
                Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.Ready, false);
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
            if (ApplicationInformations.Informations.CurrentFile.Informations.Path == null)
            {
                return;
            }
            else if (ApplicationInformations.Informations.CurrentFile.CodeBox.Text.Length == 0)
            {
                Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.TextLength, true);

                return;
            }
            else if (Path.GetExtension(ApplicationInformations.Informations.CurrentFile.Informations.Path) != ".pwn")
            {
                return;
            }
            else if (CompilerWorker.IsBusy == true)
            {
                return;
            }

            Program.output.SetOutputText(String.Empty, true);

            this.SaveAllFiles(false);

            this.saveToolStripMenuItem.Enabled = false;

            Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.Compiling, false);

            CompilerWorker.RunWorkerAsync();

        }

        private void CompilerWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Process Compiling = new Process();

            string AmxPath = Path.Combine(Path.GetDirectoryName(ApplicationInformations.Informations.CurrentFile.Informations.Path), Path.GetFileNameWithoutExtension(ApplicationInformations.Informations.CurrentFile.Informations.Path));

            this.SaveAllFiles(false);

            Compiling.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Pawn", Properties.Settings.Default["CompilerName"].ToString());
            Compiling.StartInfo.Arguments = String.Format("{0} {1} {2} {3}", "\"" + ApplicationInformations.Informations.CurrentFile.Informations.Path + "\"", "-o\"" + AmxPath + "\"", "-;+ -(+", Properties.Settings.Default["CompilerArguments"].ToString());
            Compiling.StartInfo.UseShellExecute = false;
            Compiling.StartInfo.CreateNoWindow = true;
            Compiling.StartInfo.RedirectStandardError = true;
            Compiling.StartInfo.RedirectStandardOutput = true;
            Compiling.Start();

            while (Compiling.HasExited == false)
            {
                ApplicationInformations.Informations.CompileInformations.Errors = Compiling.StandardError.ReadToEnd();
                ApplicationInformations.Informations.CompileInformations.Output = Compiling.StandardOutput.ReadToEnd();
            }
        }

        private void CompilerWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ApplicationInformations.Informations.CompileInformations.Errors.Length > 0)
                Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.CompiledWithErrors, true);
            else
                Program.main.applicationStatus.setApplicationStatus(ApplicationStatusType.Compiled, true);

            Program.output.SetOutputText(ApplicationInformations.Informations.CompileInformations.Errors.Length == 0 ? "" : ApplicationInformations.Informations.CompileInformations.Errors + Environment.NewLine, true);
            Program.output.SetOutputText(ApplicationInformations.Informations.CompileInformations.Output, false);

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
            try
            {
                ApplicationInformations.Informations.CurrentFile = ApplicationInformations.Informations.CodeEditors[getCodeEditorPath()];
            }
            catch (Exception)
            {
                // User close all tabs verry fast.
                ApplicationInformations.Informations.CurrentFile = null;
            }

            if (ApplicationInformations.Informations.CurrentFile != null)
            {
                this.closeToolStripMenuItem.Enabled = true;
                this.setMenuStripItemsStatus(true, false);

                this.saveToolStripMenuItem.Text = String.Format("Save {0}", Path.GetFileName(ApplicationInformations.Informations.CurrentFile.Informations.Path));
                this.savesAsToolStripMenuItem.Text = String.Format("Save {0} As...", Path.GetFileName(ApplicationInformations.Informations.CurrentFile.Informations.Path));

                this.ChangeLineColumn(ApplicationInformations.Informations.CurrentFile.CodeBox.Caret.LineNumber, 
                    Convert.ToInt32(ApplicationInformations.Informations.CurrentFile.CodeBox.GetColumn(ApplicationInformations.Informations.CurrentFile.CodeBox.CurrentPos)));

                this.MainTimer.Start();
            }
            else
            {
                this.closeToolStripMenuItem.Enabled = false;
                this.setMenuStripItemsStatus(false, false);

                this.saveToolStripMenuItem.Text = "Save Selected Item";
                this.savesAsToolStripMenuItem.Text = "Save Selected Item As...";

                this.MainTimer.Stop();
                this.ChangeLineColumn(-1, -1);
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
                            ApplicationInformations.Informations.Project.Name = ProjectReader.ReadString();
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
                            catch (Exception e)
                            {
                                #if DEBUG
                                MessageBox.Show(e.ToString());
                                #endif
                            }

                            break;
                    }
                }

                ProjectReader.Close();

                if (FilePath[1] == "true")
                {
                    ApplicationInformations.Informations.CodeEditors[FilePath[0]].Activate();
                    ApplicationInformations.Informations.CodeEditors[FilePath[0]].Select();
                    ApplicationInformations.Informations.CodeEditors[FilePath[0]].Focus();
                }

                ApplicationInformations.Informations.Project.Path = Path.GetDirectoryName(ProjectPath);
              
                ApplicationInformations.Informations.Project.FullPath = ProjectPath;
                ApplicationInformations.Informations.CompileInformations.Errors = String.Empty;
                ApplicationInformations.Informations.CompileInformations.Output =  String.Empty;

                if (ApplicationInformations.Informations.Project.Path.Length != 0)
                    Program.projectexplorer.LoadDirectory(Program.projectexplorer.FileTree, ApplicationInformations.Informations.Project.Path);

                this.setMenuStripItemsStatus(true, true, true);

                this.FormName.Text = "PawnPlus - " + ApplicationInformations.Informations.Project.Name;
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

            if (ApplicationInformations.Informations.Project.FullPath != String.Empty && ApplicationInformations.Informations.Project.FullPath != null)
            {
                XmlDocument ProjectXML = new XmlDocument();
                ProjectXML.LoadXml(File.ReadAllText(ApplicationInformations.Informations.Project.FullPath));

                XmlNode Project = ProjectXML.DocumentElement;

                XmlElement OpenedFiles = ProjectXML.CreateElement("OpenedFiles");

                if (ProjectXML.SelectSingleNode("//OpenedFiles") == null)
                    Project.AppendChild(OpenedFiles);
                else
                    Project.ReplaceChild(OpenedFiles, ProjectXML.SelectSingleNode("//OpenedFiles"));

                foreach (KeyValuePair<string, PawnPlus.Core.Forms.CodeEditor> CodeEditor in ApplicationInformations.Informations.CodeEditors.ToList())
                {
                    XmlNode CurrentFile = ProjectXML.CreateElement("File");
                    CurrentFile.InnerText = CodeEditor.Value.Informations.Path;

                    if (CodeEditor.Value == this.dockPanel.ActiveDocument)
                    {
                        XmlNode Attribute = ProjectXML.CreateNode(XmlNodeType.Attribute, "Active", String.Empty);
                        Attribute.Value = "1";

                        CurrentFile.Attributes.SetNamedItem(Attribute);
                    }

                    OpenedFiles.AppendChild(CurrentFile);
                }

                try
                {
                    ProjectXML.Save(ApplicationInformations.Informations.Project.FullPath);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Unexpected error: " + Environment.NewLine + e.ToString());
                }
            }

            foreach (KeyValuePair<string, PawnPlus.Core.Forms.CodeEditor> CodeEditor in ApplicationInformations.Informations.CodeEditors.ToList())
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

            ApplicationInformations.Informations.CodeEditors = new Dictionary<string, PawnPlus.Core.Forms.CodeEditor>();
            ApplicationInformations.Informations.Project = new ProjectType();
            ApplicationInformations.Informations.CurrentFile = null;
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
            if (ApplicationInformations.Informations.CodeEditors.ContainsKey(FilePath) != false)
                return;

            ApplicationInformations.Informations.CodeEditors.Add(FilePath, new PawnPlus.Core.Forms.CodeEditor());

            ApplicationInformations.Informations.CodeEditors[FilePath].Text = FilePath;
            ApplicationInformations.Informations.CodeEditors[FilePath].DockHandler.TabText = Path.GetFileName(FilePath);

            ApplicationInformations.Informations.CodeEditors[FilePath].Informations.Name = Path.GetFileName(FilePath);
            ApplicationInformations.Informations.CodeEditors[FilePath].Informations.Path = FilePath;
            ApplicationInformations.Informations.CodeEditors[FilePath].Informations.fileInfo = new FileInfo(FilePath);

            if (Path.GetExtension(FilePath) == ".pwn")
                ApplicationInformations.Informations.CodeEditors[FilePath].Icon = Properties.Resources.FileGroup_10135_32x_icon;
            else if (Path.GetExtension(FilePath) == ".inc")
                ApplicationInformations.Informations.CodeEditors[FilePath].Icon = Properties.Resources.gear_32xLG_icon;
            else
                ApplicationInformations.Informations.CodeEditors[FilePath].Icon = Properties.Resources.text_16xLG_icon;

            ApplicationInformations.Informations.CodeEditors[FilePath].Show(this.dockPanel, DockState.Document);

            // TODO: Update folds.
        }

        /// <summary>
        /// Save current file.
        /// </summary>
        /// <param name="Path">Path to the file.</param>
        public void SaveFile(string Path)
        {
            File.WriteAllText(Path, ApplicationInformations.Informations.CodeEditors[Path].CodeBox.Text, Encoding.GetEncoding(1252));
            ApplicationInformations.Informations.CodeEditors[Path].Informations.fileInfo = new FileInfo(Path);

            ApplicationInformations.Informations.CodeEditors[Path].CheckInitialContent();
        }

        /// <summary>
        /// Save all opened files.
        /// </summary>
        public void SaveAllFiles(bool CloseFiles)
        {
            foreach (KeyValuePair<string, PawnPlus.Core.Forms.CodeEditor> CodeEditor in ApplicationInformations.Informations.CodeEditors.ToList())
            {
                if (CodeEditor.Value.DockHandler.TabText.Contains("*") == true)
                    Program.main.SaveFile(CodeEditor.Key);

                if (CloseFiles == true)
                    CodeEditor.Value.Close();
            }
        }
    }
}
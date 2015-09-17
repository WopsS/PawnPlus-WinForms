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
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
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

                this.saveToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSave), Path.GetFileName(editor.FilePath));
                this.savesAsToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSaveAs), Path.GetFileName(editor.FilePath));

                this.SetMenuStatus(true, false);
            }
            else
            {
                this.SetMenuStatus(false, false);
                StatusManager.SetLineColumn(0, 0);

                this.saveToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSave), "Selected Item");
                this.savesAsToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSaveAs), "Selected Item");
            }
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Create new project.
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Create new file.
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = string.Format("PawnPlus Project|*{0}", ProjectManager.Extension);

            DialogResult dialogResult = this.openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                if (ProjectManager.Open(this.openFileDialog.FileName) == true) // Is the project opened properly?
                {
                    this.SetMenuStatus(true, true, true);
                    this.FormName.Text = "PawnPlus - " + ProjectManager.Name;
                }
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openFileDialog.Filter = "PAWN File|*.pwn|Include file|*.inc";

            DialogResult dialogResult = this.openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                CEManager.OpenFile(this.openFileDialog.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectManager.Close();
            this.SetMenuStatus(false, true);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void savesAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
            this.fileToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFile);
            this.newToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileNew);
            this.newProjectToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileProject);
            this.newFileToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileFile);
            this.openToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileOpen);
            this.openProjectToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileProject);
            this.openFileToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileFile);
            this.closeToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileClose);
            this.closeProjectToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileCloseProject);
            this.saveToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSave), "Selected Item");
            this.savesAsToolStripMenuItem.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainMenuItemFileSaveAs), "Selected Item");
            this.saveAllToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemFileSaveAll);
            
            // Edit tool strip menu item.
            this.editToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEdit);
            this.undoToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditUndo);
            this.redoToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditRedo);
            this.cutToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditCut);
            this.copyToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditCopy);
            this.pasteToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditPaste);
            this.findToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditFind);
            this.findNextToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditFindNext);
            this.findPrevToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditFindPrevious);
            this.replaceToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditReplace);
            this.goToToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemEditGoTo);

            // Build tool strip menu item.
            this.buildToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemBuild);
            this.compileToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemBuildCompile);
            this.compileOptionsToolStripMenuItem.Text = LanguageManager.GetText(LanguageEnum.MainMenuItemBuildCompileOptions);

            // Status bar.
            this.statusLabel.Text = LanguageManager.GetText(LanguageEnum.StatusReady);

            FileVersionInfo Program = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            this.versionLabel.Text = string.Format(LanguageManager.GetText(LanguageEnum.MainVersion), Program.ProductMajorPart, Program.ProductMinorPart, Program.ProductBuildPart);

            this.lineLabel.Text = string.Format(LanguageManager.GetText(LanguageEnum.MenuLine), 0);
            this.columnLabel.Text = string.Format(LanguageManager.GetText(LanguageEnum.MenuColumn), 0);
        }

        /// <summary>
        /// Change enable value for <see cref="MenuStrip"/> items.
        /// </summary>
        /// <param name="isEnabled">If it will be true, then MenuStrip items will be enabled.</param>
        /// <param name="isProject">If it will be true, then some items for project will be enabled.</param>
        private void SetMenuStatus(bool isEnabled, bool isProject, bool justProjectItems = false)
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
        }
    }
}

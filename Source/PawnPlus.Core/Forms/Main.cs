using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus.Core.Forms
{
    public partial class Main : Form
    {
        private DeserializeDockContent dockContentLayout;

        private FindReplace findReplace = new FindReplace();

        private string layoutPath = Path.Combine(ApplicationData.AppData, "Layout.xml");

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
                Stream xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("PawnPlus.Core.Resources.DefaultLayout.xml");
                this.dockPanel.LoadFromXml(xmlStream, dockContentLayout);
                xmlStream.Close();
            }

            this.TranslateUI();
            Status.Set(StatusType.Info, StatusReset.None, Localization.Status_Ready);

            if (Environment.GetCommandLineArgs().Length >= 2)
            {
                string fileName = Environment.GetCommandLineArgs()[1];
                string extension = Path.GetExtension(fileName);

                if (extension == Project.Extension)
                {
                    Project.Open(fileName);
                }
                else if (extension == ".pwn" || extension == ".inc")
                {
                    Workspace.OpenFile(fileName);
                }
            }

        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Let's check if project is open and let's close files from Workspace.Project. After that let's close all files.
            if ((Project.IsOpen == true && Workspace.Project.Close() == true) || (Workspace.Project == null && Workspace.CloseAllFiles(false) == true))
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

            // Unload all loaded plugins.
            PluginManager.Unload();
        }

        #region Main functions

        /* 
        Not necessary anymore
        
        private Point FormStartResize, FormResizePoints, FormMousePosition;
        private Rectangle[] Regions = new Rectangle[10];
        private bool FormMoving, FormRezising, FormRezisingLeft, FormRezisingRight, FormRezisingTop, FormRezisingBottom, FormRezisingTopRight, FormRezisingTopLeft, FormRezisingBottomRight, FormRezisingBottomLeft;
        */
        private bool FormMoving;
        private Point FormMousePosition;
        private const int WS_MINIMIZEBOX = 0x20000, CS_DBLCLKS = 0x8;
        private const int cGrip = 16;      // Grip size

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            // cleared for resizing performance
        }

        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            // cleared for resizing performance
        }

        private void Main_MouseUp(object sender, MouseEventArgs e)
        {
            // cleared for resizing performance
        }

        private void Main_Paint(object sender, PaintEventArgs e)
        {
            Graphics GFX = this.CreateGraphics();

            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(GFX, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, this.Height);
            GFX.FillRectangle(Brushes.Transparent, rc);
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
        
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                pos = this.PointToClient(pos);
                if (pos.Y < 25)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
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
                this.Padding = new Padding(0, 0, 0, 0);
                this.DesktopBounds.Offset(0, 0);
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
            if (this.dockPanel.ActiveDocument != null && this.dockPanel.ActiveDocument.DockHandler.Form.IsDisposed == false)
            {
                Editor editor = (Editor)this.dockPanel.ActiveDocument.DockHandler.Form;

                Workspace.CurrentEditor = editor;
                Status.SetLineColumn(Workspace.CurrentEditor.TextEditor.TextArea.Caret.Line, Workspace.CurrentEditor.TextEditor.TextArea.Caret.Column);

                this.saveToolStripMenuItem.Text = string.Format(Localization.Text_Save, Path.GetFileName(editor.FileName));
                this.savesAsToolStripMenuItem.Text = string.Format(Localization.Text_SaveAs, Path.GetFileName(editor.FileName));

                this.SetMenuStatus(true, false);
            }
            else
            {
                Workspace.CurrentEditor = null;

                this.SetMenuStatus(false, false);
                Status.SetLineColumn(0, 0);

                this.saveToolStripMenuItem.Text = string.Format(Localization.Text_Save, Localization.Text_SelectedItem);
                this.savesAsToolStripMenuItem.Text = string.Format(Localization.Text_SaveAs, Localization.Text_SelectedItem);
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
            this.openFileDialog.Filter = string.Format("PawnPlus Project|*{0}", Project.Extension);

            DialogResult dialogResult = this.openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                Project.Open(this.openFileDialog.FileName);

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
                Workspace.OpenFile(this.openFileDialog.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dockPanel.ActiveDocument.DockHandler.Form.Close();
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.Project.Close();

            if (Workspace.CurrentEditor == null)
            {
                this.SetMenuStatus(false, true);
            }
            else
            {
                this.SetMenuStatus(false, true, true);
            }
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
            foreach (Editor editor in Workspace.GetEditors().Values)
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
            Workspace.CurrentEditor.TextEditor.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.CurrentEditor.TextEditor.Redo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.CurrentEditor.Cut();
            Workspace.Output.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.CurrentEditor.Copy();
            Workspace.Output.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.CurrentEditor.TextEditor.Paste();
        }
        
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findReplace.ShowFind(this, Workspace.CurrentEditor.TextEditor.SelectedText);
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
            findReplace.ShowReplace(this, Workspace.CurrentEditor.TextEditor.SelectedText);
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToLine gotoLine = new GoToLine();
            gotoLine.ShowDialog(this);
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Workspace.Compilation.Start(Workspace.CurrentEditor.FileName);        
        }

        private void compileOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = PawnPlus.Properties.Settings.Default.Compiler_Arguments.ToString();

            if (new InputBox(Localization.Text_CompilerOptions, string.Format("{0}:", Localization.Text_Options), value).Show(ref value) == DialogResult.OK)
            {
                PawnPlus.Properties.Settings.Default.Compiler_Arguments = value;
                PawnPlus.Properties.Settings.Default.Save();
            }
        }

        private void foldingTimer_Tick(object sender, EventArgs e)
        {
            if (Workspace.CurrentEditor != null && Workspace.CurrentEditor.TextEditor.FoldingManager != null)
            {
                Workspace.CurrentEditor.TextEditor.FoldingStrategy.UpdateFoldings(Workspace.CurrentEditor.TextEditor.FoldingManager, Workspace.CurrentEditor.TextEditor.Document);
            }
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
                if (layoutString == typeof(Explorer).ToString())
                {
                    return new Explorer();
                }
                else if (layoutString == typeof(Output).ToString())
                {
                    return Workspace.Output;
                }
            }

            return null;
        }

        /// <summary>
        /// Set language for whole application buttons or menu.
        /// </summary>
        private void TranslateUI()
        {
            // File tool strip menu item.
            this.fileToolStripMenuItem.Text = Localization.Text_File;
            this.newToolStripMenuItem.Text = Localization.Text_New;
            this.newProjectToolStripMenuItem.Text = Localization.Text_Project;
            this.newFileToolStripMenuItem.Text = Localization.Text_File;
            this.openToolStripMenuItem.Text = Localization.Text_Open;
            this.openProjectToolStripMenuItem.Text = Localization.Text_Project;
            this.openFileToolStripMenuItem.Text = Localization.Text_File;
            this.closeToolStripMenuItem.Text = Localization.Text_Close;
            this.closeProjectToolStripMenuItem.Text = Localization.Text_CloseProject;
            this.saveToolStripMenuItem.Text = string.Format(Localization.Text_Save, Localization.Text_SelectedItem);
            this.savesAsToolStripMenuItem.Text = string.Format(Localization.Text_SaveAs, Localization.Text_SelectedItem);
            this.saveAllToolStripMenuItem.Text = Localization.Text_SaveAll;

            // Edit tool strip menu item.
            this.editToolStripMenuItem.Text = Localization.Text_Edit;
            this.undoToolStripMenuItem.Text = Localization.Text_Undo;
            this.redoToolStripMenuItem.Text = Localization.Text_Redo;
            this.cutToolStripMenuItem.Text = Localization.Text_Cut;
            this.copyToolStripMenuItem.Text = Localization.Text_Copy;
            this.pasteToolStripMenuItem.Text = Localization.Text_Paste;
            this.findToolStripMenuItem.Text = Localization.Text_Find;
            this.findNextToolStripMenuItem.Text = Localization.Text_FindNext;
            this.findPrevToolStripMenuItem.Text = Localization.Text_FindPrevious;
            this.replaceToolStripMenuItem.Text = Localization.Text_Replace;
            this.goToToolStripMenuItem.Text = Localization.Text_GoTo;

            // Build tool strip menu item.
            this.buildToolStripMenuItem.Text = Localization.Text_Build;
            this.compileToolStripMenuItem.Text = Localization.Text_Compile;
            this.compileOptionsToolStripMenuItem.Text = Localization.Text_CompilerOptions;

            // Status bar.
            this.statusLabel.Text = Localization.Status_Ready;

            this.versionLabel.Text = string.Format("{0} {1}", Localization.Status_Version, ApplicationData.Version);

            this.lineLabel.Text = string.Format(Localization.Status_Line, 0);
            this.columnLabel.Text = string.Format(Localization.Status_Column, 0);
        }
    }
}

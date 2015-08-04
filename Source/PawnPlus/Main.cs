using PawnPlus.CodeEditor;
using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PawnPlus
{
    public partial class Main : Form
    {
        private ProjectExplorer projectExplorer = new ProjectExplorer();
        private Editor editorForm = new Editor();
        private Output outputForm = new Output();

        public Main()
        {
            InitializeComponent();

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            editorForm.codeEditor.Encoding = Encoding.ASCII;

            projectExplorer.Show(this.dockPanel, DockState.DockLeft);
            editorForm.Show(this.dockPanel, DockState.Document);
            outputForm.Show(this.dockPanel, DockState.DockBottom);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        #endregion
    }
}

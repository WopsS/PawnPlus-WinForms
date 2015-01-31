using PawnPlus.UserControls.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus
{
    // UNDONE: Finish part with options.
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();       
        }

        private void Options_Load(object sender, EventArgs e)
        {
            ControlsPanel.Controls.Add(new GeneralControl());
        }
    }
}

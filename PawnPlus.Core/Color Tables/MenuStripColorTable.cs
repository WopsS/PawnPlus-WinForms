using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnPlus.Core.Document
{
    public class MenuStripColorTable : ProfessionalColorTable
    {
        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(240, 240, 240); }
        }

        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(240, 240, 240); }
        }

        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(240, 240, 240); }
        }

        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemBorder
        {
            //get { return Color.FromArgb(51, 153, 255); }
            get { return Color.Transparent; }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(51, 153, 255); }
        }

        public override Color MenuBorder
        {
            get { return Color.FromArgb(160, 160, 160); }
        }

        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(240, 240, 240); }
        }
    }
}

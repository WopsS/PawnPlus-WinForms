using System.Windows.Forms;

namespace PawnPlus.Core
{
    public static class StatusManager
    {
        private static StatusStrip statusStrip;

        public static void Construct(StatusStrip StatusBar)
        {
            statusStrip = StatusBar;
        }

        public static void SetStatus()
        {
            // TODO: Create function to set the status.
        }
    }
}

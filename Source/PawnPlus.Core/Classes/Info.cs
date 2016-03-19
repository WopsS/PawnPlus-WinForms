
using System.Collections.Generic;

namespace PawnPlus.Core.Classes
{
    enum settingslist
    {
        lastWindowState = 1
    }
    class Info
    {
        public const string config = "settings.xml";
        public static List<string> Settings = new List<string>();
    }
}

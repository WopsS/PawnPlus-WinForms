using PawnPlus.Core;
using PawnPlus.Core.Events;
using PawnPlus.Core.Extensibility;
using PawnPlus.Core.Forms;
using System;

namespace PawnPlus.PluginTest
{
    public class Main : IPlugin
    {
        public string Author { get { return "Dima Octavian"; } }

        public string Description { get { return "An example of plugin for PawnPlus."; } }

        public string Name { get { return "PluginSample"; } }

        public Main()
        {
            EventStorage.AddListener<Editor, CaretPositionChangedArgs>(EventKey.CaretPositionChanged, this.event_CaretPositionChanged);
        }

        ~Main()
        {
            EventStorage.RemoveListener<Editor, CaretPositionChangedArgs>(EventKey.CaretPositionChanged, this.event_CaretPositionChanged);
        }

        private void event_CaretPositionChanged(Editor editor, CaretPositionChangedArgs e)
        {
            // Do something.
        }
    }
}

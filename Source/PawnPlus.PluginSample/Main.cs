using PawnPlus.Core;
using PawnPlus.Core.Events;
using System.Windows.Forms;

namespace PawnPlus.PluginSample
{
    public class Main : IPlugin
    {
        public string Author { get { return "Your Name"; } }

        public string Description { get { return "An example of plugin for PawnPlus."; } }

        public string Name { get { return "PluginSample"; } }

        public Main()
        {
            Project.Closed += this.event_ProjectClosed;
            Project.Loaded += this.event_ProjectLoaded;
        }

        ~Main()
        {
            Project.Closed -= this.event_ProjectLoaded;
            Project.Loaded -= this.event_ProjectLoaded;
        }

        private void event_ProjectClosed(object sender, ProjectEventArgs e)
        {
            MessageBox.Show(string.Format("Project \"{0}\" closed.", e.Name), this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void event_ProjectLoaded(object sender, ProjectEventArgs e)
        {
            MessageBox.Show(string.Format("Project \"{0}\" loaded.", e.Name), this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
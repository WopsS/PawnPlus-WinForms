using PawnPlus.Core;
using PawnPlus.Core.Events;
using PawnPlus.Core.Extensibility;
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
            EventStorage.AddListener<Project, ProjectEventArgs>(EventKey.ProjectOpened, this.event_ProjectLoaded);
        }

        ~Main()
        {
            EventStorage.RemoveListener<Project, ProjectEventArgs>(EventKey.ProjectOpened, this.event_ProjectLoaded);
        }

        private void event_ProjectLoaded(Project sender, ProjectEventArgs e)
        {
            MessageBox.Show(string.Format("Project \"{0}\" loaded.", e.Name), this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
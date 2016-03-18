using System.Xml;

namespace PawnPlus.Core.Classes
{
    class Utility
    {
        public void UpdateXML(string path, string nodeToChange, string value)
        {
            XmlDocument XmlToUpdate = new XmlDocument();
            XmlToUpdate.Load(path);

            XmlNode node;
            node = XmlToUpdate.DocumentElement;

            foreach (XmlNode nodes in node.ChildNodes)
            {
                if (nodes.Name == nodeToChange)
                {
                    nodes.InnerText = value;
                    break;
                }
            }
            XmlToUpdate.Save(path);
        }
    }
}

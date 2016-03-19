using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace PawnPlus.Core.Classes
{
    class XML
    {
        public bool ReadSettings(string path)
        {
            if (System.IO.File.Exists(path))
            {
                XmlReader reader = XmlReader.Create(Info.config);
                while (reader.Read())
                {
                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "mainFrame":
                            {                                    
                                Info.Settings.Insert(1, reader.GetAttribute("lastWindowState"));
                                break;
                            }
                        }
                    }
                }
                reader.Close();
            }
            return true;
        }
        public void CreateSettings(string path)
        {
            XmlWriter writer = XmlWriter.Create(path);

            writer.WriteStartDocument();

            writer.WriteStartElement("PawnPlus");
            writer.WriteStartElement("settings"); // start settings

            writer.WriteStartElement("mainFrame");
            writer.WriteStartAttribute("lastWindowState");
            writer.WriteValue("normal");
            writer.WriteEndAttribute();
            writer.WriteEndElement();

            writer.WriteEndElement(); // end settings
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }
        public bool UpdateXML(string path, string nodeToUpdate, string attributeToChange, string value)
        {
            if (System.IO.File.Exists(path))
            {
                XDocument xmlFileToUpdate = XDocument.Load(path);
                var query = from content in xmlFileToUpdate.Elements("PawnPlus").Elements("settings").Elements("mainFrame")
                            select content;

                foreach (XElement attribute in query)
                {
                    attribute.Attribute(attributeToChange).Value = value;
                }
                xmlFileToUpdate.Save(path);
                return true;
            }
            /*else
            {
                Utility.Log("Can't Update settings");
                return false;
            }*/
            return false;
        }
    }
}

using System.IO;
using System.Xml;

namespace Scripts.Utils
{
    public static class FileUtils
    {
        private static string XmlDefaultPath = @"dasdasda";

        public static bool isFileExists(string filename)
        {
            return File.Exists(filename);
        }

        public static bool writeToXml<T>(string AttributeNode,T value,string filePath = "UserDefault.xml")
        {
            XmlDocument xml = new XmlDocument();

            if (!isFileExists(XmlDefaultPath + filePath))
            {
                XmlDeclaration dec = xml.CreateXmlDeclaration("1.0", "GB2312", null);
                xml.AppendChild(dec);
            }

            string writeValue = value.ToString();
            xml.Load(XmlDefaultPath + filePath);
            XmlNodeList nodelist = xml.SelectSingleNode("UserDefault").ChildNodes;
            bool updateSuccess = false;
            foreach(XmlNode node in nodelist)
            {
                if(node.Name == AttributeNode)
                {
                    node.InnerText = writeValue;
                    updateSuccess = true;
                    break;
                }
            }
            if(!updateSuccess)
            {
                XmlElement xmlEment = xml.CreateElement(AttributeNode);
                xmlEment.InnerText = writeValue;
            }
            xml.Save(XmlDefaultPath + filePath);
            return true;
        }
    }
}

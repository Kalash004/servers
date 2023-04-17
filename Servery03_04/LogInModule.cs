using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Servery03_04
{
    internal class LogInModule
    {
        string PATH = "M:\\csharp\\Middle\\servers03_04\\Servery03_04\\Servery03_04\\Files\\UserBase.xml";
        public bool LogIn(Client client)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(PATH);
            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;
            nodeList = root.SelectNodes("User");
            foreach (XmlNode node in nodeList)
            {
                if (node.SelectSingleNode("Nick").InnerText.Equals(client.Name))
                {
                    if (node.SelectSingleNode("Password").InnerText.Equals(client.Pass))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

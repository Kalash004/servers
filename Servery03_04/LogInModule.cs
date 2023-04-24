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
        private static LogInModule? instance = null;
        private string PATH = "M:\\csharp\\Middle\\servers03_04\\Servery03_04\\Servery03_04\\Files\\UserBase.xml";
        private LogInModule() { }

        public static LogInModule Instance()
        {
            if (instance == null)
            {
                instance = new LogInModule();
            }
            return instance;
        }
        public bool LogIn(Client client)
        {
            XmlReader reader = XmlReader.Create(PATH);
            string? currentNick = null;
            string? currentPass = null;
            while (reader.Read())
            {
                switch(reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("Nick"))
                        {
                            reader.Read();
                            currentNick = reader.Value;
                        }
                        if (reader.Name.Equals("Password"))
                        {
                            reader.Read();
                            currentPass = reader.Value;
                        }
                        break;
                }
                if (currentNick != null && currentPass != null)
                {
                    if (currentNick.Equals(client.Name) && currentPass.Equals(client.Pass)) return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04
{
    internal class Client
    {
        private String name;
        private String pass;
        public TcpClient client;

        public string Name { get => name; set => name = value; }
        public string Pass { get => pass; set => pass = value; }

        public Client (string name, string pass, TcpClient client)
        {
            this.Name = name;
            this.Pass = pass;
            this.client = client;
        }

        public override string ToString()
        {
            return $" |Name: {Name}|";
        }
    }
}

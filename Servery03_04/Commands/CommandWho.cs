using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04.Commands
{
    internal class CommandWho : ICommand
    {
        private List<Client> clients;
        public CommandWho(List<Client> clients)
        {
            this.clients = clients;
        }
        public string Execute()
        {
            string returner = "";
            foreach (var client in clients)
            {
                returner = $"Client : {client.ToString()} Ip : {client.client.Client.RemoteEndPoint.AddressFamily.ToString()} \n";
            }
            return returner;
        }
    }
}

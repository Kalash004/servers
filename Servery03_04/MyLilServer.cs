using Servery03_04.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Servery03_04
{
    public enum Stats
    {
        LoggedIn,
        FailedToLogIn,
        CommandsExecuted
    }
    public class MyLilServer
    {

        private TcpListener myServer;
        private bool isRunning;
        private List<Thread> threads = new List<Thread>();
        private List<Client> clients = new List<Client>();
        private Dictionary<Stats, Log> statlogs = new Dictionary<Stats, Log>();
        private Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>()
        {
            {"uptime",new UpTimeCommand()}
        };

        public MyLilServer(int port)
        {
            this.myServer = new TcpListener(System.Net.IPAddress.Any,port);
            myServer.Start();
            isRunning = true;
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server Started");
            while (isRunning)
            {
                Console.WriteLine("Working");
                TcpClient client = myServer.AcceptTcpClient();
                Thread t = new Thread(() => ClientLoop(client));
                t.Start();
                threads.Add(t);
            }
        }

        private void ClientLoop(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
            bool clientConnect = true;
            string? data = null;
            string? dataRecive = null;

            SendMessage("You Joined The Server", writer);
            while (clientConnect)
            {
                data = reader.ReadLine();
                if (commands.ContainsKey(data))
                {
                    string back_message = commands[data.ToLower()].Execute();
                    SendMessage(back_message, writer);
                }
            }
            SendMessage("Odpojen",writer);
        }

        private bool LegitimateCommand(string data)
        {
            foreach (var command in commands)
            {
                if (command.Equals(data)) return true;
            }
            return false;
        }

        private void SendMessage(string v, StreamWriter writer)
        {
            writer.WriteLine(v);
            writer.Flush();
        }

    }
}

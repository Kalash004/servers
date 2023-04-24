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

        public MyLilServer(int port)
        {
            this.myServer = new TcpListener(System.Net.IPAddress.Any, port);
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

        private bool ClientLoop(TcpClient client)
        {
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
            if (!LogInWrap(writer,reader,client)) {
                return false;
            }
            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>()
            {
                {"who",new CommandWho(clients)},
                {"stats",new CommandStats(statlogs)},
                {"uptime",new UpTimeCommand()}
            };

            bool clientConnect = true;
            string? data = null;
            string? dataRecive = null;

            while (clientConnect)
            {
                data = reader.ReadLine();
                if (commands.ContainsKey(data))
                {
                    string back_message = commands[data.ToLower()].Execute();
                    SendMessage(back_message, writer);
                }
            }
            SendMessage("Odpojen", writer);
            return true;
        }

        private void SendMessage(string v, StreamWriter writer)
        {
            writer.WriteLine(v);
            writer.Flush();
        }

        private bool LogInWrap(StreamWriter writer, StreamReader reader, TcpClient clientstcp)
        {
            int loginattempts;
            for (loginattempts = 0; loginattempts<2; loginattempts++)
            {
                if (LogIn(writer, reader, clientstcp)) return true;
                SendMessage($"Wrong credentials, retry {loginattempts}/3",writer);
            }
            SendMessage("Terminating connection, bad credentials",writer);
            return false;
        }

        private bool LogIn(StreamWriter writer, StreamReader reader, TcpClient clientstcp)
        {
            SendMessage("Please log in \nName \nPassword", writer);
            string name = reader.ReadLine();
            string pass = reader.ReadLine();
            Client notVerifiedClient = new Client(name, pass, clientstcp);
            if (!LogInModule.Instance().LogIn(notVerifiedClient))
            {
                statlogs.Add(Stats.LoggedIn, new Log(Stats.LoggedIn, notVerifiedClient));
                return false;
            }
            this.clients.Add(notVerifiedClient);
            return true;
        }
    }
}

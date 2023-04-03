using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04
{
    enum Commands
    {
        date,
        help,
        ipconfig,
        exit,
        wrongcommand,
        error
    }
    internal class MyLilServer
    {
        private TcpListener myServer;
        private bool isRunning;
        private String[] commands = {
            "date",
            "help",
            "ipconfig",
            "exit"
        };

        public MyLilServer(TcpListener myServer, bool isRunning)
        {
            this.myServer = myServer;
            this.isRunning = isRunning;
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server Started");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                ClientLoop(client);
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
                Commands command = GetCommand(data);
                string back_message = ExecuteCommand(command);

                dataRecive = data + " prijato";
                writer.WriteLine(dataRecive);
                writer.Flush();
            }
            writer.WriteLine("Byl jsi odpojen");
            writer.Flush();
        }

        private string ExecuteCommand(Commands command)
        {
            throw new NotImplementedException();
        }

        private Commands GetCommand(string? data)
        {
            data = data.ToLower();
            if (!LegitimateCommand(data)) return Commands.wrongcommand;
            Commands command = Commands.error;
            for (int i = 0; i < commands.Length; i++)
            {
                if (data.Equals(commands[i]))
                {
                    command = (Commands)i;
                }
            }
            if (command == Commands.error) throw new Exception("Error on command");
            return command;
        }

        private bool LegitimateCommand(string data)
        {
            throw new NotImplementedException();
        }

        private void SendMessage(string v, StreamWriter writer)
        {
            writer.WriteLine(v);
            writer.Flush();
        }

    }
}

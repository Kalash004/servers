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
                string back_message = ExecuteCommand(command, data, client);
                dataRecive = data + " prijato";
                SendMessage(dataRecive, writer);
                SendMessage(back_message, writer);
            }
            SendMessage("Odpojen",writer);
        }

        private string ExecuteCommand(Commands command, string data, TcpClient client)
        {
            switch (command)
            {
                case Commands.date:
                    return GetDate();
                    break;
                case Commands.help:
                    return GetHelp();
                    break;
                case Commands.ipconfig:
                    return GetIp(client);
                    break;
                case Commands.exit:
                    return StopClient();
                    break;
                case Commands.wrongcommand:
                    return ReturnWrongCmd(data);
                    break;
                case Commands.error:
                    return SendError();
                    break;
                default: 
                    return SendError();
                    break;
            }
                
        }

        private string SendError()
        {
            return "Error happend on Server Side";
        }

        private string ReturnWrongCmd(string data)
        {
            return $"Unexcpected Command : {data}";
        }

        private string StopClient()
        {
            return "This command isnt working right now";
        }

        private string GetIp(TcpClient client)
        {
            return "ip";
        }

        private string GetHelp()
        {
            return "Mozne commandy :  date, help, ipconfig, exit";
        }

        private string GetDate()
        {
            return DateTime.Now.ToString();
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

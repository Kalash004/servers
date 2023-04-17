using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04.Commands
{
    internal class LogInCommand : ICommand
    {
        private Client client;
        private static LogInCommand instance;
        private LogInCommand() { }

        private LogInCommand Instance(Client client)
        {
            if (instance == null )
            {
                instance = new LogInCommand();
            }
            instance.client = client;
            return instance;
        } 
        public string Execute()
        {
            if (new LogInModule().LogIn(client))
            {
                return "Logged in";
            }
            return "Didnt log in";
        }
    }
}

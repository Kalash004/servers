using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04.Commands
{
    internal class CommandStats : ICommand
    {
        private Dictionary<Stats, Log> logs;
        public CommandStats(Dictionary<Stats, Log> logs)
        {
            this.logs = logs;
        }

        public string Execute()
        {
            string returner = "";
            foreach (var log in logs)
            {
                returner += log.ToString();
            }
            return returner;
        }
    }
}

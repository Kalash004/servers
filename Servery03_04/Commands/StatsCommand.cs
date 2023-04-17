using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04.Commands
{
    internal class StatsCommand : ICommand
    {
        private Dictionary<Stats, Log> logs;
        private static StatsCommand instance;
        private StatsCommand() { }

        public StatsCommand Instance(Dictionary<Stats,Log> logs)
        {
            if (instance == null)
            {
                instance = new StatsCommand();
            }
            instance.logs = logs;
            return instance;
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

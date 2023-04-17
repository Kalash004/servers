using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04.Commands
{
    internal class UpTimeCommand : ICommand
    {
        public string Execute()
        {
            var timespan = TimeSpan.FromMilliseconds(Environment.TickCount);
            return ($"System time up: " +
            $"{timespan.Days} days " +
            $"{timespan.Hours} hours " +
            $"{timespan.Minutes} minutes " +
            $"{timespan.Seconds} seconds");
        }
    }
}

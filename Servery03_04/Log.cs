using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servery03_04
{
    internal class Log
    {
        private Stats type;
        private Client client;
        private DateTime time;

        public Log(Stats type, Client client)
        {
            this.type = type;
            this.client = client;
            this.time = DateTime.Now;
        }

        public Stats Type { get { return type; } }
        public DateTime Time { get { return time; } }
        public Client Client { get { return client; } }
        public override string ToString()
        {
            return $"{type} {client.ToString()} {time}";
        }

    }
}

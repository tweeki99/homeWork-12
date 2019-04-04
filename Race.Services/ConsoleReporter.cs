using Race.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Race.Services
{
    public class ConsoleReporter : IReporter
    {
        public void SendMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}

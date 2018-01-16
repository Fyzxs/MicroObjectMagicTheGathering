using Library.Eventing.Tcp;
using System;

namespace EventBusConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TcpListenerEventBus tcpListenerEventBus = new TcpListenerEventBus(8080);
            tcpListenerEventBus.Start();

            
            Console.ReadKey();
        }
    }
}

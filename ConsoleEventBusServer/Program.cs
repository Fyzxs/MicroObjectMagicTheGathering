using Library.Bytes;
using Library.Eventing;
using Library.Eventing.Tcp;
using System;
using System.Text;

namespace ConsoleEventBusServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TcpListenerEventBus tcpListenerEventBus = new TcpListenerEventBus(8080);
            tcpListenerEventBus.Start();

            Console.WriteLine("q to quit");
            Console.WriteLine("s to send a message");


            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.S:
                        Console.WriteLine("Sending Static Data");
                        tcpListenerEventBus.Notify(new StaticNotify());
                        break;
                    case ConsoleKey.Q:
                        Console.WriteLine("Terminated - enter to close");
                        Console.ReadLine();
                        return;
                }
            }

        }

        public class StaticNotify : IEventMessage
        {
            public IBytes Bytes() => new BytesOf(Encoding.ASCII.GetBytes("Sample Data"));
        }
    }
}

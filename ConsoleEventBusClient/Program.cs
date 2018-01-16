using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ConsoleEventBusClient
{
    public class Program
    {
        private static TcpClient tcpClient;
        public static void Main(string[] args)
        {
            tcpClient = new TcpClient("127.0.0.1", 8080);

            string msg = "abcdefghij";
            // Translate the passed message into ASCII and store it as a Byte array.
            byte[] data = System.Text.Encoding.ASCII.GetBytes(msg);

            NetworkStream stream = tcpClient.GetStream();

            while (true)
            {

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                switch (key.Key)
                {
                    case ConsoleKey.Q:
                        Console.WriteLine("Terminated - enter to close");
                        Console.ReadLine();
                        return;
                    case ConsoleKey.S:
                        lock (tcpClient)
                        {
                            // Send the message to the connected TcpServer. 
                            stream.Write(data, 0, data.Length);
                            stream.Write(new[] { (byte) '\0' }, 0, 1);
                            stream.Flush();
                        }

                        break;
                }
            }
        }

        private static bool readProcessing = false;
        private static System.Threading.Timer timer2 = new System.Threading.Timer(ReadFromStream, null, 1000, 1);
        private static void ReadFromStream(object state)
        {
            lock (timer2)
            {
                if (readProcessing) return;
                readProcessing = true;
            }


            List<byte> fullBytes = new List<byte>();
            NetworkStream stream = tcpClient.GetStream();
            try
            {
                int byteValue = 0;
                while (( byteValue = stream.ReadByte() ) != '\0')
                {
                    fullBytes.Add((byte) byteValue);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string msg = Encoding.ASCII.GetString(fullBytes.ToArray());
            Console.WriteLine($"Client Received : {msg}");

            lock (timer2)
            {
                readProcessing = false;
            }
        }
    }
}

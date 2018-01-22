using Library.Bytes;
using Library.Eventing;
using Library.Eventing.Sockets;
using System;
using System.ComponentModel;
using System.Text;
using Library.Eventing.Sockets.Server;

namespace ConsoleEventBusServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SocketListenerEventBus socketListenerEventBus = new SocketListenerEventBus();
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender, eventArgs) =>
            {
                Console.WriteLine("Server Started");
                socketListenerEventBus.Start();
            };
            bw.RunWorkerAsync();

            bool running = true;
            while (running)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        socketListenerEventBus.Stop();
                        running = false;
                        break;

                    case ConsoleKey.S:
                        {
                            socketListenerEventBus.Notify(new NullTerminatedBytesEventMessage(
                                new BytesOf(Encoding.ASCII.GetBytes("New Message - Tk492 Why aren't you at your post?"))));

                            break;
                        }
                    case ConsoleKey.L:
                        {
                            IEventMessage bytesEventMessage = new NullTerminatedBytesEventMessage(
                                new BytesOf(Encoding.ASCII.GetBytes("This is a really long message to test receiving messages over the default buffer size. " +
                                                                                           "Not actually huge because the buffer is only 256 characters; or bytes in this case. " +
                                                                                           "Which using ascii makes work. Stringbuilder is gonna BORK us bad. " +
                                                                                           "Unless we switch from ASCII encoding to some other type of encoding to properly utilize i18n. " +
                                                                                           "I want to see it piece three pieces back together. " +
                                                                                           "That means I need to write something that is over 512 characters. " +
                                                                                           "Yes; I could just shrink the buffer size. " +
                                                                                           "It'd be interesting to but it to one and see what it does. " +
                                                                                           "Just on the client would work fine. " +
                                                                                           "The cool thing about this is how the client and server will pretty much be using the " +
                                                                                           "same functionality to receive/send.")));
                            socketListenerEventBus.Notify(bytesEventMessage);
                            break;
                        }
                }
            }

            Console.WriteLine("Server Terminated");
            Console.ReadKey();
        }
    }
}

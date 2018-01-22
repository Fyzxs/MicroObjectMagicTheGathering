using Library.Bytes;
using Library.Eventing;
using Library.Eventing.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Library.Eventing.Sockets.Client;

namespace ConsoleEventBusClient
{
    public class Program
    {

        public static int Main(string[] args)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (sender, eventArgs) => Test2();
            bw.RunWorkerAsync();

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Q:
                        Console.WriteLine("Main ended ");
                        return 0;

                    case ConsoleKey.S:
                        {
                            IEventMessage message = new NonTerminatingBytesEventMessage(new BytesOf(Encoding.ASCII.GetBytes("Partial Messa")));

                            _manySockets.ForEach(socket => socket.Notify(message));
                            //byte[] clientMsg = Encoding.ASCII.GetBytes("Partial Messa");
                            //_client.BeginSend(clientMsg, 0, clientMsg.Length, SocketFlags.None, SendCallback, _client);

                            break;
                        }
                    case ConsoleKey.D:
                        {

                            IEventMessage message =
                                new NullTerminatedBytesEventMessage(new BytesOf(Encoding.ASCII.GetBytes("ge testing")));

                            _manySockets.ForEach(socket => socket.Notify(message));
                            //byte[] clientMsg = Encoding.ASCII.GetBytes("ge testing\0");
                            //_client.BeginSend(clientMsg, 0, clientMsg.Length, SocketFlags.None, SendCallback, _client);

                            break;
                        }
                    case ConsoleKey.A:
                        {

                            IEventMessage message =
                                new NullTerminatedBytesEventMessage(new BytesOf(Encoding.ASCII.GetBytes("DoubleMessage\0TestMessage")));

                            _manySockets.ForEach(socket => socket.Notify(message));
                            //byte[] clientMsg = Encoding.ASCII.GetBytes("DoubleMessage\0TestMessage\0");
                            //_client.BeginSend(clientMsg, 0, clientMsg.Length, SocketFlags.None, SendCallback, _client);

                            break;
                        }
                }
            }
        }
        public static List<AsyncSocket> _manySockets = new List<AsyncSocket>();

        private static void Test2()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            // Create a TCP/IP socket.  
            for (int i = 0; i < 100; i++)
            {
                _manySockets.Add(
                    new PrintingAsyncSocket(new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp),
                        ipAddress));
            }
            _manySockets.ForEach(socket => socket.Connect());
        }

        public class PrintingAsyncSocket : AsyncSocket
        {
            public PrintingAsyncSocket(Socket socket, IPAddress ipAddress) : base(socket, ipAddress) { }
            protected override void ReceiveOnMessageHandler(object o, IEventMessage eventMessage)
            {
                Console.WriteLine($"Printer Message msg=[{Encoding.ASCII.GetString(eventMessage.Bytes())}]");
            }
        }
        public class NonTerminatingBytesEventMessage : IEventMessage
        {
            private readonly IBytes _origin;

            public NonTerminatingBytesEventMessage(IBytes bytes) => _origin = bytes;

            public NonTerminatingBytesEventMessage(List<byte> bytes) : this(new BytesOf(bytes)) { }

            public byte[] Bytes() => _origin.Bytes();
        }

        //private class SocketAsyncDataBag
        //{
        //    public Socket ActiveSocket;

        //    // Size of receive buffer.  
        //    private const int BufferSize = 64;

        //    // Receive buffer.  
        //    public readonly byte[] Buffer = new byte[BufferSize];

        //    // Received data string.  
        //    public readonly StringBuilder StringContents = new StringBuilder();
        //}
        //// The port number for the remote device.  
        //private const int Port = 11000;

        //// ManualResetEvent instances signal completion.  
        //private static readonly ManualResetEvent ConnectDone = new ManualResetEvent(false);
        //private static readonly ManualResetEvent SendDone = new ManualResetEvent(false);
        //private static readonly ManualResetEvent ReceiveDone = new ManualResetEvent(false);
        //private static Socket _client;
        //private static AsyncSocket _asyncSocket;

        //private static void Test()
        //{
        //    IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
        //    IPAddress ipAddress = ipHostInfo.AddressList[0];
        //    IPEndPoint remoteEp = new IPEndPoint(ipAddress, Port);

        //    // Create a TCP/IP socket.  
        //    _client = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        //    // Connect to the remote endpoint.  

        //    _client.BeginConnect(remoteEp, ConnectCallback, _client);
        //    ConnectDone.WaitOne();
        //    SendDone.Reset();

        //    // Send test data to the remote device.
        //    byte[] byteData = Encoding.ASCII.GetBytes("Initial Client Message\0");
        //    _client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, SendCallback, _client);
        //    SendDone.WaitOne();

        //    ReceiveDone.Reset();
        //    SocketAsyncDataBag databag = new SocketAsyncDataBag();
        //    databag.ActiveSocket = _client;
        //    // Receive the response from the remote device.
        //    _client.BeginReceive(databag.Buffer, 0, databag.Buffer.Length, SocketFlags.None, ReceiveCallback, databag);

        //    ReceiveDone.WaitOne();
        //}

        //public interface IAsyncSocket : IListener, IConnectAsyncSocket { }
        //public class AsyncSocket : IAsyncSocket//Make Abstract
        //{
        //    private readonly ConnectAsyncSocket _connect;
        //    private readonly IListener _send;
        //    private readonly ReceiveAsyncSocket _receive;

        //    public AsyncSocket(Socket socket, IPAddress ipAddress)
        //    {
        //        _connect = new ConnectAsyncSocket(socket, ipAddress);
        //        _send = new SendToAsyncSocket(socket);
        //        _receive = new ReceiveAsyncSocket(socket);
        //        _receive.MessageHandler += ReceiveOnMessageHandler;
        //    }

        //    private void ReceiveOnMessageHandler(object o, EventMessageEventArgs eventMessageEventArgs)
        //    {
        //        Console.WriteLine($"Received Message={Encoding.ASCII.GetString(eventMessageEventArgs.EventMessage().Bytes())}");
        //    }

        //    public void Connect()
        //    {
        //        _connect.Connect();
        //        _receive.Receive();
        //    }

        //    public void Notify(IEventMessage eventMessage) => _send.Notify(eventMessage);
        //}

        //public class ReceiveAsyncSocket
        //{
        //    public event EventMessageEventHandler MessageHandler;
        //    private readonly Socket _socket;
        //    private const int BufferSize = 256;
        //    private readonly byte[] _buffer = new byte[BufferSize];
        //    private readonly EventMessageByteCollection _accumulator = new EventMessageByteCollection();

        //    public ReceiveAsyncSocket(Socket socket) => _socket = socket;

        //    public void Receive() => _socket.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, null);

        //    private void Callback(IAsyncResult ar)
        //    {
        //        try
        //        {
        //            ReadBuffer(ar);

        //            _socket.BeginReceive(_buffer, 0, BufferSize, SocketFlags.None, Callback, null);

        //        } catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }
        //    }

        //    private void ReadBuffer(IAsyncResult ar)
        //    {
        //        int byteRead = _socket.EndReceive(ar);
        //        for (int index = 0; index < byteRead; index++)
        //        {
        //            //If we hit the terminator
        //            //AND the read bytes
        //            //Currently no good object - I expect it'll simplify when sockets are micro'd
        //            if (MessageContinues(_buffer[index])) continue;

        //            OnMessageHandler(_accumulator.EventMessage());

        //            _accumulator.Clear();
        //        }
        //    }

        //    private bool MessageContinues(byte b)
        //    {
        //        if (b == '\0') return false;

        //        _accumulator.Add(b);
        //        return true;
        //    }

        //    private void OnMessageHandler(IEventMessage eventMessage) => MessageHandler?.Invoke(this, new EventMessageEventArgs(eventMessage));
        //}


        //public interface IConnectAsyncSocket
        //{
        //    void Connect();
        //}
        //public class ConnectAsyncSocket : IConnectAsyncSocket
        //{
        //    private readonly Socket _socket;
        //    private readonly ManualResetEvent _connectDone = new ManualResetEvent(false);
        //    private readonly IPEndPoint _ipEndPoint;

        //    public ConnectAsyncSocket(Socket socket, IPAddress ipAddress)
        //    {
        //        _socket = socket;
        //        _ipEndPoint = new IPEndPoint(ipAddress, 11000);
        //    }

        //    public void Connect()
        //    {
        //        _connectDone.Reset();

        //        _socket.BeginConnect(_ipEndPoint, Callback, _socket);

        //        _connectDone.WaitOne();
        //    }

        //    private void Callback(IAsyncResult ar)
        //    {
        //        try
        //        {
        //            // Retrieve the socket from the state object.  
        //            Socket client = (Socket) ar.AsyncState;

        //            // Complete the connection.  
        //            client.EndConnect(ar);

        //            Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);

        //            _connectDone.Set();
        //        } catch (Exception e)
        //        {
        //            Console.WriteLine(e.ToString());
        //        }
        //    }
        //}

        //private static void ConnectCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.  
        //        Socket client = (Socket) ar.AsyncState;

        //        // Complete the connection.  
        //        client.EndConnect(ar);

        //        Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint);

        //        // Signal that the connection has been made.  
        //        ConnectDone.Set();
        //    } catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        //private static void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.  
        //        Socket client = (Socket) ar.AsyncState;

        //        // Complete sending the data to the remote device.  
        //        int bytesSent = client.EndSend(ar);
        //        Console.WriteLine("Sent {0} bytes to server.", bytesSent);

        //        // Signal that all bytes have been sent.  
        //        SendDone.Set();
        //    } catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}
        //private static void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the state object and the client socket   
        //        // from the asynchronous state object.
        //        SocketAsyncDataBag dataBag = ar.AsyncState as SocketAsyncDataBag;
        //        Socket client = dataBag.ActiveSocket;

        //        // Read data from the remote device.  
        //        int bytesRead = client.EndReceive(ar);

        //        if (bytesRead > 0)//Guard-y
        //        {
        //            List<byte> bytes = new List<byte>();
        //            bool terminated = false;
        //            for (int index = 0; index < bytesRead; index++)
        //            {
        //                byte b = dataBag.Buffer[index];
        //                if (b == '\0')
        //                {
        //                    terminated = true;
        //                    break;
        //                }

        //                bytes.Add(b);
        //            }

        //            dataBag.StringContents.Append(Encoding.ASCII.GetString(bytes.ToArray()));
        //            if (terminated)
        //            {
        //                Console.WriteLine("Client Received Message: " + dataBag.StringContents.ToString());
        //                dataBag.StringContents.Clear();
        //            }
        //        }

        //        client.BeginReceive(dataBag.Buffer, 0, dataBag.Buffer.Length, SocketFlags.None, ReceiveCallback, dataBag);
        //    } catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //async Task ReadBuffer(IAsyncResult ar, Socket client, AsyncResultStateDataBag resultDataBag)
        //{
        //    // Read data from the remote device.  
        //    int bytesRead = client.EndReceive(ar);

        //    for (int index = 0; index < bytesRead; index++)
        //    {
        //        byte b = resultDataBag.Buffer[index];
        //        if (b != '\0')//Message Continues
        //        {
        //            resultDataBag.Accumulator.Add(b);
        //            continue;
        //        }

        //        await _eventBus.Notify(resultDataBag.Accumulator.EventMessage());
        //        resultDataBag.Accumulator.Clear();
        //    }
        //}
    }
}


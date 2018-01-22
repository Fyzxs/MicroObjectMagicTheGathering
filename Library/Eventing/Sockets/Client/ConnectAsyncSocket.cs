using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Library.Eventing.Sockets.Client
{
    public class ConnectAsyncSocket : IConnectAsyncSocket
    {
        private readonly Socket _socket;
        private readonly ManualResetEvent _connectDone = new ManualResetEvent(false);
        private readonly IPEndPoint _ipEndPoint;

        public ConnectAsyncSocket(Socket socket, IPAddress ipAddress)
        {
            _socket = socket;
            _ipEndPoint = new IPEndPoint(ipAddress, 11000);
        }

        public void Connect()
        {
            _connectDone.Reset();

            _socket.BeginConnect(_ipEndPoint, Callback, null);

            _connectDone.WaitOne();
        }

        private void Callback(IAsyncResult ar)
        {
            try
            {
                _socket.EndConnect(ar);
                _connectDone.Set();
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
    public interface IConnectAsyncSocket
    {
        void Connect();
    }
}
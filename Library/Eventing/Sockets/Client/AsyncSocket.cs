using System.Net;
using System.Net.Sockets;

namespace Library.Eventing.Sockets.Client
{
    public abstract class AsyncSocket : IAsyncSocket
    {
        private readonly IConnectAsyncSocket _connect;
        private readonly IListener _send;
        private readonly IReceiveAsyncSocket _receive;

        protected AsyncSocket(Socket socket, IPAddress ipAddress)
        {
            _connect = new ConnectAsyncSocket(socket, ipAddress);
            _send = new SendToAsyncSocket(socket);
            _receive = new ReceiveAsyncSocket(socket);
            _receive.MessageHandler += ReceiveOnMessageHandler;
        }

        protected abstract void ReceiveOnMessageHandler(object o, IEventMessage eventMessage);

        public void Connect()
        {
            _connect.Connect();
            _receive.Receive();
        }

        public void Notify(IEventMessage eventMessage) => _send.Notify(eventMessage);
    }
    public interface IAsyncSocket : IListener
    {
        void Connect();
    }
}
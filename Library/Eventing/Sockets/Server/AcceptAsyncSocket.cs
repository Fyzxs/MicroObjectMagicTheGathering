using System;
using System.Net.Sockets;
using Library.Threading;

namespace Library.Eventing.Sockets.Server
{
    public class AcceptAsyncSocket : IAcceptAsyncSocket
    {
        private readonly Socket _socketServer;
        private readonly SocketListenerEventBus _eventBus;
        private readonly IManualResetEventBookEnd _acceptEventing;
        private readonly IReceiveAcceptedAsyncSocket _receive;

        public AcceptAsyncSocket(Socket socketServer, SocketListenerEventBus eventBus)
            : this(socketServer, eventBus, new ManualResetEventBookEnd(), new ReceiveAcceptedAsyncSocket()) { }

        public AcceptAsyncSocket(
            Socket socketServer,
            SocketListenerEventBus eventBus,
            IManualResetEventBookEnd acceptEventing,
            IReceiveAcceptedAsyncSocket receive)
        {
            _socketServer = socketServer;
            _eventBus = eventBus;
            _acceptEventing = acceptEventing;
            _receive = receive;
            _receive.MessageHandler += ReceiveOnMessageHandler;
        }

        private void ReceiveOnMessageHandler(object sender, IEventMessage eventMessage) => _eventBus.Notify(eventMessage);

        public void Accept()
        {
            _acceptEventing.Reset();
            _socketServer.BeginAccept(Callback, null);//Nulls bad; yep.
            _acceptEventing.WaitOne(TimeSpan.FromSeconds(1));
        }

        private void Callback(IAsyncResult ar)
        {
            try
            {
                Socket client = _socketServer.EndAccept(ar);
                _eventBus.Attach(new SendToAsyncSocket(client));

                _receive.Receive(client);

                _acceptEventing.Set();
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Dispose() => _acceptEventing?.Dispose();
    }

    public interface IAcceptAsyncSocket : IDisposable
    {
        void Accept();
    }

}
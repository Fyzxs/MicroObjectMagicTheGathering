using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using Library.Threading;

namespace Library.Eventing.Sockets.Server
{
    public class SocketListenerEventBus : IEventBus, IDisposable
    {
        private readonly IBackgroundWorkerBookEnd _backgroundWorker;
        private readonly Socket _listener;
        private readonly IPAddress _ipAddress;
        private readonly IAcceptAsyncSocket _acceptSocket;
        private readonly IEventBus _origin;

        public SocketListenerEventBus()
        {
            _ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            _listener = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            _origin = new EventBus();
            _backgroundWorker = new BackgroundWorkerBookEnd();
            _acceptSocket = new AcceptAsyncSocket(_listener, this);
        }

        public void Start() => _backgroundWorker.RunWorker(StartListening);
        public void Stop() => _backgroundWorker.Cancel();

        private void StartListening(object sender, DoWorkEventArgs args)
        {
            try
            {
                _listener.Bind(new IPEndPoint(_ipAddress, 11000));
                _listener.Listen(100);

                while (_backgroundWorker.NotCancelled()) _acceptSocket.Accept();

            } finally
            {
                _listener.Close();
            }
        }

        public void Attach(IListener listener) => _origin.Attach(listener);

        public void Notify(IEventMessage eventMessage) => _origin.Notify(eventMessage);

        public void Dispose()
        {
            _backgroundWorker?.Dispose();
            _acceptSocket?.Dispose();
            _listener?.Dispose();
        }
    }
}
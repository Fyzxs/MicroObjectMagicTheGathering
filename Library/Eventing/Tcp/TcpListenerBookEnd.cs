using System;
using System.Net;
using System.Net.Sockets;

namespace Library.Eventing.Tcp
{
    public class TcpListenerBookEnd : ITcpListenerBookEnd
    {
        private readonly TcpListener _tcpListener;

        public TcpListenerBookEnd(int port) : this(new TcpListener(IPAddress.Any, port)) { }

        public TcpListenerBookEnd(TcpListener tcpListener) => _tcpListener = tcpListener;
        public void BeginAcceptTcpClient(AsyncCallback callback) => _tcpListener.BeginAcceptTcpClient(callback, this);
        public IListener EndAcceptTcpClient(IAsyncResult asyncResult, IEventBus eventBus)
        {
            ITcpClientBookEnd tcpClientBookEnd = new TcpClientBookEnd(_tcpListener.EndAcceptTcpClient(asyncResult));
            return new TcpClientListener(tcpClientBookEnd, eventBus);
        }

        public void Start() => _tcpListener.Start();
        public void Stop() => _tcpListener.Stop();
    }
}
using System;

namespace Library.Eventing.Tcp
{
    public interface ITcpListenerBookEnd
    {
        void BeginAcceptTcpClient(AsyncCallback callback);
        IListener EndAcceptTcpClient(IAsyncResult asyncResult, IEventBus tcpListenerEventBus);
        void Start();
        void Stop();
    }
}
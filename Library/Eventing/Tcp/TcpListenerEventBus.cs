using Library.Threading;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Library.Eventing.Tcp
{
    public class TcpListenerEventBus : IEventBus, IInfiniteLoop
    {
        private readonly IEventBus _origin = new EventBus();
        //TODO: _listener & _acceptingClient  might be able to be merged and return an IListener
        private readonly ITcpListenerBookEnd _listener;
        private readonly IResetEventBookEnd _acceptingClient;
        private readonly IBackgroundWorkerBookEnd _worker;
        private readonly SemaphoreSlimBookEnd _semaphore = new SemaphoreSlimBookEnd();
        private bool _running;

        public TcpListenerEventBus(int port) : this(new TcpListenerBookEnd(port), new ResetEventBookEnd(), new BackgroundWorkerBookEnd()) { }

        public TcpListenerEventBus(ITcpListenerBookEnd tcpListenerBookEnd, IResetEventBookEnd resetEventBookEnd, IBackgroundWorkerBookEnd worker)
        {
            _listener = tcpListenerBookEnd;
            _acceptingClient = resetEventBookEnd;
            _worker = worker;
        }

        private async void AcceptingThreadOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            if (await Started()) return;

            Running();
        }

        private void Running()
        {
            while (_running)
            {
                _acceptingClient.Reset();

                _listener.BeginAcceptTcpClient(AcceptCallback);

                _acceptingClient.WaitOne();
            }
        }

        private async Task<bool> Started()
        {
            try
            {
                await _semaphore.Wait();
                if (_running) return true;
                _running = true;
                return false;
            } finally
            {
                _semaphore.Release();
            }
        }

        public void Start()
        {
            _listener.Start();
            _worker.RunWorker(AcceptingThreadOnDoWork);
        }

        public void Stop()
        {
            _running = false;
            _listener.Stop();
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            _acceptingClient.Set();
            ITcpListenerBookEnd listener = (ITcpListenerBookEnd) ar.AsyncState;
            Attach(listener.EndAcceptTcpClient(ar, this));
        }

        public void Attach(IListener listener) => _origin.Attach(listener);

        public Task Notify(IEventMessage eventMessage) => _origin.Notify(eventMessage);

        public void Detach(IListener listener) => _origin.Detach(listener);
    }

    public interface IInfiniteLoop
    {
        void Start();
        void Stop();
    }
}
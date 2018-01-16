using Library.Bytes;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Eventing.Tcp
{
    public class TcpClientListener : IListener
    {
        private readonly ITcpClientBookEnd _client;
        private readonly IEventBus _eventBus;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable - Scoping
        private readonly ITimerBookEnd _timer;

        public TcpClientListener(ITcpClientBookEnd client, IEventBus eventBus) :
            this(client, eventBus, new TimerBookEnd(10, 100))
        { }
        public TcpClientListener(
            ITcpClientBookEnd client,
            IEventBus eventBus,
            ITimerBookEnd timer)
        {
            ThreadPool.QueueUserWorkItem(ReadClient);
            _client = client;
            _eventBus = eventBus;
            _timer = timer;
        }

        public async Task Update(IEventMessage eventMessage)
        {
            IBytesWriter bytesWriter = _client.Writer();
            await bytesWriter.Write(eventMessage.Bytes());
        }

        public void Attached() => _timer.Start(ReadClient, null);

        private async void ReadClient(object state)
        {
            IBytesReader reader = _client.Reader(); //TODO: Wrap this in a "TcpJsonEventMessage" thingy
            IBytes bytes = reader.ReadToEnd();
            Console.WriteLine("Reading Client " + Encoding.ASCII.GetString(bytes.Bytes()));
            await _eventBus.Notify(new BytesEventMessage(bytes));
        }
    }

    public interface ITimerBookEnd
    {
        void Start(TimerCallback callback, object state);
    }
    public class TimerBookEnd : ITimerBookEnd
    {
        //TODO: Not a fan of this class

        private readonly long _dueTime;
        private readonly long _period;
        private Timer _timer;

        public TimerBookEnd(long dueTime, long period)
        {
            _dueTime = dueTime;
            _period = period;
        }

        public void Start(TimerCallback callback, object state) => _timer = new Timer(callback, state, _dueTime, _period);
    }
}
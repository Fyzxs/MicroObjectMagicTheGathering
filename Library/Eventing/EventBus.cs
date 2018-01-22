using System.Collections.Concurrent;

namespace Library.Eventing
{
    public class EventBus : IEventBus
    {
        private readonly ConcurrentBag<IListener> _bag = new ConcurrentBag<IListener>();

        public void Attach(IListener listener)
        {
            _bag.Add(listener);
        }

        public void Notify(IEventMessage eventMessage)
        {
            foreach (IListener listener in _bag)
            {
                listener.Notify(eventMessage);
            }
        }
    }
}
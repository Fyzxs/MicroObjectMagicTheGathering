using Library.Collections;
using Library.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Eventing
{
    public class ListenerCollection : IListenerCollection
    {
        private readonly ISetBookEnd<IListener> _listeners;

        public ListenerCollection() : this(new BlockingSetBookEnd<IListener>()) { }

        public ListenerCollection(ISetBookEnd<IListener> listeners) => _listeners = listeners;

        public void Add(IListener listener) => _listeners.Add(listener);

        public async Task<Task[]> NotifyAll(IEventMessage eventMessage) => ( await _listeners.Select(listener => listener.Update(eventMessage)) ).ToArray();

        public void Remove(IListener listener) => _listeners.Remove(listener);
    }
}
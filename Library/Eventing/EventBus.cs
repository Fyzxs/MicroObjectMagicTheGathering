using Library.Collections;
using Library.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Eventing
{
    public class EventBus : IEventBus
    {
        private readonly ISetBookEnd<IListener> _listeners;

        public EventBus() : this(new BlockingSetBookEnd<IListener>()) { }

        public EventBus(ISetBookEnd<IListener> blockingSetBookEnd) => _listeners = blockingSetBookEnd;

        public void Attach(IListener listener)
        {
            _listeners.Add(listener);
            listener.Attached();
        }

        public async Task Notify(IEventMessage eventMessage)
        {
            Console.WriteLine("Notifying");
            IEnumerable<Task> enumerable = await _listeners.Select(listener => listener.Update(eventMessage));
            Task.WaitAll(enumerable.ToArray());
        }

        public void Detach(IListener listener) => _listeners.Remove(listener);
    }
}
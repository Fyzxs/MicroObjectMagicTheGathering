using System;

namespace Library.Eventing
{

    public class EventMessageEventArgs : EventArgs
    {
        private readonly IEventMessage _eventMessage;

        public EventMessageEventArgs(IEventMessage eventMessage) => _eventMessage = eventMessage;
        public IEventMessage EventMessage() => _eventMessage;
    }
}
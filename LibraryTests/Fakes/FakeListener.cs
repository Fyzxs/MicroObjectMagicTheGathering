using Library.Eventing;
using LibraryTests.Fakes.Builders;
using System;

namespace LibraryTests.Fakes
{
    public class FakeListener : IListener
    {
        public class Builder
        {
            private readonly BuilderItemAction<IEventMessage> _notifyItem = new BuilderItemAction<IEventMessage>("FakeListener#Update");

            public Builder Notify() => Notify(() => { });
            public Builder Notify(params Action[] actions)
            {
                _notifyItem.UpdateInvocation(actions);
                return this;
            }

            public FakeListener Build()
            {
                return new FakeListener
                {
                    _notify = _notifyItem,
                };
            }
        }

        private BuilderItemAction<IEventMessage> _notify;

        private FakeListener() { }


        public void Notify(IEventMessage eventMessage) => _notify.Invoke(eventMessage);

        public void AssertNotifyInvokedWith(IEventMessage expected) => _notify.AssertInvokedWith(expected);

        public void AssertNotifyInvokedCountMatches(int count) => _notify.InvokedCountMatches(count);
    }
}
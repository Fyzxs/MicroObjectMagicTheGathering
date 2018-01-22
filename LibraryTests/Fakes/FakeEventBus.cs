using Library.Eventing;
using LibraryTests.Fakes.Builders;

namespace LibraryTests.Fakes
{
    public class FakeEventBus : IEventBus
    {

        public class Builder
        {
            private readonly BuilderItemAction<IListener> _attachItem = new BuilderItemAction<IListener>("FakeEventBus#Attach");
            private readonly BuilderItemAction<IEventMessage> _notifyItem = new BuilderItemAction<IEventMessage>("FakeEventBus#Notify");
            private readonly BuilderItemAction<IListener> _detachItem = new BuilderItemAction<IListener>("FakeEventBus#Detach");

            public Builder Detach()
            {
                _detachItem.UpdateInvocation();
                return this;
            }
            public Builder Notify()
            {
                _notifyItem.UpdateInvocation();
                return this;
            }
            public Builder Attach()
            {
                _attachItem.UpdateInvocation();
                return this;
            }
            public FakeEventBus Build()
            {
                return new FakeEventBus
                {
                    _attach = _attachItem,
                    _notify = _notifyItem,
                    _detach = _detachItem
                };
            }
        }

        private BuilderItemAction<IListener> _detach;
        private BuilderItemAction<IListener> _attach;
        private BuilderItemAction<IEventMessage> _notify;

        private FakeEventBus() { }
        public void Attach(IListener listener) => _attach.Invoke(listener);

        public void Notify(IEventMessage eventMessage) => _notify.Invoke(eventMessage);

        public void Detach(IListener listener) => _detach.Invoke(listener);
    }
}
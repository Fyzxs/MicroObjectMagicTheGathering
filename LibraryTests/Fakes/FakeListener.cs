using Library.Eventing;
using LibraryTests.Fakes.Builders;
using System;
using System.Threading.Tasks;

namespace LibraryTests.Fakes
{
    public class FakeListener : IListener
    {

        public class Builder
        {
            private readonly BuilderItemAction<IEventMessage> _updateItem = new BuilderItemAction<IEventMessage>("FakeListener#Update");
            private readonly BuilderItemTask _attachedItem = new BuilderItemTask("FakeCLASS#Attached");

            public Builder Attached()
            {
                _attachedItem.UpdateInvocation();
                return this;
            }
            public Builder Update() => Update(() => { });
            public Builder Update(params Action[] actions)
            {
                _updateItem.UpdateInvocation(actions);
                return this;
            }

            public FakeListener Build()
            {
                return new FakeListener
                {
                    _update = _updateItem,
                    _attached = _attachedItem
                };
            }
        }

        private BuilderItemAction<IEventMessage> _update;
        public BuilderItemTask _attached;

        private FakeListener() { }


        public Task Update(IEventMessage eventMessage) => Task.Run(() => _update.Invoke(eventMessage));
        public void Attached() => _attached.Invoke();

        public void AssertUpdateInvokedWith(IEventMessage expected) => _update.AssertInvokedWith(expected);

        public void AssertUpdateInvokedCountMatches(int count) => _update.InvokedCountMatches(count);
    }
}
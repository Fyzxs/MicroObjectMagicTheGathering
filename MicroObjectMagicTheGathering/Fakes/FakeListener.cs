using MicroObjectMagicTheGathering.Fakes.Builders;
using System;
using System.Threading.Tasks;

namespace MicroObjectMagicTheGathering.Fakes
{
    public class FakeListener : IListener
    {

        public class Builder
        {
            private readonly BuilderItemAction<IEventMessage> _updateItem = new BuilderItemAction<IEventMessage>("FakeListener#Update");

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
                    _update = _updateItem
                };
            }
        }

        private BuilderItemAction<IEventMessage> _update;

        private FakeListener() { }


        public Task Update(IEventMessage eventMessage) => Task.Run(() => _update.Invoke(eventMessage));

        public void AssertUpdateInvokedWith(IEventMessage expected) => _update.AssertInvokedWith(expected);

        public void AssertUpdateInvokedCountMatches(int count) => _update.InvokedCountMatches(count);
    }
}
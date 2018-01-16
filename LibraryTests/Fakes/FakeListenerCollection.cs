using Library.Eventing;
using LibraryTests.Fakes.Builders;
using System.Threading.Tasks;

namespace LibraryTests.Fakes
{
    public class FakeListenerCollection : IListenerCollection
    {
        public class Builder
        {
            private readonly BuilderItemFunc<Task[]> _notifyAllItem = new BuilderItemFunc<Task[]>("FakeListenerCollection#NotifyAll");
            private readonly BuilderItemAction<IListener> _addItem = new BuilderItemAction<IListener>("FakeListenerCollection#Add");

            public Builder Add()
            {
                _addItem.UpdateInvocation();
                return this;
            }
            public Builder NotifyAll(Task[] expected)
            {
                _notifyAllItem.UpdateInvocation(expected);
                return this;
            }
            public FakeListenerCollection Build()
            {
                return new FakeListenerCollection
                {
                    _notifyAll = _notifyAllItem,
                    _add = _addItem
                };
            }
        }

        private BuilderItemFunc<Task[]> _notifyAll;
        private BuilderItemAction<IListener> _add;
        private FakeListenerCollection() { }
        public void Add(IListener listener) => _add.Invoke(listener);

        public Task<Task[]> NotifyAll(IEventMessage eventMessage) => Task.FromResult(_notifyAll.Invoke(eventMessage));
    }
}
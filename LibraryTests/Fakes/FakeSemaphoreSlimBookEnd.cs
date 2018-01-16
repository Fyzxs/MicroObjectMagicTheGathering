using Library.Threading;
using LibraryTests.Fakes.Builders;
using System.Threading.Tasks;

namespace LibraryTests.Fakes
{
    public class FakeSemaphoreSlimBookEnd : ISemaphoreSlimBookEnd
    {
        public class Builder
        {
            private readonly BuilderItemTask _waitItem = new BuilderItemTask("FakeSemaphoreSlimBookEnd#Wait");
            private readonly BuilderItemAction<Task> _releaseItem = new BuilderItemAction<Task>("FakeSemaphoreSlimBookEnd#Release");

            public Builder Wait()
            {
                _waitItem.UpdateInvocation();
                return this;
            }

            public Builder Release()
            {
                _releaseItem.UpdateInvocation();
                return this;
            }

            public FakeSemaphoreSlimBookEnd Build() => new FakeSemaphoreSlimBookEnd { _wait = _waitItem, _release = _releaseItem };
        }

        private BuilderItemTask _wait;
        private BuilderItemAction<Task> _release;

        private FakeSemaphoreSlimBookEnd() { }

        public Task Wait() => _wait.Invoke();

        public void Release() => _release.Invoke(Task.Run(() => { }));

        public void AssertWaitInvoked() => _wait.AssertInvoked();

        public void AssertReleaseInvoked() => _release.AssertInvoked();
    }
}
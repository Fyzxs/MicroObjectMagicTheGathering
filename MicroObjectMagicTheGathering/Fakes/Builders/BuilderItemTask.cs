using System;
using System.Threading.Tasks;

namespace MicroObjectMagicTheGathering.Fakes.Builders
{
    public class BuilderItemTask : BuilderItem<Task>
    {
        private Action _action;

        public BuilderItemTask(string name) : base(name) => _action = () => throw new TestException(name);

        public void UpdateInvocation() => _action = () => { };

        public Task Invoke()
        {
            InvokedCount++;
            _action();
            return Task.Run(() => { });
        }

        public override void Assert(Action<Task> assertion) => throw new NotImplementedException("We don't assert against Task methods, I think...");
    }
}
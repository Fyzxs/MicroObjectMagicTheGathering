using System.Threading;
using Library.Eventing;

namespace Library.Threading {
    public class ResetEventBookEnd : IResetEventBookEnd
    {
        private readonly ManualResetEvent _origin;

        public ResetEventBookEnd() : this(new ManualResetEvent(false)) { }

        public ResetEventBookEnd(ManualResetEvent manualResetEvent) => _origin = manualResetEvent;

        public void Reset() => _origin.Reset();

        public void WaitOne() => _origin.WaitOne();

        public void Set() => _origin.Set();
    }
}
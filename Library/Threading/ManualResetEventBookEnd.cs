using System;
using System.Threading;

namespace Library.Threading
{
    public class ManualResetEventBookEnd : IManualResetEventBookEnd
    {
        private readonly ManualResetEvent _origin;

        public ManualResetEventBookEnd() : this(new ManualResetEvent(false)) { }

        public ManualResetEventBookEnd(ManualResetEvent origin) => _origin = origin;

        public void Reset() => _origin.Reset();
        public void WaitOne() => WaitOne(TimeSpan.MaxValue);
        public void Set() => _origin.Set();
        public void WaitOne(TimeSpan timeSpan) => _origin.WaitOne(timeSpan);

        public void Dispose() => _origin?.Dispose();
    }
}
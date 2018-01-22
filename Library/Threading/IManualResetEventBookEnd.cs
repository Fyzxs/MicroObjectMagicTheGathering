using System;

namespace Library.Threading
{
    public interface IManualResetEventBookEnd : IDisposable
    {
        void Reset();
        void WaitOne();
        void Set();
        void WaitOne(TimeSpan timeSpan);
    }
}
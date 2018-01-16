using System.Threading;
using System.Threading.Tasks;

namespace Library.Threading
{
    public class SemaphoreSlimBookEnd : ISemaphoreSlimBookEnd
    {
        private readonly SemaphoreSlim _semaphoreSlim;

        public SemaphoreSlimBookEnd() : this(new SemaphoreSlim(1)) { }
        private SemaphoreSlimBookEnd(SemaphoreSlim semaphoreSlim) => _semaphoreSlim = semaphoreSlim;

        public async Task Wait() => await _semaphoreSlim.WaitAsync();

        public void Release() => _semaphoreSlim.Release();
    }

    public interface ISemaphoreSlimBookEnd
    {
        Task Wait();
        void Release();
    }
}
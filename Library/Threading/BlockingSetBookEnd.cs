using Library.Collections;
using System;
using System.Threading.Tasks;

namespace Library.Threading
{
    public class BlockingSetBookEnd<T> : ISetBookEnd<T>
    {
        private readonly ISetBookEnd<T> _origin;
        private readonly ISemaphoreSlimBookEnd _semaphore;

        public BlockingSetBookEnd() : this(new SetBookEnd<T>(), new SemaphoreSlimBookEnd()) { }

        public BlockingSetBookEnd(ISetBookEnd<T> origin, ISemaphoreSlimBookEnd semaphore)
        {
            _origin = origin;
            _semaphore = semaphore;
        }

        public async Task ForEach(Action<T> action)
        {
            try
            {
                await _semaphore.Wait();
                await _origin.ForEach(action);
            } finally
            {
                _semaphore.Release();
            }
        }

        public async Task Add(T t)
        {
            try
            {
                await _semaphore.Wait();
                await _origin.Add(t);
            } finally
            {
                _semaphore.Release();
            }
        }

        public async Task Remove(T t)
        {
            try
            {
                await _semaphore.Wait();
                await _origin.Remove(t);
            } finally
            {
                _semaphore.Release();
            }
        }
    }
}
using Library.Collections;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<TResult>> Select<TResult>(Func<T, TResult> selector)
        {
            try
            {
                await _semaphore.Wait();
                return await _origin.Select(selector);
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
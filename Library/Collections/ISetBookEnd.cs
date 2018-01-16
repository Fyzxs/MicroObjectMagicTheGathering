using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Collections
{
    public interface ISetBookEnd<T>
    {
        Task<IEnumerable<TResult>> Select<TResult>(Func<T, TResult> selector);
        Task Add(T t);
        Task Remove(T t);
    }
}
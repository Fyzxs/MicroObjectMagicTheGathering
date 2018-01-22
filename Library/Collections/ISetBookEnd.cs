using System;
using System.Threading.Tasks;

namespace Library.Collections
{
    public interface ISetBookEnd<T>
    {
        Task ForEach(Action<T> selector);
        Task Add(T t);
        Task Remove(T t);
    }
}
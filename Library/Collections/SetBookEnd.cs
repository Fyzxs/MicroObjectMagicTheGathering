using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Collections
{
    public class SetBookEnd<T> : ISetBookEnd<T>
    {
        private readonly ISet<T> _set;
        public SetBookEnd() : this(new HashSet<T>()) { }

        public SetBookEnd(ISet<T> set) => _set = set;

        public Task ForEach(Action<T> selector) => Task.Run(() => { foreach (T t in _set) selector(t); });

        public Task Add(T t) => Task.Run(() => _set.Add(t));

        public Task Remove(T t) => Task.Run(() => _set.Remove(t));
    }
}
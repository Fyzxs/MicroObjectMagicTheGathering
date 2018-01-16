using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Collections
{
    public class SetBookEnd<T> : ISetBookEnd<T>
    {
        private readonly ISet<T> _set;
        public SetBookEnd() : this(new HashSet<T>()) { }

        public SetBookEnd(ISet<T> set) => _set = set;

        public Task<IEnumerable<TResult>> Select<TResult>(Func<T, TResult> selector) => Task.Run(() => _set.Select(selector));

        public Task Add(T t) => Task.Run(() => _set.Add(t));

        public Task Remove(T t) => Task.Run(() => _set.Remove(t));
    }
}
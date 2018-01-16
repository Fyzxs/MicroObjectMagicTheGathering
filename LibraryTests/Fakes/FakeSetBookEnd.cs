using Library.Collections;
using LibraryTests.Fakes.Builders;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryTests.Fakes
{
    public class FakeSetBookEnd<T> : ISetBookEnd<T>
    {
        public class Builder
        {
            private readonly BuilderItemAction<T> _addItem = new BuilderItemAction<T>("FakeSetBookEnd#Add");
            private readonly BuilderItemFunc<IEnumerable<T>> _selectItem = new BuilderItemFunc<IEnumerable<T>>("FakeSetBookEnd#Select");
            private readonly BuilderItemAction<T> _removeItem = new BuilderItemAction<T>("FakeSetBookEnd#Remove");



            public Builder Remove() => Remove(() => { });
            public Builder Remove(params Action[] actions)
            {
                _removeItem.UpdateInvocation(actions);
                return this;
            }

            public Builder Select(IEnumerable<T> expected) => Select(() => expected);
            public Builder Select(params Func<IEnumerable<T>>[] funcs)
            {
                _selectItem.UpdateInvocation(funcs);
                return this;
            }

            public Builder Add() => Add(() => { });
            public Builder Add(params Action[] actions)
            {
                _addItem.UpdateInvocation(actions);
                return this;
            }
            public FakeSetBookEnd<T> Build()
            {
                return new FakeSetBookEnd<T>
                {
                    _select = _selectItem,
                    _add = _addItem,
                    _remove = _removeItem
                };
            }
        }

        private BuilderItemAction<T> _add;
        private BuilderItemFunc<IEnumerable<T>> _select;
        private BuilderItemAction<T> _remove;
        private FakeSetBookEnd() { }

        public Task<IEnumerable<TResult>> Select<TResult>(Func<T, TResult> selector) => Task.FromResult(_select.Invoke(selector) as IEnumerable<TResult>);

        public Task Add(T t) => Task.Run(() => _add.Invoke(t));

        public Task Remove(T t) => Task.Run(() => _remove.Invoke(t));

        public void AssertAddInvoked() => _add.AssertInvoked();

        public void AssertRemoveInvoked() => _remove.AssertInvoked();

        public void AssertSelectInvoked() => _select.AssertInvoked();

        public void AssertRemoveInvokedWith(T expected) => _remove.AssertInvokedWith(expected);
    }
}
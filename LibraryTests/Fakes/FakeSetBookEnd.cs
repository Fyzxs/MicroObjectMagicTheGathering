using Library.Collections;
using LibraryTests.Fakes.Builders;
using System;
using System.Threading.Tasks;

namespace LibraryTests.Fakes
{
    public class FakeSetBookEnd<T> : ISetBookEnd<T>
    {
        public class Builder
        {
            private readonly BuilderItemAction<T> _addItem = new BuilderItemAction<T>("FakeSetBookEnd#Add");
            private readonly BuilderItemAction<Action<T>> _forEachItem = new BuilderItemAction<Action<T>>("FakeSetBookEnd#ForEach");
            private readonly BuilderItemAction<T> _removeItem = new BuilderItemAction<T>("FakeSetBookEnd#Remove");



            public Builder Remove() => Remove(() => { });
            public Builder Remove(params Action[] actions)
            {
                _removeItem.UpdateInvocation(actions);
                return this;
            }

            public Builder ForEach() => ForEach(() => { });
            public Builder ForEach(params Action[] action)
            {
                _forEachItem.UpdateInvocation(action);
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
                    _forEach = _forEachItem,
                    _add = _addItem,
                    _remove = _removeItem
                };
            }
        }

        private BuilderItemAction<T> _add;
        private BuilderItemAction<Action<T>> _forEach;
        private BuilderItemAction<T> _remove;
        private FakeSetBookEnd() { }


        public Task ForEach(Action<T> selector) => Task.Run(() => _forEach.Invoke(selector));

        public Task Add(T t) => Task.Run(() => _add.Invoke(t));

        public Task Remove(T t) => Task.Run(() => _remove.Invoke(t));

        public void AssertAddInvoked() => _add.AssertInvoked();

        public void AssertRemoveInvoked() => _remove.AssertInvoked();

        public void AssertForEachInvoked() => _forEach.AssertInvoked();

        public void AssertRemoveInvokedWith(T expected) => _remove.AssertInvokedWith(expected);
    }
}
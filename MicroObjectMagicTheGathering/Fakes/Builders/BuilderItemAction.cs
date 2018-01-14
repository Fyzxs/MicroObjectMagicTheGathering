using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroObjectMagicTheGathering.Fakes.Builders
{
    public class BuilderItemAction<T> : BuilderItem<T>
    {
        private Action[] _actions;
        private readonly List<T> _values = new List<T>();
        private int _actionIndex;
        private int _valueIndex;

        public BuilderItemAction(string name) : base(name)
        {
            _actions = new Action[] { () => throw new TestException(name) };
        }

        public void UpdateInvocation() => UpdateInvocation(() => { });

        public void UpdateInvocation(params Action[] action)
        {
            Verifable = true;
            _actions = action;
        }

        private void ExecuteAction()
        {
            int length = _actions.Length;
            if (length == 1)
            {
                _actions[0]();
                return;
            }
            if (_actionIndex >= length)
            {
                _actions[length - 1]();
                return;
            }

            _actions[_actionIndex++]();
        }

        public void Invoke(T value)
        {
            _values.Add(value);
            InvokedCount++;
            ExecuteAction();
        }

        private T GetValueInOrderOfExecution()
        {
            if (!_values.Any()) AssertInvoked();
            return _valueIndex >= _values.Count
                ? _values[_valueIndex - 1]
                : _values[_valueIndex++];
        }

        public override void Assert(Action<T> assertion) => assertion(GetValueInOrderOfExecution());

        public void AssertInvokedWith(T expected) => GetValueInOrderOfExecution().Should().Be(expected);
    }
}
using System;

namespace LibraryTests.Fakes.Builders
{
    public class BuilderItemFunc<T> : BuilderItem<T>
    {
        private Func<T>[] _funcs;
        private int _funcIndex;
        private object _value;

        public BuilderItemFunc(string name) : base(name) => _funcs = new Func<T>[] { () => throw new TestException(name) };

        public void UpdateInvocation(params T[] valuesToReturn) => UpdateInvocation(FuncWrapper(valuesToReturn));

        private static Func<T>[] FuncWrapper(T[] valuesToReturn)
        {
            int length = valuesToReturn.Length;
            Func<T>[] funcs = new Func<T>[length];
            for (int idx = 0; idx < length; idx++)
            {
                int scopedIdx = idx;
                funcs[idx] = () => valuesToReturn[scopedIdx];
            }
            return funcs;
        }

        private T ExecuteFunc()
        {
            int length = _funcs.Length;
            if (length == 1) return _funcs[0]();
            if (_funcIndex >= length) return _funcs[length - 1]();

            return _funcs[_funcIndex++]();
        }

        public void UpdateInvocation(params Func<T>[] funcs)
        {
            Verifable = true;
            _funcs = funcs;
        }

        public T Invoke(object value = null)
        {
            _value = value;
            InvokedCount++;
            return ExecuteFunc();
        }

        public override void Assert(Action<T> assertion) => assertion(ExecuteFunc());

        public void AssertValue(Action<object> assertion) => assertion(_value);
    }
}
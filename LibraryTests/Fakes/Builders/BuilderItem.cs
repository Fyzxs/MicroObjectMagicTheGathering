using System;
using FluentAssertions;

namespace LibraryTests.Fakes.Builders
{
    public abstract class BuilderItem<T>
    {
        private readonly string _name;
        protected int InvokedCount;
        protected bool Verifable;

        protected BuilderItem(string name) => _name = name;
        public void AssertInvoked() => Invoked().Should().BeTrue($"{_name} was expected but not invoked.");
        public void Verify() => ( Verifable ^ Invoked() ).Should().BeFalse($"{_name} was configured but not invoked.");
        public abstract void Assert(Action<T> assertion);
        private bool Invoked() => InvokedCount > 0;
        public void InvokedCountMatches(int count) => InvokedCount.Should().Be(count, $"{_name} [InvokedCount={InvokedCount}] does not match expected [count={count}].");
    }
}
using Library.Bytes;
using Library.Eventing;
using LibraryTests.Fakes.Builders;

namespace LibraryTests.Fakes
{
    public class FakeEventMessage : IEventMessage
    {

        public class Builder
        {
            private readonly BuilderItemFunc<IBytes> _bytesItem = new BuilderItemFunc<IBytes>("FakeEventMessage#Bytes");

            public Builder Bytes(IBytes expected)
            {
                _bytesItem.UpdateInvocation(expected);
                return this;
            }
            public FakeEventMessage Build()
            {
                return new FakeEventMessage { _bytes = _bytesItem };
            }
        }
        private BuilderItemFunc<IBytes> _bytes;
        private FakeEventMessage() { }
        public IBytes Bytes() => _bytes.Invoke();

        byte[] IBytes.Bytes()
        {
            throw new System.NotImplementedException();
        }
    }
}
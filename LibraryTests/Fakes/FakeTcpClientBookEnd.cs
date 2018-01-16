using Library.Bytes;
using Library.Eventing.Tcp;
using Library.Networking;
using LibraryTests.Fakes.Builders;

namespace LibraryTests.Fakes
{
    public class FakeTcpClientBookEnd : ITcpClientBookEnd
    {
        public class Builder
        {
            private readonly BuilderItemFunc<IBytesWriter> _getStreamItem = new BuilderItemFunc<IBytesWriter>("FakeTcpClientBookEnd#GetStream");

            public Builder GetStream(IClientStreamBookEnd expected)
            {
                _getStreamItem.UpdateInvocation(expected);
                return this;
            }
            public FakeTcpClientBookEnd Build()
            {
                return new FakeTcpClientBookEnd { _getStream = _getStreamItem };
            }
        }
        private BuilderItemFunc<IBytesWriter> _getStream;
        private FakeTcpClientBookEnd() { }
        public IBytesWriter Writer() => _getStream.Invoke();
        public IBytesReader Reader()
        {
            throw new System.NotImplementedException();
        }
    }
}
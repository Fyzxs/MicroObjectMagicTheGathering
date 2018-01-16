using System.Threading.Tasks;
using Library.Bytes;
using Library.Networking;
using LibraryTests.Fakes.Builders;

namespace LibraryTests.Fakes {
    public class FakeClientStreamBookEnd : IClientStreamBookEnd
    {

        public class Builder
        {
            private readonly BuilderItemAction<IBytes> _bytesInfoItem = new BuilderItemAction<IBytes>("FakeClientStreamBookEnd#BytesInfo");

            public Builder BytesInfo()
            {
                _bytesInfoItem.UpdateInvocation();
                return this;
            }

            public FakeClientStreamBookEnd Build()
            {
                return new FakeClientStreamBookEnd { _bytesInfo = _bytesInfoItem };
            }
        }

        private BuilderItemAction<IBytes> _bytesInfo;
        private FakeClientStreamBookEnd() { }
        public Task Write(IBytes bytes) => Task.Run(() => _bytesInfo.Invoke(bytes));

        public void AssertBytesIntoInvokedWith(IBytes expected) => _bytesInfo.AssertInvokedWith(expected);
    }
}
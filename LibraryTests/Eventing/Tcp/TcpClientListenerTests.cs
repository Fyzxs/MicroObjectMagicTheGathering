using Library.Bytes;
using Library.Eventing.Tcp;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LibraryTests.Eventing.Tcp
{
    [TestClass]
    public class TcpClientListenerTests
    {
        [TestMethod, TestCategory("unit")]
        public async Task ShouldSendBytesIntoStream()
        {
            //Arrange
            FakeClientStreamBookEnd fakeClientStreamBookEnd = new FakeClientStreamBookEnd.Builder().BytesInfo().Build();
            FakeTcpClientBookEnd fakeTcpClientBookEnd = new FakeTcpClientBookEnd.Builder().GetStream(fakeClientStreamBookEnd).Build();
            IBytes expected = new BytesOf(new byte[0]);
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Bytes(expected).Build();
            FakeEventBus fakeEventBus = new FakeEventBus.Builder().Build();
            TcpClientListener subject = new TcpClientListener(fakeTcpClientBookEnd, fakeEventBus);

            //Act
            await subject.Update(fakeEventMessage);

            //Assert
            fakeClientStreamBookEnd.AssertBytesIntoInvokedWith(expected);
        }
    }
}

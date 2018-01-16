using Library.Eventing;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibraryTests.Eventing
{
    [TestClass]
    public class EventBusTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldSendEventMessageToListener()
        {
            //Arrange
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            FakeSetBookEnd<IListener> fakeSetBookEnd = new FakeSetBookEnd<IListener>.Builder().Select(new List<IListener> { fakeListener }).Build();
            IEventBus subject = new EventBus(fakeSetBookEnd);
            subject.Attach(fakeListener);

            //Act
            subject.Notify(fakeEventMessage);

            //Assert
            fakeSetBookEnd.AssertSelectInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldNotExplodeAddingMultipleOfSame()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeListener fakeListener = new FakeListener.Builder().Build();
            subject.Attach(fakeListener);

            //Act
            subject.Attach(fakeListener);

            //Assert
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldDetach()
        {
            //Arrange
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeSetBookEnd<IListener> fakeSetBookEnd = new FakeSetBookEnd<IListener>.Builder().Build();
            IEventBus subject = new EventBus(fakeSetBookEnd);

            //Act
            subject.Detach(fakeListener);

            //Assert
            fakeSetBookEnd.AssertRemoveInvokedWith(fakeListener);
        }
    }

}

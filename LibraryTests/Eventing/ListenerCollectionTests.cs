using Library.Eventing;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests.Eventing
{
    [TestClass]
    public class ListenerCollectionTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldAdd()
        {
            //Arrange
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Notify().Build();

            //Act
            subject.Add(fakeListener);

            //Assert
            subject.NotifyAll(fakeEventMessage);
            fakeListener.AssertNotifyInvokedWith(fakeEventMessage);
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldAddMultiple()
        {
            //Arrange
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Notify().Build();
            FakeListener fakeListener2 = new FakeListener.Builder().Notify().Build();

            //Act
            subject.Add(fakeListener);
            subject.Add(fakeListener2);

            //Assert
            subject.NotifyAll(fakeEventMessage);
            fakeListener.AssertNotifyInvokedWith(fakeEventMessage);
            fakeListener2.AssertNotifyInvokedWith(fakeEventMessage);
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldRemove()
        {
            //Arrange
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Notify().Build();
            subject.Add(fakeListener);

            //Act
            subject.Remove(fakeListener);

            //Assert
            subject.NotifyAll(fakeEventMessage);
            fakeListener.AssertNotifyInvokedCountMatches(0);
        }
    }
}
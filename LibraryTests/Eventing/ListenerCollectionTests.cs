using FluentAssertions;
using Library.Eventing;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LibraryTests.Eventing
{
    [TestClass]
    public class ListenerCollectionTests
    {
        [TestMethod, TestCategory("unit")]
        public async Task ShouldAdd()
        {
            //Arrange
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();

            //Act
            subject.Add(fakeListener);

            //Assert
            ( await subject.NotifyAll(null) ).Length.Should().Be(1);
        }

        [TestMethod, TestCategory("unit")]
        public async Task ShouldAddMultiple()
        {
            //Arrange
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeListener fakeListener2 = new FakeListener.Builder().Update().Build();

            //Act
            subject.Add(fakeListener);
            subject.Add(fakeListener2);

            //Assert
            ( await subject.NotifyAll(null) ).Length.Should().Be(2);
        }
        [TestMethod, TestCategory("unit")]
        public async Task ShouldNotifyAsTask()
        {
            //Arrange
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            subject.Add(fakeListener);

            //Act
            Task[] actual = await subject.NotifyAll(fakeEventMessage);

            //Assert
            await actual[0];
            fakeListener.AssertUpdateInvokedWith(fakeEventMessage);
        }

        [TestMethod, TestCategory("unit")]
        public async Task ShouldRemove()
        {
            //Arrange
            ListenerCollection subject = new ListenerCollection();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            subject.Add(fakeListener);

            //Act
            subject.Remove(fakeListener);

            //Assert
            ( await subject.NotifyAll(null) ).Length.Should().Be(0);
        }
    }
}
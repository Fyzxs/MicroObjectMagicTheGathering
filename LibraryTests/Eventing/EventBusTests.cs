using Library.Eventing;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests.Eventing
{
    [TestClass]
    public class EventBusTests
    {
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
    }

}

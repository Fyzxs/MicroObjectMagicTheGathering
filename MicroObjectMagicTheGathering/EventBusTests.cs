using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MicroObjectMagicTheGathering
{
    [TestClass]
    public class EventBusTests
    {
        [TestMethod]
        public void Exists()
        {
            new EventBus();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldImplementInterface()
        {
            //Arrange
            IEventBus eventBus = new EventBus();

            //Act

            //Assert
        }
    }

    public interface IEventBus { }

    public class EventBus : IEventBus { }
}

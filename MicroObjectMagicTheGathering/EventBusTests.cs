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
    }

    public class EventBus { }
}

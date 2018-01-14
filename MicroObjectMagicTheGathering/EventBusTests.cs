using FluentAssertions;
using MicroObjectMagicTheGathering.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public void ShouldRegisterIListener()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeListener fakeListener = new FakeListener.Builder().Build();

            //Act
            subject.Attach(fakeListener);

            //Assert
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldSendEventMessageToListener()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            subject.Attach(fakeListener);

            //Act
            subject.Notify(fakeEventMessage);

            //Assert
            fakeListener.AssertUpdateInvokedWith(fakeEventMessage);
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldSendEventMessageToMultipleListeners()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();
            FakeListener fakeListener2 = new FakeListener.Builder().Update().Build();
            subject.Attach(fakeListener);
            subject.Attach(fakeListener2);

            //Act
            subject.Notify(fakeEventMessage);

            //Assert
            fakeListener.AssertUpdateInvokedWith(fakeEventMessage);
            fakeListener2.AssertUpdateInvokedWith(fakeEventMessage);
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
        public void ShouldAddOnlyOnce()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            FakeListener fakeListener = new FakeListener.Builder().Update().Build();

            subject.Attach(fakeListener);
            subject.Attach(fakeListener);

            //Act
            subject.Notify(fakeEventMessage);

            //Assert
            fakeListener.AssertUpdateInvokedCountMatches(1);
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldSendEventMessageToMultipleListenersAsync()
        {
            //Arrange
            IEventBus subject = new EventBus();
            FakeEventMessage fakeEventMessage = new FakeEventMessage.Builder().Build();
            FakeListener fakeListener = new FakeListener.Builder().Update(() => Thread.Sleep(10)).Build();
            FakeListener fakeListener2 = new FakeListener.Builder().Update(() => Thread.Sleep(3)).Build();
            subject.Attach(fakeListener);
            subject.Attach(fakeListener2);

            //Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            subject.Notify(fakeEventMessage);
            stopwatch.Stop();

            //Assert
            stopwatch.ElapsedMilliseconds.Should().BeInRange(10, 11);
            fakeListener.AssertUpdateInvokedWith(fakeEventMessage);
            fakeListener2.AssertUpdateInvokedWith(fakeEventMessage);
        }



    }

    public interface IListener
    {
        Task Update(IEventMessage eventMessage);
    }

    public interface IEventBus
    {
        void Attach(IListener listener);
        //Detach
        void Notify(IEventMessage eventMessage);
    }

    public class EventBus : IEventBus
    {
        private readonly ISet<IListener> _listeners = new HashSet<IListener>();

        public void Attach(IListener listener) => _listeners.Add(listener);

        public void Notify(IEventMessage eventMessage) => Task.WaitAll(_listeners.Select(listener => listener.Update(eventMessage)).ToArray());
    }

    public interface IListenerCollection
    {
        void Add(IListener listener);
        Task[] NotifyAll(IEventMessage eventMessage);
    }

    public class ListenerCollection : IListenerCollection
    {
        public void Add(IListener listener)
        {

        }

        public Task[] NotifyAll(IEventMessage eventMessage)
        {
            throw new System.NotImplementedException();
        }
    }


}

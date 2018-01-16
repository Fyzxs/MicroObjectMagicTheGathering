using FluentAssertions;
using Library.Threading;
using LibraryTests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryTests.Threading
{
    [TestClass]
    public class BlockingSetBookEndTests
    {
        [TestMethod, TestCategory("unit")]
        public async Task ShouldBlockForAdd()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Add().Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            await subject.Add(null);

            //Assert
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertAddInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldReleaseOnThrowFromAdd()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Add(() => throw new Exception()).Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            Func<Task> func = async () => await subject.Add(null);

            //Assert
            func.ShouldThrow<Exception>();
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertAddInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public async Task ShouldBlockForRemove()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Remove().Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            await subject.Remove(null);

            //Assert
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertRemoveInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldReleaseOnThrowFromRemove()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Remove(() => throw new Exception()).Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            Func<Task> func = async () => await subject.Remove(null);

            //Assert
            func.ShouldThrow<Exception>();
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertRemoveInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public async Task ShouldBlockForSelect()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Select(new List<object>()).Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            await subject.Select(e => new object());

            //Assert
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertSelectInvoked();
        }

        [TestMethod, TestCategory("unit")]
        public void ShouldReleaseOnThrowFromSelect()
        {
            //Arrange
            FakeSemaphoreSlimBookEnd fakeSemaphoreSlimBookEnd = new FakeSemaphoreSlimBookEnd.Builder().Wait().Release().Build();
            FakeSetBookEnd<object> fakeSetBookEnd = new FakeSetBookEnd<object>.Builder().Select(() => throw new Exception()).Build();
            BlockingSetBookEnd<object> subject = new BlockingSetBookEnd<object>(fakeSetBookEnd, fakeSemaphoreSlimBookEnd);

            //Act
            Func<Task> func = async () => await subject.Select<object>(null);

            //Assert
            func.ShouldThrow<Exception>();
            fakeSemaphoreSlimBookEnd.AssertWaitInvoked();
            fakeSemaphoreSlimBookEnd.AssertReleaseInvoked();
            fakeSetBookEnd.AssertSelectInvoked();
        }
    }
}
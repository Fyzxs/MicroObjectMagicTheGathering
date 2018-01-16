using FluentAssertions;
using Library.Bytes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryTests.Bytes
{
    [TestClass]
    public class BytesOfTests
    {
        [TestMethod, TestCategory("unit")]
        public void ShouldReturnPassedInBytes()
        {
            //Arrange
            byte[] expected = new byte[0];
            IBytes bytes = new BytesOf(expected);

            //Act
            byte[] actual = bytes.Bytes();

            //Assert
            actual.Should().BeSameAs(expected);
        }
    }
}
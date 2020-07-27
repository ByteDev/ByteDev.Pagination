using ByteDev.Pagination.Data;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Data
{
    [TestFixture]
    public class DataPagingInfoTests
    {
        [TestFixture]
        public class Constructor : DataPagingInfoTests
        {
            [Test]
            public void WhenPageNumberLessThanZero_ThenSetPageNumberToDefaultZero()
            {
                var classUnderTest = new DataPagingInfo(-1, 10);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(0));
            }

            [Test]
            public void WhenPageSizeLessThanOne_ThenSetPageSizeToDefaultTen()
            {
                var classUnderTest = new DataPagingInfo(0, 0);

                Assert.That(classUnderTest.PageSize, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class Skip : DataPagingInfoTests
        {
            [Test]
            public void WhenCalled_ThenReturnsTheNumberOfItemsToSkip()
            {
                var classUnderTest = new DataPagingInfo(2, 50);

                Assert.That(classUnderTest.Skip, Is.EqualTo(100));
            }
        }
    }
}
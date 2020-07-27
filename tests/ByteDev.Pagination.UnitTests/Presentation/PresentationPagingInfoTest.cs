using System;
using ByteDev.Pagination.Presentation;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Presentation
{
    [TestFixture]
    public class PresentationPagingInfoTest
    {
        [TestFixture]
        public class Constructor : PresentationPagingInfoTest
        {
            [Test]
            public void WhenTotalItemsLessThanZero_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PresentationPagingInfo(-1, 10, 0));
            }

            [Test]
            public void WhenPageSizeLessThanOne_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PresentationPagingInfo(100, 0, 0));
            }

            [Test]
            public void WhenPageNumberIsLessThanZero_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PresentationPagingInfo(100, 10, -1));
            }

            [Test]
            public void WhenMaxPageNumbersToShowIsLessThanOne_ThenThrowException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _ = new PresentationPagingInfo(100, 10, 0, 0));
            }

            [Test]
            public void WhenPageNumberGreaterThanTotalPageCount_ThenSetPageNumberToLastPage()
            {
                var classUnderTest = new PresentationPagingInfo(100, 10, 10);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(classUnderTest.LastPageNumber));
            }

            [Test]
            public void WhenPageNumberLessThanOrEqualToTotalPageCount_ThenSetPageNumber()
            {
                var classUnderTest = new PresentationPagingInfo(100, 10, 9);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(classUnderTest.LastPageNumber));
            }
        }

        [TestFixture]
        public class TotalPageCount
        {
            [TestCase(0, 1, 0)]
            [TestCase(1, 1, 1)]
            [TestCase(2, 1, 2)]
            [TestCase(10, 10, 1)]
            [TestCase(11, 10, 2)]
            [TestCase(21, 10, 3)]
            [TestCase(2147483647, 10, 214748365)]
            public void WhenTotalItemsCountAndPageSizeAreValid_ThenReturnTotalPageCount(int totalItemsCount, int pageSize, int expected)
            {
                var classUnderTest = new PresentationPagingInfo(totalItemsCount, pageSize, 0);

                var result = classUnderTest.TotalPageCount;

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class LastPageNumer
        {
            [Test]
            public void WhenTotalPageCountIsOne_ThenReturnZero()
            {
                var classUnderTest = new PresentationPagingInfo(1, 10, 0);

                Assert.That(classUnderTest.LastPageNumber, Is.EqualTo(0));
            }

            [Test]
            public void WhenTotalPageCountIsTwo_ThenReturnOne()
            {
                var classUnderTest = new PresentationPagingInfo(20, 10, 0);

                Assert.That(classUnderTest.LastPageNumber, Is.EqualTo(1));
            }
        }
    }
}
using ByteDev.Pagination.Presentation;
using ByteDev.Pagination.Presentation.PageOffSet;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Presentation.PageOffSet
{
    [TestFixture]
    public class MiddlePageOffSetStrategyTests
    {
        private MiddlePageOffSetStrategy _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _classUnderTest = new MiddlePageOffSetStrategy();
        }

        private int Act(PresentationPagingInfo pagingInfo)
        {
            return _classUnderTest.GetPageOffSet(pagingInfo);
        }

        [TestFixture]
        public class GetPageOffSet : MiddlePageOffSetStrategyTests
        {
            [Test]
            public void WhenTotalItems51_AndPageSize10_AndPageNumber1_AndMaxPagesToShow3_ThenReturnOffsetZero()
            {
                var pagingInfo = new PresentationPagingInfo(51, 10, 1, 3);

                var result = Act(pagingInfo);

                Assert.That(result, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class GetPageOffSet_TenPages : MiddlePageOffSetStrategyTests
        {
            private const int TotalItems = 100;
            private const int PageSize = 10;

            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(2, 0)]
            [TestCase(3, 0)]
            [TestCase(4, 0)]
            [TestCase(5, 0)]
            [TestCase(6, 0)]
            [TestCase(7, 0)]
            [TestCase(8, 0)]
            [TestCase(9, 0)]
            [TestCase(10, 0)]
            public void WhenPageNumerIsGiven_ThenOffSetIsReturned(int pageNumber, int expected)
            {
                var pagingInfo = new PresentationPagingInfo(TotalItems, PageSize, pageNumber);

                var result = Act(pagingInfo);

                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [TestFixture]
        public class GetPageOffSet_TwentyPages : MiddlePageOffSetStrategyTests
        {
            private const int TotalItems = 200;
            private const int PageSize = 10;

            [TestCase(0, 0)]           
            [TestCase(1, 0)]           
            [TestCase(2, 0)]           
            [TestCase(3, 0)]           
            [TestCase(4, 0)]           
            [TestCase(5, 1)]           
            [TestCase(6, 2)]           
            [TestCase(7, 3)]           
            [TestCase(8, 4)]           
            [TestCase(9, 5)]           
            [TestCase(10, 6)]           
            [TestCase(11, 7)]           
            [TestCase(12, 8)]           
            [TestCase(13, 9)]           
            [TestCase(14, 10)]           
            [TestCase(15, 10)]           
            [TestCase(16, 10)]           
            [TestCase(17, 10)]           
            [TestCase(18, 10)]           
            [TestCase(19, 10)]           
            public void WhenPageNumerIsGiven_ThenOffSetIsReturned(int pageNumber, int expected)
            {
                var pagingInfo = new PresentationPagingInfo(TotalItems, PageSize, pageNumber);

                var result = Act(pagingInfo);

                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}
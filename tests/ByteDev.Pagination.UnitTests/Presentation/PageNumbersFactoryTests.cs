using System;
using ByteDev.Pagination.Presentation;
using ByteDev.Pagination.Presentation.PageOffSet;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Presentation
{
    [TestFixture]
    public class PageNumbersFactoryTests
    {
        private IPageOffSetStrategy _offSetZero;
        private IPageOffSetStrategy _offSetFive;

        [SetUp]
        public void SetUp()
        {
            _offSetZero = new PageOffSetStrategyFakeZero();
            _offSetFive = new PageOffSetStrategyFakeFive();
        }

        [TestFixture]
        public class Create : PageNumbersFactoryTests
        {
            [Test]
            public void WhenPagingInfoIsNull_ThenThrowException()
            {
                var classUnderTest = CreateClassUnderTest();

                Assert.Throws<ArgumentNullException>(() => classUnderTest.Create(null));
            }

            [TestCase(10, 3, 4)]                        // 10 / 3 = 3.33
            [TestCase(10, 6, 2)]                        // 10 / 6 = 1.66
            [TestCase(10, 2, 5)]                        // 10 / 2 = 5
            [TestCase(110, 10, 11)]                     // 110 / 10 = 11
            public void WhenZeroOffSet_ThenCreateCorrectSizeOfList(int totalItemsCount, int pageSize, int expected)
            {
                var pagingInfo = new PresentationPagingInfo(totalItemsCount, pageSize, 0, 1000);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);	

                Assert.That(pageNumbers.Count, Is.EqualTo(expected));
            }

            [Test]
            public void WhenZeroOffSet_AndPageNumberCountIsGreaterThanMaxPageNumbersToShow_ThenCreateListOfSizeMaxPageNumbersToShow()
            {
                const int maxPageNumbersToShow = 11;

                var pagingInfo = new PresentationPagingInfo(110, 10, 0, maxPageNumbersToShow);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);	// 120 / 10 = 12

                Assert.That(pageNumbers.Count, Is.EqualTo(maxPageNumbersToShow));
            }

            [Test]
            public void WhenZeroOffSet_AndTotalItemCountIsZero_ThenCreateEmptyCollection()
            {
                var pagingInfo = new PresentationPagingInfo(0, 2, 0);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);

                Assert.That(pageNumbers.Count, Is.EqualTo(0));
            }

            [Test]
            public void WhenZeroOffSet_ThenSetIsCurrentPageTo1stPage()
            {
                var pagingInfo = new PresentationPagingInfo(10, 3, 0);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);

                Assert.That(pageNumbers[0].IsCurrentPage, Is.True);
                Assert.That(pageNumbers[1].IsCurrentPage, Is.False);
                Assert.That(pageNumbers[2].IsCurrentPage, Is.False);
                Assert.That(pageNumbers[3].IsCurrentPage, Is.False);
            }

            [Test]
            public void WhenZeroOffSet_ThenSetIsCurrentPageTo4thPage()
            {
                var pagingInfo = new PresentationPagingInfo(10, 3, 3);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);

                Assert.That(pageNumbers[0].IsCurrentPage, Is.False);
                Assert.That(pageNumbers[1].IsCurrentPage, Is.False);
                Assert.That(pageNumbers[2].IsCurrentPage, Is.False);
                Assert.That(pageNumbers[3].IsCurrentPage, Is.True);
            }

            [Test]
            public void WhenZeroOffSet_ThenSetDisplayTextCorrectly()
            {
                var pagingInfo = new PresentationPagingInfo(10, 2, 0);
                var classUnderTest = CreateClassUnderTest();

                var pageNumbers = classUnderTest.Create(pagingInfo);

                Assert.That(pageNumbers[0].DisplayNumber, Is.EqualTo("1"));
                Assert.That(pageNumbers[1].DisplayNumber, Is.EqualTo("2"));
                Assert.That(pageNumbers[2].DisplayNumber, Is.EqualTo("3"));
                Assert.That(pageNumbers[3].DisplayNumber, Is.EqualTo("4"));
                Assert.That(pageNumbers[4].DisplayNumber, Is.EqualTo("5"));
            }

            private PageNumbersFactory CreateClassUnderTest(IPageOffSetStrategy pageOffSet = null)
            {
                if (pageOffSet == null)
                {
                    pageOffSet = _offSetZero;
                }
                return new PageNumbersFactory(pageOffSet);
            }
        }
    }

    public class PageOffSetStrategyFakeZero : IPageOffSetStrategy
    {
        public int GetPageOffSet(PresentationPagingInfo pagingInfo)
        {
            return 0;
        }
    }

    public class PageOffSetStrategyFakeFive : IPageOffSetStrategy
    {
        public int GetPageOffSet(PresentationPagingInfo pagingInfo)
        {
            return 5;
        }
    }
}
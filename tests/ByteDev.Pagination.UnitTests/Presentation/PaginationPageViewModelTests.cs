using ByteDev.Pagination.Presentation;
using NUnit.Framework;

namespace ByteDev.Pagination.UnitTests.Presentation
{
    [TestFixture]
    public class PaginationPageViewModelTests
    {
        [TestFixture]
        public class Constructor
        {
            [Test]
            public void WhenTotalItemsIsValid_ThenSetTotalItems()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.TotalItems, Is.EqualTo(30));
            }

            [Test]
            public void WhenTotalItemsIsLessThanZero_ThenSetTotalItemsToZero()
            {
                var classUnderTest = new PaginationPageViewModel(-1, 0, 10);

                Assert.That(classUnderTest.TotalItems, Is.EqualTo(0));
            }

            [Test]
            public void WhenPageNumberIsValid_ThenSetPageNumber()
            {
                var classUnderTest = new PaginationPageViewModel(10, 0, 10);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(0));
            }

            [Test]
            public void WhenPageNumberIsLessThanZero_ThenSetPageNumberToZero()
            {
                var classUnderTest = new PaginationPageViewModel(30, -1, 10);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(0));
            }

            [Test]
            public void WhenPageNumberIsGreaterThanLastPageNumber_ThenSetPageNumberToZero()
            {
                var classUnderTest = new PaginationPageViewModel(30, 3, 10);

                Assert.That(classUnderTest.PageNumber, Is.EqualTo(0));
            }
            
            [Test]
            public void WhenPageSizeIsValid_ThenSetPageSize()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.PageSize, Is.EqualTo(10));
            }
        }

        [TestFixture]
        public class PageNavigationNumbers : PaginationPageViewModelTests
        {
            [Test]
            public void WhenSetToNull_ThenReturnsEmpty()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                classUnderTest.PageNavigationNumbers = null;

                Assert.That(classUnderTest.PageNavigationNumbers.Count, Is.EqualTo(0));
            }
        }

        [TestFixture]
        public class HasMoreThanOnePage : PaginationPageViewModelTests
        {
            [Test]
            public void WhenHasZeroPages_ThenReturnFalse()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.HasMoreThanOnePage, Is.False);
            }

            [Test]
            public void WhenHasOnePage_ThenReturnFalse()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                classUnderTest.PageNavigationNumbers.Add(new PageNumber());

                Assert.That(classUnderTest.HasMoreThanOnePage, Is.False);
            }

            [Test]
            public void WhenHasTwoPages_ThenReturnTrue()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                classUnderTest.PageNavigationNumbers.Add(new PageNumber());
                classUnderTest.PageNavigationNumbers.Add(new PageNumber());

                Assert.That(classUnderTest.HasMoreThanOnePage, Is.True);
            }
        }

        [TestFixture]
        public class NextPageNumber : PaginationPageViewModelTests
        {
            [Test]
            public void WhenOnFirstPage_AndThereIsNextPage_ThenReturnsNextPageNumber()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.NextPageNumber, Is.EqualTo(1));
            }

            [Test]
            public void WhenOnLastPage_ThenReturnsLastPageNumber()
            {
                var classUnderTest = new PaginationPageViewModel(30, 2, 10);

                Assert.That(classUnderTest.NextPageNumber, Is.EqualTo(2));
            }
        }

        [TestFixture]
        public class IsFirstPage : PaginationPageViewModelTests
        {
            [Test]
            public void WhenPageNumberIsFirstPage_ThenIsFirstPageTrue()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.IsFirstPage, Is.True);
            }

            [Test]
            public void WhenPageNumberIsNotFirstPage_ThenIsFirstPageFalse()
            {
                var classUnderTest = new PaginationPageViewModel(30, 1, 10);

                Assert.That(classUnderTest.IsFirstPage, Is.False);
            }
        }

        [TestFixture]
        public class IsLastPage : PaginationPageViewModelTests
        {
            [Test]
            public void WhenIsLastPage_ThenIsLastPageTrue()
            {
                var classUnderTest = new PaginationPageViewModel(30, 2, 10);

                Assert.That(classUnderTest.IsLastPage, Is.True);			
            }

            [Test]
            public void WhenIsNotLastPage_ThenIsLastPageFalse()
            {
                var classUnderTest = new PaginationPageViewModel(30, 1, 10);

                Assert.That(classUnderTest.IsLastPage, Is.False);
            }
        }

        [TestFixture]
        public class PreviousPageNumber : PaginationPageViewModelTests
        {
            [Test]
            public void WhenOnFirstPage_ThenPreviousPageIsFirstPage()
            {
                var classUnderTest = new PaginationPageViewModel(30, 0, 10);

                Assert.That(classUnderTest.PreviousPageNumber, Is.EqualTo(classUnderTest.FirstPageNumber));
            }

            [Test]
            public void WhenOnSecondPage_ThenPreviousPageIsFirstPage()
            {
                var classUnderTest = new PaginationPageViewModel(30, 1, 10);

                Assert.That(classUnderTest.PreviousPageNumber, Is.EqualTo(classUnderTest.FirstPageNumber));
            }
        }

        [TestFixture]
        public class LastPageNumber : PaginationPageViewModelTests
        {
            [TestCase(0, 0)]
            [TestCase(1, 0)]
            [TestCase(10, 0)]
            [TestCase(20, 1)]
            [TestCase(30, 2)]
            [TestCase(31, 3)]
            public void WhenPageSize10_ShouldReturnLastPageNumber(int totalItems, int expected)
            {
                var classUnderTest = new PaginationPageViewModel(totalItems, 0, 10);

                Assert.That(classUnderTest.LastPageNumber, Is.EqualTo(expected));
            }            
        }
    }
}
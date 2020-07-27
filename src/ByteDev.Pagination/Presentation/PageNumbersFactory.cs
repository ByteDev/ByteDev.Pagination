using System;
using System.Collections.Generic;
using ByteDev.Pagination.Presentation.PageOffSet;

namespace ByteDev.Pagination.Presentation
{
    /// <summary>
    /// Represents a factory for collections of PageNumbers.
    /// </summary>
    public class PageNumbersFactory
    {
        private readonly IPageOffSetStrategy _pageOffSetStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Presentation.PageNumbersFactory" /> class.
        /// </summary>
        /// <param name="pageOffSetStrategy">The page offset strategy to use.</param>
        public PageNumbersFactory(IPageOffSetStrategy pageOffSetStrategy)
        {
            _pageOffSetStrategy = pageOffSetStrategy;
        }

        /// <summary>
        /// Create a collection of PageNumbers.
        /// </summary>
        /// <param name="pagingInfo">Presentation paging information.</param>
        /// <returns>PageNumbers collection.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="pagingInfo" /> is null.</exception>
        public IList<PageNumber> Create(PresentationPagingInfo pagingInfo)
        {
            if (pagingInfo == null)
                throw new ArgumentNullException(nameof(pagingInfo));

            var pageOffset = _pageOffSetStrategy.GetPageOffSet(pagingInfo);

            return CreatePageNumbers(pagingInfo, pageOffset);
        }

        private static List<PageNumber> CreatePageNumbers(PresentationPagingInfo pagingInfo, int pageOffset)
        {
            var numberOfPagesToCreate = GetNumberOfPagesToCreate(pagingInfo);
            var pageNumbers = new List<PageNumber>();

            for (var i = 0; i < numberOfPagesToCreate; i++)
            {
                var newPageNumber = i + pageOffset;

                if (newPageNumber > pagingInfo.LastPageNumber)          // make sure we dont go out of bounds
                {
                    break;
                }

                pageNumbers.Add(new PageNumber
                {
                    Number = newPageNumber,
                    IsCurrentPage = newPageNumber == pagingInfo.PageNumber
                });
            }
            return pageNumbers;
        }

        private static int GetNumberOfPagesToCreate(PresentationPagingInfo pagingInfo)
        {
            if (pagingInfo.TotalPageCount > pagingInfo.MaxPageNumbersToShow)
            {
                return pagingInfo.MaxPageNumbersToShow;
            }

            return pagingInfo.TotalPageCount;
        }
    }
}
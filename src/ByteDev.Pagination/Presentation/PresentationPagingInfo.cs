using System;

namespace ByteDev.Pagination.Presentation
{
    /// <summary>
    /// Represents presentation related paging information.
    /// </summary>
    public class PresentationPagingInfo
    {
        private const int DefaultMaxPageNumbersToShow = 10;

        /// <summary>
        /// First page number.
        /// </summary>
        public readonly int FirstPageNumber = 0;

        /// <summary>
        /// Max number of page numbers to show in the navigation.
        /// </summary>
        public int MaxPageNumbersToShow { get; }

        /// <summary>
        /// Total items count.
        /// </summary>
        public int TotalItemsCount { get; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Current page number.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Presentation.PresentationPagingInfo" /> class.
        /// </summary>
        /// <param name="totalItemsCount">Total items count.</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="maxPageNumbersToShow">Max page numbers to show.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalItemsCount" /> was less than zero.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="pageSize" /> was less than one.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="pageNumber" /> was less than the first page number.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxPageNumbersToShow" /> was less than one.</exception>
        public PresentationPagingInfo(int totalItemsCount, int pageSize, int pageNumber, int maxPageNumbersToShow = DefaultMaxPageNumbersToShow)
        {
            if (totalItemsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalItemsCount), @"Total items was less than zero.");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), @"Page size was less than one.");

            if (pageNumber < FirstPageNumber)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), $"Current page number was less than the first page number: {FirstPageNumber}.");

            if (maxPageNumbersToShow < 1)
                throw new ArgumentOutOfRangeException(nameof(maxPageNumbersToShow), @"Max page numbers to show was less than one.");

            MaxPageNumbersToShow = maxPageNumbersToShow;
            TotalItemsCount = totalItemsCount;
            PageSize = pageSize;
            PageNumber = pageNumber > LastPageNumber ? LastPageNumber : pageNumber;
        }

        /// <summary>
        /// Total page count.
        /// </summary>
        public int TotalPageCount
        {
            get
            {
                int totalPageCount = TotalItemsCount / PageSize;

                if (TotalItemsCount % PageSize > 0) // round up if has decimal point
                    totalPageCount++;

                return totalPageCount;
            }
        }

        /// <summary>
        /// Last page number.
        /// </summary>
        public int LastPageNumber
        {
            get
            {
                if (TotalPageCount <= 1)
                    return FirstPageNumber;

                return TotalPageCount - 1;
            }
        }

        public override string ToString()
        {
            return $"{nameof(MaxPageNumbersToShow)}: {MaxPageNumbersToShow}, " +
                   $"{nameof(PageNumber)}: {PageNumber}, " +
                   $"{nameof(PageSize)}: {PageSize}, " + 
                   $"{nameof(TotalItemsCount)}: {TotalItemsCount}, " +
                   $"{nameof(TotalPageCount)}: {TotalPageCount}";
        }
    }
}
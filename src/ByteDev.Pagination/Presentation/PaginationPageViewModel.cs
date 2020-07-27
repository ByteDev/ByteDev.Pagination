using System;
using System.Collections.Generic;

namespace ByteDev.Pagination.Presentation
{
    /// <summary>
    /// Represents a view model of pagination information.
    /// </summary>
    public class PaginationPageViewModel
    {
        private IList<PageNumber> _displayPageNumbers;

        /// <summary>
        /// The first page number.
        /// </summary>
        public readonly int FirstPageNumber = 0;

        /// <summary>
        /// Total items.
        /// </summary>
        public int TotalItems { get; private set; }

        /// <summary>
        /// Current page number.
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Collection of PageNumbers to use for navigation.
        /// </summary>
        public IList<PageNumber> PageNavigationNumbers
        {
            get => _displayPageNumbers ?? (_displayPageNumbers = new List<PageNumber>());
            set => _displayPageNumbers = value;
        }

        /// <summary>
        /// Indicates if there is more than one page.
        /// </summary>
        public bool HasMoreThanOnePage => PageNavigationNumbers.Count > 1;

        /// <summary>
        /// The last page number.
        /// </summary>
        public int LastPageNumber
        {
            get
            {
                if (TotalItems < 1)
                    return 0;

                var lastPage = Math.Round((double)(TotalItems / PageSize), 1, MidpointRounding.AwayFromZero);

                if (TotalItems % PageSize == 0)
                {
                    lastPage--;
                }
                return Convert.ToInt32(lastPage);
            }
        }

        /// <summary>
        /// Indicates if the current page is the first page.
        /// </summary>
        public bool IsFirstPage => PageNumber == FirstPageNumber;

        /// <summary>
        /// Indicates if the current page is the last page.
        /// </summary>
        public bool IsLastPage => PageNumber == LastPageNumber;

        /// <summary>
        /// Previous page number.
        /// </summary>
        public int PreviousPageNumber
        {
            get
            {
                if (IsFirstPage)
                    return FirstPageNumber;

                return PageNumber - 1;
            }
        }

        /// <summary>
        /// Next page number.
        /// </summary>
        public int NextPageNumber
        {
            get
            {
                if (IsLastPage)
                    return LastPageNumber;

                return PageNumber + 1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Presentation.PaginationPageViewModel" /> class.
        /// </summary>
        /// <param name="totalItems">Total items.</param>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageSize">Page size.</param>
        public PaginationPageViewModel(int totalItems, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            SetTotalItems(totalItems);
            PageNumber = pageNumber;

            CheckIfPageNumberOutOfBounds();
        }

        private void CheckIfPageNumberOutOfBounds()
        {
            if ((PageNumber < FirstPageNumber) || (PageNumber > LastPageNumber))
            {
                PageNumber = FirstPageNumber;
            }
        }

        private void SetTotalItems(int totalItems)
        {
            if (totalItems < 0)
            {
                totalItems = 0;
            }
            TotalItems = totalItems;
        }
    }
}
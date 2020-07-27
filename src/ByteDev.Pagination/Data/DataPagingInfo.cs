namespace ByteDev.Pagination.Data
{
    /// <summary>
    /// Represents paging information.
    /// </summary>
    public class DataPagingInfo
    {
        private const int DefaultPageNumber = 0;
        private const int DefaultPageSize = 10;

        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Page size.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Number of items to skip when doing LINQ Skip.
        /// </summary>
        public int Skip => PageNumber * PageSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Data.DataPagingInfo" /> class.
        /// </summary>
        public DataPagingInfo() : this(DefaultPageNumber, DefaultPageSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Data.DataPagingInfo" /> class.
        /// </summary>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        public DataPagingInfo(int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
                pageNumber = DefaultPageNumber;

            if (pageSize < 1)
                pageSize = DefaultPageSize;

            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public override string ToString()
        {
            return $"{nameof(PageNumber)}: {PageNumber}, " +
                   $"{nameof(PageSize)}: {PageSize}";
        }
    }
}
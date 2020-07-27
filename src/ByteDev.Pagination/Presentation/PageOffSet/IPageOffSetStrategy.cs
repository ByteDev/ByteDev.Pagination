namespace ByteDev.Pagination.Presentation.PageOffSet
{
    /// <summary>
    /// Represents an interface for a page offset strategy.
    /// </summary>
    public interface IPageOffSetStrategy
    {
        /// <summary>
        /// Retrieves a page offset.
        /// </summary>
        /// <param name="pagingInfo">Presentation paging information.</param>
        /// <returns>Page offset.</returns>
        int GetPageOffSet(PresentationPagingInfo pagingInfo);
    }
}
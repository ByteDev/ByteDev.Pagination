using System;

namespace ByteDev.Pagination.Presentation.PageOffSet
{
    /// <summary>
    /// Represents a strategy to get the page offset putting the current page
    /// roughly in the middle of the page set (off set slides once pageNumber is past mid point)
    /// </summary>
    public class MiddlePageOffSetStrategy : IPageOffSetStrategy
    {
        /// <summary>
        /// Gets the page offset for the specific paging information.
        /// </summary>
        /// <param name="pagingInfo">Presentation paging information.</param>
        /// <returns>Page offset.</returns>
        public int GetPageOffSet(PresentationPagingInfo pagingInfo)
        {
            double halfPage = Math.Round((double)(pagingInfo.MaxPageNumbersToShow / 2));

            if (pagingInfo.PageNumber < halfPage)
            {
                return 0;   // first half of page set, dont offset
            }

            int quarterPage = Convert.ToInt32(Math.Round(halfPage / 2, 0, MidpointRounding.AwayFromZero));

            var offSet = Convert.ToInt32(pagingInfo.PageNumber - (quarterPage + 1));

            // Handle if we are at the end of the set (we still want to show the full maxPageNumbersToShow of options)
            while (offSet + pagingInfo.MaxPageNumbersToShow > pagingInfo.LastPageNumber + 1)
            {
                offSet--;
            }

            if (offSet < 0)
                return 0;   // offset should never be less than zero

            return offSet;
        }
    }
}
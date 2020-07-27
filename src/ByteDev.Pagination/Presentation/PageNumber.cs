namespace ByteDev.Pagination.Presentation
{
    /// <summary>
    /// Represents a page number.
    /// </summary>
    public class PageNumber
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Indicates if the page is the current page.
        /// </summary>
        public bool IsCurrentPage { get; set; }

        /// <summary>
        /// Display page number.
        /// </summary>
        public string DisplayNumber => (Number + 1).ToString();

        public override string ToString()
        {
            return $"{nameof(Number)}: {Number}, " +
                   $"{nameof(DisplayNumber)}: {DisplayNumber}, " +
                   $"{nameof(IsCurrentPage)}: {IsCurrentPage}";
        }
    }
}
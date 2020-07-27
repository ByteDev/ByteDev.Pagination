using System;
using System.Collections.Generic;

namespace ByteDev.Pagination.Data
{
    /// <summary>
    /// Container for a set of entity models from a DB and paging related information.
    /// </summary>
    public class EntitiesPagingContainer<T> where T : class
    {
        /// <summary>
        /// Entities.
        /// </summary>
        public IList<T> Entities { get; }

        /// <summary>
        /// Total items count.
        /// </summary>
        public int TotalItemsCount { get; }

        /// <summary>
        /// Data paging information.
        /// </summary>
        public DataPagingInfo DataPagingInfo { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ByteDev.Pagination.Data.EntitiesPagingContainer`1" /> class.
        /// </summary>
        /// <param name="entities">Entities.</param>
        /// <param name="totalItemsCount">Total items count.</param>
        /// <param name="dataPagingInfo">Data paging information.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="totalItemsCount" /> was less than zero.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="entities" /> is null.</exception>
        public EntitiesPagingContainer(IList<T> entities, int totalItemsCount = 0, DataPagingInfo dataPagingInfo = null)
        {
            if (totalItemsCount < 0)
                throw new ArgumentOutOfRangeException(nameof(totalItemsCount), "Total items count was less than zero.");

            Entities = entities ?? throw new ArgumentNullException(nameof(entities));

            TotalItemsCount = totalItemsCount;
            DataPagingInfo = dataPagingInfo;
        }

        public override string ToString()
        {
            return $"{nameof(Entities)}: {Entities.Count}, " +
                   $"{nameof(TotalItemsCount)}: {TotalItemsCount}";
        }
    }
}
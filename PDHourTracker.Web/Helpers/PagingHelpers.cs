using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Helpers
{
    /// <summary>
    /// Contains helper methods to ensure valid page numbers and page sizes.
    /// </summary>
    public static class PagingHelpers
    {
        private const int _MAX_PAGE_SIZE = 50;

        /// <summary>
        /// Returns a positive number. If given number is less than zero,
        /// it returns 1.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static int ValidatePageNumber(int pageNumber)
        {
            return pageNumber > 0 ? pageNumber : 1;
        }

        /// <summary>
        /// Returns given page size if it is greater than 0 and less than 
        /// or equal to max page size; otherwise it returns max page size.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static int ValidatePageSize(int pageSize)
        {
            return pageSize > 0 && pageSize <= _MAX_PAGE_SIZE ? pageSize : _MAX_PAGE_SIZE;
        }

        /// <summary>
        /// Validates max page size up to 250. If given page size is less than or equal
        /// to 0 or greater than 250, it will default to 250.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="maxPageSize"></param>
        /// <returns></returns>
        public static int ValidatePageSize(int pageSize, int maxPageSize)
        {
            // Validates max page size up to 250
            maxPageSize = maxPageSize <= 0 || maxPageSize > 250 ? 250 : maxPageSize;

            return pageSize > 0 && pageSize <= maxPageSize ? pageSize : maxPageSize;
        }
    }
}

using PDHourTracker.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models
{
    public class PagingModel
    {
        public PagingModel(int pageNum, int pageSize, int totalItems,
            string controller, string action)
        {
            PageNum = PagingHelpers.ValidatePageNumber(pageNum);
            PageSize = PagingHelpers.ValidatePageSize(pageSize);
            TotalItems = totalItems;
            Controller = controller;
            Action = action;
        }

        public PagingModel()
        {
            PageNum = 1;
            PageSize = 50;
        }

        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        /// <summary>
        /// The controller to use in the page link.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// The action to use in the page link.
        /// </summary>
        public string Action { get; set; }

        // Search value for searching
        public string SearchValue { get; set; }

        /// <summary>
        /// Returns the last page number which is NumPages.
        /// </summary>
        public int LastPageNum
        {
            get { return NumPages; }
        }

        /// <summary>
        /// Returns 1 for consistency and convenience.
        /// </summary>
        public int FirstPageNum
        {
            get { return 1; }
        }

        /// <summary>
        /// Calculates the total number of pages
        /// </summary>
        public int NumPages
        {
            get
            {
                var numPages = TotalItems / PageSize;
                if ((TotalItems % PageSize) > 0)
                    numPages++;

                return numPages;
            }
        }

        /// <summary>
        /// Calculates the previous page number.
        /// Returns 1 if current page number is 1.
        /// </summary>
        public int PrevPageNum
        {
            get
            {
                return PageNum == 1 ? 1 : PageNum - 1;
            }
        }

        /// <summary>
        /// Calculates the next page number.
        /// Returns last page number if current page is last.
        /// </summary>
        public int NextPageNum
        {
            get
            {
                return PageNum == NumPages ? NumPages : PageNum + 1;
            }
        }

        /// <summary>
        /// The first item for the current page count.
        /// </summary>
        public int StartItemCount
        {
            get
            {
                return (PageNum - 1) * PageSize + 1;
            }
        }

        public int EndItemCount
        {
            get
            {
                var endItemCount = PageNum * PageSize;
                if (endItemCount > TotalItems)
                    endItemCount = TotalItems;

                return endItemCount;
            }
        }
    }
}

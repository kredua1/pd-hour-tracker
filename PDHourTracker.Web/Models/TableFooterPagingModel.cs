using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models
{
    public class TableFooterPagingModel : PagingModel
    {
        public TableFooterPagingModel(int pageNum, int pageSize, int totalItems,
            string controller, string action)
            : base(pageNum, pageSize, totalItems, controller, action) { }

        public TableFooterPagingModel() : base() { }

        /// <summary>
        /// Number of columns to span for page links
        /// </summary>
        public int PageLinkColSpan { get; set; }

        /// <summary>
        /// Number of columns to span for item counts
        /// </summary>
        public int ItemCountColspan { get; set; }
    }
}

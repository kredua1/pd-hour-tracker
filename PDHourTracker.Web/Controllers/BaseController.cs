using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    public class BaseController : Controller
    {
        protected int PageNumber { get; set; }
        protected int PageSize { get; set; }
        protected string SearchValue { get; set; }

        /// <summary>
        /// Validates page number and size and stores them in ViewData 
        /// VieData["PageNumber"] and ViewData["PageSize"] as well as
        /// local properties: PageNumber and PageSize
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        protected void StorePaging(int pageNumber, int pageSize)
        {
            // Validate given page number and size
            PageNumber = PagingHelpers.ValidatePageNumber(pageNumber);
            PageSize = PagingHelpers.ValidatePageSize(pageSize);

            // Store for use in views
            ViewData["PageNumber"] = PageNumber;
            ViewData["PageSize"] = PageSize;
            
        }

        /// <summary>
        /// Stores and validates page number and size and search value in controller
        /// and ViewData;
        /// /// local properties: PageNumber, PageSize and SearchValue
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        protected void StorePaging(int pageNumber, int pageSize, string searchValue)
        {
            StorePaging(pageNumber, pageSize);
            SearchValue = searchValue;
            ViewData["SearchValue"] = searchValue;
        }
    }
}

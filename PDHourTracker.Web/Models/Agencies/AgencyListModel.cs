using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Agencies
{
    public class AgencyListModel
    {
        public TableFooterPagingModel Pager { get; set; }

        public ICollection<AgencyViewModel> Agencies { get; set; }
            = new List<AgencyViewModel>();
    }
}

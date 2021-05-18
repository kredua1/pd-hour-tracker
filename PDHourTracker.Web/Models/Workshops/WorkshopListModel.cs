using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Workshops
{
    public class WorkshopListModel
    {
        public ICollection<WorkshopViewModel> Workshops { get; set; }
            = new List<WorkshopViewModel>();

        public TableFooterPagingModel Pager { get; set; }
    }
}

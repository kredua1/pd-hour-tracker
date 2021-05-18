using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Attendees
{
    public class AttendeeListModel
    {
        public ICollection<AttendeeWithAgencyModel> Attendees { get; set; }
            = new List<AttendeeWithAgencyModel>();

        public TableFooterPagingModel Pager { get; set; }

        
    }
}

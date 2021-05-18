using PDHourTracker.Web.Models.Attendees;
using PDHourTracker.Web.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.AttendeeHours
{
    public class AttendeeHourEditModel : AttendeeHourBaseModel
    {
        public WorkshopViewModel Workshop { get; set; }

        public ICollection<AttendeeViewModel> Attendees
            = new List<AttendeeViewModel>();

        public ICollection<WorkshopViewModel> Workshops
            = new List<WorkshopViewModel>();
    }
}

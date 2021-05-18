using PDHourTracker.Web.Models.AttendeeHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Attendees
{
    public class AttendeeDetailsModel : AttendeeViewModel
    {
        public string AgencyName { get; set; }

        public ICollection<AttendeeWorkshopHourModel> WorkshopHours { get; set; }
             = new List<AttendeeWorkshopHourModel>();
    }
}

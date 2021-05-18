using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class Attendee : BaseEntity
    {
        public Attendee()
        {
            AttendeeHours = new HashSet<AttendeeHour>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string JobTitle { get; set; }
        public int AgencyId { get; set; }
        public string CertId { get; set; }

        public virtual Agency Agency { get; set; }
        public virtual ICollection<AttendeeHour> AttendeeHours { get; set; }
    }
}

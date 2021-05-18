using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class Agency : BaseEntity
    {
        public Agency()
        {
            Attendees = new HashSet<Attendee>();
        }

        public string AgencyName { get; set; }

        public virtual ICollection<Attendee> Attendees { get; set; }
    }
}

using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class Workshop : BaseEntity
    {
        public Workshop()
        {
            AttendeeHours = new List<AttendeeHour>();
        }

        public string WorkshopName { get; set; }
        public string Description { get; set; }
        public DateTime TrainingDate { get; set; }
        public string SessionIdentifier { get; set; }
        public decimal PDHours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public int ProviderCodeId { get; set; }
        public string Comments { get; set; }

        public virtual Project Project { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ProviderCode ProviderCode { get; set; }

        public virtual ICollection<AttendeeHour> AttendeeHours { get; set; }
    }
}

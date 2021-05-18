using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Dtos
{
    public class WorkshopDto : BaseDto
    {
        public string WorkshopName { get; set; }
        public string Description { get; set; }
        public DateTime TrainingDate { get; set; }
        public string SessionIdentifier { get; set; }
        public decimal PDHours { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string Comments { get; set; }
    }
}

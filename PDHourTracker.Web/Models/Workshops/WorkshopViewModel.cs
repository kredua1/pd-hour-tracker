using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Workshops
{
    public class WorkshopViewModel
    {
        public WorkshopViewModel()
        {
            WorkshopAttendeeHours = new List<WorkshopAttendeeHourModel>();
        }

        public int Id { get; set; }
        public string WorkshopName { get; set; }
        public string Description { get; set; }
        public DateTime TrainingDate { get; set; }
        public string SessionIdentifier { get; set; }
        public decimal PDHours { get; set; }
        public string Comments { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }
        public int ProviderCodeId { get; set; }

        // Show the session identifier combined with provider code
        public string SessionIdentifierDisplay
        {
            get
            {
                if (ProviderCode != null)
                    return $"{SessionIdentifier}-{ProviderCode.Code}";
                else
                    return SessionIdentifier;
            }
        }
        

        public Projects.ProjectViewModel Project { get; set; }
        public Employees.EmployeeViewModel Employee { get; set; }
        public ProviderCodes.ProviderCodeViewModel ProviderCode { get; set; }
        public List<int> SignOutSheets { get; set; } = new List<int>();

        public ICollection<WorkshopAttendeeHourModel> WorkshopAttendeeHours { get; set; }
    }
}

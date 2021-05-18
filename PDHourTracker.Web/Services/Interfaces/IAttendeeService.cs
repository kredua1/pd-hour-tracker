using PDHourTracker.Web.Models.Attendees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IAttendeeService
    {
        void Add(AttendeeEditModel attendeeEditModel, string username);
        void Update(AttendeeEditModel attendeeEditModel, string username);
        AttendeeViewModel Get(int id);
        AttendeeEditModel GetForEdit(int id);
        List<AttendeeViewModel> GetAttendees(int pageNum, int pageSize);
        bool Exists(string firstName, string lastName, string middleName);
        List<AttendeeViewModel> Search(string searchValue);
        List<AttendeeViewModel> ByAgency(int agencyId);

        int Total();

        // Reports
        List<AttendeeWithAgencyModel> GetAttendeeWithAgencies(int pageNum, int pageSize, string searchValue);
        AttendeeDetailsModel GetAttendeeWithWorkshopHours(int id);
    }
}

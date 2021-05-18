using PDHourTracker.Web.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IWorkshopService
    {
        WorkshopEditModel AddOrUpdate(WorkshopEditModel workshopEditModel);
        WorkshopViewModel Get(int id);
        WorkshopViewModel GetDetails(int id);
        List<WorkshopViewModel> GetWorkshops(int pageNumber, int pageSize, string searchValue);
        List<WorkshopAttendeeHourModel> GetWorkshopAttendeeHours(int workshopId);
        List<WorkshopViewModel> ByEmployee(int employeeId);
        List<WorkshopViewModel> ByProject(int projectId);
        bool SessionIdentifierIsUnique(string sessionIdentifier);
        bool SessionIdentifierIsUnique(string sessionIdentifier, int workshopId);
        WorkshopEditModel GetForEdit(int id);
        int Total();
        List<WorkshopViewModel> Search(string searchValue, int pageNumber, int pageSize);
    }
}

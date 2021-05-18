using PDHourTracker.Web.Models.Agencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IAgencyService
    {
        void AddOrUpdate(AgencyViewModel agencyViewModel);
        AgencyViewModel Get(int id);
        List<AgencyViewModel> GetAgencies(int pageNum, int pageSize);
        bool Exists(string agencyName);
        List<AgencyViewModel> Search(string searchValue);
        int Total();
    }
}

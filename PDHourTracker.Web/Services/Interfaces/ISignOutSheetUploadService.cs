using PDHourTracker.Web.Models.SignOutSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface ISignOutSheetUploadService
    {
        int Add(SignOutSheetUploadModel model);
        void Delete(int id);
        SignOutSheetUploadModel Get(int id);
        List<int> GetIdsByWorkshop(int workshopId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IExcelService
    {
        byte[] WorkshopAttendeeHours(int workshopId);
        byte[] AttendeeWorkshopHours(int attendeeId);
    }
}

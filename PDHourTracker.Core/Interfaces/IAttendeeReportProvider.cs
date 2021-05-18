using PDHourTracker.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IAttendeeReportProvider
    {
        List<AttendeeWithAgency> AttendeeWithAgencies(int pageNum, int pageSize, string searchValue);
    }
}

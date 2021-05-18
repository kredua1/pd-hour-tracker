using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Health
{
    public class HealthStatusData
    {
        public bool IsReady { get; set; } = true;
        public bool IsLiveness { get; set; } = true;
    }
}

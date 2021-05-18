using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class ProviderCode : BaseEntity
    {
        public ProviderCode()
        {
            Workshops = new HashSet<Workshop>();
        }

        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }
    }
}

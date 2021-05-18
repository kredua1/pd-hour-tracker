using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {
            Workshops = new HashSet<Workshop>();
        }

        public string ProjectName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }
    }
}

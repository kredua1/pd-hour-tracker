using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.SharedKernel
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string ModUser { get; set; }
        public DateTime? ModDate { get; set; }
        public string RecUser { get; set; }
        public DateTime? RecDate { get; set; }
    }
}

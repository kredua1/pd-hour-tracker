using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class SignOutSheetUpload : BaseEntity
    {
        public int WorkshopId { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }
    }
}

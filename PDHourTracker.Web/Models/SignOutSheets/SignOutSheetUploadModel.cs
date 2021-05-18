using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.SignOutSheets
{
    public class SignOutSheetUploadModel
    {
        public int WorkshopId { get; set; }
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string ContentType { get; set; }
        public byte[] FileData { get; set; }

        [Required(ErrorMessage = "File required")]
        public IFormFile UploadFile { get; set; }
    }
}

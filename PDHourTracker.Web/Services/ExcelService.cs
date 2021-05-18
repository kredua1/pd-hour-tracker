using OfficeOpenXml;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class ExcelService : IExcelService
    {
        private IWorkshopService _workshopService;
        private IAttendeeService _attendeeService;
        private IAttendeeHourService _attendeeHourService;

        public ExcelService(IWorkshopService workshopService,
            IAttendeeService attendeeService,
            IAttendeeHourService attendeeHourService)
        {
            _workshopService = workshopService;
            _attendeeService = attendeeService;
            _attendeeHourService = attendeeHourService;
        }

        /// <summary>
        /// Creates and Excel spreadsheet with the name of the atteneee
        /// and a list of all workshops he/she has attended with hours.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        public byte[] AttendeeWorkshopHours(int attendeeId)
        {
            // Get attendee and hours from db
            var attendee = _attendeeService.Get(attendeeId);
            var workshopHours = _attendeeHourService.GetAttendeeWorkshopHours(attendeeId);

            using (var ms = new MemoryStream())
            {
                using (var package = new ExcelPackage(ms))
                {
                    // Create a new worksheet
                    var worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    // Attendee name and ID centered across columns A, B and C
                    worksheet.Cells["A1"].Value = attendee.FullName;
                    worksheet.Cells["A2"].Value = attendee.CertId;
                    worksheet.Cells["A1:C1"].Style.HorizontalAlignment =
                        OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A1:C1"].Merge = true;
                    worksheet.Cells["A1:C1"].Style.Font.Size = 18;
                    worksheet.Cells["A2:C2"].Style.HorizontalAlignment =
                        OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A2:C2"].Merge = true;

                    var row = 4;
                    // Headings on row 4
                    worksheet.Cells[row, 1].Value = "Workshop";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Value = "Date";
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    worksheet.Cells[row, 3].Value = "PD Hours";
                    worksheet.Cells[row, 3].Style.Font.Bold = true;

                    // Set column widths
                    worksheet.Column(1).Width = 55;
                    worksheet.Column(2).Width = 12;
                    worksheet.Column(3).Width = 10;

                    

                    // Loop through the workshops 
                    row++;
                    var col = 1;
                    foreach(var workshop in workshopHours)
                    {
                        // Reset col to 1
                        col = 1;
                        worksheet.Cells[row, col].Value = workshop.WorkshopName;

                        col++;
                        worksheet.Cells[row, col].Value = workshop.TrainingDate.ToShortDateString();

                        col++;
                        worksheet.Cells[row, col].Value = workshop.PDHours;

                        // Update row
                        row++;
                    }

                    // Set number format of PD hours column to two decimals
                    worksheet.Column(3).Style.Numberformat.Format = "0.00";

                    // Sum the hours
                    worksheet.Cells[row, col].Formula = $"SUM(C4:C{row-1})";
                    worksheet.Cells[row, col].Style.Border.Bottom.Style =
                        OfficeOpenXml.Style.ExcelBorderStyle.Double;
                    worksheet.Cells[row, col].Style.Border.Top.Style =
                        OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                    worksheet.Cells[row, 1].Value = "Total";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    // Return the Excel file data. All data is in memory
                    return package.GetAsByteArray();
                }
            }
        }

        /// <summary>
        /// Creates an Excel spreadsheet with the name of the workshop
        /// and all attendees with PD hours listed.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public byte[] WorkshopAttendeeHours(int workshopId)
        {
            // Get workshop data and attendee hours from db
            var workshop = _workshopService.GetDetails(workshopId);
            var workshopAttendeeHours = _workshopService.GetWorkshopAttendeeHours(workshopId);

            using (var ms = new MemoryStream())
            {
                using (var package = new ExcelPackage(ms))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                    // Print workshop name, date and session id at top
                    
                    // Merge cells A-D on first row so workshop name is centered across data
                    var workshopNameRange = "A1:E1";
                    var workshopDateRange = "A2:E2";
                    var workshopIdRange = "A3:E3";
                    worksheet.Cells[workshopNameRange].Merge = true;
                    worksheet.Cells[workshopNameRange].Style.HorizontalAlignment 
                        = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[workshopNameRange].Style.Font.Bold = true;
                    worksheet.Cells[workshopNameRange].Style.Font.Size = 18;
                    worksheet.Cells[workshopNameRange].Value = workshop.WorkshopName;

                    // Workshop Date
                    worksheet.Cells[workshopDateRange].Merge = true;
                    worksheet.Cells[workshopDateRange].Style.HorizontalAlignment
                        = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[workshopDateRange].Style.Font.Bold = true;
                    worksheet.Cells[workshopDateRange].Style.Font.Size = 16;
                    worksheet.Cells[workshopDateRange].Value = workshop.TrainingDate.ToShortDateString();

                    // Workshop Session Id
                    worksheet.Cells[workshopIdRange].Merge = true;
                    worksheet.Cells[workshopIdRange].Style.HorizontalAlignment
                        = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[workshopIdRange].Style.Font.Italic = true;
                    worksheet.Cells[workshopIdRange].Value
                        = $"{workshop.SessionIdentifier}-{workshop.ProviderCode.Code}";

                    // Column Headings
                    worksheet.Cells["A4"].Value = "Name";
                    worksheet.Cells["A4"].Style.Font.Bold = true;
                    worksheet.Cells["B4"].Value = "ID";
                    worksheet.Cells["B4"].Style.Font.Bold = true;
                    worksheet.Cells["C4"].Value = "Hours";
                    worksheet.Cells["C4"].Style.Font.Bold = true;
                    worksheet.Cells["D4"].Value = "Title";
                    worksheet.Cells["D4"].Style.Font.Bold = true;
                    worksheet.Cells["E4"].Value = "Agency";
                    worksheet.Cells["E4"].Style.Font.Bold = true;

                    // Print attendee name, ID, PD hours, title and agency
                    // in columns A, B, C, D and E
                    var row = 5;
                    var col = 1;
                    foreach (var attendee in workshopAttendeeHours)
                    {
                        // Reset column back to 1 (A)
                        col = 1;

                        // Attendee name
                        worksheet.Cells[row, col].Value = attendee.FullName;

                        // ID
                        col++;
                        worksheet.Cells[row, col].Value = attendee.CertId;

                        // PD Hours
                        col++;
                        worksheet.Cells[row, col].Value = attendee.PDHours;

                        // Job title
                        col++;
                        worksheet.Cells[row, col].Value = attendee.JobTitle;

                        // Agency
                        col++;
                        worksheet.Cells[row, col].Value = attendee.AgencyName;

                        // Update counter which is row number
                        row++;
                    }

                    // Style PD Hours column for two decimal places
                    worksheet.Column(3).Style.Numberformat.Format = "0.00";
                    worksheet.Column(3).Style.HorizontalAlignment
                        = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    // Set column widths
                    // name
                    worksheet.Column(1).Width = 20;
                    // id
                    worksheet.Column(2).Width = 10;
                    // pd hours
                    worksheet.Column(3).Width = 7;
                    // job title
                    worksheet.Column(4).Width = 20;
                    // agency
                    worksheet.Column(5).Width = 30;

                    return package.GetAsByteArray();
                }
            }
        }

        #region Helpers
        private string GetRowCol(int row, string col)
        {
            return $"{col}{row.ToString()}";
        }
        #endregion
    }
}

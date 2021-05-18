using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Helpers;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Models.Attendees;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AttendeesController : BaseController
    {
        private IAgencyService _agencyService;
        private IExcelService _excelService;
        private IAttendeeService _attendeeService;

        public AttendeesController(IAttendeeService attendeeService,
            IAgencyService agencyService,
            IExcelService excelService)
        {
            _attendeeService = attendeeService;
            _agencyService = agencyService;
            _excelService = excelService;
        }

        // Default action : Get
        // Lists attendees with agency name
        public ActionResult Index(int p = 1, int ps = 50, string sv = "")
        {
            StorePaging(p, ps, sv);

            var model = new AttendeeListModel();
            model.Pager = new Models.TableFooterPagingModel(
                PageNumber, PageSize,
                _attendeeService.Total(),
                "Attendees", "Index");

            // Enter the number of columns that page links and counts should span
            model.Pager.PageLinkColSpan = 4;
            model.Pager.ItemCountColspan = 1;

            // Save search value to model
            model.Pager.SearchValue = sv;

            // Get attendees from DB
            model.Attendees = _attendeeService.GetAttendeeWithAgencies(
                model.Pager.PageNum, model.Pager.PageSize, sv);

            return View(model);
        }

        // Attendee details including workshop hours : Get
        public ActionResult Details(int id = 0, int p = 1, int ps = 50, string sv = "")
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // Store paging info
            StorePaging(p, ps, sv);

            var model = _attendeeService.GetAttendeeWithWorkshopHours(id);

            // Ensure attendee was found by checking id > 0
            if(model.Id <= 0)
                return RedirectToAction(nameof(Index));

            return View(model);

        }

        // Attendees by Agency : Get
        // id is Agency Id
        public ActionResult ByAgency(int id = 0, int p = 1, int ps = 50)
        {
            StorePaging(p, ps);

            // Get the agency and then the attendees
            var model = _agencyService.Get(id);
            model.Attendees = _attendeeService.ByAgency(id);

            return View(model);
        }

        // Add Attenddee : Get
        public ActionResult Add(int p = 1, int ps = 50)
        {
            StorePaging(p, ps);

            var model = new AttendeeEditModel();
            model.Agencies = _agencyService.GetAgencies(1, 50);

            return View(model);
        }

        // Add Attendee : Post
        [HttpPost]
        public ActionResult Add(AttendeeEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Agencies = _agencyService.GetAgencies(1, 50);
                return View(model);
            }

            // Ensure attendee doesn't already exist
            if(_attendeeService.Exists(model.FirstName, model.LastName, model.MiddleName))
            {
                ModelState.AddModelError("", "An attendee with that name already exists.");
                model.Agencies = _agencyService.GetAgencies(1, 50);
                return View(model);
            }

            // Add attendee to db
            _attendeeService.Add(model, User.Identity.Name);

            return RedirectToAction(nameof(Index));
        }

        // Update attendee : Get
        public ActionResult Update(int id = 0, int p = 1, int ps = 50, string sv = "",
            string ra = "Index")
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var model = _attendeeService.GetForEdit(id);

            StorePaging(p, ps, sv);
            model.PageNum = PageNumber;
            model.PageSize = PageSize;

            // Store the Return Action - ra
            ViewData["ReturnAction"] = ra == "Details" ? "Details" : "Index";

            // If the id is 0, return to list of attendees
            // This means attendee was not found
            if (model.Id == 0)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        // Update attendee : Post
        [HttpPost]
        public ActionResult Update(AttendeeEditModel model, string ra = "Index")
        {
            if (!ModelState.IsValid)
            {
                model.Agencies = _agencyService.GetAgencies(1, 100);
                return View(model);
            }

            // Save changes to db
            _attendeeService.Update(model, User.Identity.Name);

            if(ra == "Details")
                return RedirectToAction(nameof(Details), new { id = model.Id });

            return RedirectToAction(nameof(Index));
        }

        // Get attendee hours in Excel
        public ActionResult WorkshopHoursExcel(int id = 0)
        {
            if (id == 0)
                return null;

            return File(_excelService.AttendeeWorkshopHours(id), MimeTypes.Excel,
                "AttendeeHours.xlsx");
        }
    }
}

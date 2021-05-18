using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AttendeeHoursController : Controller
    {
        private IAttendeeHourService _attendeeHourService;
        private IWorkshopService _workshopService;
        private IAttendeeService _attendeeService;

        public AttendeeHoursController(IAttendeeHourService attendeeHourService,
            IWorkshopService workshopService,
            IAttendeeService attendeeService)
        {
            _attendeeHourService = attendeeHourService;
            _workshopService = workshopService;
            _attendeeService = attendeeService;
        }

        // Default action : Get
        // Redirects to workshops
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Workshops");
        }

        // Add AttendeeHour : Get
        // wid = workshop Id
        public ActionResult Add(int wid = 1)
        {
            var model = new AttendeeHourEditModel();
            model.WorkshopId = wid;
            model.Attendees = _attendeeService.GetAttendees(1, 100);

            // Get workshop we are adding attendee hours to
            model.Workshop = _workshopService.GetDetails(wid);

            return View(model);
        }

        // Add AttendeeHour : Post
        [HttpPost]
        public ActionResult Add(AttendeeHourEditModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Workshop = _workshopService.GetDetails(model.WorkshopId);
                model.Attendees = _attendeeService.GetAttendees(1, 100);
                return View(model);
            }

            // If entry exists for selected attendee and workshop, update it
            var attendeeHourViewModel = _attendeeHourService
                .GetByAttendeeAndWorkshop(model.AttendeeId, model.WorkshopId);
            // Assign Id to model. If greater than zero, it will update
            model.Id = attendeeHourViewModel.Id;

            // Save attendee hour to db
            _attendeeHourService.AddOrUpdate(model);

            return RedirectToAction("Details", "Workshops",
                new { id = model.WorkshopId });
        }

        #region Helpers
        void LoadAttendeeHourLists(AttendeeHourEditModel model)
        {
            model.Attendees = _attendeeService.GetAttendees(1, 100);
            //model.Workshops = _workshopService.GetWorkshops(1, 100);
        }
        #endregion
    }
}

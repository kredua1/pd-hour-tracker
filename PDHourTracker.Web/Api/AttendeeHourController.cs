using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// This controller handles adding/removing attendee hours.
    /// </summary>
    [ApiController]
    [Route("/api/attendeehours")]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AttendeeHourController : ControllerBase
    {
        private IAttendeeHourService _attendeeHourService;

        public AttendeeHourController(IAttendeeHourService attendeeHourService)
        {
            _attendeeHourService = attendeeHourService;
        }

        // Add AttendeeHour : Post
        [Route("/api/attendeehours/add")]
        [HttpPost]
        public IActionResult Add(AttendeeHourEditModel model)
        {
            // Ensure good input
            if (!ModelState.IsValid)
                return BadRequest("Please check input.");

            // Ensure an entry doesn't already exist for this attendee and workshop
            if (!_attendeeHourService.Exists(model.AttendeeId, model.WorkshopId))
            {
                var attendeeHourViewModel = _attendeeHourService.AddOrUpdate(model);
                return Ok(attendeeHourViewModel); // Need to return Id
            }
            else
            {
                return BadRequest("Attendee hours already added to workshop.");
            }
        }

        // Remove AttendeeHour : Delete
        [HttpDelete]
        [Route("/api/attendeehours/removehour")]
        public IActionResult RemoveHour(AttendeeHourRemoveModel model)
        {
            _attendeeHourService.Delete(model.Id);

            return Ok(model.Id);
        }
    }
}

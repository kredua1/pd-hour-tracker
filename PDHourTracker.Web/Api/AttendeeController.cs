using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.Attendees;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// This controller handles attendee autocomplete and add.
    /// </summary>
    [ApiController]
    [Route("api/attendees")]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AttendeeController : ControllerBase
    {
        private IAttendeeService _attendeeService;

        public AttendeeController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }

        // Search for attendees : Post
        // Returns the full name of the attendees found
        [Route("/api/attendees/autocomplete/{sv}")]
        [HttpPost]
        public List<AttendeeAutoComplete> Search(string sv)
        {
            var attendees = _attendeeService.Search(sv);

            var attendeeAutoCompletes = (from a in attendees
                                         select new AttendeeAutoComplete
                                         {
                                             value = a.Id,
                                             label = a.FullName,
                                             certId = a.CertId
                                         })
                                         .ToList();

            return attendeeAutoCompletes;
        }

        // Get the first 20 attendees
        // Mainly used with autocomplete for an initial
        // list of attendees prior to searching
        [HttpPost]
        [Route("/api/attendees/top20")]
        public List<AttendeeAutoComplete> Top20()
        {
            var attendees = _attendeeService.GetAttendees(1, 20);

            var attendeeAutoCompletes = (from a in attendees
                                         select new AttendeeAutoComplete
                                         {
                                             value = a.Id,
                                             label = a.FullName
                                         })
                                         .ToList();

            return attendeeAutoCompletes;
        }

        // Add an attendee : Post
        [HttpPost]
        [Route("/api/attendees/add")]
        public IActionResult Add(AttendeeEditModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please check input");

            // Add to db
            _attendeeService.Add(model, User.Identity.Name);

            // Ensure an entry for this attendee doesn't already exist
            //if (!_attendeeService.Exists(model.FirstName, model.LastName, model.MiddleName))
            //    _attendeeService.Add(model, User.Identity.Name);
            //else
            //    return BadRequest($"Attendee {model.FirstName} {model.MiddleName} {model.LastName} already exists.");

            return Ok();
        }
    }
}

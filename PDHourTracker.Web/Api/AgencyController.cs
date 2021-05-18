using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// This controller handles agency autocomplete and add.
    /// </summary>
    [ApiController]
    [Route("/api/agencies")]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AgencyController : ControllerBase
    {
        private IAgencyService _agencyService;

        public AgencyController(IAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        // Agencies autocomplete : Post
        // Get agencies filtered by name
        [HttpPost]
        [Route("/api/agencies/autocomplete/{sv}")]
        public List<AutoCompleteModel> Autocomplete(string sv)
        {
            var agencies = _agencyService.Search(sv);

            var autoCompletes = (from a in agencies
                                 select new AutoCompleteModel
                                 {
                                     label = a.AgencyName,
                                     value = a.Id
                                 })
                                 .ToList();

            return autoCompletes;
        }

        // Add Agency : Post
        [HttpPost]
        [Route("/api/agencies/add")]
        public IActionResult Add(AgencyViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_agencyService.Exists(model.AgencyName))
                _agencyService.AddOrUpdate(model);
            else
                return BadRequest($"Agency {model.AgencyName} already exists.");

            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.ProviderCodes;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// Controller gets provider codes and code dates.
    /// </summary>
    [Route("api/providercodes")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class ProviderCodeController : ControllerBase
    {
        private IProviderCodeService _providerCodeService;

        public ProviderCodeController(IProviderCodeService providerCodeService)
        {
            _providerCodeService = providerCodeService;
        }

        [HttpGet]
        [Route("/api/providercodes/{id}")]
        public ProviderCodeViewModel Get(int id)
        {
            return _providerCodeService.Get(id);
        }


        [HttpGet]
        [Route("/api/providercodes/dates/{id}")]
        public string Dates(int id)
        {
            var providerCodeDates = "";

            var providerCode = _providerCodeService.Get(id);

            if(providerCode != null)
            {
                providerCodeDates = $"{providerCode.StartDate.ToShortDateString()} - "
                    + $"{providerCode.EndDate.ToShortDateString()}";
            }

            return providerCodeDates;
        }
    }
}

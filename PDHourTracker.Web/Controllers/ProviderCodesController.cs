using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.ProviderCodes;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller lists/updates/adds provider codes.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class ProviderCodesController : Controller
    {
        IProviderCodeService _providerCodeService;

        public ProviderCodesController(IProviderCodeService providerCodeService)
        {
            _providerCodeService = providerCodeService;
        }

        // Default view. Lists all Provider Codes
        public ActionResult Index()
        {
            // Only one provider code per year so not many expected
            return View(_providerCodeService.GetProviderCodes(1, 50));
        }

        // Create provider code. Get
        public ActionResult Add()
        {
            var model = new ProviderCodeViewModel();
            model.StartDate = new DateTime(DateTime.Now.Year, 7, 1);
            model.EndDate = new DateTime(DateTime.Now.Year + 1, 6, 30);

            return View(model);
        }

        // Create provider code. Post
        [HttpPost]
        public ActionResult Add(ProviderCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Ensure code doesn't already exist
            if(_providerCodeService.Exists(model.Code))
            {
                ModelState.AddModelError("", "Provider code entered already exists."
                    + "Please enter a different code.");
                return View(model);
            }

            // Add new provider code to db
            _providerCodeService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        // Update provider code : Get
        public ActionResult Update(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // get provider code from db
            var model = _providerCodeService.Get(id);

            // check to ensure we got code by checking id
            if(model.Id <= 0)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        // Update provider code : Post
        [HttpPost]
        public ActionResult Update(ProviderCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Update provider code in db
            _providerCodeService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Helpers;
using PDHourTracker.Web.Models.Agencies;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller allows agencies to be viewed/added/updated.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class AgenciesController : BaseController
    {
        private IAgencyService _agencyService;

        public AgenciesController(IAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        // Default action
        public ActionResult Index(int p = 1, int ps = 50)
        {
            StorePaging(p, ps);

            var model = new AgencyListModel();
            model.Pager = new Models.TableFooterPagingModel(PageNumber,
                PageSize, _agencyService.Total(),
                "Agencies", "Index");
            model.Pager.PageLinkColSpan = 1;
            model.Pager.ItemCountColspan = 1;

            model.Agencies = _agencyService.GetAgencies(PageNumber, PageSize);

            return View(model);
        }

        // Add Agency : Get
        public ActionResult Add(int p = 1, int ps = 50)
        {
            StorePaging(p, ps);

            var model = new AgencyViewModel();
            model.PageNum = PageNumber;
            model.PageSize = PageSize;

            return View(model);
        }

        // Add Agency : Post
        [HttpPost]
        public ActionResult Add(AgencyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Add agency to db
            _agencyService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        // Update agency : Get
        public ActionResult Update(int id = 0, int p = 1, int ps = 50)
        {
            if (id == 0)
                return RedirectToAction(nameof(Index));

            var model = _agencyService.Get(id);

            StorePaging(p, ps);
            model.PageNum = PageNumber;
            model.PageSize = PageSize;

            return View(model);
        }

        // Update agency : Post
        [HttpPost]
        public ActionResult Update(AgencyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _agencyService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }
    }
}

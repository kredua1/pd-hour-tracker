using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Helpers;
using PDHourTracker.Web.Models.SignOutSheets;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller shows attendees for a workshop and allows 
    /// attendees to be added to a workshop.
    /// It also provides a way to upload a scanned sign-out sheet to the database.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class SignOutSheetController : BaseController
    {
        private ISignOutSheetUploadService _signOutSheetUploadService;
        private IWorkshopService _workshopService;

        public SignOutSheetController(IWorkshopService workshopService,
            ISignOutSheetUploadService signOutSheetUploadService)
        {
            _workshopService = workshopService;
            _signOutSheetUploadService = signOutSheetUploadService;
        }

        // Default action : Get
        // Shows workshop details and list of attendees with hours
        // with form to add more attendees and hours
        // id is workshop id
        public ActionResult Index(int id = 0, int p = 1, int ps = 50)
        {
            StorePaging(p, ps);

            // Id is for workshop and must be > 0
            if (id <= 0)
                return RedirectToAction("Index", "Workshops", new { p = PageNumber, ps = PageSize });

            // Get workshop with project, employee and provider code
            var model = _workshopService.GetDetails(id);

            // Get attendee hours for workshop
            model.WorkshopAttendeeHours = _workshopService.GetWorkshopAttendeeHours(id);

            return View(model);
        }

        // Upload scanned SignOut Sheet : Get
        // id is Workshop Id
        public ActionResult Upload(int id = 0)
        {
            // Ensure we have an id > 0
            if (id == 0)
                return RedirectToAction("Index", "Workshops");

            var model = new SignOutSheetUploadModel
            {
                WorkshopId = id
            };

            return View(model);
        }

        // Upload scanned SignOut Sheet : Post
        [HttpPost]
        public ActionResult Upload(SignOutSheetUploadModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Only allow PDF uploads
            if (model.UploadFile.ContentType != MimeTypes.PDF)
                return View(model);

            _signOutSheetUploadService.Add(model);
            
            return RedirectToAction("Details", "Workshops", new { id = model.WorkshopId });
        }

        // Get the uploaded file : Get
        public ActionResult Download(int id = 0)
        {
            if(id == 0)
                return RedirectToAction("Index", "Workshops");

            var signOutSheetUploadModel = _signOutSheetUploadService.Get(id);

            if (signOutSheetUploadModel.FileData != null
                && signOutSheetUploadModel.FileData.Any())
                return File(signOutSheetUploadModel.FileData, signOutSheetUploadModel.ContentType,
                    signOutSheetUploadModel.FileName);
            else
                return RedirectToAction(nameof(SignOutSheetNotFound));
        }

        // Sign-Out sheet not found : Get
        public ActionResult SignOutSheetNotFound()
        {
            return View();
        }
    }
}

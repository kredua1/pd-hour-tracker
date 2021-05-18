using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    [Route("/Error")]
    public class ErrorsController : Controller
    {
        [Route("/Error/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var statusCodeData = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            
            var model = new ErrorModel();

            // Attempt to get error thrown
            try
            {
                var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                model.ExceptionError = exception.Error.Message;
            }
            catch (Exception ex) { /* no need to do anything this time */ }

            switch (statusCode)
            {
                case 404:
                    {
                        model.ErrorMessage = "Sorry the page you requested could not be found.";
                        model.RouteOfException = statusCodeData.OriginalPath;
                        break;
                    }
                case 500:
                    {
                        model.ErrorMessage = "Sorry something went wrong on the server.";
                        model.RouteOfException = statusCodeData.OriginalPath = statusCodeData.OriginalPath;
                        break;
                    }
            }

            return View(model);
        }
    }
}

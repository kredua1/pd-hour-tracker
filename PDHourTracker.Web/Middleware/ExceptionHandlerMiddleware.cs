using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Middleware
{
    /// <summary>
    /// This class catches error in the application and logs them.
    /// After logging them, it re-throws so an error page will be
    /// displayed.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Log the error with Serilog and throw so
                // the error page will be displayed
                Log.Error(ex, "");
                throw;
            }
        }
    }
}

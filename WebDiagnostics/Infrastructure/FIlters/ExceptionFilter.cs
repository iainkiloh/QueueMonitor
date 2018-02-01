using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;

namespace WebDiagnostics.Infrastructure.FIlters
{
    public class ExceptionFilter : FilterAttribute, IExceptionFilter
    {

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public void OnException(ExceptionContext filterContext)
        {

             var message = "";

            if (!filterContext.ExceptionHandled && filterContext.Exception is AccessViolationException)
            {
                var exc = (AccessViolationException) filterContext.Exception;
                message = "You do not have permissions to access this resource";
            }
            else
            {
                //log the exception here 

                message = "The following error occurred: " + filterContext.Exception.Message;
                _logger.Log(LogLevel.Info, filterContext.Exception.Message);
            }

            filterContext.ExceptionHandled = true;

            filterContext.Result = new RedirectToRouteResult(
            new RouteValueDictionary 
                { 
                    { "controller", "ErrorHandler" }, 
                    { "action", "Index" },
                    { "exception", message }
                });

        }

    }
}
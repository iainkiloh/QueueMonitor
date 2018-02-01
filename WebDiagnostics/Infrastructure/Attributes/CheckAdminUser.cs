using System;
using System.Web.Mvc;

namespace WebDiagnostics.Infrastructure.Attributes
{
    public class CheckAdminUser : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            var user = System.Web.HttpContext.Current.User;

            bool valid = Domain.Settings.AdminUsers.Contains(user.Identity.Name.ToLower());

            if (!valid)
            {
                throw new AccessViolationException("Access to the request resource has been denied");

            }

        }

    }
}
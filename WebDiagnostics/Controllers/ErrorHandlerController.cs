using System.Web.Mvc;

namespace WebDiagnostics.Controllers
{
    public class ErrorHandlerController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(string exception)
        {
            return View("Index", (object)exception);
        }
    }
}
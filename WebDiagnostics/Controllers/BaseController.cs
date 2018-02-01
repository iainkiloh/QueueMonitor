using System.Net.Mime;
using System.Web.Mvc;
using WebDiagnostics.Domain.Interfaces;
using WebDiagnostics.QueryObjects.Interfaces;

namespace WebDiagnostics.Controllers
{
    public abstract class BaseController : Controller
    {

        protected readonly IContext DataContext;
        protected readonly IMediator Mediator;

        protected BaseController(IContext dataContext, IMediator mediator)
        {
            DataContext = dataContext;
            Mediator = mediator;
        }

    }
}
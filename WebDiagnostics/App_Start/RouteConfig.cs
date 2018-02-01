using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebDiagnostics
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               null,
               "",
               new
               {
                   controller = "QueueMonitor",
                   action = "Index",
                   startDate = DateTime.Now.AddDays(-13).ToString("dd-MM-yyyy"),
                   endDate = DateTime.Now.ToString("dd-MM-yyyy"),
                   status = "All",
                   page = 1
               }
               );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );


        }
    }
}



 //routes.MapRoute(
 //              null,
 //              "{startDate}/{endDate}/{status}/{page}"
 //               ,
 //              new
 //              {
 //                  controller = "QueuedTasks",
 //                  action = "Index"

 //              },
 //                  new { page = @"\d+" }
 //              );
           

 //           routes.MapRoute(
 //              null,
 //              "{status}/{page}"
 //               ,
 //              new
 //              {
 //                  controller = "QueuedTasks",
 //                  action = "Index",
 //                  startDate = DateTime.Now.ToString("dd-MM-yyyy"),
 //                  endDate = DateTime.Now.ToString("dd-MM-yyyy")
 //              },
 //                  new { page = @"\d+" }
 //              );

 //           routes.MapRoute(
 //              null,
 //              "{page}"
 //               ,
 //              new
 //              {
 //                  controller = "QueuedTasks",
 //                  action = "Index",
 //                  startDate = DateTime.Now.ToString("dd-MM-yyyy"),
 //                  endDate = DateTime.Now.ToString("dd-MM-yyyy"),
 //                  status = "All"
 //              },
 //                  new { page = @"\d+" }
 //              );

//routes.MapRoute(
//    name: "Default",
//    url: "{controller}/{action}/{id}",
//    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
//);



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

     
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ShopProject.Controllers" }
            ).DataTokens["area"] = "Shopper";

            routes.MapRoute(
              name: "Admin",
              url: "{controller}/{action}",
              defaults: new { controller = "Home", action = "Index"},
              namespaces: new string[] { "ShopProject.Controllers" }
          ).DataTokens["area"] = "Administrator";

            routes.MapRoute(
               name: "Home",
               url: "{controller}/{action}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "ShopProject.Controllers" }
           ).DataTokens["area"] = "Shopper";
        }
    }
}

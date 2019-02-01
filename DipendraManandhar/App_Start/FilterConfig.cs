using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DipendraManandhar
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            GlobalFilters.Filters.Add(new RedirectToCanonicalUrlAttribute(
    RouteTable.Routes.AppendTrailingSlash,
    RouteTable.Routes.LowercaseUrls));
            filters.Add(new HandleErrorAttribute());
        }
    }
}

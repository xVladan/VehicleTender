using DataAccessLayer_DAL;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VehicleTender
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Seed user(admin) and roles for app

            ApplicationDbContext context = new ApplicationDbContext();
            SeedRolesAndUsers.Seed(context);
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;
using MVCTask1.App_Start;

namespace MVCTask1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}

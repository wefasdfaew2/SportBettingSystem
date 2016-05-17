namespace SportBettingSystem.Api
{
    using System.Web.Http;
    using System.Web.Optimization;
    using System.Web.Routing;

    using App_Start;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            DatabaseConfig.Populate();
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

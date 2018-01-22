using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;

namespace Sky
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger("Logger");
        private static readonly ILog applicationInfoLog = LogManager.GetLogger("ApplicationInfoLog");

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
            logger.Info("klkjfldsajflkdsakfd;saklf;ldsakf");
        }
    }
}

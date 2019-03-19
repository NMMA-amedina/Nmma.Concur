using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Tracing;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Api.Nmma.Configuration;
using Api.Nmma.Filters;

namespace Api.Nmma
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// 
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            IoCConfig.RegisterContainer(GlobalConfiguration.Configuration);
            ErrorPolicyConfig.RegisterPolicy(GlobalConfiguration.Configuration);

            TracingConfig.RegisterTracing(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionHandlingAttribute());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ExternalConfig.RegisterAutoMapperConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void Application_Error()
        {
            ITraceWriter tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            if (tracer != null)
                tracer.Error(new HttpRequestMessage(), "Fatal", Server.GetLastError());
        }
    }
}
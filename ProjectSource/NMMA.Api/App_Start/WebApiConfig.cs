using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

using NMMA.Api.MessageHandlers;
using Nmma.Business.Services;

namespace NMMA.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new ClientAuthenticationMessageHandler(new WebApiClientService()));

            // http://aspnetwebstack.codeplex.com/wikipage?title=CORS%20support%20for%20ASP.NET%20Web%20API
            // Enablie CORS globally
            //config.EnableCors(new EnableCorsAttribute());
            // Enablie per controller
            //config.EnableCors();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //    );

            //  */api/controller/20 
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithId",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" } //integers only
                );

            //  */api/controller/actionname/25
            config.Routes.MapHttpRoute(
                "DefaultApiWithActionId",
                "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional },
                constraints: new { id = @"\d+" });

            //  */api/controller/actionname
            config.Routes.MapHttpRoute(
                "DefaultApiWithAction",
                "api/{controller}/{action}"
                );

            config.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //    );

            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}"//{id}",
            //    //defaults: new { id = RouteParameter.Optional }
            //    );



            //returns responses in JSON be default
            //but it will still allow you to return XML if you pass text/xml as the request Accept header
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}

            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "api/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
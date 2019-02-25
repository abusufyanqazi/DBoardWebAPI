using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DashBoardAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            config.Routes.MapHttpRoute(
                name: "param5",
                routeTemplate: "{controller}/{token}/{code}/{age}/{phase}/{trf}",
                defaults: new { controller = "Values", action = "Get", token = RouteParameter.Optional, code = RouteParameter.Optional, age = RouteParameter.Optional, phase = RouteParameter.Optional, trf = RouteParameter.Optional}
            );
            
            config.Routes.MapHttpRoute(
                name: "Default",
                routeTemplate: "{controller}/{token}/{code}",
                defaults: new { controller = "Values", action = "Get", token = RouteParameter.Optional, code = RouteParameter.Optional }
            );

            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //    .Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}

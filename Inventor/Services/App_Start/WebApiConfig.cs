using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept",
                              "text/html",
                              StringComparison.InvariantCultureIgnoreCase,
                              true,
                              "application/json"));
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.Indent = true;

            // Enable CORS globally for OPTIONS preflight requests
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);



            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}"
            );

            // Ignoring circular reference globally
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Dummy route for returning a default HTTP status 200 when root domain is queried
            config.Routes.MapHttpRoute(
                name: "DummyApi",
                routeTemplate: "",
                defaults: new { controller = "Dummy", action = "Root" }
            );
        }
    }
}

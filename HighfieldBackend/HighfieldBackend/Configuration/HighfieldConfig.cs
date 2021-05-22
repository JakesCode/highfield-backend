using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HighfieldBackend.Configuration
{
    public class HighfieldConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.EnableCors(new EnableCorsAttribute(origins: "http://localhost:3000", headers: "*", methods: "GET"));

            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "HighfieldApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }
    }
}
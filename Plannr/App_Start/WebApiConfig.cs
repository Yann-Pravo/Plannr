using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Plannr
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            // Handle circular references
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;

            // No XML for now.
            config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}

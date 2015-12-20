using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace FamilyPhotos.API
{
    public static class WebApiConfig
    {
        public static object SerializerSettings { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
              name: "AlbumUserName",
              routeTemplate: "api/{controller}/{action}/{UserName}",
              defaults: new { UserName = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

    }
}
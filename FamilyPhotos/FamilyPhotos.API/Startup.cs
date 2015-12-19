using FamilyPhotos.API;
using FamilyPhotos.WebAPI;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]
namespace FamilyPhotos.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);

            ConfigureAuthZero(app);

            // TODO: Not recommended for production
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}
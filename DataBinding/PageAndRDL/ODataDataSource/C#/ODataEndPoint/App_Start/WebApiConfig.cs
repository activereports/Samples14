using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using ODataDataSource.Models;

namespace ODataDataSource
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			ODataModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Customer>("Customers");
			builder.EntitySet<Movie>("Movies");
			config.MapODataServiceRoute(
				"odata",
				 null,
				 builder.GetEdmModel(),
				new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));

			config.EnsureInitialized();
		}
    }
}

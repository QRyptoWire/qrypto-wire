using System.Web.Http;
using Newtonsoft.Json;
using QRyptoWire.Service.Api.Models;

namespace QRyptoWire.Service.Api
{
	public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// Web API configuration and services
			InitDb.UpdateDatabase();

			var json = config.Formatters.JsonFormatter;
			json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;
			json.SerializerSettings.Formatting = Formatting.None;
			config.Formatters.Remove(config.Formatters.XmlFormatter);

			// Web API routes
			config.MapHttpAttributeRoutes();
        }
    }
}

using System.Web;
using System.Web.Http;
using QRyptoWire.Service.Core;

namespace QRyptoWire.Service.Api
{
	public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
			//UnityConfig.RegisterComponents();
		}
    }
}

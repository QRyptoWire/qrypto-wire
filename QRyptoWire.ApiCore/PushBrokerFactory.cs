using System.Web;
using PushSharp;

namespace QRyptoWire.Service.Core
{
	class PushBrokerFactory
	{
		public static PushBroker GetBroker()
		{
			PushBroker pushBroker;
			if (HttpContext.Current.Application["PushBroker"] == null)
			{
				pushBroker = new PushBroker();
				pushBroker.RegisterWindowsPhoneService();
				HttpContext.Current.Application["PushBroker"] = pushBroker;
			}
			else
			{
				pushBroker = HttpContext.Current.Application["PushBroker"] as PushBroker;
			}
			return pushBroker;
		}
	}
}

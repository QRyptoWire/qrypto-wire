using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRyptoWire.ApiCore.Services
{
	public interface IPushService
	{
		bool RegisterPushToken(string sessionKey, string pushToken);
		bool IsPushAllowed(string sessionKey);
		bool SetPushAllowed(string sessionKey, bool isPushAllowed);
		bool Push(string pushToken, string message);


	}
}

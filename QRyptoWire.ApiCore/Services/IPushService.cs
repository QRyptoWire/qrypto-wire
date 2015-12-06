using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRyptoWire.Service.Data;

namespace QRyptoWire.ApiCore.Services
{
	public interface IPushService
	{
		bool RegisterPushToken(string sessionKey, string pushToken);
		bool IsPushAllowed(string sessionKey);
		bool SetPushAllowed(string sessionKey, bool isPushAllowed);
		bool Push(User user, string message);


	}
}

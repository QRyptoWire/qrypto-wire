using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PushSharp;
using PushSharp.WindowsPhone;
using QRyptoWire.Service.Core;

namespace QRyptoWire.ApiCore.Services
{
	public class PushService : IPushService
	{

		public bool RegisterPushToken(string sessionKey, string pushToken)
		{
			var dbContext = DbContextFactory.GetContext();
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return false;
			user.PushToken = pushToken;

			dbContext.SaveChanges();

			return true;
		}

		public bool IsPushAllowed(string sessionKey)
		{
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			return user.AllowPush;
		}
		
		public bool SetPushAllowed(string sessionKey, bool isPushAllowed)
		{
			var dbContext = DbContextFactory.GetContext();
			var sessionService = new SessionService();
			var user = sessionService.GetUser(sessionKey);
			if (user == null) return false;
			user.AllowPush = isPushAllowed;
			dbContext.SaveChanges();
			return true;
		}

		public bool Push(string pushToken, string message)
		{
			var push = PushBrokerFactory.GetBroker();
			if (string.IsNullOrWhiteSpace(pushToken)) return false;
			try
			{
				push.QueueNotification(new WindowsPhoneToastNotification()
					.ForEndpointUri(new Uri(pushToken))
					.ForOSVersion(WindowsPhoneDeviceOSVersion.MangoSevenPointFive)
					.WithBatchingInterval(BatchingInterval.Immediate)
					.WithNavigatePath("Views/LoginView.xaml")
					.WithText1("Hey there!")
					.WithText2(message));
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}
	}
}

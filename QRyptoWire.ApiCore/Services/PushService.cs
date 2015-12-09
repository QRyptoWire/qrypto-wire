using System;
using PushSharp;
using PushSharp.WindowsPhone;
using QRyptoWire.Service.Core;
using QRyptoWire.Service.Data;

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

		public bool Push(User user, string message)
		{
			var push = PushBrokerFactory.GetBroker();
			if (string.IsNullOrWhiteSpace(user.PushToken)
			|| ! user.AllowPush) return false;
			try
			{
				push.QueueNotification(new WindowsPhoneToastNotification()
					.ForEndpointUri(new Uri(user.PushToken))
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

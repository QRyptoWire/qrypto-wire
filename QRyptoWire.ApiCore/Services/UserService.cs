using System;
using System.Linq;
using System.Web.UI.WebControls;
using PushSharp;
using PushSharp.WindowsPhone;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class UserService : IUserService
	{
		public int GetId(string sessionKey)
		{
			using (var dbContext = new DataModel())
			{
				var session = dbContext.Sessions
					.Single(p
						=> p.SessionKey == sessionKey);

				return session.User.Id;
			}
		}

		public User GetUserById(int userId)
		{
			using (var dbContext = new DataModel())
			{
				try
				{
					var user = dbContext.Users
						.Single(p
							=> p.Id == userId);
					return user;
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		public string Login(string deviceId, string password)
		{
			var sessionService = new SessionService();
			return sessionService.CreateSession(deviceId, password);

		}

		public int Register(string deviceId, string password)
		{

			var dbContext = DbContextFactory.GetContext();
			if (dbContext.Users
							.Any(p
								=> p.DeviceId == deviceId))
				return 0;

			var newUsr = new User
			{
				PasswordHash = password,
				DeviceId = deviceId,
				AllowPush = true
			};
			dbContext.Add(newUsr);
			dbContext.SaveChanges();

			return newUsr.Id;
		}

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

		public bool Push(string pushToken)
		{
			var push = PushBrokerFactory.GetBroker();
			if (string.IsNullOrWhiteSpace(pushToken)) return false;
			push.QueueNotification(new WindowsPhoneToastNotification()
				.ForEndpointUri(new Uri(pushToken))
				.ForOSVersion(WindowsPhoneDeviceOSVersion.MangoSevenPointFive)
				.WithBatchingInterval(BatchingInterval.Immediate)
				.WithNavigatePath("Views/LoginView.xaml")
				.WithText1("Hey there!")
				.WithText2("You have new messages."));
			return true;
		}
	}
}
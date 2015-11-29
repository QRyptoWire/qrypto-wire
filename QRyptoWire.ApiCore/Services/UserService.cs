using System;
using System.Linq;
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

		public bool Register(string deviceId, string password)
		{

			var dbContext = DbContextFactory.GetContext();
			if (dbContext.Users
							.Any(p
								=> p.PasswordHash == password
								   && p.DeviceId == deviceId))
				return false;

			var newUsr = new User
			{
				PasswordHash = password,
				DeviceId = deviceId
			};
			dbContext.Add(newUsr);
			dbContext.SaveChanges();

			return true;
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

		public bool Push(int recieverId, string message)
		{
			//todo: fix that shit
			var push = PushBrokerFactory.GetBroker();
			var user = GetUserById(recieverId);
			if (user?.PushToken == null) return false;
			push.QueueNotification(new WindowsPhoneToastNotification()
				.ForEndpointUri(new Uri(user.PushToken))
				.ForOSVersion(WindowsPhoneDeviceOSVersion.MangoSevenPointFive)
				.WithBatchingInterval(BatchingInterval.Immediate)
				.WithNavigatePath("Views/LoginView.xaml")
				.WithText1("PushSharp")
				.WithText2(message));
			push.StopAllServices();
			return true;
		}
	}
}
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
	}
}
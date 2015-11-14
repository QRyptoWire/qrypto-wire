using System;
using System.Linq;
using QRyptoWire.Service.Core;
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

		public string Login(string deviceId, string password)
		{

			using (var dbContext = new DataModel())
			{
				var sessionService = new SessionService(dbContext);
				return sessionService.CreateSession(deviceId, password);
			}
		}

		public bool Register(string deviceId, string password)
		{
			using (var dbContext = new DataModel())
			{
				if (dbContext.Users
						.Any(p
							=> p.PasswordHash == password
							   && p.DeviceId == deviceId))
					return false;

				var newUsr = new User
				{
					PasswordHash = password,
					AllowPush = true,
					DeviceId = deviceId
				};
				dbContext.Add(newUsr);
				dbContext.SaveChanges();
			}
			return true;
		}
	}
}
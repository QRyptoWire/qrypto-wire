using System;
using System.Linq;
using QRyptoWire.Service.Data;
using Telerik.OpenAccess;

namespace QRyptoWire.Service.Core
{
	public class SessionService : ISessionService
	{
		public bool ValidateSession(string sessionKey)
		{

			var dbContext = DbContextFactory.GetContext();
			var oneHourAgo = DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0));
			return (
				dbContext.Sessions
				.Count(p
					=> p.SessionKey == sessionKey
					  // && p.StarTime > oneHourAgo
				) == 1
			);
		}


		public User GetUser(string sessionKey)
		{

			var dbContext = DbContextFactory.GetContext();
			if (!ValidateSession(sessionKey)) return null;
			var session = dbContext.
				Sessions
				.Single(p
					=> p.SessionKey == sessionKey);
			return session.User;
		}

		public string CreateSession(string deviceId, string password)
		{

			var dbContext = DbContextFactory.GetContext();
			if (dbContext.Users
					.Count(p
						=> p.PasswordHash == password
						   && p.DeviceId == deviceId) != 1)
				return null;

			var user =
				dbContext.Users.Single(
					p => p.PasswordHash == password
						 && p.DeviceId == deviceId);

			string sessionKey = SessionKeyGenerator.GetUniqueKey();

			dbContext.Sessions.Where(s => s.User == user).DeleteAll();

			dbContext.SaveChanges();
			var newSession = new Session
			{
				User = user,
				SessionKey = sessionKey,
				//StarTime = DateTime.Now
			};
			dbContext.Add(newSession);
			dbContext.SaveChanges();

			return sessionKey;
		}
	}
}

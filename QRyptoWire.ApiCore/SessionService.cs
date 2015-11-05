using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class SessionService
	{
		public static bool ValidateSession(string sessionKey)
		{
			using (var dbContext = new DataModel())
			{
				DateTime onHourAgo = DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0));
                if (dbContext.Sessions
					.Count(p
						=> p.SessionKey == sessionKey) != 1)
					return false;
				return true;
			}
		}


		public static User GetUser(string sessionKey)
		{
			using (var dbContext = new DataModel())
			{
				if (ValidateSession(sessionKey))
				{
					var session = dbContext.
                    Sessions
						.Single(p
							=> p.SessionKey == sessionKey); 
							return session.User;
				}
				return null;
			}
		}

		public static string CreateSession(string deviceId, string password)
		{

			using (var dbContext = new DataModel())
			{
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
				var newSession = new Session
				{
					User = user,
					SessionKey = sessionKey,
					StarTime = DateTime.Now
				};
				dbContext.Add(newSession);
				dbContext.SaveChanges();

				return sessionKey;
			}
		}
	}
}

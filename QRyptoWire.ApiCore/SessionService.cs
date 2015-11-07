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
		private DataModel DbContext { set; get; }
		 
		public SessionService(DataModel ctx)
		{
			this.DbContext = ctx;
		}

		public bool ValidateSession(string sessionKey)
		{
				DateTime onHourAgo = DateTime.Now.Subtract(new TimeSpan(0, 1, 0, 0));
                if (DbContext.Sessions
					.Count(p
						=> p.SessionKey == sessionKey) != 1)
					return false;
				return true;
		}


		public User GetUser(string sessionKey)
		{
				if (ValidateSession(sessionKey))
				{
					var session = DbContext.
                    Sessions
						.Single(p
							=> p.SessionKey == sessionKey); 
							return session.User;
				}
				return null;
		}

		public string CreateSession(string deviceId, string password)
		{
				if (DbContext.Users
					.Count(p
						=> p.PasswordHash == password
						   && p.DeviceId == deviceId) != 1)
					return null;

				var user =
					DbContext.Users.Single(
						p => p.PasswordHash == password
						     && p.DeviceId == deviceId);

				string sessionKey = SessionKeyGenerator.GetUniqueKey();
				var newSession = new Session
				{
					User = user,
					SessionKey = sessionKey,
					StarTime = DateTime.Now
				};
				DbContext.Add(newSession);
				DbContext.SaveChanges();

				return sessionKey;
		}
	}
}

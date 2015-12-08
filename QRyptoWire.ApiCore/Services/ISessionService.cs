using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public interface ISessionService
	{
		bool ValidateSession(string sessionKey);
		User GetUser(string sessionKey);
		string CreateSession(User user);
	}
}

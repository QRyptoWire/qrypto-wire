using System;

namespace QRyptoWire.Core.Services
{
	public interface IUserService
	{
		bool Login(string password);
		int Register(string password);
		[Obsolete]
		int GetUserId();
		bool GetPushSettings();
		void SetPushSettings(bool pushesAllowed);
	}
}

namespace QRyptoWire.Core.Services
{
	public interface IUserService
	{
		bool Login(string password);
		bool Register(string password);
		int GetUserId();
		bool GetPushSettings();
		void SetPushSettings(bool pushesAllowed);
	}
}

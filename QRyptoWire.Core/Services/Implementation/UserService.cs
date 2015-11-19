namespace QRyptoWire.Core.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IQryptoWireServiceClient _serviceClient;

		public UserService(IQryptoWireServiceClient serviceClient)
		{
			_serviceClient = serviceClient;
		}

		public bool Login(string password)
		{
			return _serviceClient.Login(password);
		}

		public void Register(string password)
		{
			_serviceClient.Register(password);
		}

		public bool GetPushSettings()
		{
			return _serviceClient.PushesAllowed();
		}

		public void SetPushSettings(bool pushesAllowed)
		{
			_serviceClient.AllowPushes(pushesAllowed);
		}
	}
}

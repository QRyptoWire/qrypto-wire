namespace QRyptoWire.Core.Services.Implementation
{
	public class UserService : IUserService
	{
		private readonly IQryptoWireServiceClient _serviceClient;
		private readonly IStorageService _storageService;

		public UserService(IQryptoWireServiceClient serviceClient, IStorageService storageService)
		{
			_serviceClient = serviceClient;
			_storageService = storageService;
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
			_storageService.SetPushSettings(pushesAllowed);
			_serviceClient.AllowPushes(pushesAllowed);
		}
	}
}

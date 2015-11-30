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

		public bool Register(string password)
		{
			return _serviceClient.Register(password);
		}

		public bool GetPushSettings()
		{
			return _serviceClient.PushesAllowed();
		}

		public void SetPushSettings(bool pushesAllowed)
		{
			var result = _serviceClient.AllowPushes(pushesAllowed);
			if(result)
				_storageService.SetPushSettings(pushesAllowed);
		}
	}
}

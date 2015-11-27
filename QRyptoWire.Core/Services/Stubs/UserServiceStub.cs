namespace QRyptoWire.Core.Services.Stubs
{
	public class UserServiceStub : IUserService
	{
		private static bool settings = false;
		public bool Login(string password)
		{
			return true;
		}

		public void Register(string password)
		{
			throw new System.NotImplementedException();
		}

		public bool GetPushSettings()
		{
			return settings;
		}

		public void SetPushSettings(bool pushesAllowed)
		{
			settings = pushesAllowed;
		}
	}
}

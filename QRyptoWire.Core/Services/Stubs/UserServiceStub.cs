namespace QRyptoWire.Core.Services.Stubs
{
	public class UserServiceStub : IUserService
	{
		private static bool settings = false;
		public bool Login(string password)
		{
			return true;
		}

		public int Register(string password)
		{
			return 1;
		}

		public int GetUserId()
		{
			return 0;
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

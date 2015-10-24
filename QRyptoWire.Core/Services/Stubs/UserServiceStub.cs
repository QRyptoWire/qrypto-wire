namespace QRyptoWire.Core.Services.Stubs
{
	public class UserServiceStub : IUserService
	{
		public bool Login(string password)
		{
			return true;
		}

		public bool Register(string password)
		{
			throw new System.NotImplementedException();
		}
	}
}

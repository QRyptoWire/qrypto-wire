namespace QRyptoWire.Core.Services.Stubs
{
	public class StorageServiceStub : IStorageService
	{
		public bool PublicKeyExists()
		{
			return true;
		}

		public void ClearMessages()
		{
		}
	}
}

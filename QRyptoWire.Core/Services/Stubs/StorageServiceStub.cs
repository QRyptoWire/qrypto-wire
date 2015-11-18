namespace QRyptoWire.Core.Services.Stubs
{
	public class StorageServiceStub : IStorageService
	{
		public bool PublicKeyExists()
		{
			return true;
		}

	    public string GetPublicKey()
	    {
	        return string.Empty;
	    }

	    public int GetUserId()
	    {
	        return 0;
	    }

	    public void ClearMessages()
		{
		}
	}
}

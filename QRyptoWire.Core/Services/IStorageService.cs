namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
		bool PublicKeyExists();
        string GetPublicKey();
	    int GetUserId();
        void ClearMessages();
	}
}

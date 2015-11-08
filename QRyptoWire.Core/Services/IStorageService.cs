namespace QRyptoWire.Core.Services
{
	public interface IStorageService
	{
		bool PublicKeyExists();
		void ClearMessages();
	}
}

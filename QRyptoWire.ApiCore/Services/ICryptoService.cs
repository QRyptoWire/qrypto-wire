namespace QRyptoWire.ApiCore.Services
{
	internal interface ICryptoService
	{
		string CreateSessionKey();
		string CreateSalt();
		string CreatePasswordHash(string password);
		bool ValidatePassword(string hash, string password);
	}

}

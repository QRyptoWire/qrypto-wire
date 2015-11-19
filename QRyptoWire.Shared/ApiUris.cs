namespace QRyptoWire.Shared
{
	public static class ApiUris
	{
		#if LOCAL
		public static readonly string Base = "http://localhost:64433/QryptoWire/";
		#else
		public static readonly string Base = "http://qryptowire.azurewebsites.net/api/";
		#endif

		public static readonly string Login = "Login/";
		public static readonly string Register = "Register/";
		public static readonly string SendMessage = "AddMessage/";
		public static readonly string FetchMessages = "FetchMessages/";
		public static readonly string AddContact = "AddContact/";
		public static readonly string FetchContacts = "FetchContacts/";
		public static readonly string AddToken = "AddPushToken/";
		public static readonly string GetUserId = "GetUserId/";
		public static readonly string GetPushesAllowed = "PushesAllowed/";
		public static readonly string AllowPushes = "AllowPushes/";
	}
}

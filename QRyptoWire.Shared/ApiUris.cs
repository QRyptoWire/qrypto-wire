namespace QRyptoWire.Shared
{
	public static class ApiUris
	{
		#if LOCAL
		public static readonly string Base = "http://localhost:64433/QryptoWire/";
		#else
		public static readonly string Base = "http://qryptowire.azurewebsites.net";
		#endif

		public static readonly string Login = "api/Login";
		public static readonly string Register = "api/Register";
		public static readonly string SendMessage = "api/SendMessage/";
		public static readonly string FetchMessages = "api/FetchMessages/";
		public static readonly string AddContact = "api/AddContact/";
		public static readonly string FetchContacts = "api/FetchContacts/";
		public static readonly string AddToken = "api/RegisterPushToken/";
		public static readonly string GetUserId = "api/GetUserId/";
		public static readonly string GetPushesAllowed = "api/IsPushAllowed/";
		public static readonly string AllowPushes = "api/SetPushAllowed/";
	}
}

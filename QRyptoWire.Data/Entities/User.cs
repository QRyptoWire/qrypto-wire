namespace QRyptoWire.Service.Data
{
	public class User
	{
		public int Id { get; set; }
		public string PasswordHash { set; get; }
		public bool AllowPush { set; get; }
	}
}

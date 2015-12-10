using System;

namespace QRyptoWire.Service.Data
{
	public class Session
	{
		public User User { set; get; }
		public int UserId { set; get; }
		public string SessionKey { set; get; }
		public DateTime StarTime { set; get; }
	}
}

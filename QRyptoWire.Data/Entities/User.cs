using System.Collections.Generic;

namespace QRyptoWire.Service.Data
{
	public class User
	{
		public User()
		{
			ReceivedMessages = new List<Message>();
		}
		public int Id { get; set; }
		public string DeviceId { get; set; }
		public string PasswordHash { set; get; }
		public bool AllowPush { set; get; }
		public IList<Message> ReceivedMessages { get; set; }
		public IList<Contact> ReceivedContacts { get; set; }
	}
}

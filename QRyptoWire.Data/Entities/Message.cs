using System;

namespace QRyptoWire.Service.Data
{
	public class Message
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public string Signature { get; set; }
		public string SessionKey { get; set; }
		public string InitVector { get; set; }
		public DateTime SentTime { get; set; }
		public int RecipientId { get; set; }
		public User Recipient { get; set; }
		public int SenderId { get; set; }
		public User Sender { get; set; }
	}
}

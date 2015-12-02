using System;

namespace QRyptoWire.Shared.Dto
{
	public class Message
	{
		public string Body { get; set; }
		public string DigitalSignature { get; set; }
		public string SymmetricKey { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public DateTime DateSent { get; set; }
		public string Iv { get; set; }
	}
}

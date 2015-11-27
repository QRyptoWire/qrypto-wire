using System;

namespace QRyptoWire.Core
{
	public class StoredMessage
	{
		public string Body { get; set; }
		public bool Sent { get; set; }
		public DateTime Date { get; set; }
	}
}

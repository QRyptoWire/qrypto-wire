﻿namespace QRyptoWire.Service.Data
{
	public class Contact
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public User Recipient { get; set; }
		public User Sender { get; set; }
	}
}

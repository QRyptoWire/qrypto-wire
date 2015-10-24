namespace QRyptoWire.Service.Data
{
	public class Message
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public User Recipient { get; set; }
		public User Sender { get; set; }
	}
}

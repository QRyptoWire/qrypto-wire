namespace QRyptoWire.Shared.Dto
{
    public class Contact
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Name { get; set; }
		public string PublicKey { get;  set; }
    }
}
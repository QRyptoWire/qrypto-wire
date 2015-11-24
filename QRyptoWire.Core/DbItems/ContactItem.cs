namespace QRyptoWire.Core.DbItems
{
    public class ContactItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublicKey { get; set; }
        public bool IsNew { get; set; }
    }
}

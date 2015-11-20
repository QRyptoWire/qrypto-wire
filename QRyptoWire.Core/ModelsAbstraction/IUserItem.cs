namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IUserItem
    {
        int Id { get; set; }
        string PublicKey { get; set; }
        bool IsPushEnabled { get; set; }
    }
}

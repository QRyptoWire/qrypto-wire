namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IUserItem
    {
        int Id { get; set; }
        string KeyPair { get; set; }
        bool IsPushEnabled { get; set; }
    }
}

namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IUserModel
    {
        int Id { get; set; }
        string KeyPair { get; set; }
        string Name { get; set; }
    }
}

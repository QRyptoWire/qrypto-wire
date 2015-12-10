namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IContactModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string PublicKey { get; set; }
        bool IsNew { get; set; }
    }
}
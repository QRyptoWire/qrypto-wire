namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IMessageItem
    {
        int Id { get; set; }
        int SenderId { get; set; }
        string Body { get; set; }
        bool IsNew { get; set; }
    }
}

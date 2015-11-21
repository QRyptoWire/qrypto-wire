using System;

namespace QRyptoWire.Core.ModelsAbstraction
{
    public interface IMessageItem
    {
        int Id { get; set; }
        int SenderId { get; set; }
        int ReceiverId { get; set; }
        string Body { get; set; }
        bool IsNew { get; set; }
        DateTime Date { get; set; }
    }
}

using System;

namespace QRyptoWire.Core.DbItems
{
    public class MessageItem
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Body { get; set; }
        public bool IsNew { get; set; }
        public DateTime Date { get; set; }
    }
}

using System.Data.Linq.Mapping;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.App.WPhone.Models
{
    [Table(Name = "Message")]
    public class MessageItem : IMessageItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public int SenderId { get; set; }

        [Column(CanBeNull = false)]
        public string Body { get; set; }

        [Column(CanBeNull = false)]
        public bool IsNew { get; set; }
    }
}

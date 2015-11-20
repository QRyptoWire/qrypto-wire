using System.Data.Linq;
using System.Data.Linq.Mapping;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.App.WPhone.Models
{
    [Table(Name = "Contact")]
    public class ContactItem : IContactItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string Name { get; set; }

        [Column(CanBeNull = false)]
        public string PublicKey { get; set; }

        [Column(CanBeNull = false)]
        public bool IsNew { get; set; }

        [Association(ThisKey = "Id", OtherKey = "SenderId")]
        public EntitySet<MessageItem> Messages { get; set; }
    }
}

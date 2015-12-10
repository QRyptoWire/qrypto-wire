using System.Data.Linq;
using System.Data.Linq.Mapping;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.App.WPhone.Models
{
    [Table(Name = "Contact")]
    public class ContactModel : IContactModel
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string Name { get; set; }

        [Column(CanBeNull = false)]
        public string PublicKey { get; set; }

        [Column(CanBeNull = false)]
        public bool IsNew { get; set; }

        // messages that this contact has sent to user
        [Association(ThisKey = "Id", OtherKey = "SenderId")]
        public EntitySet<MessageModel> MessagesSentToUser { get; set; }

        // messages that user has sent to this contact
        [Association(ThisKey = "Id", OtherKey = "ReceiverId")]
        public EntitySet<MessageModel> MessagesSentByUser { get; set; }
    }
}

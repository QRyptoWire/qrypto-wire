using System.Data.Linq.Mapping;
using QRyptoWire.Core.ModelsAbstraction;

namespace QRyptoWire.App.WPhone.Models
{
    [Table(Name = "User")]
    public class UserItem : IUserItem
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string KeyPair { get; set; }
    }
}

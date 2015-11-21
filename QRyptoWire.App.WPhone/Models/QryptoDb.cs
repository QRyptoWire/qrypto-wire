using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace QRyptoWire.App.WPhone.Models
{
    [Database(Name = "QRyptoDb")]
    public class QRyptoDb : DataContext
    {
        public Table<UserItem> Users;
        public Table<MessageItem> Messages;
        public Table<ContactItem> Contacts;

        public const string ConnectionString = "DataSource='isostore:/QryptoDb.sdf';Password={0};";

        public QRyptoDb(string connectionString)
            : base(connectionString)
        {
        }
    }
}

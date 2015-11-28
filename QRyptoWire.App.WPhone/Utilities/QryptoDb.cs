using System.Data.Linq;
using System.Data.Linq.Mapping;
using QRyptoWire.App.WPhone.Models;

namespace QRyptoWire.App.WPhone.Utilities
{
    [Database(Name = "QRyptoDb")]
    public class QRyptoDb : DataContext
    {
        public Table<UserModel> Users;
        public Table<MessageModel> Messages;
        public Table<ContactModel> Contacts;

        public const string ConnectionString = "DataSource='isostore:/QryptoDb.sdf';Password={0};";

        public QRyptoDb(string connectionString)
            : base(connectionString)
        {
        }
    }
}

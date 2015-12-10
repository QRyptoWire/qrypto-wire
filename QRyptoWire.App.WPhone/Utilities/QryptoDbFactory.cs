using QRyptoWire.App.WPhone.Models;

namespace QRyptoWire.App.WPhone.Utilities
{
    public static class QryptoDbFactory
    {
        public static QRyptoDb GetDb()
        {
            QRyptoDb db = new QRyptoDb(string.Format(QRyptoDb.ConnectionString, QryptoDbSecurity.GetPassword()));

            if (!db.DatabaseExists())
            {
                db.CreateDatabase();
            }

            return db;
        }
    }
}

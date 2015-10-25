using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace QRyptoWire.Service.Data
{
	public class DataModel : OpenAccessContext
	{
		private static string connectionStringName = @"AzureMySql";

		private static BackendConfiguration backend =
			GetBackendConfiguration();

		private static MetadataSource metadataSource =
			new TelerikMetadataSource();

		public DataModel()
        :base(connectionStringName, backend, metadataSource)
    { }

		public IQueryable<Message> Messages
		{
			get
			{
				return this.GetAll<Message>();
			}
		}

		public IQueryable<Contact> Contacts
		{
			get
			{
				return this.GetAll<Contact>();
			}
		}

		public IQueryable<User> Users
		{
			get
			{
				return this.GetAll<User>();
			}
		}

		public IQueryable<Session> Sessions
		{
			get
			{
				return this.GetAll<Session>();
			}
		}

		public static BackendConfiguration GetBackendConfiguration()
		{
			BackendConfiguration backend = new BackendConfiguration();
			backend.Backend = "MySql";
			backend.ProviderName = "MySql.Data.MySqlClient";

			return backend;
		}
	}
}

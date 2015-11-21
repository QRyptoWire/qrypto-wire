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

		public IQueryable<Message> Messages => this.GetAll<Message>();

		public IQueryable<Contact> Contacts => this.GetAll<Contact>();

		public IQueryable<User> Users => this.GetAll<User>();

		public IQueryable<Session> Sessions => this.GetAll<Session>();

		public static BackendConfiguration GetBackendConfiguration()
		{
			var configuration = new BackendConfiguration
			{
				Backend = "MySql",
				ProviderName = "MySql.Data.MySqlClient"
			};

			return configuration;
		}
	}
}

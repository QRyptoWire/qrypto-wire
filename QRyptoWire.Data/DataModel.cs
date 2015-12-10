using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace QRyptoWire.Service.Data
{
	public class DataModel : OpenAccessContext
	{
		private static string connectionStringName = @"AzureMySql";

		private static readonly BackendConfiguration Backend =
			GetBackendConfiguration();

		private static readonly MetadataSource MetadataSource =
			new TelerikMetadataSource();

		public DataModel()
        :base(connectionStringName, Backend, MetadataSource)
    { }

		public IQueryable<Message> Messages => GetAll<Message>();

		public IQueryable<Contact> Contacts => GetAll<Contact>();

		public IQueryable<User> Users => GetAll<User>();

		public IQueryable<Session> Sessions => GetAll<Session>();

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

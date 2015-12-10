using Telerik.OpenAccess;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Api.Models
{
	public static class InitDb
	{

		public static void UpdateDatabase()
		{
			using (var context = new DataModel())
			{
				var schemaHandler = context.GetSchemaHandler();
				EnsureDb(schemaHandler);
			}
		}

		private static void EnsureDb(ISchemaHandler schemaHandler)
		{
			string script;
			if (schemaHandler.DatabaseExists())
			{
				script = schemaHandler.CreateUpdateDDLScript(null);
			}
			else
			{
				schemaHandler.CreateDatabase();
				script = schemaHandler.CreateDDLScript();
			}

			if (!string.IsNullOrEmpty(script))
			{
				schemaHandler.ExecuteDDLScript(script);
			}
		}
	}
}

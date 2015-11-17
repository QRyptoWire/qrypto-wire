using System.Web;
using QRyptoWire.Service.Data;

namespace QRyptoWire.Service.Core
{
	public class DbContextFactory
	{
		public static DataModel GetContext()
		{
			DataModel dbContext;
			if (HttpContext.Current.Application["dbContext"] == null)
			{
				dbContext = new DataModel();
				HttpContext.Current.Application["dbContext"] = dbContext;
			}
			else
			{
				dbContext = HttpContext.Current.Application["dbContext"] as DataModel;
			}
			return dbContext;
		}

		public static void Dispose()
		{
			var dbContext = HttpContext.Current.Application["dbContext"] as DataModel;
			dbContext?.Dispose();
		}
	}
}

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using QRyptoWire.Shared;
using RestSharp.Portable;

namespace QRyptoWire.Core
{
	public abstract class ApiClientBase
	{
		public void Execute(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			try
			{
				var response = Task.Run(async () => await client.Execute(request));
				response.Wait();

				if (response.Result.IsSuccess)
				{
					return;
				}
				if (response.Result.StatusCode == HttpStatusCode.NotFound)
				{
					throw new HttpRequestException("404");
				}
				throw new HttpRequestException("Something went terribly wrong");
			}
			catch (Exception)
			{
				// ignored
			}
		}

		public TRet Execute<TRet>(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			try
			{
				var response = Task.Run(async () => await client.Execute<TRet>(request));
				response.Wait();

				if (response.Result.IsSuccess)
				{
					return response.Result.Data;
				}
				if (response.Result.StatusCode == HttpStatusCode.NotFound)
				{
					throw new HttpRequestException("404");
				}
				throw new HttpRequestException("Request to service failed");
			}
			catch (Exception)
			{
				// ignored
			}

			return default(TRet);
		}
	}
}

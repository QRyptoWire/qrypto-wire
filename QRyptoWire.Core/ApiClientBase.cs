using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cirrious.MvvmCross.Plugins.Messenger;
using QRyptoWire.Core.Messages;
using QRyptoWire.Shared;
using RestSharp.Portable;

namespace QRyptoWire.Core
{
	public abstract class ApiClientBase
	{
		private readonly IMvxMessenger _messenger;

		protected ApiClientBase(IMvxMessenger messenger)
		{
			_messenger = messenger;
		}

		protected void Execute(IRestRequest request)
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
				throw new HttpRequestException("Request to service failed");
			}
			catch (Exception ex)
			{
				_messenger.Publish(new RequestFailedMessage(this));
			}
		}

		protected TRet Execute<TRet>(IRestRequest request)
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
				throw new HttpRequestException("Request to service failed");
			}
			catch (Exception ex)
			{
				_messenger.Publish(new RequestFailedMessage(this));
			}

			return default(TRet);
		}

		protected bool TryExecute(IRestRequest request)
		{
			var client = new RestClient(ApiUris.Base);
			try
			{
				var response = Task.Run(async () => await client.Execute(request));
				response.Wait();

				if (response.Result.IsSuccess)
				{
					return true;
				}
				throw new HttpRequestException("Request to service failed");
			}
			catch (Exception ex)
			{
				_messenger.Publish(new RequestFailedMessage(this));
			}

			return false;
		}
	}
}
